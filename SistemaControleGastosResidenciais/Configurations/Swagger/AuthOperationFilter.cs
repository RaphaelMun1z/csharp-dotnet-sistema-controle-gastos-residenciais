using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SistemaControleGastosResidenciais.Configurations.Swagger {
    public class AuthOperationFilter : IOperationFilter {
        public void Apply(OpenApiOperation operation, OperationFilterContext context) {
            string? actionName = context.MethodInfo.Name;

            switch (actionName) {
                case "Register":
                    operation.Summary = "Registra um novo usuário";
                    operation.Description =
                        "Cria uma nova pessoa e uma conta associada utilizando nome, data de nascimento, e-mail e senha";
                    break;

                case "Login":
                    operation.Summary = "Autentica um usuário";
                    operation.Description =
                        "Autentica uma conta utilizando e-mail e senha e retorna um token JWT para acesso aos endpoints protegidos";
                    break;
            }
        }
    }
}