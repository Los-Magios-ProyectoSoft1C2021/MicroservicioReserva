using System.Threading.Tasks;

namespace Template.Application.Services
{

    public interface IEstadoReservaService
    {
        Task<bool> CheckIfEstadoExists(int estadoId);
    }

}
