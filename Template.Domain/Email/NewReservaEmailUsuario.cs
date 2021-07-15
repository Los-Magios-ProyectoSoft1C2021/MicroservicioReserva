using System;

namespace Template.Domain.Email
{
    public class NewReservaEmailUsuario
    {
        public string Usuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string Hotel { get; set; }
        public string CorreoHotel { get; set; }
        public string TelefonoHotel { get; set; }
        public string DireccionHotel { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Habitacion { get; set; }
        public string TipoHabitacion { get; set; }

        public string GetBodyEmail()
        {
            return
                $"<h1>Hola {Usuario}, ¡se ha registrado su reserva!</h1>" +
                $"<h2>Los detalles de la reserva son:</h2>" +
                $"<p>Correo del usuario: {CorreoUsuario}</p>" +
                $"<p>Usuario: {Usuario}</p>" +
                $"<p>Fecha de entrada: {FechaInicio}</p>" +
                $"<p>Fecha de salida: {FechaFin}</p>" +
                $"<p>Habitación (tipo): {Habitacion} ({TipoHabitacion})</p>" +
                $"<h2>Los detalles de contacto del hotel son:</h2>" +
                $"<p>Correo: {CorreoHotel}</p>" +
                $"<p>Teléfono: {TelefonoHotel}</p>" +
                $"<p>Dirección: {DireccionHotel}";
        }

        public string GetSubjectEmail()
        {
            return "Booking UNAJ - ¡Se ha registrado su reserva!";
        }

    }
}
