using ProgettoApi.models;

namespace ProgettoApi.Service
{
    public class ParkingService
    {
        private Dictionary<string, DateTime> _activeParkings = new Dictionary<string, DateTime>();
        private List<IrregularityRecord> _irregularities = new List<IrregularityRecord>();

        public string Entry(InputDati input)
        {
            _activeParkings[input.Plate] = input.Data;
            return $"{input.Plate} è entrato nel parcheggio alle {input.Data}.";
        }

        public string Exit(InputDati input)
        {
            if (!_activeParkings.ContainsKey(input.Plate))
            {
                return $"{input.Plate} non risulta parcheggiato.";
            }

            DateTime entryTime = _activeParkings[input.Plate];
            _activeParkings.Remove(input.Plate);

            TimeSpan duration = input.Data - entryTime;

            if (duration.TotalHours > 2)
            {
                RecordIrregularity(input.Plate);
                return $"{input.Plate} ha lasciato il parcheggio con un'irregolarità. Tempo totale: {duration.TotalMinutes} minuti.";
            }

            return $"{input.Plate} ha lasciato il parcheggio.";
        }

        private void RecordIrregularity(string plate)
        {
            DateTime today = DateTime.Today;
            var record = _irregularities.FirstOrDefault(r => r.Plate == plate && r.Date.Date == today);
            if (record != null)
            {
                record.Count++;
            }
            else
            {
                _irregularities.Add(new IrregularityRecord { Plate = plate, Date = today, Count = 1 });
            }
        }
        public List<IrregularityRecord> ShowIrregularities()
        {
            return _irregularities;
        }
    }
}
