using App.Clientes.API.Models;
using App.Core.Data;

namespace App.Clientes.API.Contracts
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorCpf(string cpf);

        // endereco
        void AdicionarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);
        Task<Endereco> ObterEnderecoPorId(Guid id);
    }
}