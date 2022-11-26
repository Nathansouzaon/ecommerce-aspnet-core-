using App.Catalogo.API.Models;
using App.Core.Data;

namespace App.Catalogo.API.Contracts
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<PagedResult<Produto>> ObterTodos(int pageSize, int pageIndex, string query = null);
        Task<Produto> ObterPorId(Guid id);
        Task<List<Produto>> ObterProdutosPorId(string ids);

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Remover(Produto produto);
    }
}