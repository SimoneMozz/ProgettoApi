using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoApi.models
{
    [Table("Targhe")]
    public class Targa
    {
        [Key]
        public int Id { get; set; }
        public required string Valore { get; set; }

        public int PersonaId { get; set; }
        public required Persona Persona { get; set; }
    }

}
