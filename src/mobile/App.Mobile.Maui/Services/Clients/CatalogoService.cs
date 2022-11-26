using App.Mobile.Maui.Models;

namespace App.Mobile.Maui.Services.Clients
{
    sealed class CatalogoService : Service
    {
        #region Inicializacao lazy load
        private static readonly Lazy<CatalogoService> _autenticacaoService = new(() => new CatalogoService());
        public static CatalogoService Current => _autenticacaoService.Value;
        #endregion

        public async Task<PagedViewModel<Produto>> ObterTodos(int pageSize, int pageIndex, string? query = null)
        {
            var response = await _httpClient
                .GetAsync($"{BaseAddress}:6201/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<Produto>>(response);
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            var response = await _httpClient
                .GetAsync($"{BaseAddress}:6201/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<Produto>(response);
        }
    }
}
