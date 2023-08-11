using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Reporting.Common.Models.Comms;

namespace Sperry.MxS.Reporting.Server.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddRoutingConfiguration(this IServiceCollection service, IConfiguration config)
        {
            service.Configure<MxSDataProviderCommunicationInfo>(config.GetSection("DataProvider"));
            service.TryAddSingleton(sp => sp.GetRequiredService<IOptions<MxSDataProviderCommunicationInfo>>().Value);

            service.Configure<MxSServerInfo>(config.GetSection("MxSWebAppServer:MxSServerInfo"));
            service.TryAddSingleton(sp => sp.GetRequiredService<IOptions<MxSServerInfo>>().Value);

            return service;
        }
    }
}
