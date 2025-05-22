using Microsoft.AspNetCore.Mvc;
using ProgettoApi.models;
using ProgettoApi.Service;

namespace ProgettoApi.Controllers
{
    [ApiController]
    [Route("parking")]
    public class ParkingController : ControllerBase
    {
        private readonly ParkingService service = new ParkingService();

        [HttpPost()]
        public string GetTest(Parkingin parkingin)
        {
            var now = DateTime.Today.Date;


            if (parkingin.Ingresso.Date < now)
            {
                return $"La data {parkingin.Ingresso} è passata";
            }
            else if (parkingin.Ingresso.Date > now)
            {
                return $"La data {parkingin.Ingresso} è futura";
            }
            else
            {
                return $"La data {parkingin.Ingresso} è presente";
            }
        }

        [HttpPost("in")]
        public string Ingresso(InputDatiTest input)
        {
            return service.Entry(input);
        }

        [HttpPost("out")]

        [HttpGet("irregolari")]

    }
}
