using SistemaControleGastosResidenciais.Entities;

namespace SistemaControleGastosResidenciais.Repositories.Interfaces {
    public interface ITransactionRepository : IRepository<Transaction> {
        List<Transaction> FindByPersonId(Guid personId, int page, int pageSize);
        int CountByPersonId(Guid personId);
    }
}