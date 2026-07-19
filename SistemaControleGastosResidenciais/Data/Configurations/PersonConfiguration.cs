using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Data.Configurations {
    // Define as regras de configuração da entidade Person no banco de dados
    public class PersonConfiguration : IEntityTypeConfiguration<Person> {
        public void Configure(EntityTypeBuilder<Person> builder) {
            // Define o identificador da pessoa como chave primária
            builder.HasKey(person => person.Id);

            // Define as regras dos campos de nome e data de nascimento
            builder.Property(person => person.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(person => person.BirthDate)
                .IsRequired();

            // Ignora a idade no mapeamento, pois ela é calculada a partir da data de nascimento
            builder.Ignore(person => person.Age);
        }
    }
}
