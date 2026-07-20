using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Enums;
using SistemaControleGastosResidenciais.Mappings.Implementations;
using SistemaControleGastosResidenciais.Repositories.Interfaces;
using SistemaControleGastosResidenciais.Services.Interfaces;

namespace SistemaControleGastosResidenciais.Services.Implementations {
    public class TransactionServiceImpl : ITransactionService {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<Person> _personRepository;

        private readonly TransactionMapper _transactionMapper = new TransactionMapper();

        // Recebe os repositórios por injeção de dependência
        public TransactionServiceImpl(
            ITransactionRepository transactionRepository,
            IRepository<Person> personRepository
        ) {
            _transactionRepository = transactionRepository;
            _personRepository = personRepository;
        }

        public TransactionResponseDTO FindById(Guid id) {
            // Valida o ID informado
            if (id == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido");
            }

            // Busca a transação pelo ID
            Transaction? foundTransaction = _transactionRepository.FindById(id);

            // Se a transação não for encontrada, lança uma exceção
            if (foundTransaction == null) {
                throw new KeyNotFoundException("Transação não encontrada");
            }

            // Converte a entidade encontrada para DTO de resposta
            return _transactionMapper.ToResponse(foundTransaction);
        }

        public PagedResponseDTO<TransactionResponseDTO> FindAll(int page, int pageSize) {
            // Valida os parâmetros de paginação
            ValidatePagination(page, pageSize);

            // Busca as transações utilizando paginação
            List<Transaction> transactionList = _transactionRepository.FindAll(page, pageSize);

            // Busca a quantidade total de transações cadastradas
            int totalElements = _transactionRepository.Count();

            // Calcula a quantidade total de páginas
            int totalPages = (int)Math.Ceiling(
                totalElements / (double)pageSize
            );

            // Converte as entidades encontradas para DTOs de resposta
            List<TransactionResponseDTO> transactionResponseList = _transactionMapper.ToResponseList(transactionList);

            return new PagedResponseDTO<TransactionResponseDTO> {
                Content = transactionResponseList,
                Page = page,
                PageSize = pageSize,
                TotalElements = totalElements,
                TotalPages = totalPages
            };
        }

        public PagedResponseDTO<TransactionResponseDTO> FindByPersonId(Guid personId, int page, int pageSize) {
            // Valida o ID informado
            if (personId == Guid.Empty) {
                throw new BadHttpRequestException("Informe um ID válido");
            }

            // Valida os parâmetros de paginação
            ValidatePagination(page, pageSize);

            // Verifica se a pessoa informada existe
            Person? person = _personRepository.FindById(personId);

            // Se a pessoa não for encontrada, lança uma exceção
            if (person == null) {
                throw new KeyNotFoundException("Pessoa não encontrada");
            }

            // Busca as transações associadas à pessoa utilizando paginação
            List<Transaction> transactionList =
                _transactionRepository.FindByPersonId(
                    personId,
                    page,
                    pageSize
                );

            // Busca a quantidade total de transações associadas à pessoa
            int totalElements = _transactionRepository.CountByPersonId(personId);

            // Calcula a quantidade total de páginas
            int totalPages = (int)Math.Ceiling(
                totalElements / (double)pageSize
            );

            // Converte as entidades encontradas para DTOs de resposta
            List<TransactionResponseDTO> transactionResponseList = _transactionMapper.ToResponseList(transactionList);

            return new PagedResponseDTO<TransactionResponseDTO> {
                Content = transactionResponseList,
                Page = page,
                PageSize = pageSize,
                TotalElements = totalElements,
                TotalPages = totalPages
            };
        }

        public TransactionResponseDTO Create(CreateTransactionRequestDTO transactionDTO) {
            // Verifica se a pessoa informada existe
            Person? person = _personRepository.FindById(transactionDTO.PersonId);

            // Se a pessoa não for encontrada, lança uma exceção
            if (person == null) {
                throw new KeyNotFoundException("Pessoa não encontrada");
            }

            // Pessoas menores de 18 anos podem registrar apenas despesas
            if (person.Age < 18 && transactionDTO.Type == TransactionTypeEnum.Revenue) {
                throw new InvalidOperationException("Pessoas menores de 18 anos podem registrar apenas despesas");
            }

            // Cria uma nova transação
            // As validações dos atributos são realizadas pela própria entidade
            Transaction newTransaction = _transactionMapper.ToResponse(transactionDTO);

            // Persiste a transação no banco de dados
            Transaction savedTransaction = _transactionRepository.Create(newTransaction);

            // Converte a entidade persistida para DTO de resposta
            return _transactionMapper.ToResponse(savedTransaction);
        }

        private static void ValidatePagination(int page, int pageSize) {
            // Valida o número da página informado
            if (page < 1) {
                throw new BadHttpRequestException("A página deve ser maior ou igual a 1");
            }

            // Valida a quantidade de registros por página
            if (pageSize < 1 || pageSize > 100) {
                throw new BadHttpRequestException("A quantidade de registros por página deve estar entre 1 e 100");
            }
        }
    }
}