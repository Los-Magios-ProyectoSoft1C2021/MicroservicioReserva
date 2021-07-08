using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace Template.Domain.Queries
{
    public interface IReservaQuery
    {
        ReservaDTO GetReservaById(Guid id);

        List<ReservaDTO> GetReservaByUserId(int id);

        List<ReservaDTO> GetReservaByHotelId(int id);

        List<ReservaDTO> GetAllReserva();

        Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin);
    }


}
