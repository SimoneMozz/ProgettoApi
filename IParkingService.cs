using ProgettoApi.models;

namespace ProgettoApi
{
    public interface IParkingService
    {
        public string Exit(InputDati input);
        public string Entry(InputDati input);
        public int Count();
        public void RecordIrregularity(string plate);
        public List<IrregularityRecord> ShowIrregularities();
    }
}
