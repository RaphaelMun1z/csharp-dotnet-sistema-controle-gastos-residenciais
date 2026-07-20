// Utilizei um pré-fixo "api" nas endpoints da API, para indicar que se trata de uma API Rest
// Também utilizei a versão "v1" para indicar que é a primeira versão da API
// Isso permite que futuras versões da API sejam lançadas sem quebrar a compatibilidade com clientes existentes
using Microsoft.AspNetCore.Mvc;
using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Services.Interfaces;

namespace SistemaControleGastosResidenciais.Controllers {
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountController : ControllerBase {
        private readonly IAccountService _accountService;

        // O construtor recebe uma instância do serviço de contas, que é injetada pelo mecanismo de injeção de dependência
        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public ActionResult<AccountResponse> GetById(Guid id) {
            // Chama o método de busca presente no serviço, passando o ID da conta a ser buscada
            // Retorna a conta encontrada, com status 200, indicando que a operação foi bem sucedida
            AccountResponse accountFound = _accountService.FindById(id);
            return Ok(accountFound);
        }

        [HttpPost]
        public ActionResult<AccountResponse> Create([FromBody] CreateAccountRequest accountDTO) {
            // Solicita ao serviço a criação da conta
            AccountResponse createdAccount = _accountService.Create(accountDTO);
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdAccount.Id },
                createdAccount
            );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id) {
            // Solicita ao serviço a exclusão da conta pelo ID
            _accountService.Delete(id);
            return NoContent();
        }
    }
}
