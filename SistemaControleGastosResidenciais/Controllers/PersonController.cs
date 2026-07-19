using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace SistemaControleGastosResidenciais.Controllers
{
    [ApiController]
    [Route("api/people/v1")]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IActionResult FindAll(string? name, int? age)
        {
            if(age <= 0)
            {
                return BadRequest("Informe uma idade válida! Igual ou maior a 1.");
            }
            return Ok("1");
        }
    }
}
