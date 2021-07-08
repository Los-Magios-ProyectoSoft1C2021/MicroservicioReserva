using System.Threading.Tasks;
using Template.Domain.Queries;

namespace Template.Application.Services
{

    public class EstadoReservaService : IEstadoReservaService
    {
        private readonly IEstadoReservaQuery _query;

        public EstadoReservaService(IEstadoReservaQuery query)
        {
            _query = query;
        }

        public Task<bool> CheckIfEstadoExists(int estadoId)
        {
            return _query.CheckIfEstadoExists(estadoId);
        }
    }
}
