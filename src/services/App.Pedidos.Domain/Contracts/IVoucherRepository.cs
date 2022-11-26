using App.Core.Data;
using App.Pedidos.Domain.Vouchers;

namespace App.Pedidos.Domain.Contracts
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<IEnumerable<Voucher>> ObterTodos();
        Task<Voucher> ObterPorId(Guid id);
        Task<Voucher> ObterVoucherPorCodigo(string codigo);
        void Atualizar(Voucher voucher);
        void Adicionar(Voucher voucher);
        void Remover(Voucher voucher);
    }
}
