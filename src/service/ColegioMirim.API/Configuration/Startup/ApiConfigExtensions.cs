using ColegioMirim.API.Common.Filters;
using ColegioMirim.WebAPI.Core.Identity;
using System.Text.Json.Serialization;

namespace ColegioMirim.API.Configuration.Startup
{
    public static class ApiConfigExtensions
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddMvcOptions(opt =>
                {
                    opt.Filters.Add(typeof(AlunoValidoFilter));
                    opt.Filters.Add(typeof(UnitOfWorkFilter));
                    opt.Filters.Add(typeof(ExceptionFilter));
                })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
