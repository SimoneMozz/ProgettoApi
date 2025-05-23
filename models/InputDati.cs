namespace ProgettoApi.models
{
    public class InputDati
    {
        public required string Plate { get; set; }
        public required DateTime Data { get; set; }
        public Guid? TicketId { get; set; }
    }
}
