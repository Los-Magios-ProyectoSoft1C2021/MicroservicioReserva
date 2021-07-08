using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.DTOs.Request;
using Template.Domain.Entities;

namespace Template.Application.Services
{

    public interface IReservaService
    {
        Task<ReservaDTO> CreateReserva(int usuarioId, RequestCreateReservaDTO reserva);
        Task<ReservaDTO> UpdateReserva(int usuarioId, RequestUpdateReservaDTO reserva);
        List<ReservaDTO> GetReservaByUserId(int UserId);
        List<ReservaDTO> GetReservaByHotelId(int hotelId);
        List<ReservaDTO> GetAllReserva();
        Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin);
    }





}
