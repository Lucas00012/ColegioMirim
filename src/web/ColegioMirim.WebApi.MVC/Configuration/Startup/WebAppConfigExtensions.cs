using ColegioMirim.WebApi.MVC.Common.Middlewares;

namespace ColegioMirim.WebApi.MVC.Configuration.Startup
{
    public static class WebAppConfigExtensions
    {
        public static void AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
