namespace SistemaControleGastosResidenciais.DTOs.Requests {
    public class CreatePersonRequest {
        public string Name { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }
    }
}
