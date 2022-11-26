using App.Core.Mediator;
using App.Pedidos.API.Application.Commands;
using App.Pedidos.API.Application.Events;
using App.Pedidos.API.Application.Queries;
using App.Pedidos.API.Contracts;
using App.Pedidos.Domain.Contracts;
using App.Pedidos.Infra.Data;
using App.Pedidos.Infra.Data.Repository;
using App.WebAPI.Core.Usuario;
using FluentValidation.Results;
using MediatR;

namespace App.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // api
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            // commands
            services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, PedidoCommandHandler>();

            // events
            services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

            // application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            // acesso a dados
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<PedidosContext>();
        }
    }
}