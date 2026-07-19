using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Repositories.Interfaces {
    public interface ITransactionRepository {
        Transaction? FindById(Guid id);
        List<Transaction> FindAll(int page, int pageSize);
        List<Transaction> FindByPersonId(Guid personId, int page, int pageSize);
        int Count();
        int CountByPersonId(Guid personId);
        Transaction Create(Transaction transaction);
    }
}
