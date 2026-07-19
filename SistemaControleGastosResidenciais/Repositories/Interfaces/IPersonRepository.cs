using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Repositories.Interfaces {
    public interface IPersonRepository {
        Person? FindById(Guid id);

        List<Person> FindAll(int page, int pageSize);

        int Count();

        Person Create(Person person);

        void Delete(Person person);
    }
}
