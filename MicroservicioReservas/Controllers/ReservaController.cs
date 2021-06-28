using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Template.Application.Services;
using Template.Domain.Entities;

namespace MicroservicioReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service; 
        }

        [Route("/api/reserva/")]
        [HttpPost]
        public IActionResult PostReserva([FromBody] ReservaDTO reserva)
        {

             _service.CreateReserva(reserva);
             return Created(uri: "api/reserva/",null);
        }


        [Route("/api/reserva/GetByUserId/")]
        [HttpGet]
        public List<ReservaDTO> GetReservaByUserId([FromBody]int id)
        {
            return _service.GetReservaByUserId(id);    
        }
        
        [Route("/api/reserva/update")]
        [HttpPatch]       
        public IActionResult PatchReserva([FromBody] ReservaDTO reserva)
        {

            _service.UpdateReserva(reserva);    
            
            if (reserva == null)
                return NotFound();
            return Ok(reserva);

        }

        // Se utiliza para chequear disponibilidad de habitaciones
        [Route("/api/reserva/GetByHotelId/")]
        [HttpGet]
        public List<ReservaDTO> GetReservaByHotelId([FromBody] int hotelId)
        {
            return _service.GetReservaByHotelId(hotelId);
        }

        [Route("/api/reserva/GetAll/")]
        [HttpGet]
        public List<ReservaDTO> GetAllReserva()
        {
            return _service.GetAllReserva();
        }

        [Route("/api/reserva/GetAllHabitacionesReservadasEntre/")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllHabitacionesReservadasEntre(
                                        [FromQuery(Name = "FechaInicio")] string strFechaInicio,
                                        [FromQuery(Name = "FechaFin")] string strFechaFin)
        {

            DateTime fechaInicio = new DateTime();
            DateTime fechaFin = new DateTime();

            if ( DateTime.TryParse(strFechaInicio, out fechaInicio) &&
                 DateTime.TryParse(strFechaFin, out fechaFin))                

                return BadRequest("Formato de ingreso de fechas incorrecto");

            if (fechaFin < fechaInicio)

                return BadRequest("La fecha de fin es menor a la fecha de inicio de la reserva");

            List<ReservasGroupByHotelIdDTO> habitacionesReservadas;

            habitacionesReservadas = await _service.GetAllHabitacionesReservadasEntre(fechaInicio,fechaFin);

            return Ok(habitacionesReservadas);
         
        }

    }
}
