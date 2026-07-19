using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Repositories.Interfaces;
using SistemaControleGastosResidenciais.Services.Interfaces;

namespace SistemaControleGastosResidenciais.Services.Impl {
    public class PersonServiceImpl : IPersonService {
        private readonly IPersonRepository _personRepository;

        public PersonServiceImpl(IPersonRepository personRepository) {
            _personRepository = personRepository;
        }

        public List<PersonResponse> FindAll() {
            // Busca todas as pessoas registradas
            List<Person> peopleList = _personRepository.FindAll();

            // Converte as entidades encontradas para DTO
            return peopleList.Select(person => new PersonResponse {
                Id = person.Id,
                Name = person.Name,
                BirthDate = person.BirthDate,
                Age = person.Age
            }).ToList();
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
            return new PersonResponse {
                Id = foundPerson.Id,
                Name = foundPerson.Name,
                BirthDate = foundPerson.BirthDate,
                Age = foundPerson.Age
            };
        }

        public PersonResponse Create(CreatePersonRequest personDTO) {
            // Realiza a validação do nome, não pode ser nulo nem estar vazio
            if (personDTO.Name == null || personDTO.Name.Trim().Length == 0) {
                throw new BadHttpRequestException("Informe um nome válido!");
            }

            // Referência para a data de hoje
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            // Define que a idade máxima permitida é de 150 anos
            DateOnly minimumBirthDate = today.AddYears(-150);

            // Realiza a validação da data de nascimento
            // Não pode ter 'nascido no futuro'
            // Não pode possuir idade superior a 150 anos
            if (personDTO.BirthDate > today || personDTO.BirthDate < minimumBirthDate) {
                throw new BadHttpRequestException("Informe uma data de nascimento válida!");
            }

            Person newPerson = new Person {
                Id = Guid.NewGuid(),
                Name = personDTO.Name,
                BirthDate = personDTO.BirthDate
            };

            // Persiste a pessoa no banco de dados
            Person savedPerson = _personRepository.Create(newPerson);

            PersonResponse savedPersonResponse = new PersonResponse {
                Id = savedPerson.Id,
                Name = savedPerson.Name,
                BirthDate = savedPerson.BirthDate,
                Age = savedPerson.Age
            };

            return savedPersonResponse;
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

            // Deleta a pessoa, caso encontrada
            _personRepository.Delete(person);
        }

    }
}
