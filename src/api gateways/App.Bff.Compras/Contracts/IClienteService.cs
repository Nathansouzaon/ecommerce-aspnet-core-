using App.Bff.Compras.Models;

namespace App.Bff.Compras.Contracts
{
    public interface IClienteService
    {
        Task<EnderecoDTO> ObterEndereco();
    }
}
