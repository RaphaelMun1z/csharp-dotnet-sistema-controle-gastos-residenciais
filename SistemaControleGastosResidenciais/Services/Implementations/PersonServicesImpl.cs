using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Services.Interfaces;
using System;
using System.Xml.Linq;

namespace SistemaControleGastosResidenciais.Services.Impl {
    public class PersonServicesImpl : IPersonServices {
        public List<Person> FindAll() {
            List<Person> people = new List<Person>();
            for (int ii = 0; ii < 10; ii++) {
                people.Add(new Person {
                    Id = Guid.NewGuid(),
                    Name = $"Person {ii}",
                    Age = 20 + ii
                });
            }

            return people;
        }

        public Person FindById(Guid id) {
            // Verifica se o ID não é nulo ou vazio
            if (id == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido!");
            }

            return new Person {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Age = 30
            };
        }

        public Person Create(Person person) {
            // Realiza a validação do nome, não pode ser nulo nem estar vazio
            if (person.Name == null || person.Name.Trim().Length == 0) {
                throw new BadHttpRequestException("Informe um nome válido!");
            }

            // Realiza a validação da idade, não pode ser nula nem menor ou igual a zero
            if (person.Age <= 0) {
                throw new BadHttpRequestException("Informe uma idade válida! Igual ou maior a 1.");
            }

            person.Id = Guid.NewGuid();

            return person;
        }

        public void Delete(Guid id) {
            // Verifica se o ID não é nulo ou vazio
            if (id == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido!");
            }

            // Deleta pessoa com esse ID
        }

    }
}
