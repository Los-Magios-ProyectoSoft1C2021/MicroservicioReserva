using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.DTOs.HttpResponse
{
    public class ResponseUsuarioDto
    {
        public int UsuarioId { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public int Dni { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Nacionalidad { get; set; }
        public string Imagen { get; set; }
    }
}
