using App.Catalogo.API.Configuration;
using App.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnviromentConfiguration();

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddMessageBusConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();