using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Data.Configurations {
    // Define as regras de configuração da entidade Account no banco de dados
    public class AccountConfiguration : IEntityTypeConfiguration<Account> {
        public void Configure(EntityTypeBuilder<Account> builder) {
            // Define o identificador da conta como chave primária
            builder.HasKey(account => account.Id);

            // Define as regras dos campos de e-mail e senha
            builder.Property(account => account.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(account => account.Password)
                .IsRequired()
                .HasMaxLength(255);

            // Garante que não existam duas contas com o mesmo e-mail
            builder.HasIndex(account => account.Email)
                .IsUnique();

            // Define a relação entre Account e Person
            // A conta pertence a uma pessoa e será excluída caso essa pessoa seja removida
            builder.HasOne(account => account.Person)
                .WithOne(person => person.Account)
                .HasForeignKey<Account>(account => account.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
