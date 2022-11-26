using App.Core.Communication;
using App.Mobile.Maui.Extensions;
using App.Mobile.Maui.Services.Cache;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace App.Mobile.Maui.Services.Clients
{
    internal abstract class Service
    {
        protected readonly HttpClient _httpClient;

        //ip localhost
        protected readonly string BaseAddress = "http://192.168.1.7";

        public Service()
        {
            _httpClient = new HttpClient();
        }

        protected async Task AdicionarAutenticacaoHeader()
        {
            var token = await UsuarioCacheService.Current.GetUserToken();

            if (string.IsNullOrEmpty(token)) throw new Exception("Erro ao identificar usuario!");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected StringContent ObterConteudo(object conteudo)
        {
            return new StringContent(
                JsonSerializer.Serialize(conteudo),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options) ??
                throw new Exception("Erro ao obter objeto do response");
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }


        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }
    }
}
