using Microsoft.OpenApi.Models;

namespace App.Bff.Compras.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(lbda =>
            {
                lbda.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Gateway de Compras",
                    Description = "api que os gerencia as requisoes e comunicacoes com apis para realizar compras e pedidos",
                    Contact = new OpenApiContact { Name = "Mateus Castellar", Email = "mateuscastellar@outlook.com" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });

                lbda.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                lbda.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
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
