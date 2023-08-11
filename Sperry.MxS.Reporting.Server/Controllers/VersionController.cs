using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Sperry.MxS.Core.Common.Models.Results;
using Sperry.MxS.Reporting.Common.Constants.Routes.CustomerReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Server.Controllers
{
    [ApiController]
    [Route(CustomerReportRouteConstants.BaseUrl)]
    public class VersionController : ControllerBase
    {
        private static Version _version = null;
        private readonly ILogger _logger;

        public VersionController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<VersionController>();
            if (_version == null)
            {
                _version = GetType().Assembly.GetName().Version;
            }
        }

        [HttpGet]
        [Route("serverversion")]
        public IActionResult GetVersion()
        {
            return Ok(new ResultObject<string>() { Data = _version.ToString() });
        }
    }
}
