using App.Backoffice.Mvc.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace App.Backoffice.Mvc.Configuration
{
    public static class MvcConfiguration
    {
        public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.Configure<AppSettings>(configuration);
        }

        public static void UseMvcConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/erro/500");

            app.UseStatusCodePagesWithRedirects("/erro/{0}");

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAutenticacaoConfiguration();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
