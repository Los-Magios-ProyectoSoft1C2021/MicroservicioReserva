using Template.Domain.Commands;

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
