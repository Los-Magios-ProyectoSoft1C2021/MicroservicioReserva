using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.DTOs.Request;
using Template.Domain.Entities;

namespace Template.Application.Services
{

    public interface IReservaService
    {
        Task<ResponseReservaDTO> CreateReserva(int usuarioId, RequestCreateReservaDTO reserva);
        Task<ResponseReservaDTO> UpdateReserva(int usuarioId, RequestUpdateReservaDTO reserva);
        Task<List<ResponseReservaDTO>> GetReservaByUserId(int UserId);
        Task<List<ResponseReservaDTO>> GetReservaByHotelId(int hotelId);
        Task<List<ResponseReservaDTO>> GetAllReserva(string token);
        Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin);
    }





}
