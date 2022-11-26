using App.Pedidos.API.Configuration;
using App.WebAPI.Core.Identidade;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnviromentConfiguration();

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddMessageBusConfiguration(builder.Configuration);

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.RegisterServices();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(builder.Environment);

app.Run();
