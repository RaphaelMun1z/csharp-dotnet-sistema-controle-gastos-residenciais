using Microsoft.AspNetCore.Mvc;
using SistemaControleGastosResidenciais.Entities;
using SistemaControleGastosResidenciais.Services.Interfaces;

// Utilizei um pré-fixo "api" nas endpoints da API, para indicar que se trata de uma API Rest
// Também utilizei a versão "v1" para indicar que é a primeira versão da API.
// Isso permite que futuras versões da API sejam lançadas sem quebrar a compatibilidade com clientes existentes.
namespace SistemaControleGastosResidenciais.Controllers {
    [ApiController]
    [Route("api/people")]
    public class PersonController : ControllerBase {
        private IPersonService _personService;

        // O construtor recebe uma instância do serviço de pessoas, que é injetada pelo mecanismo de injeção de dependência
        public PersonController(IPersonService personService) {
            _personService = personService;
        }

        [HttpGet("v1")]
        public IActionResult Get() {
            // Chama o método de busca presente no serviço, que retorna a lista de todas as pessoas registradas
            List<Person> peopleList = _personService.FindAll();
            return Ok(peopleList);
        }

        [HttpGet("v1/{id}")]
        public ActionResult<Person> Get(Guid id) {
            // Chama o método de busca presente no serviço, passando o ID da pessoa a ser buscada
            // Retorna a pessoa encontrada, com status 200, indicando que a operação foi bem sucedida
            Person personFound = _personService.FindById(id);
            return Ok(personFound);
        }

        [HttpPost("v1")]
        public IActionResult Post([FromBody] Person person) {
            // Solicita a criação da pessoa via serviço, e recebe a instância cadastrada
            // Retorna status 200, indicando que a pessoa foi cadastrada com sucesso
            Person createdPerson = _personService.Create(person);
            return Ok(createdPerson);
        }


        [HttpDelete("v1/{id}")]
        public IActionResult DeleteById(Guid id) {
            // Chama o método de deletar presente no serviço, passando o ID da pessoa a ser deletada
            _personService.Delete(id);

            // Retorna 'no content', indicando que a operação foi bem sucedida, mas sem conteúdo para retornar
            return NoContent();
        }
    }
}
