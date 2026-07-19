using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleGastosResidenciais.Entities {
    [Table("tb_people", Schema = "dbo")]
    public class Person {
        // Campos privados para armazenar os valores das propriedades
        private DateOnly _birthDate;
        private string _name = string.Empty;

        [Key]
        [Column("Id", TypeName = "uniqueidentifier")]
        public Guid Id { get; private set; }
        // Obs.: Utilizei 'private' em alguns atributos para garantir que eles só possam ser modificados dentro da própria classe, mantendo a integridade dos dados.

        [Column("Name", TypeName = "nvarchar(150)")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O nome não pode ter mais de 150 caracteres.")]
        public string Name {
            get => _name;

            // Valida o nome antes de atribuí-lo à propriedade
            private set {
                _name = ValidateName(value);
            }
        }

        [Column("BirthDate", TypeName = "date")]
        public DateOnly BirthDate {
            get => _birthDate;

            // Valida a data de nascimento antes de atribuí-la à propriedade
            private set {
                ValidateBirthDate(value);
                _birthDate = value;
            }
        }

        [NotMapped]
        public int Age {
            // Calcula a idade da pessoa com base na data de nascimento e na data atual
            get {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                int age = today.Year - BirthDate.Year;
                if (BirthDate > today.AddYears(-age)) {
                    age--;
                }

                return age;
            }
        }

        public Account? Account { get; private set; }

        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        protected Person() {
        }

        public Person(
            string name,
            DateOnly birthDate
        ) {
            // Gera um novo identificador único para a pessoa
            Id = Guid.NewGuid();
            Name = name;
            BirthDate = birthDate;
        }

        private static string ValidateName(string name) {
            // Se o nome for nulo, vazio ou contiver apenas espaços em branco, lança uma exceção
            if (string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException("Informe um nome válido", nameof(name));
            }

            // Remove espaços em branco no início e no final do nome
            string normalizedName = name.Trim();

            // Verifica se o nome excede 150 caracteres
            if (normalizedName.Length > 150) {
                throw new ArgumentException("O nome não pode ter mais de 150 caracteres", nameof(name));
            }

            return normalizedName;
        }

        private static void ValidateBirthDate(DateOnly birthDate) {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            // Define a data mínima de nascimento como 150 anos atrás a partir de hoje
            DateOnly minimumBirthDate = today.AddYears(-150);

            // Verifica se a data de nascimento é uma data futura
            if (birthDate > today) {
                throw new ArgumentException("A data de nascimento não pode ser uma data futura", nameof(birthDate));
            }

            // Verifica se a data de nascimento representa uma idade superior a 150 anos
            if (birthDate < minimumBirthDate) {
                throw new ArgumentException("A data de nascimento não pode representar uma idade superior a 150 anos", nameof(birthDate));
            }
        }
    }
}