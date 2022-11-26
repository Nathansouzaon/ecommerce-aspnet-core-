using App.Backoffice.Mvc.Models;
using App.Core.Communication;

namespace App.Backoffice.Mvc.Contracts
{
    public interface ICatalogoService
    {
        Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null);
        Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<ResponseResult> Adicionar(ProdutoViewModel produtoViewModel);
        Task<ResponseResult> Atualizar(Guid id, ProdutoViewModel produtoViewModel);
        Task<ResponseResult> Remover(Guid id);
    }
}