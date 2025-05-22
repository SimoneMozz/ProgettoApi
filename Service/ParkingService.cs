using ProgettoApi.models;
using System;

namespace ProgettoApi.Service
{
    public class ParkingService : IParkingService
    {
        private List<ParkingRecord> _activeParkings = new List<ParkingRecord>();
        private List<IrregularityRecord> _irregularities = new List<IrregularityRecord>();

        

        public string Entry(InputDati input)
        {
            try
            {
                // Verifica Targa
                if (string.IsNullOrWhiteSpace(input.Plate))
                {
                   throw new ArgumentException("La targa è nulla o vuota.");
                }

                // Verifica Data
                if (input.Data == null)
                {
                    throw new ArgumentException("La data non è valida o è nulla.");
                }

                // Se tutto va bene
                return "Il veicolo con targa " + input.Plate + " può entrare, orario di ingresso: " + input.Data;
            }
            catch (ArgumentException ex)
            {
                return "Errore di input: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Errore imprevisto: " + ex.Message;
            }
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

        public int Count()
        {
            throw new NotImplementedException();
        }

        void IParkingService.RecordIrregularity(string plate)
        {
            RecordIrregularity(plate);
        }
    }
}
