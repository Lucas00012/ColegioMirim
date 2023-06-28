using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ColegioMirim.WebAPI.Core.Identity
{
    public static class AuthConfigExtensions
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var identityConfigSection = configuration.GetSection("IdentityConfig");
            var identityConfig = identityConfigSection.Get<IdentityConfiguration>();
            services.Configure<IdentityConfiguration>(identityConfigSection);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.MapInboundClaims = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = identityConfig.Key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = identityConfig.Issuer,
                    ValidAudience = identityConfig.Audience,
                    RoleClaimType = "role"
                };
            });

            return services;
        }

        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
