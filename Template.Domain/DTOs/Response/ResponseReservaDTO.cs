using System;

namespace Template.Domain.Entities
{
    public class ResponseReservaDTO      //esto seria para un post
    {
        public Guid ReservaId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int HabitacionId { get; set; }
        public int HotelId { get; set; }
        public string Hotel { get; set; }
        public string HotelDireccion { get; set; }
        public int HotelEstrellas { get; set; }
        public string HabitacionNombre { get; set; }
        public string HabitacionTipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string EstadoReserva { get; set; }
        public bool ReservaActiva { get; set; }
    }
}
