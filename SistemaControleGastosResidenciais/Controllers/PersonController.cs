using Microsoft.AspNetCore.Mvc;
using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;
using SistemaControleGastosResidenciais.Services.Interfaces;

// Utilizei um pré-fixo "api" nas endpoints da API, para indicar que se trata de uma API Rest
// Também utilizei a versão "v1" para indicar que é a primeira versão da API.
// Isso permite que futuras versões da API sejam lançadas sem quebrar a compatibilidade com clientes existentes.
namespace SistemaControleGastosResidenciais.Controllers {
    [ApiController]
    [Route("api/v1/people")]
    public class PersonController : ControllerBase {
        private readonly IPersonService _personService;

        // O construtor recebe uma instância do serviço de pessoas, que é injetada pelo mecanismo de injeção de dependência
        public PersonController(IPersonService personService) {
            _personService = personService;
        }

        [HttpGet]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(PagedResponseDTO<PersonResponseDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PagedResponseDTO<PersonResponseDTO>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        ) {
            // Chama o método de busca presente no serviço, passando os parâmetros de paginação
            PagedResponseDTO<PersonResponseDTO> people = _personService.FindAll(page, pageSize);
            return Ok(people);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonResponseDTO> GetById(Guid id) {
            // Chama o método de busca presente no serviço, passando o ID da pessoa a ser buscada
            // Retorna a pessoa encontrada, com status 200, indicando que a operação foi bem sucedida
            PersonResponseDTO personFound = _personService.FindById(id);
            return Ok(personFound);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonResponseDTO> Create([FromBody] CreatePersonRequestDTO personDTO) {
            // Solicita ao serviço a criação da pessoa
            PersonResponseDTO createdPerson = _personService.Create(personDTO);
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdPerson.Id },
                createdPerson
            );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteById(Guid id) {
            // Solicita ao serviço a exclusão da pessoa pelo ID
            _personService.Delete(id);
            return NoContent();
        }
    }
}
