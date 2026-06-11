using System.ComponentModel.DataAnnotations;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.DTO
{
    public class ClienteNaturalCreaDTO
    {

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNac { get; set; }

        // FK Genero
        [Required(ErrorMessage = "El género es obligatorio")]
        public int GeneroId { get; set; }

        // FK Cliente unico 1:1
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }

    }
}
