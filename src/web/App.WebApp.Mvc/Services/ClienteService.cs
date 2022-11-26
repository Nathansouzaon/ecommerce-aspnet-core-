using App.Core.Communication;
using App.WebApp.Mvc.Contracts;
using App.WebApp.Mvc.Extensions;
using App.WebApp.Mvc.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace App.WebApp.Mvc.Services
{
    public class ClienteService : Service, IClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ClienteUrl);
        }

        public async Task<EnderecoViewModel> ObterEndereco()
        {
            var response = await _httpClient.GetAsync("/cliente/endereco/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<EnderecoViewModel>(response);
        }

        public async Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco)
        {
            var enderecoContent = ObterConteudo(endereco);

            var response = await _httpClient.PostAsync("/cliente/endereco/", enderecoContent);

            if (TratarErrosResponse(response) is false) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}