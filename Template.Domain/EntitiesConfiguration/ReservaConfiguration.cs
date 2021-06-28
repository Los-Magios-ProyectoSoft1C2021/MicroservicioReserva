using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Entities;

namespace Template.Domain.EntitiesConfiguration
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(c => c.ReservaId);
            builder.Property(c => c.ReservaId)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.UsuarioId)
                   .IsRequired(true);

            builder.Property(c => c.HabitacionId)
                   .IsRequired(true);

            builder.Property(c => c.FechaInicio)
                   .IsRequired(true);

            builder.Property(c => c.FechaFin)
                   .IsRequired(true);

            builder.Property(c => c.EstadoReservaId)
                   .IsRequired(true);

            
        }
    }
}
