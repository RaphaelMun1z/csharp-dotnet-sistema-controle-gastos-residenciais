using SistemaControleGastosResidenciais.Data;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Repositories.Interfaces;

namespace SistemaControleGastosResidenciais.Repositories.Implementations {
    public class TransactionRepository : ITransactionRepository {
        private readonly AppDbContext _context;

        // Recebe o contexto do banco de dados por injeção de dependência
        public TransactionRepository(AppDbContext context) {
            _context = context;
        }

        // Busca uma transação pelo ID
        public Transaction? FindById(Guid id) {
            return _context.Transactions
                .FirstOrDefault(transaction => transaction.Id == id);
        }

        // Busca todas as transações utilizando paginação
        public List<Transaction> FindAll(int page, int pageSize) {
            return _context.Transactions
                .OrderBy(transaction => transaction.Description)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // Busca as transações associadas a uma pessoa utilizando paginação
        public List<Transaction> FindByPersonId(
            Guid personId,
            int page,
            int pageSize
        ) {
            return _context.Transactions
                .Where(transaction => transaction.PersonId == personId)
                .OrderBy(transaction => transaction.Description)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // Retorna a quantidade total de transações cadastradas
        public int Count() {
            return _context.Transactions.Count();
        }

        // Retorna a quantidade total de transações associadas a uma pessoa
        public int CountByPersonId(Guid personId) {
            return _context.Transactions
                .Count(transaction => transaction.PersonId == personId);
        }

        // Adiciona a transação ao contexto e persiste os dados no banco
        public Transaction Create(Transaction transaction) {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction;
        }
    }
}