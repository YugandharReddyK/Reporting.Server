using Sperry.MxS.Core.Common.Models.CustomerReport;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Results;
using Sperry.MxS.Reporting.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Models.Results;
using Sperry.MxS.Reporting.ReportDoc;
using Sperry.MxS.Reporting.ReportDoc.Interface;
//using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Sperry.MxS.Reporting.Common.Interfaces.IServiceManager;
using Sperry.MxS.Reporting.Common.Constants.Routes.DataProvider;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Logging;
using DocumentFormat.OpenXml.Wordprocessing;
using Sperry.MxS.Core.Common.Constants;
using System.Globalization;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Requests;
using Sperry.MxS.Reporting.ReportDoc.Extensions;

namespace Sperry.MxS.Reporting.Services
{
    public class MxSCustomerReportService : IMxSCustomerReportService
    {
        #region Private Members
        private readonly ILogger _logger;
        private readonly IMxSCustomerReport _customerReport;
        private readonly IMxSWellService _wellService;
        private readonly IMxSDataProviderCommunicator _dataProviderCommunicator;
        private readonly IMxSReportTemplateRepository _templateRepository;
        //private readonly IMxSRealtimeService _mxSRealtimeService;

        #endregion

        #region Constructor

        public MxSCustomerReportService(ILoggerFactory loggerFactory, IMxSCustomerReport customerReport, IMxSWellService wellService, IMxSReportTemplateRepository templateRepository, IMxSDataProviderCommunicator dataProviderCommunicator)
        {
            _logger = loggerFactory.CreateLogger<MxSCustomerReportService>();
            _customerReport = customerReport;
            _templateRepository = templateRepository;
            _dataProviderCommunicator = dataProviderCommunicator;
            this._wellService = wellService;
            //_mxSRealtimeService = mxSRealtimeService;
        }

        #endregion

        //public CustomerReportResult<CustomerReport> GenerateMasterTemplate(Guid templateId, Guid wellId, ReportParameters reportParameters, string username, bool isProcessing)
        //{
        //    CustomerReportResult<CustomerReport> result = new CustomerReportResult<CustomerReport>();
        //    if (!isProcessing)
        //    {
        //        Func<Task> doAsync = async () =>
        //        {
        //            isProcessing = true;
        //            result = GenerateFromMasterTemplate(templateId, wellId, reportParameters, username, isProcessing);
        //        };
        //        var staThread = new Thread(() =>
        //        {
        //            doAsync();
        //        });
        //        staThread.CurrentCulture = GetCulterForReport();
        //        staThread.SetApartmentState(ApartmentState.STA);
        //        staThread.IsBackground = true;
        //        staThread.Start();
        //        staThread.Join();
        //        isProcessing = false;
        //    }
        //    else
        //    {
        //        result.AddError("Error on report generation. Another request is already in progress. Please try again later.");
        //    }
        //    return result;
        //}


        //public CustomerReportResult<CustomerReport> GenerateFromMasterTemplate(Guid templateId, Guid wellId, ReportParameters reportParameters, string user, bool isProcessing)
        //{
        //    CustomerReportResult<CustomerReport> result = new CustomerReportResult<CustomerReport>();
        //    _logger.LogCritical($"GenerateFromMasterTemplate service");
        //    try
        //    {
        //        ResultObject<byte[]> compressedWell = _wellService.GetWell(wellId);
        //        Well well = GZipCompression.DecompressAndDeserialize<Well>(compressedWell.Data);
        //        _logger.LogCritical($"Well information {well.Name}");
        //        if (well != null)
        //        {
        //            var templateFile = GenerateTemplateFileFromId(templateId);

        //            result = VerifyMissingImages(well, new List<string>() { templateFile });
        //            _logger.LogCritical($"VerifyMissingImages success - Path : {Path.GetTempFileName()} - {JsonConvert.SerializeObject(result)}");
        //            if (result.Success)
        //            {
        //                var generatedReportPath = Path.GetTempFileName();
        //                var reportResponse = _customerReport.GenerateReport(well, templateFile, generatedReportPath, reportParameters);
        //                _logger.LogCritical($"Yug 87 -- Sandy {JsonConvert.SerializeObject(reportResponse)}");
        //                result = ProcessReportResponse(well, user, reportResponse, templateId);
        //                //need to call reatime service
        //                //_mxSRealtimeService.BroadcastCustomerReportGeneratedForWell(wellId);
        //            }
        //            else
        //            {
        //                string st = "eroor on VerifyMissingImages";
        //                result.AddMessage(st);
        //            }
        //        }
        //        else
        //        {
        //            string st = "error on get well";
        //            result.AddMessage(st);
        //            // result.AddMessage(well.Message);
        //            // _log.LogCritical(0, well.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical($"Yug 104 -- Sandy {ex.Message}");
        //        result.AddMessageWithException("Error in GenerateFromMasterTemplate", ex);
        //        _logger.LogCritical($"Yug 105 -- Sandy {ex.InnerException}");

        //    }
        //    _logger.LogCritical($"Yug 106 -- Sandy {result.Data}");
        //    _logger.LogCritical($"Yug 107 -- Sandy {result.Message}");
        //    return result;
        //}

        //public ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>> GetChartsForTemplates(List<Guid> templates)
        //{

        //    var result = new ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>>();

        //    Dictionary<Guid, string> templateFiles = new Dictionary<Guid, string>();
        //    foreach (var template in templates)
        //    {
        //        templateFiles.Add(template, GenerateTemplateFileFromId(template));
        //    }
        //    var charts = _customerReport.GetChartsForTemplates(templateFiles);
        //    result.Data = charts;

        //    return result;
        //}

        private CustomerReportResult<CustomerReport> VerifyMissingImages(Well well, IEnumerable<string> templateFiles)
        {
            var result = new CustomerReportResult<CustomerReport>();
            var missingImages = _customerReport.GetMissingImagesForTemplates(well, templateFiles);
            if (missingImages.Any())
            {
                result.AddMessage("Images are Missing");
                result.AddMissingImageError(missingImages);
            }
            return result;
        }

        private CustomerReportResult<CustomerReport> ProcessReportResponse(Well well, string userName, HalReport reportResponse, Guid? templateId)
        {
            var result = new CustomerReportResult<CustomerReport>();
            _logger.LogCritical($"Yug 88 -- Sandy {JsonConvert.SerializeObject(result)}");
            if (!reportResponse.ErrorMessages.Any() && File.Exists(reportResponse.ReturnUri.LocalPath))
            {
                _logger.LogCritical("Yug 101 -- Sandy ProcessReportResponse inside if conduction  is not reportResponse.ErrorMessages.Any() && File.Exists(reportResponse.ReturnUri.LocalPath)");
                result.Data = SaveReportToDatabase(well, userName, reportResponse, templateId);
            }
            else
            {
                _logger.LogCritical("Yug 102 -- Sandy ProcessReportResponse Inside else condition is getting error ");
                reportResponse.ErrorMessages.ForEach((error) => result.AddError(error));
            }
            _logger.LogCritical($"Yug 103 -- Sandy ProcessReportResponse returning {JsonConvert.SerializeObject(result)}");
            return result;
        }

        public ResultObject<CustomerReport> GetCustomerReport(Guid wellId)
        {
            return _dataProviderCommunicator.GetServerResponse<CustomerReport>($"{DataProviderRouteConstants.GetCustomerReport}?wellId={wellId}");
        }

        private CustomerReport SaveReportToDatabase(Well well, string userName, HalReport reportResult, Guid? templateId)
        {
            byte[] reportBytes = File.ReadAllBytes(reportResult.ReturnUri.LocalPath);
            _logger.LogCritical($"Yug 89 -- Sandy {reportBytes}");
            var customerReportResponse = GetCustomerReport(well.Id);

            _logger.LogCritical($"Yug 90 -- Sandy {customerReportResponse}");

            var reportDataResponse = DownloadReport(well.Id);

            _logger.LogCritical($"Yug 91 -- Sandy {reportDataResponse}");

            CustomerReport customerReport = null;
            CustomerReportData reportData = null;

            if (userName.Equals(well.LockedBy, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogCritical("Yug 94 -- Sandy Well is locked");
                if (customerReportResponse.Success && reportDataResponse.Success)
                {
                    _logger.LogCritical("Yug 95 -- Sandy customerReportResponse.Success true && reportDataResponse.Success true");
                    customerReport = customerReportResponse.Data;

                    _logger.LogCritical($"Yug 96 -- Sandy {JsonConvert.SerializeObject(customerReport)}");

                    reportData = reportDataResponse.Data;

                    _logger.LogCritical($"Yug 97 -- Sandy {JsonConvert.SerializeObject(reportData)}");

                    reportData.UpdateFrom(new CustomerReportData() { ReportData = reportBytes, WellId = well.Id });

                    _logger.LogCritical($"Yug 98 -- Sandy UpdateFrom {reportData}");

                    customerReport.ReportData = reportData;
                    customerReport.TemplateId = templateId.HasValue ? templateId : null;
                    customerReport.LastEditedBy = userName;

                    _logger.LogCritical($"Yug 99 -- Sandy {customerReport}");
                }
                else
                {
                    customerReport = CreateCustomerReport(well, userName, templateId, reportBytes);
                    reportData = customerReport.ReportData;
                }
                //SaveCustomerReportData(reportData);
                //SaveCustomerReport(customerReport);
            }
            else
            {
                _logger.LogCritical("Yug 92 -- Sandy SaveReportToDatabase in else");
                customerReport = CreateCustomerReport(well, userName, templateId, reportBytes);
                _logger.LogCritical($"Yug 93 -- Sandy {customerReport}");
            }

            _logger.LogCritical($"Yug 100 -- Sandy returning the SaveReportToDatabase {JsonConvert.SerializeObject(customerReport)}");

            return customerReport;
        }

        public ResultObject<CustomerReportTemplateData> DownloadMetaData(Guid id)
        {
            var result = new ResultObject<CustomerReportTemplateData>();
            var metafilePath = Path.GetTempFileName().Replace(".tmp", ".xml");
            try
            {
                ResultObject<byte[]> compressedWell = _wellService.GetWell(id);
                if (compressedWell.Data != null)
                {
                    Well well = GZipCompression.DecompressAndDeserialize<Well>(compressedWell.Data);
                    if (well != null)
                    {
                        _customerReport.CreateReportDataFile(well, metafilePath, null);
                        if (File.Exists(metafilePath))
                        {
                            byte[] reportBytes = File.ReadAllBytes(metafilePath);
                            if (reportBytes.Count() > 0)
                            {
                                result.Data = new CustomerReportTemplateData { Data = reportBytes };
                            }
                            else
                            {
                                result.AddError("Metadata not generated, content");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMsg = "Error on DownloadMetaData";
                result.AddMessageWithException(errorMsg, ex);
                _logger.LogCritical(0, ex, errorMsg);
            }
            finally
            {
                try
                {
                    File.Delete(metafilePath);
                }
                catch { }

            }
            return result;
        }

        public CustomerReportResult<CustomerReport> GenerateFromSubReportTemplates(IEnumerable<CustomerReportTemplate> templates, Guid wellId, ReportParameters reportParameters, string user)
        {
            CustomerReportResult<CustomerReport> result = new CustomerReportResult<CustomerReport>();

            ResultObject<byte[]> compressedWell = _wellService.GetWell(wellId);
            ResultObject<Well> well = GZipCompression.DecompressAndDeserialize<ResultObject<Well>>(compressedWell.Data);
            if (well.Success && well.Data != null)
            {
                try
                {
                    well.Data.MagneticValuesNeedsUpdating = true;
                    well.Data.CalculateMagneticInfomation();
                }
                catch { }
            }
            //TODO: Suhail - need to uncomment
            //SetTemplateFilesFromId(templates);

            result = VerifyMissingImages(well.Data, templates.Select(s => s.TemplateFile));

            if (result.Success)
            {
                var generatedReportPath = Path.GetTempFileName();
                var reportResponse = _customerReport.GenerateReport(well.Data, templates, generatedReportPath, reportParameters);
                result = ProcessReportResponse(well.Data, user, reportResponse, null);
                //_serverRealtimeService.Value.BroadcastCustomerReportGeneratedForWell(wellId);
            }

            return result;
        }

        //public ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>> GetImagesForTemplates(Guid wellId, IEnumerable<Guid> templates)
        //{
        //    var result = new ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>>();
        //    var filesToClean = new List<string>();
        //    try
        //    {
        //        var templateFiles = new Dictionary<Guid, IEnumerable<CustomerImages>>();
        //        foreach (var templateId in templates)
        //        {
        //            var templateFile = GenerateTemplateFileFromId(templateId);
        //            //filesToClean.Add(templateFile);
        //            var images = _customerReport.GetImagesForTemplates(wellId, new List<string>() { templateFile });
        //            var customerImages = new List<CustomerImages>();
        //            foreach (var image in images)
        //            {
        //                var customerImage = GetImage(wellId, image);
        //                if (customerImage.Success)
        //                {
        //                    customerImages.Add(customerImage.Data);
        //                }
        //                else
        //                {
        //                    customerImages.Add(new CustomerImages() { FileName = image });
        //                }
        //            }
        //            templateFiles.Add(templateId, customerImages);
        //        }
        //        result.Data = templateFiles;
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorMsg = $"GetImagesForTemplates";
        //        result.AddMessageWithException(errorMsg, ex);
        //        _logger.LogCritical(0, ex, errorMsg);
        //    }
        //    finally
        //    {
        //        FileCleanup(result, filesToClean);
        //    }
        //    return result;

        //}

        public CustomerReportResult<CustomerReport> GenerateSubReportTemplates(IEnumerable<CustomerReportTemplate> templates, Guid wellId, ReportParameters reportParameters, string userName)
        {
            CustomerReportResult<CustomerReport> result = new CustomerReportResult<CustomerReport>();
            var staThread = new Thread(() =>
            {
                result = GenerateFromSubReportTemplates(templates, wellId, reportParameters, userName);
            }
            );
            staThread.CurrentCulture = GetCulterForReport();
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            return result;
        }

        public ResultObject<CustomerImages> GetImage(Guid wellId, string imageName)
        {
            return _dataProviderCommunicator.GetServerResponse<CustomerImages>($"{DataProviderRouteConstants.DownloadReport}?wellId={wellId}&imageName={imageName}");
        }

        public ResultObject<CustomerReportData> DownloadReport(Guid wellId)
        {
            return _dataProviderCommunicator.GetServerResponse<CustomerReportData>($"{DataProviderRouteConstants.DownloadReport}?wellId={wellId}");

        }

        //TODO: Suhail - need to uncomment
        //private void SetTemplateFilesFromId(IEnumerable<CustomerReportTemplate> templates)
        //{
        //    templates.ToList().ForEach(template =>
        //    {
        //        template.TemplateFile = GenerateTemplateFileFromId(template.Id);
        //    });
        //}


        private void FileCleanup<T>(ResultObject<T> result, List<string> filesToClean)
        {
            filesToClean.ForEach(file => File.Delete(file));
        }

        private ResultObject<bool> SaveCustomerReportData(CustomerReportData customerReportData)
        {
            return _dataProviderCommunicator.PostToServer<bool>($"{DataProviderRouteConstants.SaveCustomerReportData}", customerReportData);
        }

        private ResultObject<bool> SaveCustomerReport(CustomerReport customerReport)
        {
            return _dataProviderCommunicator.PostToServer<bool>($"{DataProviderRouteConstants.SaveCustomerReport}", customerReport);
        }

        private static CustomerReport CreateCustomerReport(Well well, string userName, Guid? templateId, byte[] reportBytes)
        {
            return new CustomerReport(userName, well.Id, templateId)
            {
                ReportData = new CustomerReportData() { WellId = well.Id, ReportData = reportBytes, }
            };
        }

        //private string GenerateTemplateFileFromId(Guid templateId)
        //{
        //    return _templateRepository.Get(templateId).FullName;

        //}

        private CultureInfo GetCulterForReport()
        {
            CultureInfo cultureInfo = new CultureInfo(MxSConstants.DefaultCultureCode);
            cultureInfo.DateTimeFormat.ShortDatePattern = MxSConstants.DefaultDatePattern;
            cultureInfo.DateTimeFormat.ShortDatePattern = MxSConstants.DefaultDatePattern;
            cultureInfo.DateTimeFormat.LongTimePattern = MxSConstants.DefaultTimePattern;
            cultureInfo.DateTimeFormat.LongTimePattern = MxSConstants.DefaultTimePattern;
            return cultureInfo;
        }


        #region MyRegion

        public CustomerReportResult<CustomerReport> GenerateMasterTemplate(JobRequest request, string username, bool isProcessing)
        {
            CustomerReportResult<CustomerReport> result = new CustomerReportResult<CustomerReport>();
            if (!isProcessing)
            {
                Func<Task> doAsync = async () =>
                {
                    isProcessing = true;
                    result = GenerateFromMasterTemplate(request, username, isProcessing);
                };
                var staThread = new Thread(() =>
                {
                    doAsync();
                });
                staThread.CurrentCulture = GetCulterForReport();
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.IsBackground = true;
                staThread.Start();
                staThread.Join();
                isProcessing = false;
            }
            else
            {
                result.AddError("Error on report generation. Another request is already in progress. Please try again later.");
            }
            return result;
        }

        public CustomerReportResult<CustomerReport> GenerateFromMasterTemplate(JobRequest request, string user, bool isProcessing)
        {
            CustomerReportResult<CustomerReport> result = new CustomerReportResult<CustomerReport>();
            _logger.LogCritical($"GenerateFromMasterTemplate service");
            try
            {
                byte[] compressedWell = request.Payload;
                Well well = GZipCompression.DecompressAndDeserialize<Well>(compressedWell);
                _logger.LogCritical($"Well information {well.Name}");
                if (well != null)
                {
                    request.Data.TryGetValue("templateData", out string value);
                    CustomerReportTemplateData templateData = JsonConvert.DeserializeObject<CustomerReportTemplateData>(value);
                    var templateFile = GenerateTemplateFileFromId(templateData);
                   // TODO: Suhail - Need to uncomment
                   // result = VerifyMissingImages(well, new List<string>() { templateFile });
                    _logger.LogCritical($"VerifyMissingImages success - Path : {Path.GetTempFileName()} - {JsonConvert.SerializeObject(result)}");
                    if (result.Success)
                    {
                        var generatedReportPath = Path.GetTempFileName();
                        request.Data.TryGetValue("reportParameters", out string parameterValue);
                        ReportParameters reportParameters = JsonConvert.DeserializeObject<ReportParameters>(parameterValue);
                        var reportResponse = _customerReport.GenerateReport(well, templateFile, generatedReportPath, reportParameters);
                        _logger.LogCritical($"Yug 87 -- Sandy {JsonConvert.SerializeObject(reportResponse)}");
                        result = ProcessReportResponse(well, user, reportResponse, templateData.Id, request);
                        //need to call reatime service
                        //_mxSRealtimeService.BroadcastCustomerReportGeneratedForWell(wellId);
                    }
                    else
                    {
                        string st = "eroor on VerifyMissingImages";
                        result.AddMessage(st);
                    }
                }
                else
                {
                    string st = "error on get well";
                    result.AddMessage(st);
                    // result.AddMessage(well.Message);
                    // _log.LogCritical(0, well.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Yug 104 -- Sandy {ex.Message}");
                result.AddMessageWithException("Error in GenerateFromMasterTemplate", ex);
                _logger.LogCritical($"Yug 105 -- Sandy {ex.InnerException}");

            }
            _logger.LogCritical($"Yug 106 -- Sandy {result.Data}");
            _logger.LogCritical($"Yug 107 -- Sandy {result.Message}");
            return result;
        }

        public ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>> GetImagesForTemplates(JobRequest request)
        {
            var result = new ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>>();
            var filesToClean = new List<string>();
            try
            {
                request.Data.TryGetValue("templateData", out string value);
                List<CustomerReportTemplateData> templateDatas = JsonConvert.DeserializeObject<List<CustomerReportTemplateData>>(value);

                var templateFiles = new Dictionary<Guid, IEnumerable<CustomerImages>>();
                foreach (var templatedata in templateDatas)
                {
                    var templateFile = GenerateTemplateFileFromId(templatedata);
                    //filesToClean.Add(templateFile);
                    var images = _customerReport.GetImagesForTemplates(request.Payload, new List<string>() { templateFile });
                    var customerImages = new List<CustomerImages>();
                    //foreach (var image in images)
                    //{
                    //    var customerImage = GetImage(wellId, image);
                    //    if (customerImage.Success)
                    //    {
                    //        customerImages.Add(customerImage.Data);
                    //    }
                    //    else
                    //    {
                    //        customerImages.Add(new CustomerImages() { FileName = image });
                    //    }
                    //}
                    templateFiles.Add(templatedata.Id, customerImages);
                }
                result.Data = templateFiles;
            }
            catch (Exception ex)
            {
                var errorMsg = $"GetImagesForTemplates";
                result.AddMessageWithException(errorMsg, ex);
                _logger.LogCritical(0, ex, errorMsg);
            }
            finally
            {
                FileCleanup(result, filesToClean);
            }
            return result;

        }

        public ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>> GetChartsForTemplates(List<CustomerReportTemplateData> templatesData)
        {

            var result = new ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>>();

            Dictionary<Guid, string> templateFiles = new Dictionary<Guid, string>();
            foreach (var template in templatesData)
            {
                templateFiles.Add(template.Id, GenerateTemplateFileFromId(template));
            }
            var charts = _customerReport.GetChartsForTemplates(templateFiles);
            result.Data = charts;

            return result;
        }

        private string GenerateTemplateFileFromId(CustomerReportTemplateData templateData)
        {
            return _templateRepository.Get(templateData).FullName;

        }

        private CustomerReportResult<CustomerReport> ProcessReportResponse(Well well, string userName, HalReport reportResponse, Guid? templateId, JobRequest request)
        {
            var result = new CustomerReportResult<CustomerReport>();
            _logger.LogCritical($"Yug 88 -- Sandy {JsonConvert.SerializeObject(result)}");
            if (!reportResponse.ErrorMessages.Any() && File.Exists(reportResponse.ReturnUri.LocalPath))
            {
                _logger.LogCritical("Yug 101 -- Sandy ProcessReportResponse inside if conduction  is not reportResponse.ErrorMessages.Any() && File.Exists(reportResponse.ReturnUri.LocalPath)");
                result.Data = SaveReportToDatabase(well, userName, reportResponse, templateId, request);
            }
            else
            {
                _logger.LogCritical("Yug 102 -- Sandy ProcessReportResponse Inside else condition is getting error ");
                reportResponse.ErrorMessages.ForEach((error) => result.AddError(error));
            }
            _logger.LogCritical($"Yug 103 -- Sandy ProcessReportResponse returning {JsonConvert.SerializeObject(result)}");
            return result;
        }

        private CustomerReport SaveReportToDatabase(Well well, string userName, HalReport reportResult, Guid? templateId, JobRequest request)
        {
            byte[] reportBytes = File.ReadAllBytes(reportResult.ReturnUri.LocalPath);
            _logger.LogCritical($"Yug 89 -- Sandy {reportBytes}");
            request.Data.TryGetValue("customerReport", out string report);
            var customerReportResponse = JsonConvert.DeserializeObject<ResultObject<CustomerReport>>(report);
            //var customerReportResponse = GetCustomerReport(well.Id);

            _logger.LogCritical($"Yug 90 -- Sandy {customerReportResponse}");
            request.Data.TryGetValue("customerReportData", out string custReportData);

            var reportDataResponse = JsonConvert.DeserializeObject<ResultObject<CustomerReportData>>(custReportData);
            //var reportDataResponse = DownloadReport(well.Id);

            _logger.LogCritical($"Yug 91 -- Sandy {reportDataResponse}");

            CustomerReport customerReport = null;
            CustomerReportData reportData = null;

            if (userName.Equals(well.LockedBy, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogCritical("Yug 94 -- Sandy Well is locked");
                if (customerReportResponse.Success && reportDataResponse.Success)
                {
                    _logger.LogCritical("Yug 95 -- Sandy customerReportResponse.Success true && reportDataResponse.Success true");
                    customerReport = customerReportResponse.Data;

                    _logger.LogCritical($"Yug 96 -- Sandy {JsonConvert.SerializeObject(customerReport)}");

                    reportData = reportDataResponse.Data;

                    _logger.LogCritical($"Yug 97 -- Sandy {JsonConvert.SerializeObject(reportData)}");

                    reportData.UpdateFrom(new CustomerReportData() { ReportData = reportBytes, WellId = well.Id });

                    _logger.LogCritical($"Yug 98 -- Sandy UpdateFrom {reportData}");

                    customerReport.ReportData = reportData;
                    customerReport.TemplateId = templateId.HasValue ? templateId : null;
                    customerReport.LastEditedBy = userName;

                    _logger.LogCritical($"Yug 99 -- Sandy {customerReport}");
                }
                else
                {
                    customerReport = CreateCustomerReport(well, userName, templateId, reportBytes);
                    reportData = customerReport.ReportData;
                }
                //SaveCustomerReportData(reportData);
                //SaveCustomerReport(customerReport);
            }
            else
            {
                _logger.LogCritical("Yug 92 -- Sandy SaveReportToDatabase in else");
                customerReport = CreateCustomerReport(well, userName, templateId, reportBytes);
                _logger.LogCritical($"Yug 93 -- Sandy {customerReport}");
            }

            _logger.LogCritical($"Yug 100 -- Sandy returning the SaveReportToDatabase {JsonConvert.SerializeObject(customerReport)}");

            return customerReport;
        }

        #endregion

    }
}
