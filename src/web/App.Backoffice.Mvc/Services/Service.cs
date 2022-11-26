using App.Backoffice.Mvc.Extensions;
using App.Core.Communication;
using System.Text;
using System.Text.Json;

namespace App.Backoffice.Mvc.Services
{
    public abstract class Service
    {
        protected StringContent ObterConteudo(object conteudo)
        {
            return new StringContent(JsonSerializer.Serialize(conteudo), Encoding.UTF8, "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
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
