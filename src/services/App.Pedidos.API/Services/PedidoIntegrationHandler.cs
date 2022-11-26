using App.Core.DomainObjects;
using App.Core.Messages.Integration;
using App.MessageBus;
using App.Pedidos.Domain.Contracts;

namespace App.Pedidos.API.Services
{
    public class PedidoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PedidoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<PedidoCanceladoIntegrationEvent>("PedidoCancelado",
                async request => await CancelarPedido(request));

            _bus.SubscribeAsync<PedidoPagoIntegrationEvent>("PedidoPago",
               async request => await FinalizarPedido(request));
        }

        private async Task CancelarPedido(PedidoCanceladoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var pedidoRepository = scope.ServiceProvider.GetRequiredService<IPedidoRepository>();

            var pedido = await pedidoRepository.ObterPorId(message.PedidoId);
            pedido.CancelarPedido();

            pedidoRepository.Atualizar(pedido);

            if (await pedidoRepository.UnitOfWork.Commit() is false) throw new DomainException($"Problemas ao cancelar o pedido {message.PedidoId}");
        }

        private async Task FinalizarPedido(PedidoPagoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var pedidoRepository = scope.ServiceProvider.GetRequiredService<IPedidoRepository>();

            var pedido = await pedidoRepository.ObterPorId(message.PedidoId);
            pedido.FinalizarPedido();

            pedidoRepository.Atualizar(pedido);

            if (await pedidoRepository.UnitOfWork.Commit() is false) throw new DomainException($"Problemas ao finalizar o pedido {message.PedidoId}");
        }
    }
}