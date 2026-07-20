using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Mappings;
using SistemaControleGastosResidenciais.Repositories.Interfaces;
using SistemaControleGastosResidenciais.Services.Interfaces;

namespace SistemaControleGastosResidenciais.Services.Impl {
    public class PersonServiceImpl : IPersonService {
        private readonly IRepository<Person> _personRepository;

        // Recebe o repositório por injeção de dependência
        public PersonServiceImpl(IRepository<Person> personRepository) {
            _personRepository = personRepository;
        }

        public PagedResponse<PersonResponse> FindAll(int page, int pageSize) {
            // Valida os parâmetros de paginação
            if (page < 1) {
                throw new BadHttpRequestException("A página deve ser maior ou igual a 1");
            }

            if (pageSize < 1 || pageSize > 100) {
                throw new BadHttpRequestException("A quantidade de registros por página deve estar entre 1 e 100");
            }

            // Busca somente os registros pertencentes à página solicitada
            List<Person> peopleList = _personRepository.FindAll(page, pageSize);

            // Busca a quantidade total de pessoas cadastradas
            int totalElements = _personRepository.Count();

            // Calcula a quantidade total de páginas
            int totalPages = (int)Math.Ceiling(totalElements / (double)pageSize);

            // Converte as entidades para DTO
            List<PersonResponse> peopleResponse =
                peopleList.Select(person => new PersonResponse(
                person.Id,
                person.Name,
                person.BirthDate,
                person.Age
            )).ToList();

            return new PagedResponse<PersonResponse> {
                Content = peopleResponse,
                Page = page,
                PageSize = pageSize,
                TotalElements = totalElements,
                TotalPages = totalPages
            };
        }

        public PersonResponse FindById(Guid id) {
            // Verifica se o ID não é nulo ou vazio
            if (id == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido!");
            }

            // Busca a pessoa pelo id
            Person? foundPerson = _personRepository.FindById(id);

            // Verifica se a pessoa existe
            if (foundPerson == null) {
                throw new KeyNotFoundException("Pessoa não encontrada!");
            }

            // Retorna os dados da pessoa encontrada
            return PersonMapper.ToResponse(foundPerson);
        }

        public PersonResponse Create(CreatePersonRequest personDTO) {
            // Cria uma nova instância de pessoa
            // As validações de nome e data de nascimento são realizadas pela própria entidade
            // O ID é gerado automaticamente dentro do construtor da entidade
            Person newPerson = new Person(
                personDTO.Name,
                personDTO.BirthDate
            );

            // Persiste a pessoa no banco de dados
            Person savedPerson = _personRepository.Create(newPerson);

            // Converte a entidade persistida para DTO de resposta
            return PersonMapper.ToResponse(savedPerson);
        }

        public void Delete(Guid id) {
            // Verifica se o ID não é nulo ou vazio
            if (id == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido!");
            }

            // Busca a pessoa pelo id
            Person? person = _personRepository.FindById(id);

            // Verifica se a pessoa existe
            if (person == null) {
                throw new KeyNotFoundException("Pessoa não encontrada!");
            }

            // Deleta a pessoa pelo ID
            _personRepository.Delete(id);
        }
    }
}