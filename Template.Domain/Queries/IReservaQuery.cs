using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace Template.Domain.Queries
{
    public interface IReservaQuery
    {
        Task<ResponseReservaDTO> GetReservaById(Guid id);

        Task<List<ResponseReservaDTO>> GetReservaByUserId(int id);

        Task<List<ResponseReservaDTO>> GetReservaByHotelId(int id);

        Task<List<ResponseReservaDTO>> GetAllReserva();

        Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin);
    }


}
