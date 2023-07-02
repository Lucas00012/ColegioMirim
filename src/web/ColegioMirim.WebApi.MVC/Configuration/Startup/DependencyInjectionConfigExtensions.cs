using ColegioMirim.WebApi.MVC.Configuration.Settings;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Identity;

namespace ColegioMirim.WebApi.MVC.Configuration.Startup
{
    public static class DependencyInjectionConfigExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IUserSession, UserSession>();

            services.AddScoped<AlunosService>();
            services.AddScoped<TurmasService>();
            services.AddScoped<UsuariosService>();
            services.AddScoped<AlunosTurmaService>();

            var baseUrlsConfigSection = configuration.GetSection("BaseUrlsConfig");
            services.Configure<BaseUrlsConfiguration>(baseUrlsConfigSection);

            var identityConfigSection = configuration.GetSection("IdentityConfig");
            services.Configure<IdentityConfiguration>(identityConfigSection);
        }
    }
}
