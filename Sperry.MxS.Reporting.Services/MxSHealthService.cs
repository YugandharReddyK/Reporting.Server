using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Services
{
    public class MxSHealthService : IMxSHealthService
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        public MxSHealthService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<MxSHealthService>();
        }


        public HealthState GetHealthState()
        {
            HealthState healthState = new HealthState()
            {
                Name = $"Reporting {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}",
                Message = new List<string> { $"Reached Reporting 7 ---- {JsonConvert.SerializeObject(_loggerFactory?.CreateLogger<MxSHealthService>())}"},
                Status = Core.Common.Enums.MxSHealthStatusEnum.Pass
            };
            try
            {
                //HealthState dataProviderHealthState = GetHealthState(_dataProviderCommunicator, "Data Provider", accessToken);
                //if (dataProviderHealthState != null)
                //{
                //    healthState.HealthStates.Add(dataProviderHealthState);
                //}
                //else
                //{
                //    healthState.Message.Add("Failed to connect to Data Provider");
                //}

            }
            catch (Exception ex)
            {

            }
            return healthState;
        }
    }
}
