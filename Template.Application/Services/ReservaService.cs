using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application.HttpServices;
using Template.Domain.Commands;
using Template.Domain.DTOs.Request;
using Template.Domain.Entities;
using Template.Domain.Queries;

namespace Template.Application.Services
{

    public class ReservaService : IReservaService
    {

        private readonly IGenericsRepository _repository;
        private readonly IReservaQuery _reservaQuery;
        private readonly MicroservicioHotelService _hotelService;

        public ReservaService(IGenericsRepository repository, IReservaQuery reservaQuery, MicroservicioHotelService hotelService)
        {
            _repository = repository;
            _reservaQuery = reservaQuery;
            _hotelService = hotelService;
        }

        public async Task<ReservaDTO> CreateReserva(int usuarioId, RequestCreateReservaDTO reserva)
        {
            var habitaciones = await _hotelService.GetHabitacionesByHotelAndTipo(reserva.HotelId, reserva.TipoHabitacionId);
            var hReservadas = await _reservaQuery.GetAllHabitacionesReservadasEntre(reserva.FechaInicio, reserva.FechaFin);
            var habitacionesReservadas = hReservadas.Select(x => x.Habitaciones).FirstOrDefault();

            var habitacionId = 0;
            if (habitacionesReservadas != null)
            {
                habitacionId = habitaciones
                    .Where(x => !habitacionesReservadas.Contains(x))
                    .FirstOrDefault();
            }
            else
            {
                return null;
            }

            var entity = new Reserva
            {
                ReservaId = Guid.NewGuid(),  // aqui se genera el Id de la reserva
                UsuarioId = usuarioId,
                HabitacionId = habitacionId,
                HotelId = reserva.HotelId,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                EstadoReservaId = 2  //Reservado
            };

            _repository.Add<Reserva>(entity);

            return new ReservaDTO
            {
                ReservaId = entity.ReservaId,
                UsuarioId = entity.UsuarioId,
                HotelId = entity.HotelId,
                HabitacionId = entity.HabitacionId,
                FechaInicio = entity.FechaInicio,
                FechaFin = entity.FechaFin,
                EstadoReservaId = entity.EstadoReservaId
            };
        }

        public async Task<ReservaDTO> UpdateReserva(int usuarioId, RequestUpdateReservaDTO reserva)
        {
            var r = _reservaQuery.GetReservaById(reserva.ReservaId);
            
            if (r.UsuarioId != usuarioId)
                return null;

            var entity = new Reserva
            {
                ReservaId = r.ReservaId,
                UsuarioId = usuarioId,
                HabitacionId = r.HabitacionId,
                HotelId = r.HotelId,
                FechaInicio = r.FechaInicio,
                FechaFin = r.FechaFin,
                EstadoReservaId = reserva.EstadoReservaId
            };

            _repository.Update<Reserva>(entity);

            return new ReservaDTO
            {
                ReservaId = entity.ReservaId,
                UsuarioId = entity.UsuarioId,
                HotelId = entity.HotelId,
                HabitacionId = entity.HabitacionId,
                FechaInicio = entity.FechaInicio,
                FechaFin = entity.FechaFin,
                EstadoReservaId = entity.EstadoReservaId
            };
        }

        public List<ReservaDTO> GetReservaByUserId(int userId)
        {

            return _reservaQuery.GetReservaByUserId(userId);

        }

        public List<ReservaDTO> GetReservaByHotelId(int userId)
        {
            return _reservaQuery.GetReservaByHotelId(userId);
        }

        public List<ReservaDTO> GetAllReserva()
        {
            return _reservaQuery.GetAllReserva();
        }

        public async Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _reservaQuery.GetAllHabitacionesReservadasEntre(fechaInicio, fechaFin);
        }

    }

}
