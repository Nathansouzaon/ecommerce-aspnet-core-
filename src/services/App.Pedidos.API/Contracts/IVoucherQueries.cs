using App.Pedidos.API.Application.DTO;

namespace App.Pedidos.API.Contracts
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
        Task<VoucherDTO> ObterVoucherPorId(Guid id);
        Task<IEnumerable<VoucherDTO>> ObterTodos();
    }
}
