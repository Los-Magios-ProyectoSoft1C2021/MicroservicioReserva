using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Commands;
using Template.Domain.Entities;

namespace Template.Application.Services
{

    public class EstadoReservaService : IEstadoReservaService
    {

        private readonly IGenericsRepository _repository;

        public EstadoReservaService(IGenericsRepository repository)
        {

            _repository = repository;

        }

        
    }
}
