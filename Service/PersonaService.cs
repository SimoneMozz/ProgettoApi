using Microsoft.EntityFrameworkCore;
using ProgettoApi.models;
using ProgettoApi.Data;
using ParkingsDbContext = ProgettoApi.Data.ParkingsDbContext;

namespace ProgettoApi.Service
{
    public class PersonaService : IPersonaService
    {
        private readonly ParkingsDbContext _context;

        public PersonaService(ParkingsDbContext context)
        {
            _context = context;
        }

        public Persona CreatePersona(Persona persona)
        {
            _context.Persone.Add(persona);
            _context.SaveChanges();

            var primaTarga = persona.Targhe?.FirstOrDefault();
            if (primaTarga != null)
            {
                var record = new ParkingRecord
                {
                    TicketId = Guid.NewGuid(),
                    Plate = primaTarga.Valore,
                    EntryTime = DateTime.Now
                };

                _context.ParkingRecords.Add(record);
                _context.SaveChanges();
            }

            return persona;
        }

        public List<Persona> GetAll() => _context.Persone.Include(p => p.Targhe).ToList();

        public Persona? GetById(int id) => _context.Persone.Include(p => p.Targhe).FirstOrDefault(p => p.Id == id);

        public Persona? GetByEmail(string email) => _context.Persone.Include(p => p.Targhe).FirstOrDefault(p => p.Email == email);

        public void Delete(int id)
        {
            var persona = _context.Persone.Include(p => p.Targhe).FirstOrDefault(p => p.Id == id);
            if (persona != null)
            {
                _context.Persone.Remove(persona);
                _context.SaveChanges();
            }
        }

        public void Update(int id, Persona updated)
        {
            var persona = _context.Persone.Include(p => p.Targhe).FirstOrDefault(p => p.Id == id);
            if (persona != null)
            {
                persona.Nome = updated.Nome;
                persona.Cognome = updated.Cognome;
                persona.DataNascita = updated.DataNascita;
                persona.Email = updated.Email;
                persona.Password = updated.Password;

                _context.Targhe.RemoveRange(persona.Targhe);
                persona.Targhe = updated.Targhe;

                _context.SaveChanges();
            }
        }
    }
}
