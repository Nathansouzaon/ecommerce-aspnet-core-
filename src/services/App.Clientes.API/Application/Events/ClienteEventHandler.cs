using MediatR;

namespace App.Clientes.API.Application.Events
{
    public class ClienteEventHandler : INotificationHandler<ClienteRegistradoEvent>
    {
        public Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            //enviar um evento de confirmação que o cliente esta registrado
            return Task.CompletedTask;
        }
    }
}