using ColegioMirim.API.Configuration.Startup;
using ColegioMirim.WebAPI.Core.Identity;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ColegioMirim.API.Tests.Setup
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; set; }

        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddAuthConfiguration(Configuration);

            services.AddFluentMigratorConfiguration(Configuration);

            services.RegisterServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            FluentMigratorConfigExtensions.UseFluentMigratorConfiguration(migrationRunner);

            app.UseApiConfiguration(env);
        }
    }
}
