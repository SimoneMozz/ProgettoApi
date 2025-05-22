namespace ProgettoApi.models
{
    public class ParkingRecord
    {
        public Guid Id { get; set; }
        public string Plate { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
