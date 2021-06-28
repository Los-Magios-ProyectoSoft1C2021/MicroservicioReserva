using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace Template.Application.Services
{

    public interface IReservaService
    {
        void CreateReserva(ReservaDTO reserva);
        void UpdateReserva(ReservaDTO reserva); 
        List<ReservaDTO> GetReservaByUserId(int UserId);
        List<ReservaDTO> GetReservaByHotelId(int hotelId);
        List<ReservaDTO> GetAllReserva();
        Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin);
    }

   

     
    
}
