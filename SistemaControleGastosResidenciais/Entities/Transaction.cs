using SistemaControleGastosResidenciais.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleGastosResidenciais.Entities {
    [Table("tb_transactions", Schema = "dbo")]
    public class Transaction {
        private decimal _amount;
        private TransactionTypeEnum _type;
        private string _description = string.Empty;

        [Key]
        [Column("Id", TypeName = "uniqueidentifier")]
        public Guid Id { get; private set; }

        [Column("PersonId", TypeName = "uniqueidentifier")]
        public Guid PersonId { get; private set; }

        [Column("Amount", TypeName = "decimal(18,2)")]
        public decimal Amount {
            get => _amount;

            // Valida o valor antes de atribuí-lo à propriedade
            private set {
                _amount = ValidateAmount(value);
            }
        }

        [Column("Type", TypeName = "int")]
        public TransactionTypeEnum Type {
            get => _type;

            // Valida o tipo antes de atribuí-lo à propriedade
            private set {
                _type = ValidateType(value);
            }
        }

        [Column("Description", TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A descrição não pode ter mais de 255 caracteres.")]
        public string Description {
            get => _description;

            // Valida a descrição antes de atribuí-la à propriedade
            private set {
                _description = ValidateDescription(value);
            }
        }

        public Person Person { get; private set; } = null!;

        protected Transaction() {
        }

        public Transaction(
            Guid personId,
            decimal amount,
            TransactionTypeEnum type,
            string description
        ) {
            if (personId == Guid.Empty) {
                throw new ArgumentException("Informe um identificador de pessoa válido", nameof(personId));
            }

            // Gera um novo identificador único para a transação
            Id = Guid.NewGuid();
            PersonId = personId;
            Amount = amount;
            Type = type;
            Description = description;
        }

        private static decimal ValidateAmount(decimal amount) {
            // Verifica se o valor da transação é maior que zero
            if (amount <= 0) {
                throw new ArgumentException("O valor da transação deve ser maior que zero", nameof(amount));
            }

            return amount;
        }

        private static TransactionTypeEnum ValidateType(TransactionTypeEnum type) {
            // Verifica se o tipo informado corresponde a um valor válido do enum
            if (!Enum.IsDefined(typeof(TransactionTypeEnum), type)) {
                throw new ArgumentException("Informe um tipo de transação válido", nameof(type));
            }

            return type;
        }

        private static string ValidateDescription(string description) {
            // Verifica se a descrição é nula, vazia ou contém apenas espaços em branco
            if (string.IsNullOrWhiteSpace(description)) {
                throw new ArgumentException("Informe uma descrição válida", nameof(description));
            }

            // Remove espaços em branco no início e no final da descrição
            string normalizedDescription = description.Trim();

            // Verifica se a descrição excede 255 caracteres
            if (normalizedDescription.Length > 255) {
                throw new ArgumentException("A descrição não pode ter mais de 255 caracteres", nameof(description));
            }

            return normalizedDescription;
        }
    }
}
