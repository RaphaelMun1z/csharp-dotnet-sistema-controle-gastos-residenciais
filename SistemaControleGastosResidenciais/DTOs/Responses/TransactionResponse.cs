using SistemaControleGastosResidenciais.Enums;

namespace SistemaControleGastosResidenciais.DTOs.Responses {
    public record TransactionResponse(
        Guid Id,
        Guid PersonId,
        decimal Amount,
        TransactionTypeEnum Type,
        string Description
    );
}
