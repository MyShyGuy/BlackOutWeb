using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Blackout.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Lot> Lots { get; set; } = null!;
        public DbSet<Unit> Units { get; set; } = null!;

        // DbSets: define context for models and corresponding database tables

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder); standardmäßig wird die Basisklasse aufgerufen, um die Standardkonfigurationen zu übernehmen. Wenn Sie jedoch benutzerdefinierte Konfigurationen hinzufügen möchten, können Sie dies tun, ohne die Basisklasse aufzurufen.

            // Konfigurieren der Beziehungen zwischen den Entitäten
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Lots)
                .WithOne(l => l.Product)
                .HasForeignKey(l => l.ProductID)
                .OnDelete(DeleteBehavior.Restrict); // Optional: Festlegen des Löschverhaltens

            modelBuilder.Entity<Unit>()
                .HasMany(u => u.Products)
                .WithOne(p => p.Unit)
                .HasForeignKey(p => p.UnitID)
                .OnDelete(DeleteBehavior.Restrict); // Optional: Festlegen des Löschverhaltens
        }
    }
}