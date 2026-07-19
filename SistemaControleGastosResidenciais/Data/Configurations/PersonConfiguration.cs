using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Data.Configurations
{
    // Define as regras de configuração da entidade Person no banco de dados
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Define o identificador da pessoa como chave primária
            builder.HasKey(person => person.Id);

            // Define as regras dos campos de nome e idade
            builder.Property(person => person.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(person => person.Age)
                .IsRequired();

            // Define a relação entre Person e Transaction
            // Uma pessoa pode possuir várias transações e elas serão excluídas junto com a pessoa
            builder.HasMany(person => person.Transactions)
                .WithOne(transaction => transaction.Person)
                .HasForeignKey(transaction => transaction.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define a relação entre Person e Account
            // Uma pessoa pode possuir uma conta e ela será excluída caso a pessoa seja removida
            builder.HasOne(person => person.Account)
                .WithOne(account => account.Person)
                .HasForeignKey<Account>(account => account.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
