using App.Clientes.API.Application.Events;
using App.Clientes.API.Contracts;
using App.Clientes.API.Models;
using App.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace App.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>, IRequestHandler<AdicionarEnderecoCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (message.EhValido() is false) return message.ValidationResult;

            var cliente = new Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            if (clienteExistente is not null)
            {
                AdicionarErro("Este CPF já está em uso");
                return ValidationResult;
            }

            _clienteRepository.Adicionar(cliente);

            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AdicionarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (message.EhValido() is false) return message.ValidationResult;

            var endereco = new Endereco(message.Logradouro, message.Numero, message.Complemento,
                message.Bairro, message.Cep, message.Cidade, message.Estado, message.ClienteId);

            var enderecoCadastrado = await _clienteRepository.ObterEnderecoPorId(message.ClienteId);

            if (enderecoCadastrado is null) _clienteRepository.AdicionarEndereco(endereco);

            else
            {
                endereco.Id = enderecoCadastrado.Id;
                _clienteRepository.AtualizarEndereco(endereco);
            }

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}