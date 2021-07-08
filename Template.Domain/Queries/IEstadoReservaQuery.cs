using System.Threading.Tasks;

namespace Template.Domain.Queries
{
    public interface IEstadoReservaQuery
    {
        Task<bool> CheckIfEstadoExists(int estadoId);
    }
}
