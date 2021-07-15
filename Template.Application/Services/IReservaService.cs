using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.DTOs.Request;
using Template.Domain.Entities;

namespace Template.Application.Services
{
    public interface IReservaService
    {
        Task<ResponseReservaDTO> CreateReserva(int usuarioId, string token, RequestCreateReservaDTO reserva);
        Task<ResponseReservaDTO> UpdateReserva(string token, Guid reservaId, RequestUpdateReservaDTO reserva);
        Task<List<ResponseReservaDTO>> GetReservaByUserId(int usuarioId, string token);
        Task<List<ResponseReservaDTO>> GetReservaByHotelId(int hotelId, string token);
        Task<List<ResponseReservaDTO>> GetAllReserva(string token);
        Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin);
    }
}
