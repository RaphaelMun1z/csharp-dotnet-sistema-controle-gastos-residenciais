using SistemaControleGastosResidenciais.Data;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Repositories.Interfaces;

namespace SistemaControleGastosResidenciais.Repositories.Implementations {
    public class AccountRepository : IAccountRepository {
        private readonly AppDbContext _context;

        // Recebe o contexto do banco de dados por injeção de dependência
        public AccountRepository(AppDbContext context) {
            _context = context;
        }

        // Busca uma conta pelo ID
        public Account? FindById(Guid id) {
            return _context.Accounts
                .FirstOrDefault(account => account.Id == id);
        }

        // Busca uma conta pelo e-mail
        public Account? FindByEmail(string email) {
            return _context.Accounts
                .FirstOrDefault(account => account.Email == email);
        }

        // Busca a conta pelo ID da pessoa associada a conta
        public Account? FindByPersonId(Guid personId) {
            return _context.Accounts
                .FirstOrDefault(account => account.PersonId == personId);
        }

        // Adiciona a conta ao contexto e persiste os dados no banco
        public Account Create(Account account) {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        // Remove a conta do contexto e persiste a alteração no banco
        public void Delete(Account account) {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
