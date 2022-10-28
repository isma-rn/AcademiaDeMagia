using Academia.MapeoDatos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Academia.MapeoDatos
{
    public class BaseDatosContext : DbContext
    {
        public BaseDatosContext(DbContextOptions<BaseDatosContext> options)
            : base(options)
        {

        }

        public DbSet<Afinidad> Afinidad { get; set; }
        public DbSet<Grimorio> Grimonio { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Solicitud> Solicitud { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Afinidad>().ToTable("Afinidad");
            modelBuilder.Entity<Grimorio>().ToTable("Grimorio");
            modelBuilder.Entity<Estudiante>().ToTable("Estudiante");
            modelBuilder.Entity<Solicitud>().ToTable("Solicitud");
        }
    }
}