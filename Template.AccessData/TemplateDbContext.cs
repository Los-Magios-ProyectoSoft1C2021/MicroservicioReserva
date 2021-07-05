using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.EntitiesConfiguration;

namespace Template.AccessData
{
    // DbContext sirve para indicar qué tablas se generan e indicar el motor de la BBDD
    public class TemplateDbContext : DbContext
    {

        public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
        { }

        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<EstadoReserva> EstadoReserva { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Reserva>(new ReservaConfiguration());
            modelBuilder.ApplyConfiguration<EstadoReserva>(new EstadoReservaConfiguration());

        }



    }
}
