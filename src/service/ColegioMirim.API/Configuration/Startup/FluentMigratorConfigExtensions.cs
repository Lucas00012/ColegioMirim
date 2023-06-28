using ColegioMirim.Infrastructure.Data;
using FluentMigrator.Runner;

namespace ColegioMirim.API.Configuration.Startup
{
    public static class FluentMigratorConfigExtensions
    {
        public static void AddFluentMigratorConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(cfg => cfg
                    .AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("Default"))
                    .ScanIn(typeof(ColegioMirimContext).Assembly).For.Migrations()
                );
        }

        public static void UseFluentMigratorConfiguration(IMigrationRunner migrationRunner)
        {
            migrationRunner.MigrateUp();
        }
    }
}
