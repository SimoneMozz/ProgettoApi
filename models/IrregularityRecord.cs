using System.ComponentModel.DataAnnotations;

namespace ProgettoApi.models
{
    public class IrregularityRecord
    {
        [Key]
        public int Id { get; set; }
        public string Plate { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}

