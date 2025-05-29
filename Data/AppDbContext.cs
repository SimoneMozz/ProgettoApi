using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProgettoApi.models
{
    public class ParkingDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ParkingDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


        public DbSet<ParkingRecord> ParkingRecords { get; set; }
        public DbSet<ParkingExit> ParkingExits { get; set; }
        public DbSet<IrregularityRecord> Irregularities { get; set; }
        public DbSet<Persona> Persone { get; set; }
        public DbSet<Targa> Targhe { get; set; }
    }
}