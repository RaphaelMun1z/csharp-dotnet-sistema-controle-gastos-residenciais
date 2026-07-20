using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Mappings {
    public static class PersonMapper {
        // Converte uma entidade Person para um DTO de resposta
        public static PersonResponse ToResponse(Person person) {
            return new PersonResponse(
                person.Id,
                person.Name,
                person.BirthDate,
                person.Age
            );
        }

        // Converte uma lista de entidades Person para uma lista de DTOs de resposta
        public static List<PersonResponse> ToResponseList(IEnumerable<Person> people) {
            return people
                .Select(ToResponse)
                .ToList();
        }
    }
}
