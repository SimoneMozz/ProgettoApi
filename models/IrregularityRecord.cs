using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoApi.models
{
    [Table("Irregularities")]
    public class IrregularityRecord
    {
        [Key]
        public int Id { get; set; }

        public string Plate { get; set; }

        public DateTime Date { get; set; }

        public int Count { get; set; } = 1;
    }

    public class IrregularityRecordConfiguration : IEntityTypeConfiguration<IrregularityRecord>
    {
        public void Configure(EntityTypeBuilder<IrregularityRecord> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Plate).IsRequired().HasMaxLength(20);
            builder.Property(i => i.Date).IsRequired(); 
            builder.Property(i => i.Count).IsRequired();
        }
    }
}