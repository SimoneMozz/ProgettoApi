using Microsoft.EntityFrameworkCore;
using ProgettoApi.models;

public class ParkingDbContext : DbContext
{
    public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ParkingRecordConfiguration());
        modelBuilder.ApplyConfiguration(new ParkingExitConfiguration());
        modelBuilder.ApplyConfiguration(new IrregularityRecordConfiguration());
    }

    public DbSet<ParkingRecord> ParkingRecords { get; set; }
    public DbSet<ParkingExit> ParkingExits { get; set; }
    public DbSet<IrregularityRecord> Irregularities { get; set; }
}
