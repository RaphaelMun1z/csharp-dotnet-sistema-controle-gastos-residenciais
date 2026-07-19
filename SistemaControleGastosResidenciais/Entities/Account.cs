namespace SistemaControleGastosResidenciais.Entities
{
    public class Account
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Person Person { get; set; } = null!;
    }
}
