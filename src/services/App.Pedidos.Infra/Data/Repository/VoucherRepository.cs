using App.Core.Data;
using App.Pedidos.Domain.Contracts;
using App.Pedidos.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;

namespace App.Pedidos.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly PedidosContext _context;

        public VoucherRepository(PedidosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Voucher>> ObterTodos()
        {
            return await _context.Vouchers.AsNoTracking()
                .Where(voucher => voucher.Excluido == false)
                .ToListAsync();
        }

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public void Atualizar(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Adicionar(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
        }

        public async Task<Voucher> ObterPorId(Guid id)
        {
            return await _context.Vouchers
                .FirstOrDefaultAsync(lbda => lbda.Id == id);
        }

        public void Remover(Voucher voucher)
        {
            _context.Vouchers.Remove(voucher);
        }
    }
}