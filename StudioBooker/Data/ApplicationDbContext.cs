using Microsoft.EntityFrameworkCore;
using StudioBooker.Models;

namespace StudioBooker.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Cabina> Cabinas => Set<Cabina>();
    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<Reserva> Reservas => Set<Reserva>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración Fluent API para las relaciones N:N automáticas de EF Core
        modelBuilder.Entity<Reserva>()
            .HasMany(r => r.Equipos)
            .WithMany(e => e.Reservas)
            .UsingEntity(j => j.ToTable("ReservaEquipos"));

        modelBuilder.Entity<Reserva>()
            .HasMany(r => r.Servicios)
            .WithMany(s => s.Reservas)
            .UsingEntity(j => j.ToTable("ReservaServicios"));
    }
}
