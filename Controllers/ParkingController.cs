using Microsoft.AspNetCore.Mvc;
using ProgettoApi.models;
using ProgettoApi.Service;

namespace ProgettoApi.Controllers
{
    [ApiController]
    [Route("parking")]
    public class ParkingController : ControllerBase
    {
        //ciao ciccio prova
        private readonly ParkingService _service = new ParkingService();

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

        [HttpGet("irregolari")]//ads
        public IActionResult GetIrregolari()
        {
            var result = _service.ShowIrregularities();
            int totalIrregularities = result.Count; 
            return Ok(new { TotaleIrregolarità = totalIrregularities, Dettagli = result });
        }

    }
}