using App.Pagamentos.API.Contracts;
using App.Pagamentos.API.Data;
using App.Pagamentos.API.Data.Repository;
using App.Pagamentos.API.Facade;
using App.Pagamentos.API.Services;
using App.WebAPI.Core.Usuario;

namespace App.Pagamentos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoFacade, PagamentoCartaoCreditoFacade>();

            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<PagamentosContext>();
        }
    }
}