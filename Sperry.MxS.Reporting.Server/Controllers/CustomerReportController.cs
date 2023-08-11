using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Sperry.MxS.Reporting.Common.Constants.Routes.CustomerReport;
using Sperry.MxS.Reporting.Common.Interfaces;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Results;
using Sperry.MxS.Core.Common.Results;
using Sperry.MxS.Core.Common.Models.CustomerReport;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Requests;

namespace Sperry.MxS.Reporting.Server.Controllers
{
    //[Authorize]
    [Route(CustomerReportRouteConstants.BaseUrl)]
    public class CustomerReportController : Controller
    {
        #region Private Members
        private readonly IMxSCustomerReportService _customerReportService;

        private static bool _isProcessing = false;
        private readonly ILogger _logger;
        #endregion

        #region Constructor

        public CustomerReportController(ILoggerFactory loggerFactory,IMxSCustomerReportService customerReportService)
        {
            _logger = loggerFactory.CreateLogger<CustomerReportController>();
            _customerReportService = customerReportService;
        }

        #endregion

        //[HttpPost]
        //[Route(CustomerReportRouteConstants.GenerateFromMasterTemplate)]
        //public IActionResult GenerateFromMasterTemplate(Guid templateId, Guid wellId, [FromBody] ReportParameters reportParameters, string username)
        //{
        //    ResultObject<CustomerReportResult<CustomerReport>> result = new ResultObject<CustomerReportResult<CustomerReport>>();
        //    try
        //    {
        //        _logger.LogCritical($"GenerateFromMasterTemplate start");
        //        result.Data = _customerReportService.GenerateMasterTemplate(templateId, wellId, reportParameters, username, _isProcessing);
        //        _logger.LogCritical($"GenerateFromMasterTemplate end result data");
        //        if (result != null)
        //        {
        //            result.AddMessage("Succes");
        //            return Ok(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical($"GenerateFromMasterTemplate getting Excepton {ex}");
        //        _isProcessing = false;
        //        result.AddMessageWithException("GenerateFromMasterTemplate first", ex);
        //        _logger.LogCritical(0, ex, "Error on GenerateFromMasterTemplate templates");
        //    }
        //    _logger.LogCritical($"GenerateFromMasterTemplate BadRequest");
        //    result.AddMessage("Error on controller new");
        //    return BadRequest(result);
        //}

        //[HttpPost]
        //[Route(CustomerReportRouteConstants.GetChartsForTemplates)]
        //public IActionResult GetChartsForTemplates([FromBody] List<Guid> templates)
        //{
        //    ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>> result;
        //    _logger.LogCritical($"GetChartsForTemplates start");
        //    try
        //    {
        //        result = _customerReportService.GetChartsForTemplates(templates);
        //        _logger.LogCritical($"GetChartsForTemplates getting result");
        //        result.AddMessage("Chart is not there");
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical($"GetChartsForTemplates gettinf exception{ex}");
        //        result = new ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>>();
        //        string errorMsg = $"GetChartsForTemplates";
        //        result.AddMessageWithException(errorMsg, ex);
        //        _logger.LogCritical(0, ex, errorMsg);
        //    }
        //    return BadRequest(result);
        //}

        [HttpGet]
        [Route(CustomerReportRouteConstants.DownloadMetaData)]
        public IActionResult DownloadMetaData(Guid wellId)
        {
            ResultObject<CustomerReportTemplateData> result;
            try
            {
                result = _customerReportService.DownloadMetaData(wellId);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                result = new ResultObject<CustomerReportTemplateData>();
                result.AddException(ex);
                _logger.LogCritical(0, ex, "Error on DownloadMetaData");
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route(CustomerReportRouteConstants.GenerateFromSubReportTemplates)]
        public IActionResult GenerateFromSubReportTemplates(Guid wellId, [FromBody] SubReportPayload subReportBody, string username)
        {
            CustomerReportResult<CustomerReport> result = null;
            try
            {
                result = _customerReportService.GenerateSubReportTemplates(subReportBody.subreportTemplates, wellId, subReportBody.ReportParameters, username);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                result = new CustomerReportResult<CustomerReport>();
                result.AddException(ex);
                _logger.LogCritical(0, ex, "Error on GenerateFromSubReportTemplates");
            }
            return BadRequest(result);
        }

        //[HttpPost]
        //[Route(CustomerReportRouteConstants.GetImagesForTemplates)]
        //public IActionResult GetImagesForTemplates([FromQuery] Guid wellId, [FromBody] List<Guid> templateIds)
        //{
        //    ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>> result = null;
        //    _logger.LogCritical($"GetImagesForTemplates start");
        //    try
        //    {
        //        result = _customerReportService.GetImagesForTemplates(wellId, templateIds);
        //        _logger.LogCritical($"GetImagesForTemplates getting result");
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical($"GetImagesForTemplates getting exception{ex}");
        //        result = new ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>>();
        //        result.AddException(ex);
        //        _logger.LogCritical(0, ex, "Error on GetImagesForTemplates");
        //    }
        //    return BadRequest(result);
        //}

        #region MyRegion

        [HttpPost]
        [Route(CustomerReportRouteConstants.GenerateFromMasterTemplate)]
        public IActionResult GenerateFromMasterTemplate([FromQuery] string username, [FromBody] JobRequest request)
        {
            ResultObject<CustomerReportResult<CustomerReport>> result = new ResultObject<CustomerReportResult<CustomerReport>>();
            try
            {
                _logger.LogCritical($"GenerateFromMasterTemplate start");
                result.Data = _customerReportService.GenerateMasterTemplate(request, username, _isProcessing);
                _logger.LogCritical($"GenerateFromMasterTemplate end result data");
                if (result != null)
                {
                    result.AddMessage("Succes");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"GenerateFromMasterTemplate getting Excepton {ex}");
                _isProcessing = false;
                result.AddMessageWithException("GenerateFromMasterTemplate first", ex);
                _logger.LogCritical(0, ex, "Error on GenerateFromMasterTemplate templates");
            }
            _logger.LogCritical($"GenerateFromMasterTemplate BadRequest");
            result.AddMessage("Error on controller new");
            return BadRequest(result);
        }

        [HttpPost]
        [Route(CustomerReportRouteConstants.GetImagesForTemplates)]
        public IActionResult GetImagesForTemplates([FromBody] JobRequest request)
        {
            ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>> result;
            _logger.LogCritical($"GetImagesForTemplates start");
            try
            {
                result = _customerReportService.GetImagesForTemplates(request);
                _logger.LogCritical($"GetImagesForTemplates getting result");
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"GetImagesForTemplates getting exception{ex}");
                result = new ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>>();
                result.AddException(ex);
                _logger.LogCritical(0, ex, "Error on GetImagesForTemplates");
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route(CustomerReportRouteConstants.GetChartsForTemplates)]
        public IActionResult GetChartsForTemplates([FromBody] List<CustomerReportTemplateData> templatesData)
        {
            ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>> result;
            _logger.LogCritical($"GetChartsForTemplates start");
            try
            {
                result = _customerReportService.GetChartsForTemplates(templatesData);
                _logger.LogCritical($"GetChartsForTemplates getting result");
                result.AddMessage("Chart is not there");
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"GetChartsForTemplates gettinf exception{ex}");
                result = new ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>>();
                string errorMsg = $"GetChartsForTemplates";
                result.AddMessageWithException(errorMsg, ex);
                _logger.LogCritical(0, ex, errorMsg);
            }
            return BadRequest(result);
        }

        #endregion
    }
}
