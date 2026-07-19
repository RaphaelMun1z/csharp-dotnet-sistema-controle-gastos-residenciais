using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Data.Configurations
{
    // Define as regras de configuração da entidade Transaction no banco de dados
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Define o identificador da transação como chave primária
            builder.HasKey(transaction => transaction.Id);

            // Define as regras dos campos de descrição, valor e tipo
            builder.Property(transaction => transaction.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(transaction => transaction.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(transaction => transaction.Type)
                .IsRequired();

            // Define a relação entre Transaction e Person
            // Cada transação pertence a uma pessoa e será excluída caso essa pessoa seja removida
            builder.HasOne(transaction => transaction.Person)
                .WithMany(person => person.Transactions)
                .HasForeignKey(transaction => transaction.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
