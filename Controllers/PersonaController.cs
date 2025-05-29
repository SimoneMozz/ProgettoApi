using Microsoft.AspNetCore.Mvc;
using ProgettoApi.models;
using ProgettoApi.Service;

namespace ProgettoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Persona persona)
        {
            var created = _personaService.CreatePersona(persona);
            return CreatedAtAction(nameof(GetByEmail), new { email = created.Email }, created);
        }

        [HttpGet]
        public ActionResult<List<Persona>> GetAll()
        {
            return Ok(_personaService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Persona> Get(int id)
        {
            var persona = _personaService.GetById(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personaService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Persona persona)
        {
            _personaService.Update(id, persona);
            return NoContent();
        }

        [HttpGet("by-email/{email}")]
        public ActionResult<Persona> GetByEmail(string email)
        {
            var persona = _personaService.GetByEmail(email);
            if (persona == null) return NotFound();
            return Ok(persona);
        }
    }
}
