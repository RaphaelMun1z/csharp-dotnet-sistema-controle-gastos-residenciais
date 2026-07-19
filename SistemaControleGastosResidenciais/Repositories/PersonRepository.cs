using SistemaControleGastosResidenciais.Data;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Repositories.Interfaces;

namespace SistemaControleGastosResidenciais.Repositories {
    public class PersonRepository : IPersonRepository {
        private readonly AppDbContext _context;

        // Recebe o contexto do banco de dados por injeção de dependência
        public PersonRepository(AppDbContext context) {
            _context = context;
        }

        // Busca uma pessoa pelo id
        public Person? FindById(Guid id) {
            return _context.People.FirstOrDefault(person => person.Id == id);
        }

        // Busca todas as pessoas com paginação
        public List<Person> FindAll(int page, int pageSize) {
            return _context.People
                .OrderBy(person => person.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int Count() {
            return _context.People.Count();
        }

        // Adiciona a pessoa ao contexto e persiste os dados no banco
        public Person Create(Person person) {
            _context.People.Add(person);
            _context.SaveChanges();

            return person;
        }

        // Remove a pessoa do contexto e persiste a alteração no banco
        public void Delete(Person person) {
            _context.People.Remove(person);
            _context.SaveChanges();
        }
    }
}
