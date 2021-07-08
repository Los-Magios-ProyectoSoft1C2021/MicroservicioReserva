using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.DTOs.Request
{
    public class RequestCreateReservaDTO
    {
        public int HotelId { get; set; }
        public int TipoHabitacionId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
