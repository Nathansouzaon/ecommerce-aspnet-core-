using App.Backoffice.Mvc.Contracts;
using App.Backoffice.Mvc.Extensions;
using App.Backoffice.Mvc.Models;
using App.Core.Communication;
using Microsoft.Extensions.Options;

namespace App.Backoffice.Mvc.Services
{
    public class VoucherService : Service, IVoucherService
    {
        private readonly HttpClient _httpClient;

        public VoucherService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
            _httpClient = httpClient;
        }

        public async Task<ResponseResult> Adicionar(VoucherViewModel voucherViewModel)
        {
            var voucherContent = ObterConteudo(voucherViewModel);

            var response = await _httpClient.PostAsync("/vouchers", voucherContent);

            if (TratarErrosResponse(response) is false) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> Atualizar(Guid id, VoucherViewModel voucherViewModel)
        {
            var voucherContent = ObterConteudo(voucherViewModel);

            var response = await _httpClient.PutAsync($"/vouchers/{id}", voucherContent);

            if (TratarErrosResponse(response) is false) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<VoucherViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/vouchers/obter/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VoucherViewModel>(response);

        }

        public async Task<IEnumerable<VoucherViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/vouchers");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<VoucherViewModel>>(response);
        }

        public async Task<ResponseResult> Remover(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/vouchers/{id}");

            if (TratarErrosResponse(response) is false) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
