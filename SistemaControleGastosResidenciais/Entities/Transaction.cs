namespace SistemaControleGastosResidenciais.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public string Description { get; set; } = string.Empty;

        public Person Person { get; set; } = null!;
    }
}
