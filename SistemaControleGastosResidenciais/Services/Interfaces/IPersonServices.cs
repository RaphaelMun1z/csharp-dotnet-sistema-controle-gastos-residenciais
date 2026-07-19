using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Services.Interfaces {
    public interface IPersonServices {
        Person FindById(Guid id);
        List<Person> FindAll();
        Person Create(Person person);
        void Delete(Guid id);
    }
}
