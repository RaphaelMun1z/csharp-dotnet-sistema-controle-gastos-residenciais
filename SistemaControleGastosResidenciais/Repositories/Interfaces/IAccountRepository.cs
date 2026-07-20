using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Repositories.Interfaces {
    public interface IAccountRepository {
        Account? FindById(Guid id);
        Account? FindByEmail(string email);
        Account? FindByPersonId(Guid personId);
        Account Create(Account account);
        void Delete(Account account);
    }
}
