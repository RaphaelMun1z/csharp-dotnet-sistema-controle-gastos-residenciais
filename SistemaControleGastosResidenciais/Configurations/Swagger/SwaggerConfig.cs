using Microsoft.OpenApi;

namespace SistemaControleGastosResidenciais.Configurations.Swagger {
    public static class SwaggerConfig {
        private const string AppName = "Sistema de Controle de Gastos Residenciais";

        // Configura a geração da especificação Swagger/OpenAPI
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services) {
            services.AddSwaggerGen(options => {
                // Adiciona as informações gerais da API
                options.SwaggerDoc("v1", OpenAPIConfig.CreateOpenApiInfo());

                // Configura nomes únicos para os schemas
                options.CustomSchemaIds(type => type.FullName);

                // Adiciona as descrições detalhadas das operações
                options.OperationFilter<AccountOperationFilter>();
                options.OperationFilter<PersonOperationFilter>();
                options.OperationFilter<TransactionOperationFilter>();
            });

            return services;
        }

        // Configura o Swagger UI
        public static IApplicationBuilder UseSwaggerSpecification(this IApplicationBuilder app) {
            app.UseSwagger();

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

                // Define a rota do Swagger UI
                options.RoutePrefix = "swagger-ui";

                // Define o título exibido no navegador
                options.DocumentTitle = AppName;
            });

            return app;
        }
    }
}