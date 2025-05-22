using ProgettoApi.models;
using System;

namespace ProgettoApi.Service
{
    public class ParkingService : IParkingService
    {
        private List<ParkingRecord> _activeParkings;
        private List<IrregularityRecord> _irregularities;

        public ParkingService()
        {
            _activeParkings = new List<ParkingRecord>();
            _irregularities = new List<IrregularityRecord>();
        }

        public string Entry(InputDati input)
        {
            _activeParkings.Add(new ParkingRecord { Plate = input.Plate, EntryTime = input.Data });
            return $"{input.Plate} è entrato nel parcheggio alle {input.Data}.";
        }

        public string Exit(InputDati input)
        {
            //da spiegare 
            var record = _activeParkings.FirstOrDefault(r => r.Plate == input.Plate);
            if (record == null)
            {
                return $"{input.Plate} non risulta parcheggiato.";
            }

            TimeSpan duration = input.Data - record.EntryTime;
            _activeParkings.Remove(record);

            if (duration.TotalHours > 2)
            {
                RecordIrregularity(input.Plate);
                return $"{input.Plate} ha lasciato il parcheggio con un'irregolarità. Tempo totale: {duration.TotalMinutes} minuti.";
            }

            return $"{input.Plate} ha lasciato il parcheggio senza irregolarità.";
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

        void IParkingService.RecordIrregularity(string plate)
        {
            RecordIrregularity(plate);
        }
    }
}
