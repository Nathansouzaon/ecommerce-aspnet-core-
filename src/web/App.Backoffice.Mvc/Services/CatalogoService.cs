using App.Backoffice.Mvc.Contracts;
using App.Backoffice.Mvc.Extensions;
using App.Backoffice.Mvc.Models;
using App.Core.Communication;
using Microsoft.Extensions.Options;

namespace App.Backoffice.Mvc.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
            _httpClient = httpClient;
        }

        public async Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }

        public async Task<ResponseResult> Adicionar(ProdutoViewModel produtoViewModel)
        {
            var produtoContent = ObterConteudo(produtoViewModel);

            var response = await _httpClient.PostAsync("/catalogo/produtos/adicionar", produtoContent);

            if (TratarErrosResponse(response) is false) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            var produtoContent = ObterConteudo(produtoViewModel);

            var response = await _httpClient.PutAsync($"/catalogo/produtos/atualizar/{id}", produtoContent);

            if (TratarErrosResponse(response) is false) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> Remover(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/catalogo/produtos/remover/{id}");

            if (TratarErrosResponse(response) is false) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}