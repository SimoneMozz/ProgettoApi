using ProgettoApi.models;

namespace ProgettoApi.Service
{
    public interface IPersonaService
    {
        Persona CreatePersona(Persona persona);
        List<Persona> GetAll();
        Persona? GetById(int id);
        void Delete(int id);
        void Update(int id, Persona persona);
        Persona? GetByEmail(string email);
    }
}