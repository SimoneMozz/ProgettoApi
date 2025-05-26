using Microsoft.AspNetCore.Mvc;
using ProgettoApi.models;
using ProgettoApi.Service;

namespace ProgettoApi.Controllers
{
    [ApiController]
    [Route("parking")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _service;

        public ParkingController(IParkingService service)
        {
            _service = service;
        }

        [HttpPost("in")]
        public IActionResult Ingresso([FromBody] InputDati input)
        {
            var result = _service.Entry(input);
            return Ok(result);
        }

        [HttpPost("out")]
        public IActionResult Uscita([FromBody] InputDati input)
        {
            var result = _service.Exit(input);
            return Ok(result);
        }

        [HttpGet("irregolari")]
        public IActionResult GetIrregolari()
        {
            var result = _service.GetAllInfractions();
            int total = result?.Count ?? 0;
            return Ok(new { TotaleIrregolarità = total, Dettagli = result });
        }
    }
}


