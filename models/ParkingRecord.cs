using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoApi.models
{
    [Table("ParkingRecords")]
    public class ParkingRecord
    {
        [Key]
        public Guid TicketId { get; set; }
        public string Plate { get; set; }
        public DateTime EntryTime { get; set; }
    }

    public class ParkingRecordConfiguration : IEntityTypeConfiguration<ParkingRecord>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ParkingRecord> builder)
        {
            builder.HasKey(pr => pr.TicketId);
            builder.Property(pr => pr.Plate).IsRequired().HasMaxLength(20);
            builder.Property(pr => pr.EntryTime).IsRequired();
        }
    }
}
