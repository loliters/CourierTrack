using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class DireccionDestinoCreaDTO
    {


        [Required(ErrorMessage = "La referencia es obligatoria")]
        [StringLength(150, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string Referencia { get; set; }

        // FK Ubicacion
        [Required(ErrorMessage = "La ubicación es obligatoria")]
        public int UbicacionId { get; set; }
    }
}
