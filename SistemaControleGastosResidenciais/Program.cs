using SistemaControleGastosResidenciais.Configurations;
using SistemaControleGastosResidenciais.Repositories.Implementations;
using SistemaControleGastosResidenciais.Repositories.Interfaces;
using SistemaControleGastosResidenciais.Services.Impl;
using SistemaControleGastosResidenciais.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Adiciona a configuração do banco de dados
builder.Services.AddDatabaseConfiguration(builder.Configuration);
// Adiciona os serviços
builder.Services.AddScoped<IPersonService, PersonServiceImpl>();
// Adiciona o repositório genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
