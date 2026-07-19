using System.Security.Principal;
using System.Transactions;

namespace SistemaControleGastosResidenciais.Entities
{
    public class Person
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public Account? Account { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
