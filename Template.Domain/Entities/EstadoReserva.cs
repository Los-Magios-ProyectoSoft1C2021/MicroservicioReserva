using System.Collections.Generic;

namespace Template.Domain.Entities
{
    public class EstadoReserva
    {
        public int EstadoReservaId { get; set; }
        public string Descripcion { get; set; }

        // Reserva contiene una FK de esta tabla, este es el otro extremo de la relación
        public ICollection<Reserva> Reserva { get; set; }

    }
}
