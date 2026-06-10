using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class ClienteNatural
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNac { get; set; }

        // FK Genero
        [Required(ErrorMessage = "El género es obligatorio")]
        public int GeneroId { get; set; }

        public Genero Genero { get; set; }

        // FK Cliente unico
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}