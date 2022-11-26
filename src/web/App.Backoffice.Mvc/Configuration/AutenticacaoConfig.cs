using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Backoffice.Mvc.Configuration
{
    public static class AutenticacaoConfig
    {
        public static void AddAutenticacaoConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LogoutPath = "/login";
                    options.AccessDeniedPath = "/acesso-negado";
                });
        }

        public static void UseAutenticacaoConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
