using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoApi.models
{
    [Table("ParkingExits")]
    public class ParkingExit
    {
        [Key]
        public int Id { get; set; }

        public Guid TicketId { get; set; }
        public string Plate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
    }

    public class ParkingExitConfiguration : IEntityTypeConfiguration<ParkingExit>
    {
        public void Configure(EntityTypeBuilder<ParkingExit> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TicketId).IsRequired();
            builder.Property(e => e.Plate).IsRequired().HasMaxLength(20);
            builder.Property(e => e.EntryTime).IsRequired();
            builder.Property(e => e.ExitTime).IsRequired();
        }
    }
}


