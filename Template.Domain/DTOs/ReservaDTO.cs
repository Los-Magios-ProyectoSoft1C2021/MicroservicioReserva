using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.Entities
{
    public class ReservaDTO      //esto seria para un post
    {
        public Guid ReservaId { get; set; }
        public int UsuarioId { get; set; }
        public int HabitacionId { get; set; }
        public int HotelId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EstadoReservaId { get; set; }

    }
}
