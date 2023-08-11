using DocumentFormat.OpenXml.Packaging;
using Hal.Core.ReportDoc.MasterGenerators;
using Hal.Core.ReportDoc.ReportGenerator;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.CustomerReport;
using Sperry.MxS.Reporting.Common.Interfaces;
using Sperry.MxS.Reporting.Common.Models.XmlDataDefinition;
using Sperry.MxS.Reporting.Control.Lib.Chart;
using Sperry.MxS.Reporting.ReportDoc;
using Sperry.MxS.Reporting.ReportDoc.Interface;
using Sperry.MxS.Reporting.ReportDoc.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OXmlWordProc = DocumentFormat.OpenXml.Drawing.Wordprocessing;

namespace Sperry.MxS.Reporting.Services
{
    [Export(typeof(IMxSCustomerReport)), PartCreationPolicy(CreationPolicy.Shared)]
    public class MxSSurveyReport : IMxSCustomerReport
    {
        #region Private Members
        private readonly IMxSCustomerReportDataProvider _surveyReportDataProvider;
        private const ushort DeleteRetry = 5;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;


        [ImportingConstructor]
        public MxSSurveyReport(IMxSCustomerReportDataProvider surveyReportDataProvider, IMxSCustomerReportImageProvider customerReportImageProvider, ILoggerFactory loggerFactory)
        {
            _surveyReportDataProvider = surveyReportDataProvider;
            _logger = loggerFactory.CreateLogger<MxSSurveyReport>();
            _loggerFactory = loggerFactory;
        }

        #endregion

        public Dictionary<string, string> GetMissingImagesForTemplates(Well well, IEnumerable<string> templates)
        {
            Dictionary<string, string> missingImages = new Dictionary<string, string>();
            foreach (var template in templates)
            {
                Dictionary<string, string> subReportMissingImages = GetMissingImagesForTemplate(well, template);
                foreach (var item in subReportMissingImages)
                {
                    missingImages[item.Key] = item.Value;
                }
            }
            return missingImages;
        }

        public HalReport GenerateReport(Well well, string templatePath, string outputReportPath, ReportParameters reportParameters)
        {
            HalReport reportResult = new HalReport();
            if (string.IsNullOrEmpty(templatePath) || string.IsNullOrEmpty(outputReportPath) || well == null)
            {
                reportResult.ResultMessage = MxSReportResultMessage.DataLoading;
            }
            else
            {
                string tempFilePath = Path.GetTempFileName();
                try
                {
                    _logger.LogCritical($"Yug -- TempFilePath");
                    bool b = _surveyReportDataProvider.CreateReportDataFile(well, tempFilePath, reportParameters);
                    _logger.LogCritical($"Yug 1 -- {b}");
                    if (b)
                    {
                        try
                        {
                            using (IMxSReportGenerator reportGenerator = new OpenXMLReportGenerator(templatePath, outputReportPath, false, loggerFactory: _loggerFactory))
                            {
                                _logger.LogCritical("Yug 7 -- Sandy reportGenerator OpenXMLReportGenerator");
                                if (reportGenerator.ReturnReport == null)
                                {
                                    _logger.LogCritical("Yug 8 -- Sandy reportGenerator.ReturnReport == null");
                                    if (AddinBase.ErrorMessages.Count == 0)
                                    {
                                        _logger.LogCritical("Yug 9 -- Sandy AddinBase.ErrorMessages.Count == 0");
                                        List<ChartInfo> chartSettings = ConvertToChartInfos(reportParameters.ChartParameters);
                                        _logger.LogCritical($"Yug 12 -- Sandy chartSettings : {JsonConvert.SerializeObject(chartSettings)}");
                                        reportResult = reportGenerator.CreateReport(tempFilePath, chartSettings);
                                    }
                                }
                                else
                                {
                                    _logger.LogCritical("Yug 10 -- Sandy reportGenerator.ReturnReport != null in else start");
                                    reportResult = reportGenerator.ReturnReport;
                                    _logger.LogCritical($"Yug 11 -- Sandy reportGenerator.ReturnReport != null in else End : {JsonConvert.SerializeObject(reportResult)}");
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            _logger.LogCritical($"Yug 2 -- {ex.Message}");
                            if(ex.InnerException != null)
                            {
                                _logger.LogCritical($"Yug 3 - {ex.InnerException.Message}");
                            }
                        }
                    }
                    else
                    {

                        reportResult.ResultMessage = MxSReportResultMessage.XMLFile;
                    }
                }
                finally
                {
                    DeleteFile(tempFilePath);
                }


            }
            _logger.LogCritical($"Yug 14 -- Sandy : {JsonConvert.SerializeObject( reportResult)}");
            return reportResult;
        }

        public HalReport GenerateReport(Well well, IEnumerable<CustomerReportTemplate> templates, string outputReportPath, ReportParameters reportParameters)
        {
            var result = new HalReport();

            string reportDataFile = Path.GetTempFileName();
            try
            {
                if (_surveyReportDataProvider.CreateReportDataFile(well, reportDataFile, reportParameters))
                {
                    using (IMxSMasterGenerator masterRpt = new OpenXMLMasterGenerator())
                    {
                        result = CreateMasterReport(templates, outputReportPath, reportDataFile, masterRpt, reportParameters.ChartParameters);
                    }
                }
                else
                {
                    result.ResultMessage = MxSReportResultMessage.XMLFile;
                }
            }
            finally
            {
                DeleteFile(reportDataFile);
                _surveyReportDataProvider.ResetImageCacheForWell(well.Id);
            }
            return result;
        }

        public Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>> GetChartsForTemplates(Dictionary<Guid, string> templates)
        {
            Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>> chartsForTemplates = new Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>();
            foreach (var template in templates)
            {
                Dictionary<string, ChartProperties> charts = GetChartsForTemplate(template.Value);
                Dictionary<string, CustomerReportChartInfo> chartsDictionary = new Dictionary<string, CustomerReportChartInfo>();

                if (charts != null && charts.Any())
                {
                    foreach (var chart in charts)
                    {
                        ChartProperties chartProperties = chart.Value;
                        CustomerReportChartInfo customerReportChartSetting = new CustomerReportChartInfo()
                        {
                            ChartName = chartProperties.ChartName,
                            XAxisMax = chartProperties.MaximumValue,
                            XAxisMin = chartProperties.MinimumValue,
                            YAxisMax = chartProperties.YaxisMax,
                            YAxisMin = chartProperties.YaxisMin,
                            XAxisInterval = chartProperties.XAxisIntervalNumber,
                            TemplateId = template.Key
                        };
                        chartsDictionary.Add(customerReportChartSetting.ChartName, customerReportChartSetting);
                    }
                    chartsForTemplates.Add(template.Key, chartsDictionary);
                }
            }
            return chartsForTemplates;
        }

        public Dictionary<string, ChartProperties> GetChartsForTemplate(string templateFile)
        {
            Dictionary<string, ChartProperties> chartSettings = OpenXMLReportGenerator.GetChartSettingsFromTemplate(templateFile);
            return chartSettings;
        }

        //public IEnumerable<string> GetImagesForTemplates(Guid wellId, IEnumerable<string> templates)
        //{
        //    var imagesFromTemplate = new List<string>();
        //    foreach (var template in templates)
        //    {
        //        imagesFromTemplate.AddRange(GetImagesForTemplate(template));
        //    }
        //    return imagesFromTemplate;
        //}

        private HalReport CreateMasterReport(IEnumerable<CustomerReportTemplate> templates, string outputReportPath, string reportDataFile, IMxSMasterGenerator masterRpt, IEnumerable<CustomerReportChartInfo> chartParameters)
        {
            HalReport result;
            var subReports = new List<Uri>();
            masterRpt.SaveLocation = outputReportPath;
            foreach (var tableMasterReport in templates)
            {
                var reportPath = CreateReport(tableMasterReport.TemplateFile, null, reportDataFile, true, chartParameters);
                if (reportPath.ResultMessage == MxSReportResultMessage.NoErrors)
                {
                    switch (tableMasterReport.ReportType)
                    {
                        case MxSCustomerReportTemplateType.ReportHeader:
                            masterRpt.ReportHeader = reportPath.ReturnUri;
                            break;
                        case MxSCustomerReportTemplateType.PageHeader:
                            masterRpt.PageHeader = reportPath.ReturnUri;
                            break;
                        case MxSCustomerReportTemplateType.PageFooter:
                            masterRpt.PageFooter = reportPath.ReturnUri;
                            break;

                        case MxSCustomerReportTemplateType.ReportFooter:
                            masterRpt.ReportFooter = reportPath.ReturnUri;
                            break;
                        case MxSCustomerReportTemplateType.SubReport:
                            subReports.Add(reportPath.ReturnUri);
                            break;
                    }
                }
                else
                    break;
            }
            masterRpt.SubReports = subReports;
            result = masterRpt.ExecuteMasterReport(true);

            if (result.ResultMessage == MxSReportResultMessage.NoErrors)
            {
                if (result.ReturnUri != null)
                {
                    masterRpt.SaveLocation = outputReportPath;
                }
            }

            return result;
        }

        private HalReport CreateReport(string reportLocation, string saveLocation, string xmlLocation, bool enableLog, IEnumerable<CustomerReportChartInfo> chartParameters)
        {
            var returnLocation = new HalReport();
            try
            {
                if (string.IsNullOrWhiteSpace(reportLocation) || string.IsNullOrWhiteSpace(xmlLocation))
                {
                    returnLocation.ResultMessage = MxSReportResultMessage.DataLoading;
                }
                else
                {
                    using (IMxSReportGenerator rpt = new OpenXMLReportGenerator(reportLocation, saveLocation, enableLog))
                    {
                        if (rpt.ReturnReport == null)
                        {
                            if (AddinBase.ErrorMessages.Count == 0)
                            {
                                returnLocation = rpt.CreateReport(xmlLocation, ConvertToChartInfos(chartParameters));
                            }
                        }
                        else
                        {
                            returnLocation = rpt.ReturnReport;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnLocation.ResultMessage = MxSReportResultMessage.TemplateFile;
                AddinBase.ErrorMessages.Add(ex.ToString());
            }
            return returnLocation;
        }

        private static List<ChartInfo> ConvertToChartInfos(IEnumerable<CustomerReportChartInfo> chartInfo)
        {
            List<ChartInfo> chartInfos = new List<ChartInfo>();
            if (chartInfo != null)
            {
                foreach (var customerReportChartInfo in chartInfo)
                {
                    chartInfos.Add(new ChartInfo()
                    {
                        ChartName = customerReportChartInfo.ChartName,
                        //TemplateId = customerReportChartInfo.TemplateId,
                        YAxisMin = customerReportChartInfo.YAxisMin,
                        YAxisMax = customerReportChartInfo.YAxisMax,
                        XAxisMin = customerReportChartInfo.IsChartSettingsOverride ? customerReportChartInfo.XAxisMin : null,
                        XAxisMax = customerReportChartInfo.IsChartSettingsOverride ? customerReportChartInfo.XAxisMax : null,
                        XAxisIntervalNumber = (customerReportChartInfo.IsChartSettingsOverride && customerReportChartInfo.XAxisInterval.HasValue) ? customerReportChartInfo.XAxisInterval : -1
                    });
                }
            }
            else
            {
                string st = "error on chartInfo";
                //chartInfo.First().ChartName = st;
                chartInfos.First().ChartName = st;
            }
            return chartInfos;
        }

        private Dictionary<string, string> GetMissingImagesForTemplate(Well well, string templateFile)
        {
            Dictionary<string, string> missingImages = new Dictionary<string, string>();
            List<string> imagesForTemplate = GetImagesForTemplate(templateFile);
            IEnumerable<ReportPlot> plotsFromMetaData = GetReportPlotsFromWell(well);

            imagesForTemplate.ForEach(title =>
            {
                if (plotsFromMetaData != null)
                {
                    ReportPlot plot = plotsFromMetaData.FirstOrDefault(p => p.Name == title);
                    if (plot != null)
                    {
                        FileInfo fileInfo = new FileInfo(plot.Path);
                        if (!fileInfo.Exists)
                        {
                            // Image is available in data file But physically not available
                            missingImages[title] = fileInfo.FullName;
                        }
                    }
                    else
                    {
                        // Image is Missing in data file
                        missingImages[title] = string.Empty;
                    }
                }
            });
            return missingImages;
        }

        public List<string> GetImagesForTemplate(string templateFile)
        {
            List<string> images = new List<string>();
            using (WordprocessingDocument doc = WordprocessingDocument.Open(templateFile, false))
            {
                List<OXmlWordProc.Inline> templateShapes = doc.MainDocumentPart.Document.Body
                    .Descendants<OXmlWordProc.Inline>().ToList();

                templateShapes.ForEach(templateShape =>
                {
                    string title = templateShape.DocProperties.Title;
                    if (!string.IsNullOrEmpty(title)
                        && (!title.StartsWith("SA:"))
                        && (!title.StartsWith("CHART:")))
                    {
                        images.Add(title);
                    }
                });
            }
            return images;
        }

        private IEnumerable<ReportPlot> GetReportPlotsFromWell(Well well)
        {
            IEnumerable<ReportPlot> plots = null;
            string xmlDataFile = Path.GetTempFileName();
            try
            {
                if (_surveyReportDataProvider.CreateReportDataFile(well, xmlDataFile, null))
                {
                    using (FileStream fileStream = new FileStream(xmlDataFile, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Report));
                        Report metaData = (Report)serializer.Deserialize(fileStream);
                        plots = metaData != null ? metaData.From_App.Plots : null;
                    }
                }
            }
            finally
            {
                DeleteFile(xmlDataFile);
            }
            return plots;
        }

        private void DeleteFile(string tempFilePath)
        {
            if (File.Exists(tempFilePath))
            {
                //There are few occasions where delete file is not successful as the windows has not released the handler. So added the retry logic.
                for (int i = 0; i < DeleteRetry; i++)
                {
                    try
                    {
                        File.Delete(tempFilePath);
                        break;
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }

        public void CreateReportDataFile(Well well, string outputPath, ReportParameters reportParameters)
        {
            _surveyReportDataProvider.CreateReportDataFile(well, outputPath, reportParameters);
        }

        #region MyRegion

        public IEnumerable<string> GetImagesForTemplates(byte[] compressedWell, IEnumerable<string> templates)
        {
            var imagesFromTemplate = new List<string>();
            foreach (var template in templates)
            {
                imagesFromTemplate.AddRange(GetImagesForTemplate(template));
            }
            return imagesFromTemplate;
        }

        #endregion
    }
}
