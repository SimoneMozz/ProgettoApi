using ProgettoApi.models;
using System;
using System.Linq;

namespace ProgettoApi.Service
{
    public class ParkingService : IParkingService
    {
        private readonly ParkingDbContext _context;

        public ParkingService(ParkingDbContext context) // iniettato qui
        {
            _context = context;
        }


        public EntryResponse Entry(InputDati input)
        {
            if (string.IsNullOrWhiteSpace(input.Plate))
            {
                return new EntryResponse { TicketId = Guid.Empty, Messaggio = "Targa non valida." };
            }

            if (input.TicketId == Guid.Empty)
            {
                return new EntryResponse { TicketId = Guid.Empty, Messaggio = "TicketId non valido." };
            }

            // Controlla se il ticket esiste già nel DB (ingresso già registrato)
            if (_context.ParkingRecords.Any(r => r.TicketId == input.TicketId))
            {
                return new EntryResponse { TicketId = input.TicketId, Messaggio = "Ticket già presente." };
            }

            // Controlla se la targa è già nel parcheggio (senza uscita)
            bool plateInParking = _context.ParkingRecords.Any(r => r.Plate == input.Plate);
            bool plateNotExited = _context.ParkingExits.Any(e => e.Plate == input.Plate && e.ExitTime == default(DateTime));
            if (plateInParking && !plateNotExited)
            {
                return new EntryResponse { TicketId = input.TicketId, Messaggio = "Questa targa è già nel parcheggio." };
            }

            // Aggiunge nuovo record di ingresso
            var newRecord = new ParkingRecord
            {
                TicketId = input.TicketId,
                Plate = input.Plate,
                EntryTime = input.Data
            };

            _context.ParkingRecords.Add(newRecord);
            _context.SaveChanges();

            return new EntryResponse
            {
                TicketId = input.TicketId,
                Messaggio = $"{input.Plate} è entrata nel parcheggio alle {input.Data}."
            };
        }

        public string Exit(InputDati input)
        {
            if (input.TicketId == Guid.Empty)
            {
                return "TicketId non valido.";
            }

            // Cerca ingresso corrispondente al TicketId
            var parkingRecord = _context.ParkingRecords.FirstOrDefault(r => r.TicketId == input.TicketId);
            if (parkingRecord == null)
            {
                return "Nessuna auto trovata con questo TicketId.";
            }

            // Controllo uscita non può avvenire prima dell'ingresso
            if (input.Data < parkingRecord.EntryTime)
            {
                return "Errore: la data di uscita è precedente alla data di ingresso.";
            }

            // Registra l'uscita nel DB
            var parkingExit = new ParkingExit
            {
                TicketId = input.TicketId,
                Plate = parkingRecord.Plate,
                EntryTime = parkingRecord.EntryTime,
                ExitTime = input.Data
            };

            _context.ParkingExits.Add(parkingExit);
            _context.ParkingRecords.Remove(parkingRecord);
            _context.SaveChanges();

            // Calcola durata e controlla infrazioni
            var duration = input.Data - parkingRecord.EntryTime;

            if (duration.TotalHours > 2)
            {
                RecordIrregularity(parkingRecord.Plate);
                return $"{parkingRecord.Plate} ha superato il tempo consentito ({duration.TotalMinutes} minuti). Infrazione registrata.";
            }

            return $"{parkingRecord.Plate} ha lasciato il parcheggio senza infrazioni.";
        }


        public List<IrregularityRecord> GetAllInfractions()
        {
            return _context.Irregularities.ToList();
        }


        private void RecordIrregularity(string plate)
        {
            var today = DateTime.Today;
            var record = _context.Irregularities.FirstOrDefault(r => r.Plate == plate && r.Date.Date == today);

            if (record != null)
            {
                record.Count++;
                _context.Irregularities.Update(record);
            }
            else
            {
                _context.Irregularities.Add(new IrregularityRecord
                {
                    Plate = plate,
                    Date = today,
                    Count = 1
                });
            }
            _context.SaveChanges();
        }
    }
}




