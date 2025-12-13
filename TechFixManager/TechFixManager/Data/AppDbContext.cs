using Microsoft.EntityFrameworkCore;
using TechFixManager.Models;

namespace TechFixManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Reparacion> Reparaciones { get; set; }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
    }
}
