using App.Identidade.API.Services;
using App.WebAPI.Core.Identidade;
using App.WebAPI.Core.Usuario;

namespace App.Identidade.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            {
                options.AddPolicy("Total", buider =>
                    buider
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<AutenticacaoService>();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("Total");

            app.UseHttpsRedirection();

            app.UseAuthConfiguration();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseJwksDiscovery();

            return app;
        }
    }
}
