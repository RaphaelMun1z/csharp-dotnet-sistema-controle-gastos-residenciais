using System.Security.Principal;
using System.Transactions;

namespace SistemaControleGastosResidenciais.Entities {
    public class Person {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public int Age {
            get {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                int age = today.Year - BirthDate.Year;

                if (BirthDate > today.AddYears(-age)) {
                    age--;
                }

                return age;
            }
        }

        public Account? Account { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
