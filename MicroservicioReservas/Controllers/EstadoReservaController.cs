using Microsoft.AspNetCore.Mvc;
using Template.Application.Services;

namespace MicroservicioReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoReservaController : ControllerBase
    {

        private readonly IReservaService _service;

        public EstadoReservaController(IReservaService service)
        {
            _service = service;
        }




    }
}
