namespace SistemaControleGastosResidenciais.DTOs.Responses {
    public class PersonResponse {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public int Age { get; set; }
    }
}
