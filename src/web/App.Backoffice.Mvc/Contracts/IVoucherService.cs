using App.Backoffice.Mvc.Models;
using App.Core.Communication;

namespace App.Backoffice.Mvc.Contracts
{
    public interface IVoucherService
    {
        Task<IEnumerable<VoucherViewModel>> ObterTodos();
        Task<ResponseResult> Adicionar(VoucherViewModel voucherViewModel);
        Task<ResponseResult> Remover(Guid id);
        Task<VoucherViewModel> ObterPorId(Guid id);
        Task<ResponseResult> Atualizar(Guid id, VoucherViewModel voucherViewModel);
    }
}
