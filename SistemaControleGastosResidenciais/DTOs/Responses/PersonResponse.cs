namespace SistemaControleGastosResidenciais.DTOs.Responses {
    public record PersonResponse(
        Guid Id,
        string Name,
        DateOnly BirthDate,
        int Age
    );
}
