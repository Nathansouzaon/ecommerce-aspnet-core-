﻿using App.Clientes.API.Data;
using App.WebAPI.Core.Identidade;
using Microsoft.EntityFrameworkCore;

namespace App.Clientes.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClientesContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.MapControllers();

            return app;
        }
    }
}
