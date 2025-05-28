using System;
using System.ComponentModel.DataAnnotations;

namespace ProgettoApi.models
{
    public class ParkingExit
    {
        [Key]
        public Guid TicketId { get; set; }
        public string Plate { get; set; } = null!;
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
    }
}

