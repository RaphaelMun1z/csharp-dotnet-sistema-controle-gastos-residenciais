namespace SistemaControleGastosResidenciais.DTOs.Responses {
    public record AccountResponse(
        Guid Id,
        Guid PersonId,
        string Email
    );
}
