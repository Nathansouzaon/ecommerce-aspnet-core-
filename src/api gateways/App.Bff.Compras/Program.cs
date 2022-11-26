using App.Bff.Compras.Configuration;
using App.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnviromentConfiguration();

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.RegisterServices();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration();

app.Run();
