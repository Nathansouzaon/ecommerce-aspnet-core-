using App.Bff.Compras.Contracts;
using App.Bff.Compras.Extensions;
using Microsoft.Extensions.Options;

namespace App.Bff.Compras.Services
{
    public class PagamentoService : Service, IPagamentoService
    {
        private readonly HttpClient _httpClient;

        public PagamentoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PagamentoUrl);
        }
    }
}