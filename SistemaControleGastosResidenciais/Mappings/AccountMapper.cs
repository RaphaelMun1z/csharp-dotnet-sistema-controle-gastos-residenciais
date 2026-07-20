using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Mappings {
    public static class AccountMapper {
        // Converte uma entidade Account para um DTO de resposta
        public static AccountResponse ToResponse(Account account) {
            return new AccountResponse(
                account.Id,
                account.PersonId,
                account.Email
            );
        }

        // Converte uma lista de entidades Account para uma lista de DTOs de resposta
        public static List<AccountResponse> ToResponseList(IEnumerable<Account> accounts) {
            return accounts
                .Select(ToResponse)
                .ToList();
        }
    }
}