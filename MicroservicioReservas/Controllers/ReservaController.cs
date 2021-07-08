using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Template.Application.Services;
using Template.Domain.DTOs.Request;
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
        public async Task<ActionResult> PostReserva([FromBody] RequestCreateReservaDTO reserva)
        {
            var usuario = HttpContext.User;
            var usuarioId = usuario.FindFirst("UsuarioId");
            
            if (usuarioId != null)
            {
                var r = await _service.CreateReserva(int.Parse(usuarioId.Value), reserva);

                if (r != null)
                    return Created(uri: "api/reserva/", r);
                else
                    return Problem(statusCode: 500, detail: "No es posible computar la reserva");
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
        public ActionResult PutReserva([FromBody] RequestUpdateReservaDTO reserva)
        {
            if (reserva == null)
                return BadRequest("El body no puede estar vacío");

            var usuario = HttpContext.User;
            var usuarioId = usuario.FindFirst("UsuarioId");

            if (usuarioId != null)
            {
                var modifiedReserva = _service.UpdateReserva(int.Parse(usuarioId.Value), reserva);

                if (modifiedReserva != null)
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
            if (!(DateTime.TryParse(strFechaInicio, out DateTime fechaInicio) &&
                DateTime.TryParse(strFechaFin, out DateTime fechaFin)))
                return Problem(statusCode: 400, detail: "Las fechas no tienen el formato correcto");

            if (fechaFin < fechaInicio)
                return Problem(statusCode: 400, detail: "Las fecha de fin debe ser mayor a la fecha de inicio");

            var habitacionesReservadas = await _service.GetAllHabitacionesReservadasEntre(fechaInicio, fechaFin);
            return Ok(habitacionesReservadas);
        }

    }
}
