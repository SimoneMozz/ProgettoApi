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

        public EntryResponse Entry(InputDati input)
        {
            var ticketId = Guid.NewGuid();

            _activeParkings.Add(new ParkingRecord
            {
                TicketId = ticketId,
                Plate = input.Plate,
                EntryTime = input.Data
            });

            return new EntryResponse
            {
                TicketId = ticketId,
                Messaggio = $"{input.Plate} è entrato nel parcheggio alle {input.Data}."
            };
        }

        public string Exit(InputDati input)
        {
            if (!input.TicketId.HasValue)
            {
                return "Ticket ID mancante.";
            }

            var record = _activeParkings.FirstOrDefault(r => r.TicketId == input.TicketId.Value);

            if (record == null)
            {
                return $"Nessun parcheggio attivo con Ticket ID: {input.TicketId}.";
            }

            TimeSpan duration = input.Data - record.EntryTime;
            _activeParkings.Remove(record);

            if (duration.TotalHours > 2)
            {
                RecordIrregularity(record.Plate);
                return $"{record.Plate} ha lasciato il parcheggio con un'irregolarità. Tempo totale: {duration.TotalMinutes} minuti.";
            }

            return $"{record.Plate} ha lasciato il parcheggio senza irregolarità.";
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
                _irregularities.Add(new IrregularityRecord
                {
                    Plate = plate,
                    Date = today,
                    Count = 1
                });
            }
        }

        public List<IrregularityRecord> ShowIrregularities()
        {
            return _irregularities;
        }

        public int Count()
        {
            return _activeParkings.Count;
        }

        void IParkingService.RecordIrregularity(string plate)
        {
            RecordIrregularity(plate);
        }
    }

}
