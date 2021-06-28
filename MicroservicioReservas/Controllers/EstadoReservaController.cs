using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application.Services;
using Template.Domain.Entities;

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
