using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.DTOs.Request
{
    public class RequestUpdateReservaDTO
    {
        public Guid ReservaId { get; set; }
        public int EstadoReservaId { get; set; }
    }
}
