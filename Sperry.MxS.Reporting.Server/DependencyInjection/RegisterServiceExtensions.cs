using Microsoft.Extensions.DependencyInjection;
using Sperry.MxS.Reporting.Common.Interfaces.IServiceManager;
using Sperry.MxS.Reporting.ReportDoc.Interface;
using Sperry.MxS.Reporting.Common.Interfaces;
using Sperry.MxS.Reporting.Common.Services;
using Sperry.MxS.Reporting.Common;
using Sperry.MxS.Reporting.Services;
using Sperry.MxS.Core.Common.Interfaces;

namespace Sperry.MxS.Reporting.Server.DependencyInjection
{
    public static class RegisterServiceExtensions
    {
        public static IServiceCollection AddReportingServices(this IServiceCollection services)
        {
            services.AddScoped<IMxSCustomerReportService, MxSCustomerReportService>();
            services.AddScoped<IMxSWellService, MxSWellService>();
            services.AddScoped<IMxSCustomerReport, MxSSurveyReport>();
            services.AddScoped<IMxSReportTemplateRepository, MxSReportTemplateRepository>();
            services.AddScoped<IMxSCustomerReportDataProvider, MxSSurveyReportDataProvider>();
            services.AddScoped<IMxSDataProviderCommunicator, MxSDataProviderCommunicatorService>();
            services.AddScoped<IMxSCustomerReportImageProvider, MxSCustomerReportImageProvider>();
            services.AddScoped<IMxSCustomerReportImageRepository, MxSCustomerReportImageRepository>();
            services.AddScoped<IMxSReportingService, MxSReportingService>();
            services.AddScoped<IMxSHealthService, MxSHealthService>();
            //services.AddSingleton<IMxSRealtimeService, MxSRealtimeservice>();

            return services;
        }
    }
}
