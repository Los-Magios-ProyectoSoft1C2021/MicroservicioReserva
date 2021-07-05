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

        public List<ReservaDTO> GetReservaByUserId(int id)
        {

            List<Reserva> listaDeReservas = new List<Reserva>();
            List<ReservaDTO> listaDeReservasDTO = new List<ReservaDTO>();

            listaDeReservas = _context.Set<Reserva>().
                                       Where(x => x.UsuarioId == id).
                                                 ToList();

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

            listaDeReservas = _context.Set<Reserva>().
                                       Where(x => x.HotelId == hotelId).
                                                 ToList();

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

            List<Reserva> listaDeReservas, listaDeValores, listaAux;
            List<ReservasGroupByHotelIdDTO> listaDeReservasPorHotel = new List<ReservasGroupByHotelIdDTO>();
            Dictionary<int, List<Reserva>> diccionarioDeHoteles = new Dictionary<int, List<Reserva>>();

            listaDeReservas = await _context.Set<Reserva>().
                   Where(x => (x.FechaInicio >= fechaInicio && fechaInicio <= x.FechaFin) ||
                              (x.FechaInicio >= fechaFin && fechaFin <= x.FechaFin)).ToListAsync();

            foreach (Reserva elem in listaDeReservas)
            {
                if (diccionarioDeHoteles.TryGetValue(elem.HotelId, out listaDeValores))
                {
                    listaDeValores.Add(elem);
                    diccionarioDeHoteles.Remove(elem.HotelId);
                    diccionarioDeHoteles.Add(elem.HotelId, listaDeValores);
                }
                else
                {
                    listaAux = new List<Reserva>();
                    listaAux.Add(elem);
                    diccionarioDeHoteles.Add(elem.HotelId, listaAux);
                }
            }

            foreach (var elem in diccionarioDeHoteles)
            {
                ReservasGroupByHotelIdDTO elementoListaReservaPorHotel = new ReservasGroupByHotelIdDTO();
                elementoListaReservaPorHotel.HotelId = elem.Key;
                elementoListaReservaPorHotel.Reservas = elem.Value;
                listaDeReservasPorHotel.Add(elementoListaReservaPorHotel);
            }

            return listaDeReservasPorHotel;

        }
    }
}
