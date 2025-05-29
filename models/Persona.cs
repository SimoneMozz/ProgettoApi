using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProgettoApi.models
{
    
    public class Persona
    {
        [Key]

        [JsonIgnore] // Nasconde l'ID in output
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cognome { get; set; }
        public required DateTime DataNascita { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public required ICollection<Targa> Targhe { get; set; }

    }
}
