using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.Commands;
using Template.Domain.Entities;
using Template.Domain.Queries;

namespace Template.Application.Services
{

    public class ReservaService : IReservaService
    {

        private readonly IGenericsRepository _repository;
        private readonly IReservaQuery _reservaQuery;

        public ReservaService(IGenericsRepository repository, IReservaQuery reservaQuery)
        {
            _repository = repository;
            _reservaQuery = reservaQuery;
        }


        public void CreateReserva(ReservaDTO reserva)
        {
            //Aca "parsea" metiendo los datos de ReservaDTO en reserva
            //Hay maneras mas felices de hacer esto...


            var entity = new Reserva
            {
                ReservaId = Guid.NewGuid(),  // aqui se genera el Id de la reserva
                UsuarioId = reserva.UsuarioId,
                HabitacionId = reserva.HabitacionId,
                HotelId = reserva.HotelId,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                EstadoReservaId = 2  //Reservado

            };

            _repository.Add<Reserva>(entity);

        }

        public void UpdateReserva(ReservaDTO reserva)
        {

            var entity = new Reserva
            {
                ReservaId = reserva.ReservaId,
                UsuarioId = reserva.UsuarioId,
                HabitacionId = reserva.HabitacionId,
                HotelId = reserva.HotelId,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                EstadoReservaId = reserva.EstadoReservaId
            };

            _repository.Update<Reserva>(entity);
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

        public async Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(
                                                              DateTime fechaInicio, DateTime fechaFin)
        {

            List<ReservasGroupByHotelIdDTO> listaReservasPorHotelIdDTO = new List<ReservasGroupByHotelIdDTO>();

            return await _reservaQuery.GetAllHabitacionesReservadasEntre(fechaInicio, fechaFin);
        }


    }

}
