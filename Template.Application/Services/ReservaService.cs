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
        private readonly MicroservicioUsuarioService _usuarioService;

        public ReservaService(IGenericsRepository repository, IReservaQuery reservaQuery, MicroservicioHotelService hotelService, MicroservicioUsuarioService usuarioService)
        {
            _repository = repository;
            _reservaQuery = reservaQuery;
            _hotelService = hotelService;
            _usuarioService = usuarioService;
        }

        public async Task<ResponseReservaDTO> CreateReserva(int usuarioId, RequestCreateReservaDTO reserva)
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

            return await _reservaQuery.GetReservaById(entity.ReservaId);
        }

        public async Task<ResponseReservaDTO> UpdateReserva(int usuarioId, RequestUpdateReservaDTO reserva)
        {
            var r = await _reservaQuery.GetReservaById(reserva.ReservaId);

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

            return await _reservaQuery.GetReservaById(entity.ReservaId);
        }

        public async Task<List<ResponseReservaDTO>> GetReservaByUserId(int userId)
        {
            return await _reservaQuery.GetReservaByUserId(userId);
        }

        public async Task<List<ResponseReservaDTO>> GetReservaByHotelId(int userId)
        {
            return await _reservaQuery.GetReservaByHotelId(userId);
        }

        public async Task<List<ResponseReservaDTO>> GetAllReserva(string token)
        {
            var reservas = await _reservaQuery.GetAllReserva();

            foreach (var reserva in reservas)
            {
                var hotel = await _hotelService.GetHotelById(reserva.HotelId);
                var usuario = await _usuarioService.GetUsuarioById(reserva.UsuarioId, token);

                reserva.Hotel = hotel.Nombre;
                reserva.NombreUsuario = usuario.NombreUsuario;
                reserva.Nombre = usuario.Nombre;
                reserva.Apellido = usuario.Apellido;
            }

            return reservas;
        }

        public async Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _reservaQuery.GetAllHabitacionesReservadasEntre(fechaInicio, fechaFin);
        }

    }

}
