using Microsoft.AspNetCore.Mvc;
using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Services.Interfaces;

// Utilizei um pré-fixo "api" nas endpoints da API, para indicar que se trata de uma API Rest
// Também utilizei a versão "v1" para indicar que é a primeira versão da API
// Isso permite que futuras versões da API sejam lançadas sem quebrar a compatibilidade com clientes existentes
namespace SistemaControleGastosResidenciais.Controllers {
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionController : ControllerBase {
        private readonly ITransactionService _transactionService;

        // O construtor recebe uma instância do serviço de transações, que é injetada pelo mecanismo de injeção de dependência
        public TransactionController(ITransactionService transactionService) {
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<PagedResponseDTO<TransactionResponseDTO>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        ) {
            // Chama o método de busca presente no serviço, passando os parâmetros de paginação
            PagedResponseDTO<TransactionResponseDTO> transactions = _transactionService.FindAll(page, pageSize);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionResponseDTO> GetById(Guid id) {
            // Chama o método de busca presente no serviço, passando o ID da transação a ser buscada
            // Retorna a transação encontrada, com status 200, indicando que a operação foi bem sucedida
            TransactionResponseDTO transactionFound = _transactionService.FindById(id);
            return Ok(transactionFound);
        }

        [HttpGet("person/{personId}")]
        public ActionResult<PagedResponseDTO<TransactionResponseDTO>> GetByPersonId(
            Guid personId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        ) {
            // Chama o método de busca presente no serviço, passando o ID da pessoa e os parâmetros de paginação
            PagedResponseDTO<TransactionResponseDTO> transactions =
                _transactionService.FindByPersonId(
                    personId,
                    page,
                    pageSize
                );
            return Ok(transactions);
        }

        [HttpPost]
        public ActionResult<TransactionResponseDTO> Create([FromBody] CreateTransactionRequestDTO transactionDTO) {
            // Solicita ao serviço a criação da transação
            TransactionResponseDTO createdTransaction = _transactionService.Create(transactionDTO);
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdTransaction.Id },
                createdTransaction
            );
        }
    }
}
