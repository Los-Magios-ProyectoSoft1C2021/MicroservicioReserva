using System.Collections.Generic;

namespace Template.Domain.Entities
{

    public class ReservasGroupByHotelIdDTO
    {
        public int HotelId { get; set; }

        public List<Reserva> Reservas { get; set; }
    }
   
}
