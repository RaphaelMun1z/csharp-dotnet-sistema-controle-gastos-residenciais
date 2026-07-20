using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Mappings;
using SistemaControleGastosResidenciais.Repositories.Interfaces;
using SistemaControleGastosResidenciais.Services.Interfaces;

namespace SistemaControleGastosResidenciais.Services.Implementations {
    public class AccountServiceImpl : IAccountService {
        private readonly IAccountRepository _accountRepository;
        private readonly IRepository<Person> _personRepository;

        // Recebe os repositórios por injeção de dependência
        public AccountServiceImpl(
            IAccountRepository accountRepository,
            IRepository<Person> personRepository
        ) {
            _accountRepository = accountRepository;
            _personRepository = personRepository;
        }

        public AccountResponse FindById(Guid id) {
            // Valida o ID informado
            if (id == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido");
            }

            // Busca a conta pelo ID
            Account? foundAccount = _accountRepository.FindById(id);

            // Se a conta não for encontrada, lança uma exceção
            if (foundAccount == null) {
                throw new KeyNotFoundException("Conta não encontrada");
            }

            // Converte a entidade encontrada para DTO de resposta
            return AccountMapper.ToResponse(foundAccount);
        }

        public AccountResponse Create(CreateAccountRequest accountDTO) {
            // Verifica se a pessoa informada existe
            Person? person = _personRepository.FindById(accountDTO.PersonId);

            // Se a pessoa não for encontrada, lança uma exceção
            if (person == null) {
                throw new KeyNotFoundException("Pessoa não encontrada");
            }

            // Verifica se a pessoa já possui uma conta cadastrada
            Account? accountByPerson = _accountRepository.FindByPersonId(accountDTO.PersonId);

            // Se a pessoa já tiver uma conta, lança uma exceção
            if (accountByPerson != null) {
                throw new InvalidOperationException("Esta pessoa já possui uma conta");
            }

            // Verifica se o e-mail informado já está cadastrado
            Account? accountByEmail = _accountRepository.FindByEmail(accountDTO.Email);

            // Se o e-mail já estiver cadastrado, lança uma exceção
            if (accountByEmail != null) {
                throw new InvalidOperationException("Já existe uma conta cadastrada com este e-mail");
            }

            // Cria uma nova conta
            // As validações dos atributos são realizadas pela própria entidade
            Account newAccount = new Account(
                accountDTO.PersonId,
                accountDTO.Email,
                accountDTO.Password
            );

            // Persiste a conta no banco de dados
            Account savedAccount = _accountRepository.Create(newAccount);

            // Converte a entidade persistida para DTO de resposta
            return AccountMapper.ToResponse(savedAccount);
        }

        public void Delete(Guid id) {
            // Valida o ID informado
            if (id == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido");
            }

            // Busca a conta pelo id
            Account? account = _accountRepository.FindById(id);

            // Se a conta não for encontrada, lança uma exceção
            if (account == null) {
                throw new KeyNotFoundException("Conta não encontrada");
            }

            // Remove a conta pelo ID
            _accountRepository.Delete(id);
        }
    }
}