using Microsoft.OpenApi.Models;

namespace App.Identidade.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(lbda =>
            {
                lbda.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Identidade",
                    Description = "api que gerenciara o login e registro de usuários",
                    Contact = new OpenApiContact { Name = "Mateus Castellar", Email = "mateuscastellar@outlook.com" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(lbda =>
            {
                lbda.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}