using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sperry.MxS.Reporting.Server.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Server
{
    public class Startup
    {
        const string OktaOldSchemeName = "OktaOldJwtToken";
        const string OktaPKCESchemeName = "OktaPKCEJwtToken";
        public Startup(IConfiguration configuration)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("FridayTesting - Reporting 1");
            if (Convert.ToBoolean(Convert.ToBoolean(Configuration["MxSWebAppServer:MxSServerInfo:IsCloudDeployment"])))
            {
                services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions()
                {
                    ConnectionString = "InstrumentationKey=764b7996-951b-49e9-bef1-1d7d99975478;IngestionEndpoint=https://southcentralus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://southcentralus.livediagnostics.monitor.azure.com/",
                    EnablePerformanceCounterCollectionModule = false
                });
            }
            Console.WriteLine("FridayTesting - Reporting 2");

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddAuthentication()
                .AddJwtBearer(OktaOldSchemeName, options =>
                {
                    options.Audience = Configuration.GetSection("AngularClient:OldScheme:Audience").Value;
                    options.Authority = Configuration.GetSection("AngularClient:OldScheme:Authority").Value;
                })
                .AddJwtBearer(OktaPKCESchemeName, options =>
                {
                    options.Audience = Configuration.GetSection("AngularClient:PKCEScheme:Audience").Value;
                    options.Authority = Configuration.GetSection("AngularClient:PKCEScheme:Authority").Value;
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRoutingConfiguration(Configuration);
            services.AddReportingServices();
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(OktaOldSchemeName)
                .AddAuthenticationSchemes(OktaPKCESchemeName)
                .Build();
            });
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
