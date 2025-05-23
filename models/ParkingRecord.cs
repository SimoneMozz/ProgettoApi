namespace ProgettoApi.models
{
    public class ParkingRecord
    {
        public Guid TicketId { get; set; }
        public string Plate { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
