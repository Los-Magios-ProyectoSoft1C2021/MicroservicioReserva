using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Entities;

namespace Template.Domain.EntitiesConfiguration
{
    public class EstadoReservaConfiguration : IEntityTypeConfiguration<EstadoReserva>
    {
        public void Configure(EntityTypeBuilder<EstadoReserva> builder)
        {
            builder.HasKey(c => c.EstadoReservaId);
            builder.Property(c => c.EstadoReservaId)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Descripcion)
                   .IsRequired(true)
                   .HasMaxLength(50);

            builder.HasData(
               
               // No existe estado Dispobible porque:
               //  - Si la habitacion nunca se reservo no figura en la tabla Reserva
               //  - Si se reservo pero se cancelo figura como cancelada
               //  - Si se reservo y ya esta disponible queda registrada como Finalizado
               
               new EstadoReserva { EstadoReservaId = 2, Descripcion = "Reservado" }, 
               new EstadoReserva { EstadoReservaId = 3, Descripcion = "Cancelado" },
               new EstadoReserva { EstadoReservaId = 4, Descripcion = "CanceladoAdmin" },
               new EstadoReserva { EstadoReservaId = 5, Descripcion = "Finalizado"}

               );


        }
    }
}
