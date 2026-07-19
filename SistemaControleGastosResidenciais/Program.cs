using SistemaControleGastosResidenciais.Configurations;
using SistemaControleGastosResidenciais.Repositories;
using SistemaControleGastosResidenciais.Repositories.Interfaces;
using SistemaControleGastosResidenciais.Services.Impl;
using SistemaControleGastosResidenciais.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Adiciona a configuração do banco de dados
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Adiciona os serviços
builder.Services.AddScoped<IPersonService, PersonServiceImpl>();

// Adiciona os repositórios
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
