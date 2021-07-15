using System;

namespace Template.Domain.Email
{
    public class UpdateReservaEmailHotel
    {
        public string Usuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public string Hotel { get; set; }
        public string CorreoHotel { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string EstadoReserva { get; set; }

        public string GetBodyEmail()
        {
            return
                $"<h1>Hola {Hotel}, ¡se ha registrado una nueva reserva!</h1>" +
                $"<h2>Los detalles de la reserva son: </h2>" +
                $"<p>Usuario: {Usuario}</p>" +
                $"<p>Correo del usuario: {CorreoUsuario}</p>" +
                $"<p>Teléfono del usuario: {TelefonoUsuario}</p>" +
                $"<p>Fecha de entrada: {FechaInicio}</p>" +
                $"<p>Fecha de salida: {FechaFin}</p>" +
                $"<h2>El nuevo estado de la reserva es: {EstadoReserva}</h2>";
        }

        public string GetSubjectEmail()
        {
            return $"Booking UNAJ - ¡Se ha actualizado una reserva!";
        }
    }
}
