using Microsoft.EntityFrameworkCore;
using ProgettoApi.models;

namespace ProgettoApi.Data
{
    public class ParkingsDbContext : DbContext
    {
        public ParkingsDbContext(DbContextOptions<ParkingsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>()
                .HasMany(p => p.Targhe)
                .WithOne(t => t.Persona)
                .HasForeignKey(t => t.PersonaId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Persona> Persone { get; set; }
        public DbSet<Targa> Targhe { get; set; }
        public DbSet<ParkingRecord> ParkingRecords { get; set; }
    }
}

