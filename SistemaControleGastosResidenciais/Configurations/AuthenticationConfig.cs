using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SistemaControleGastosResidenciais.Authentication.Models;
using System.Text;

namespace SistemaControleGastosResidenciais.Configurations {
    public static class AuthenticationConfig {
        public static IServiceCollection AddAuthenticationConfiguration(
            this IServiceCollection services,
            IConfiguration configuration
        ) {
            JwtSettings jwtSettings =
                configuration
                    .GetSection("Jwt")
                    .Get<JwtSettings>()
                ?? throw new InvalidOperationException(
                    "Configurações JWT não encontradas"
                );

            if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey)) {
                throw new InvalidOperationException("A chave JWT não foi configurada");
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters =
                        new TokenValidationParameters {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,

                            IssuerSigningKey = securityKey,

                            ClockSkew = TimeSpan.Zero
                        };
                });

            services.AddAuthorization();

            return services;
        }
    }
}