using App.Core.Communication;
using App.WebApp.Mvc.Models;

namespace App.WebApp.Mvc.Contracts
{
    public interface IClienteService
    {
        Task<EnderecoViewModel> ObterEndereco();
        Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco);
    }
}