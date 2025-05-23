using ProgettoApi.models;

namespace ProgettoApi
{
    public interface IParkingService
    {
        EntryResponse Entry(InputDati input);
        string Exit(InputDati input);
        void RecordIrregularity(string plate);
        List<IrregularityRecord> ShowIrregularities();
        int Count();
    }

}
