using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Domain.Entities;
using Template.Domain.Queries;

namespace Template.AccessData.Queries
{
    public class ReservaQuery : IReservaQuery
    {

        private readonly TemplateDbContext _context;

        public ReservaQuery(TemplateDbContext context)
        {
            _context = context;
        }

        public ReservaDTO GetReservaById(Guid id)
        {
            var reserva = _context.Reserva
                .Where(x => x.ReservaId == id)
                .Select(x => new ReservaDTO
                {
                    ReservaId = x.ReservaId,
                    HotelId = x.HotelId,
                    HabitacionId = x.HabitacionId,
                    UsuarioId = x.UsuarioId,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    EstadoReservaId = x.EstadoReservaId
                })
                .FirstOrDefault();

            return reserva;
        }

        public List<ReservaDTO> GetReservaByUserId(int id)
        {

            List<Reserva> listaDeReservas = new List<Reserva>();
            List<ReservaDTO> listaDeReservasDTO = new List<ReservaDTO>();

            listaDeReservas = _context.Set<Reserva>()
                .Where(x => x.UsuarioId == id)
                .ToList();

            // mapeo entre Reserva y ReservaDTO

            foreach (Reserva elem in listaDeReservas)
            {

                ReservaDTO reservaDTO = new ReservaDTO
                {
                    ReservaId = elem.ReservaId,
                    UsuarioId = elem.UsuarioId,
                    HabitacionId = elem.HabitacionId,
                    HotelId = elem.HotelId,
                    FechaInicio = elem.FechaInicio,
                    FechaFin = elem.FechaFin,
                    EstadoReservaId = elem.EstadoReservaId
                };

                listaDeReservasDTO.Add(reservaDTO);

            }

            return listaDeReservasDTO;
        }


        public List<ReservaDTO> GetReservaByHotelId(int hotelId)
        {

            List<Reserva> listaDeReservas = new List<Reserva>();
            List<ReservaDTO> listaDeReservasDTO = new List<ReservaDTO>();

            listaDeReservas = _context.Set<Reserva>()
                .Where(x => x.HotelId == hotelId)
                .ToList();

            // mapeo entre Reserva y ReservaDTO

            foreach (Reserva elem in listaDeReservas)
            {

                ReservaDTO reservaDTO = new ReservaDTO
                {
                    ReservaId = elem.ReservaId,
                    UsuarioId = elem.UsuarioId,
                    HabitacionId = elem.HabitacionId,
                    HotelId = elem.HotelId,
                    FechaInicio = elem.FechaInicio,
                    FechaFin = elem.FechaFin,
                    EstadoReservaId = elem.EstadoReservaId
                };

                listaDeReservasDTO.Add(reservaDTO);

            }

            return listaDeReservasDTO;
        }

        public List<ReservaDTO> GetAllReserva()
        {

            List<Reserva> listaDeReservas;
            List<ReservaDTO> listaDeReservasDTO = new List<ReservaDTO>();

            listaDeReservas = _context.Set<Reserva>().ToList();

            foreach (Reserva elem in listaDeReservas)
            {

                ReservaDTO reservaDTO = new ReservaDTO
                {
                    ReservaId = elem.ReservaId,
                    UsuarioId = elem.UsuarioId,
                    HabitacionId = elem.HabitacionId,
                    HotelId = elem.HotelId,
                    FechaInicio = elem.FechaInicio,
                    FechaFin = elem.FechaFin,
                    EstadoReservaId = elem.EstadoReservaId
                };

                listaDeReservasDTO.Add(reservaDTO);

            }

            // mapeo entre Reserva y ReservaDTO

            return listaDeReservasDTO;
        }

        public async Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin)
        {
            var results = await _context.Reserva
                .Where(x => (fechaInicio >= x.FechaInicio && fechaInicio <= x.FechaFin) ||
                    (fechaFin >= x.FechaInicio && fechaFin <= x.FechaFin) ||
                    (fechaInicio <= x.FechaInicio && fechaFin >= x.FechaFin))
                .ToListAsync();

            var reservados = new Dictionary<int, List<int>>();
            foreach (var result in results)
            {
                if (!reservados.ContainsKey(result.HotelId))
                    reservados[result.HotelId] = new List<int>();

                reservados[result.HotelId].Add(result.HabitacionId);
            }

            var reservas = new List<ReservasGroupByHotelIdDTO>();
            foreach (KeyValuePair<int, List<int>> pair in reservados)
                reservas.Add(new ReservasGroupByHotelIdDTO { HotelId = pair.Key, Habitaciones = pair.Value });

            return reservas;
        }
    }
}
