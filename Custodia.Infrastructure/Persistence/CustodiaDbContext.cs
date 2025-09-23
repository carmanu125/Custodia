using Custodia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Custodia.Infrastructure.Persistence
{
    public class CustodiaDbContext : DbContext
    {
        public CustodiaDbContext(DbContextOptions<CustodiaDbContext> options) : base(options) { }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vigencia> Vigencias { get; set; }
        public DbSet<Anaquel> Anaqueles { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<Dependencia> Dependencias { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Folio> Folios { get; set; }
        public DbSet<Trazabilidad> Trazabilidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Índice único contrato + número de folio
            modelBuilder.Entity<Folio>()
                .HasIndex(f => new { f.ContratoId, f.Numero })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
