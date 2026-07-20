using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Mappings {
    public static class TransactionMapper {
        // Converte uma entidade Transaction para um DTO de resposta
        public static TransactionResponse ToResponse(Transaction transaction) {
            return new TransactionResponse(
                transaction.Id,
                transaction.PersonId,
                transaction.Amount,
                transaction.Type,
                transaction.Description
            );
        }

        // Converte uma lista de entidades Transaction para uma lista de DTOs de resposta
        public static List<TransactionResponse> ToResponseList(IEnumerable<Transaction> transactions) {
            return transactions
                .Select(ToResponse)
                .ToList();
        }
    }
}