using ColegioMirim.API.Configuration.Startup;
using ColegioMirim.WebAPI.Core.Identity;
using FluentMigrator.Runner;

namespace ColegioMirim.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddAuthConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.AddFluentMigratorConfiguration(Configuration);

            services.RegisterServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            FluentMigratorConfigExtensions.UseFluentMigratorConfiguration(migrationRunner);

            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(env);
        }
    }
}
