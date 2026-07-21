using SistemaControleGastosResidenciais.Configurations;
using SistemaControleGastosResidenciais.Configurations.Scalar;
using SistemaControleGastosResidenciais.Configurations.Swagger;
using SistemaControleGastosResidenciais.Exceptions;
using SistemaControleGastosResidenciais.Hateoas.Assemblers;
using SistemaControleGastosResidenciais.Repositories.Implementations;
using SistemaControleGastosResidenciais.Repositories.Interfaces;
using SistemaControleGastosResidenciais.Services.Impl;
using SistemaControleGastosResidenciais.Services.Implementations;
using SistemaControleGastosResidenciais.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a configuração do Serilog para logging
builder.AddSerilogLogging();

builder.Services.AddControllers();

// Adiciona a configuração do OpenAPI (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

// Adiciona a configuração do CORS
builder.Services.AddCorsConfiguration(builder.Configuration);

// Adiciona a configuração das rotas
builder.Services.AddRouteConfig();

// Adiciona a configuração do AutoMapper
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PersonHateoasAssembler>();
builder.Services.AddScoped<AccountHateoasAssembler>();
builder.Services.AddScoped<TransactionHateoasAssembler>();

// Adiciona a configuração do banco de dados
builder.Services.AddDatabaseConfiguration(builder.Configuration);
// Adiciona a configuração do Evolve para migrações de banco de dados
builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);

// Adiciona os serviços
builder.Services.AddScoped<IPersonService, PersonServiceImpl>();
builder.Services.AddScoped<IAccountService, AccountServiceImpl>();
builder.Services.AddScoped<ITransactionService, TransactionServiceImpl>();

// Adiciona o repositório genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
// Adiciona os repositórios específicos
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Adiciona o tratamento global de exceções
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
// Adiciona o middleware global de tratamento de exceções
app.UseExceptionHandler();

app.UseHttpsRedirection();
app.UseCorsConfiguration();
app.UseAuthorization();
app.MapControllers();
app.UseSwaggerSpecification();
app.UseScalarSpecification();
app.Run();