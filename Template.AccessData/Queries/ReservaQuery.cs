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

        public async Task<ResponseReservaDTO> GetReservaById(Guid id)
        {
            var reserva = await _context.Reserva
                .Where(x => x.ReservaId == id)
                .OrderBy(x => x.FechaInicio)
                .Select(x => new ResponseReservaDTO
                {
                    ReservaId = x.ReservaId,
                    HotelId = x.HotelId,
                    HabitacionId = x.HabitacionId,
                    UsuarioId = x.UsuarioId,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    EstadoReserva = x.EstadoReserva.Descripcion,
                    ReservaActiva = x.EstadoReservaId == 2
                })
                .FirstOrDefaultAsync();

            return reserva;
        }

        public async Task<List<ResponseReservaDTO>> GetReservaByUserId(int id)
        {
            var listaDeReservas = await _context.Set<Reserva>()
                .Where(x => x.UsuarioId == id)
                .OrderBy(x => x.FechaInicio)
                .Select(x => new ResponseReservaDTO
                {
                    ReservaId = x.ReservaId,
                    UsuarioId = x.UsuarioId,
                    HabitacionId = x.HabitacionId,
                    HotelId = x.HotelId,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    EstadoReserva = x.EstadoReserva.Descripcion,
                    ReservaActiva = x.EstadoReservaId == 2
                })
                .ToListAsync();

            return listaDeReservas;
        }


        public async Task<List<ResponseReservaDTO>> GetReservaByHotelId(int hotelId)
        {
            var listaDeReservas = await _context.Set<Reserva>()
                .Where(x => x.HotelId == hotelId)
                .OrderBy(x => x.FechaInicio)
                .Select(x => new ResponseReservaDTO
                {
                    ReservaId = x.ReservaId,
                    UsuarioId = x.UsuarioId,
                    HabitacionId = x.HabitacionId,
                    HotelId = x.HotelId,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    EstadoReserva = x.EstadoReserva.Descripcion,
                    ReservaActiva = x.EstadoReservaId == 2
                })
                .ToListAsync();

            return listaDeReservas;
        }

        public async Task<List<ResponseReservaDTO>> GetAllReserva()
        {
            var listaDeReservas = await _context.Set<Reserva>()
                .OrderBy(x => x.FechaInicio)
                .Select(x => new ResponseReservaDTO
                {
                    ReservaId = x.ReservaId,
                    UsuarioId = x.UsuarioId,
                    HabitacionId = x.HabitacionId,
                    HotelId = x.HotelId,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    EstadoReserva = x.EstadoReserva.Descripcion,
                    ReservaActiva = x.EstadoReservaId == 2
                })
                .ToListAsync();

            return listaDeReservas;
        }

        public async Task<List<ReservasGroupByHotelIdDTO>> GetAllHabitacionesReservadasEntre(DateTime fechaInicio, DateTime fechaFin)
        {
            var results = await _context.Reserva
                .Where(x => (fechaInicio >= x.FechaInicio && fechaInicio <= x.FechaFin) ||
                    (fechaFin >= x.FechaInicio && fechaFin <= x.FechaFin) ||
                    (fechaInicio <= x.FechaInicio && fechaFin >= x.FechaFin))
                .Where(x => x.EstadoReservaId == 2)
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
