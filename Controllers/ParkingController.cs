﻿using Microsoft.AspNetCore.Mvc;
using ProgettoApi.models;
using ProgettoApi.Service;

namespace ProgettoApi.Controllers
{
    [ApiController]
    [Route("parking")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _service;

        public ParkingController(IParkingService parkingService)
        {
            _service = parkingService;
        }

        [HttpPost("in")]
        public IActionResult Ingresso([FromBody] InputDati input)
        {
            var result = _service.Entry(input);
            return Ok(result); // Restituisce TicketId e messaggio
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
            var result = _service.ShowIrregularities();
            int totalIrregularities = result.Count;
            return Ok(new { TotaleIrregolarità = totalIrregularities, Dettagli = result });
        }
    }

}