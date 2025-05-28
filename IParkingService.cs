using ProgettoApi.models;

namespace ProgettoApi
{
    public interface IParkingService
    {
        EntryResponse Entry(InputDati input);
        string Exit(InputDati input);
        List<IrregularityRecord> GetAllInfractions();
    }
}
