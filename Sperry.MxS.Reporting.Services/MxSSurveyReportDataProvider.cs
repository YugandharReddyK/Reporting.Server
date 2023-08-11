using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.CoordSys;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Reporting.Common.Constants;
using Sperry.MxS.Reporting.Common.Interfaces;
using Sperry.MxS.Reporting.Common.Models.XmlDataDefinition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Hal.Core.XmlDocumentExtensions;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Reporting.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Sperry.MxS.Reporting.Services
{
    [Export(typeof(IMxSCustomerReportDataProvider))]
    public class MxSSurveyReportDataProvider : IMxSCustomerReportDataProvider
    {
        //TODO - There is no consolidated list of Charts. We need to figure out a better way to get list of Charts
        private List<string> _chartsSupported = new List<string>()
        {
            "CHART_IMAGE_Dip",
            "CHART_IMAGE_Inc",
            "CHART_IMAGE_Azimuth",
            "CHART_IMAGE_BTotal",
            "CHART_IMAGE_GtHsg",
            "CHART_IMAGE_GTotal",
            "CHART_IMAGE_Declination",
            "CHART_IMAGE_BtDip",
            "CHART_IMAGE_BgBhDepth"
        };

        private Dictionary<string, PropertyInfo> _wellParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> _correctedSurveyParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> _rawSurveyParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> _planSurveyParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> _runParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> _solutionParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> _ifr1ParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> _mfmParametersToPropertyInfos = new Dictionary<string, PropertyInfo>();


        private List<string> _specialHandlingProperties = new List<string>() { "Latitude", "Longitude", "WgsLatitude", "WgsLongitude" };

        private string _reportMetaData = "";
        IMxSCustomerReportImageProvider customerReportImageProvider;

        private readonly ILogger _logger;

        [ImportingConstructor]
        public MxSSurveyReportDataProvider(IMxSCustomerReportImageProvider customerReportImageProvider,ILoggerFactory loggerFactory)
        {
            this.customerReportImageProvider = customerReportImageProvider;
            InitPropertiesCollection();
             _logger = loggerFactory.CreateLogger<MxSSurveyReportDataProvider>();
        }

        private void InitPropertiesCollection()
        {
            _wellParametersToPropertyInfos = typeof(Well).GetSimpleTypeProperties("", "");
            _correctedSurveyParametersToPropertyInfos = typeof(CorrectedSurvey).GetSimpleTypeProperties("", "");
            typeof(ShortSurvey).GetSimpleTypeProperties("", "");
            _rawSurveyParametersToPropertyInfos = typeof(RawSurvey).GetSimpleTypeProperties("", "");
            _planSurveyParametersToPropertyInfos = typeof(PlanSurvey).GetSimpleTypeProperties("", "");
            _runParametersToPropertyInfos = typeof(Run).GetSimpleTypeProperties("", "");
            _solutionParametersToPropertyInfos = typeof(Solution).GetSimpleTypeProperties("", "");
            _mfmParametersToPropertyInfos = typeof(Waypoint).GetSimpleTypeProperties("", "");
            _ifr1ParametersToPropertyInfos = typeof(Waypoint).GetSimpleTypeProperties("", "");
        }

        public string GetMetaData(Well well)
        {
            Report reportData = new Report();
            var unitSystem = well.UnitSystem;
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.WELL_NAME, _wellParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.TABLE_Run_NAME, _runParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.TABLE_Solution_NAME, _solutionParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.TABLE_CorrectedSurvey_NAME, _correctedSurveyParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.TABLE_RawSurvey_NAME, _rawSurveyParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.TABLE_PlanSurvey_NAME, _planSurveyParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.TABLE_IFR1_NAME, _ifr1ParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));
            reportData.MetaData.AddRange(GetMetaDataList(MxSSurveyReportDataProviderConstants.TABLE_MFM_NAME, _mfmParametersToPropertyInfos, unitSystem, SurveyFormatConstant.ReportFormats));

            AddImagesMetaData(well, reportData);
            return GetXMLString(reportData);
        }

        public bool CreateReportDataFile(Well well, string outputFilePath, ReportParameters reportParameters)
        {
            try
            {
                _logger.LogCritical("Yug middel CreateReportDataFile start");
                XmlDocument doc = new XmlDocument();
                _reportMetaData = GetMetaData(well);
                doc.LoadXmlString(_reportMetaData);
                AddDataNodesToXmlDoc(well, ref doc, reportParameters);
                doc.Save(outputFilePath);
                _logger.LogCritical("Yug middel CreateReportDataFile end");
                return true;
            }
            catch (Exception e)
            {
                //TODO: Log Error
            }
            return false;
        }

        public void ResetImageCacheForWell(Guid wellId)
        {
            customerReportImageProvider.ResetImageCacheForWell(wellId);
        }

        private void AddDataNodesToXmlDoc(Well well, ref XmlDocument doc, ReportParameters reportParameters)
        {
            XmlNode reportNode = doc.SelectSingleNode("Report");
            XmlElement dataElement = doc.CreateElement("Data");
            AddWellData(well, doc, ref dataElement);
            AddCorrectedSurveyData(well, doc, ref dataElement, reportParameters);
            AddFullCorrectedSurveyData(well, doc, ref dataElement, reportParameters);
            AddShortCorrectedSurveyData(well, doc, ref dataElement, reportParameters);
            AddPlanSurveyData(well, doc, ref dataElement);
            AddRawSurveyData(well, doc, ref dataElement);
            AddRunData(well, doc, ref dataElement);
            AddSolutionData(well, doc, ref dataElement);
            AddMFMData(well, doc, ref dataElement);
            AddIFR1Data(well, doc, ref dataElement);
            reportNode?.AppendChild(dataElement);
        }

        private void AddWellData(Well well, XmlDocument doc, ref XmlElement dataElement)
        {
            AddDataNode(MxSSurveyReportDataProviderConstants.WELL_NAME, well, _wellParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
        }

        private void AddCorrectedSurveyData(Well well, XmlDocument doc, ref XmlElement dataElement, ReportParameters reportParameters)
        {
            var allSurveys = new List<Tuple<double, DateTime, CorrectedSurvey>>();

            var correctedSurveys = reportParameters != null && reportParameters.IsDefinitiveSurveyOnly
                ? well.AllCorrectedSurveys.Where(cs => cs.SurveyType == MxSSurveyType.Definitive).ToList()
                : well.AllCorrectedSurveys.ToList();

            var shortSurveys = reportParameters != null && reportParameters.IsDefinitiveSurveyOnly
                ? well.AllShortSurveys.Where(cs => cs.SurveyType == MxSSurveyType.Definitive).ToList()
                : well.AllShortSurveys.ToList();

            if (correctedSurveys.Any())
            {
                allSurveys.AddRange(correctedSurveys.Select(correctedSurvey => new Tuple<double, DateTime, CorrectedSurvey>(correctedSurvey.Depth, correctedSurvey.DateTime, correctedSurvey)));
            }

            if (shortSurveys.Any())
            {
                allSurveys.AddRange(shortSurveys.Select(shortSurvey => new Tuple<double, DateTime, CorrectedSurvey>(shortSurvey.Depth, shortSurvey.DateTime, new CorrectedSurvey(shortSurvey))));
            }

            allSurveys = allSurveys.OrderBy(s => s.Item1).ThenBy(s => s.Item2).ToList();

            foreach (var survey in allSurveys)
            {
                AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_CorrectedSurvey_NAME, survey.Item3, _correctedSurveyParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
            }
        }

        private void AddFullCorrectedSurveyData(Well well, XmlDocument doc, ref XmlElement dataElement, ReportParameters reportParameters)
        {
            var allSurveys = new List<Tuple<double, DateTime, CorrectedSurvey>>();

            var correctedSurveys = reportParameters != null && reportParameters.IsDefinitiveSurveyOnly
                ? well.AllCorrectedSurveys.Where(cs => cs.SurveyType == MxSSurveyType.Definitive).ToList()
                : well.AllCorrectedSurveys.ToList();

            if (correctedSurveys.Any())
            {
                allSurveys.AddRange(correctedSurveys.Select(correctedSurvey => new Tuple<double, DateTime, CorrectedSurvey>(correctedSurvey.Depth, correctedSurvey.DateTime, correctedSurvey)));
            }

            allSurveys = allSurveys.OrderBy(s => s.Item1).ThenBy(s => s.Item2).ToList();
            foreach (var survey in allSurveys)
            {
                AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_FullCorrectedSurvey_NAME, survey.Item3, _correctedSurveyParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
            }
        }

        private void AddShortCorrectedSurveyData(Well well, XmlDocument doc, ref XmlElement dataElement, ReportParameters reportParameters)
        {
            var allSurveys = new List<Tuple<double, DateTime, CorrectedSurvey>>();

            var shortSurveys = reportParameters != null && reportParameters.IsDefinitiveSurveyOnly
                ? well.AllShortSurveys.Where(cs => cs.SurveyType == MxSSurveyType.Definitive).ToList()
                : well.AllShortSurveys.ToList();

            if (shortSurveys.Any())
            {
                allSurveys.AddRange(shortSurveys.Select(shortSurvey => new Tuple<double, DateTime, CorrectedSurvey>(shortSurvey.Depth, shortSurvey.DateTime, new CorrectedSurvey(shortSurvey))));
            }

            allSurveys = allSurveys.OrderBy(s => s.Item1).ThenBy(s => s.Item2).ToList();

            foreach (var survey in allSurveys)
            {
                AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_ShortCorrectedSurvey_NAME, survey.Item3, _correctedSurveyParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
            }
        }

        private void AddPlanSurveyData(Well well, XmlDocument doc, ref XmlElement dataElement)
        {
            foreach (PlanSurvey survey in well.AllPlanSurveys.OrderBy(s => s, new SurveyComparer()))
            {
                AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_PlanSurvey_NAME, survey, _planSurveyParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
            }
        }

        private void AddRawSurveyData(Well well, XmlDocument doc, ref XmlElement dataElement)
        {
            foreach (RawSurvey survey in well.AllRawSurveys.OrderBy(s => s, new SurveyComparer()))
            {
                AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_RawSurvey_NAME, survey, _rawSurveyParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
            }
        }

        private void AddRunData(Well well, XmlDocument doc, ref XmlElement dataElement)
        {
            foreach (Run run in well.Runs.OrderBy(r => r.RunNumber))
            {
                AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_Run_NAME, run, _runParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
            }
        }

        private void AddSolutionData(Well well, XmlDocument doc, ref XmlElement dataElement)
        {
            foreach (Solution solution in well.AllSolutions)
            {
                AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_Solution_NAME, solution, _solutionParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
            }
        }

        private void AddMFMData(Well well, XmlDocument doc, ref XmlElement dataElement)
        {
            foreach (Waypoint waypoint in well.Waypoints.OrderBy(i => i, new SurveyComparer()))
            {
                if (waypoint.Type == MxSWaypointType.MFM)
                {
                    AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_MFM_NAME, waypoint, _mfmParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
                }
            }
        }

        private void AddIFR1Data(Well well, XmlDocument doc, ref XmlElement dataElement)
        {
            foreach (Waypoint waypoint in well.Waypoints.OrderBy(i => i, new SurveyComparer()))
            {
                if (waypoint.Type == MxSWaypointType.IFR1)
                {
                    AddDataNode(MxSSurveyReportDataProviderConstants.TABLE_IFR1_NAME, waypoint, _ifr1ParametersToPropertyInfos, doc, well.UnitSystem, ref dataElement);
                }
            }
        }

        private void AddDataNode(string tableName, object data, Dictionary<string, PropertyInfo> propertyInfos, XmlDocument doc, MxSUnitSystemEnum unitSystem, ref XmlElement dataElement)
        {
            XmlElement dataNode = doc.CreateElement(tableName);
            foreach (string parameter in propertyInfos.Keys)
            {
                if (IsSpecialHandling(parameter))
                {
                    dataNode.SetAttribute(parameter, GetValueFromSpecialHandling(parameter, propertyInfos[parameter].GetValue(data)));
                }
                else
                {
                    var actualValue = Convert.ToString(propertyInfos[parameter].GetValue(data));
                    var unitConvertedValue = FormatNumberHelper.ConvertAndFormatByTypeWhenNumber(actualValue, parameter, SurveyFormatConstant.ReportFormats, unitSystem, false);
                    var handledValue = HandleDecimalExponentialNotation(unitConvertedValue);
                    dataNode.SetAttribute(parameter, handledValue);
                }
            }
            dataElement.AppendChild(dataNode);
        }

        private bool IsSpecialHandling(string propertyName)
        {
            return _specialHandlingProperties.Contains(propertyName);
        }

        private string GetValueFromSpecialHandling(string propertyName, object value)
        {
            if (propertyName.Equals("Latitude") || propertyName.Equals("WgsLatitude"))
            {
                return CoordService.ToGeoLatitudeString(Convert.ToDouble(value));
            }
            else if (propertyName.Equals("Longitude") || propertyName.Equals("WgsLongitude"))
            {
                return CoordService.ToGeoLongitudeString(Convert.ToDouble(value));
            }
            return Convert.ToString(value);
        }

        private string HandleDecimalExponentialNotation(string unitConvertedValue)
        {
            double number;
            if (double.TryParse(unitConvertedValue, out number) && !double.IsNaN(number))
            {
                return Decimal.Parse(unitConvertedValue, NumberStyles.Any, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            }
            return unitConvertedValue;
        }

        private List<ReportMetaData> GetMetaDataList(string tableWell, Dictionary<string, PropertyInfo> parametersToPropertyInfos, MxSUnitSystemEnum unitSystem, Dictionary<string, MxSFormatType> formatTypes)
        {
            List<ReportMetaData> metaDataCollection = new List<ReportMetaData>();
            if (parametersToPropertyInfos != null)
            {
                foreach (string parameter in parametersToPropertyInfos.Keys)
                {
                    var metaData = new ReportMetaData() { Table = tableWell, Field = parameter, Label = parameter };
                    if (formatTypes != null && formatTypes.ContainsKey(parameter))
                    {
                        var unitInfo = FormatHelper.GetPrecision(formatTypes[parameter], unitSystem);
                        metaData.Unit_Label = unitInfo.UnitName;
                        metaData.Decimal_Places = unitInfo.Precision;
                    }
                    metaDataCollection.Add(metaData);
                }
            }
            return metaDataCollection;
        }

        private void AddImagesMetaData(Well well, Report reportData)
        {
            reportData.From_App = new ReportFromApp();
            reportData.From_App.Plots = new List<ReportPlot>();
            foreach (string chart in _chartsSupported)
            {
                reportData.From_App.Plots.Add(new ReportPlot() { Name = chart, Path = "" });
            }

            foreach (var plot in customerReportImageProvider.GetAll(well.Id))
            {
                reportData.From_App.Plots.Add(new ReportPlot() { Name = plot.Name.StripFileExtension(), Path = plot.FullName });
            }
        }

        private string GetXMLString(Report reportData)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(reportData.GetType());
            serializer.Serialize(stringwriter, reportData);
            return stringwriter.ToString();
        }
    }
}
