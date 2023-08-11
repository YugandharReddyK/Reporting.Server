using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sperry.MxS.Core.Common.Constants.Routes;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models.Results;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Infrastructure.Helper;
using Sperry.MxS.Reporting.Common.Constants.Routes.CustomerReport;
using System;

namespace Sperry.MxS.Reporting.Server.Controllers
{
    [Route(CustomerReportRouteConstants.BaseUrl)]
    public class HealthController : ControllerBase
    {
        private IMxSHealthService _healthService;
        private readonly ILogger _logger;

        public HealthController(IMxSHealthService healthService, ILoggerFactory loggerFactory)
        {
            _healthService = healthService;
            _logger = loggerFactory.CreateLogger<HealthController>();
        }

        [HttpGet]
        public string Default()
        {
            return "You have reached the MxSSurvey Reporting - Health Server !";
        }

        [HttpGet]
        [Route(HealthRouteConstants.GetHealthRoute)]
        public IActionResult GetHealth()
        {
            ResultObject<HealthState> result = new ResultObject<HealthState>();
            try
            {
                result.Data = _healthService.GetHealthState();
                if (result.Success)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
            }
            return BadRequest(result);
        }



    }
}
