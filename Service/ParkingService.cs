using ProgettoApi.models;

namespace ProgettoApi.Service
{
    public class ParkingService : IParkingService
    {
        public List<ParkingRecord> _activeParkings = new();
        private readonly List<IrregularityRecord> _irregularities = new();

        public EntryResponse Entry(InputDati input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input.Plate))
                    return new EntryResponse { TicketId = Guid.Empty, Messaggio = "Targa non valida." };

                if (input.TicketId == Guid.Empty)
                    return new EntryResponse { TicketId = Guid.Empty, Messaggio = "TicketId non valido." };

                if (_activeParkings.Any(r => r.TicketId == input.TicketId))
                    return new EntryResponse { TicketId = input.TicketId, Messaggio = "Ticket già presente." };

                if (_activeParkings.Any(r => r.Plate == input.Plate))
                    return new EntryResponse { TicketId = input.TicketId, Messaggio = "Questa targa è già nel parcheggio." };

                _activeParkings.Add(new ParkingRecord
                {
                    TicketId = input.TicketId,
                    Plate = input.Plate,
                    EntryTime = input.Data
                });

                return new EntryResponse
                {
                    TicketId = input.TicketId,
                    Messaggio = $"{input.Plate} è entrata nel parcheggio alle {input.Data}."
                };
            }
            catch (Exception ex)
            {
                return new EntryResponse { TicketId = Guid.Empty, Messaggio = $"Errore: {ex.Message}" };
            }
        }

        public string Exit(InputDati input)
        {
            try
            {
                if (input.TicketId == Guid.Empty)
                    return "TicketId non valido.";

                var record = _activeParkings.FirstOrDefault(r => r.TicketId == input.TicketId);

                if (record == null)
                    return "Nessuna auto trovata con questo TicketId.";

                if (input.Data < record.EntryTime)
                {
                    return "Uscita non consentita: è stata inserita una data precedente a quella di ingresso. Il mezzo non può uscire.";
                }

                TimeSpan duration = input.Data - record.EntryTime;
                _activeParkings.Remove(record);

                if (duration.TotalHours > 2)
                {
                    RecordIrregularity(record.Plate);
                    return $"{record.Plate} ha superato il tempo consentito ({duration.TotalMinutes} min). Infrazione registrata.";
                }

                return $"{record.Plate} ha lasciato il parcheggio senza infrazioni.";
            }
            catch (Exception ex)
            {
                return $"Errore durante l'uscita: {ex.Message}";
            }
        }

        public List<IrregularityRecord> GetAllInfractions()
        {
            return _irregularities;
        }

        private void RecordIrregularity(string plate)
        {
            var today = DateTime.Today;
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
    }
}
