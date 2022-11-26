using App.WebApp.Mvc.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnviromentConfiguration();

builder.Services.AddAutenticacaoConfiguration();

builder.Services.AddMvcConfiguration(builder.Configuration);

builder.Services.RegisterServices();

var app = builder.Build();

app.UseMvcConfiguration(builder.Environment);

app.Run();