namespace Template.Domain.DTOs.Request
{
    public class ContactoEmail
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Motivo { get; set; }
        public string Mensaje { get; set; }

        public string GetMensaje()
        {
            return 
                $"<h1>{Motivo}</h1><p>Ha recibido una solicitud de contacto de: {Nombre}</p>" +
                $"<p>Email de contacto: {Correo}</p>" +
                $"<p>{Mensaje}</p>"; 
        }
    }
}
