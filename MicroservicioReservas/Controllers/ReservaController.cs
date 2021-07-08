using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Template.Application.Services;
using Template.Domain.Entities;

namespace MicroservicioReservas.Controllers
{
    [Route("api/reserva")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service;
        }

        [Authorize(Policy = "UsuarioOnly")]
        [HttpPost]
        public ActionResult PostReserva([FromBody] ReservaDTO reserva)
        {
            var usuario = HttpContext.User;
            var usuarioId = usuario.FindFirst("UsuarioId");
            if (usuarioId != null && (int.Parse(usuarioId.Value) == reserva.UsuarioId))
            {
                _service.CreateReserva(reserva);
                return Created(uri: "api/reserva/", null);
            }

            return Unauthorized();
        }

        [Authorize(Policy = "UsuarioOnly")]
        [HttpGet("usuario")]
        public ActionResult<List<ReservaDTO>> GetReservaByUser()
        {
            var usuario = HttpContext.User;
            var usuarioId = usuario.FindFirst("UsuarioId");
            if (usuarioId != null)
                return _service.GetReservaByUserId(int.Parse(usuarioId.Value));

            return Unauthorized();
        }

        [Authorize(Policy = "UsuarioOnly")]
        [HttpPut]
        public ActionResult PutReserva([FromBody] ReservaDTO reserva)
        {
            if (reserva == null)
                return BadRequest("El body no puede estar vacío");

            var usuario = HttpContext.User;
            var usuarioId = usuario.FindFirst("UsuarioId");
            if (usuarioId != null)
            {
                if (usuarioId != null && (int.Parse(usuarioId.Value) == reserva.UsuarioId))
                    _service.UpdateReserva(reserva);

                return Ok(reserva);
            }

            return Unauthorized();
        }

        // Se utiliza para chequear disponibilidad de habitaciones
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("hotel")]
        public ActionResult<List<ReservaDTO>> GetReservaByHotelId([FromQuery] int hotelId)
        {
            return Ok(_service.GetReservaByHotelId(hotelId));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public ActionResult<List<ReservaDTO>> GetAllReserva()
        {
            return Ok(_service.GetAllReserva());
        }

        [AllowAnonymous]
        [HttpGet("fecha")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllHabitacionesReservadasEntre(
                                        [FromQuery(Name = "FechaInicio")] string strFechaInicio,
                                        [FromQuery(Name = "FechaFin")] string strFechaFin)
        {
            DateTime fechaInicio = new DateTime();
            DateTime fechaFin = new DateTime();

            if (!(DateTime.TryParse(strFechaInicio, out fechaInicio) &&
                DateTime.TryParse(strFechaFin, out fechaFin)))
                return BadRequest("Formato de ingreso de fechas incorrecto");

            if (fechaFin < fechaInicio)
                return BadRequest("La fecha de fin es menor a la fecha de inicio de la reserva");

            List<ReservasGroupByHotelIdDTO> habitacionesReservadas;

            habitacionesReservadas = await _service.GetAllHabitacionesReservadasEntre(fechaInicio, fechaFin);

            return Ok(habitacionesReservadas);
        }

    }
}
