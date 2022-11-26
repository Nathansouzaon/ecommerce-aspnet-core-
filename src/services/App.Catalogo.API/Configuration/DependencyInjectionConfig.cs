using App.Catalogo.API.Contracts;
using App.Catalogo.API.Data;
using App.Catalogo.API.Data.Repository;

namespace App.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<CatalogoContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}