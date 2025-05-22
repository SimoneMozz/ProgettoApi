namespace ProgettoApi.models
{
    public class InputDatiTest
    {
        public required string Plate { get; set; }
        public required DateTime Data { get; set; }
    }

    public class OutputDati
    {
        public required string PlateO { get; set; }
        public required DateTime DataO { get; set; }
        public int contatore = 0;
    }
}