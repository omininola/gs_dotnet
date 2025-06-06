using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Drone> Drones { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
    }
}
