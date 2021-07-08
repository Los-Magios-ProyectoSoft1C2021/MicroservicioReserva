using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Template.Domain.Queries;

namespace Template.AccessData.Queries
{
    public class EstadoReservaQuery : IEstadoReservaQuery
    {
        private readonly TemplateDbContext _context;

        public EstadoReservaQuery(TemplateDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfEstadoExists(int estadoId)
        {
            var exists = _context.EstadoReserva.
                AnyAsync(e => e.EstadoReservaId == estadoId);

            return await exists;
        }
    }
}
