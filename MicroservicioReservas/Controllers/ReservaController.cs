using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
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

            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (usuarioId != null)
            {
                var r = await _service.CreateReserva(int.Parse(usuarioId.Value), accessToken, reserva);

                if (r != null)
                    return Created(uri: "api/reserva/", r);
                else
                    return Problem(statusCode: 500, detail: "No es posible computar la reserva");
            }

            return Unauthorized();
        }

        [Authorize(Policy = "UsuarioOnly")]
        [HttpGet("usuario")]
        public async Task<ActionResult<List<ResponseReservaDTO>>> GetReservaByUser()
        {
            var usuario = HttpContext.User;
            var usuarioId = usuario.FindFirst("UsuarioId");

            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (usuarioId != null)
                return await _service.GetReservaByUserId(int.Parse(usuarioId.Value), accessToken);

            return Unauthorized();
        }

        [Authorize(Policy = "UsuarioAndAdminOnly")]
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutReserva(Guid id, [FromBody] RequestUpdateReservaDTO reserva)
        {
            if (reserva == null)
                return BadRequest("El body no puede estar vacío");

            var usuario = HttpContext.User;
            var usuarioId = usuario.FindFirst("UsuarioId");

            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (usuarioId != null)
            {
                var modifiedReserva = await _service.UpdateReserva(int.Parse(usuarioId.Value), accessToken, id, reserva);

                if (modifiedReserva != null)
                    return Ok(modifiedReserva);
            }

            return Unauthorized();
        }

        // Se utiliza para chequear disponibilidad de habitaciones
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("hotel")]
        public async Task<ActionResult<List<ResponseReservaDTO>>> GetReservaByHotelId([FromQuery] int hotelId)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            return await _service.GetReservaByHotelId(hotelId, accessToken);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<List<ResponseReservaDTO>>> GetAllReserva()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            return Ok(await _service.GetAllReserva(accessToken));
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
