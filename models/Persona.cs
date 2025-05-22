namespace ProgettoApi.models
{
    public class Persona
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public int Id { get; set; }

        public Persona(string nome, string cognome, int id)
        {
            Nome = nome;
            Cognome = cognome;
            Id = id;
        }
    }
}
