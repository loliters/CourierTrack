using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class ClienteJuridicoCreaDTO
    {

        [Required(ErrorMessage = "La razón social es obligatoria")]
        [StringLength(100, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El NIT es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string Nit { get; set; }

        // FK Cliente 1:1
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }
    }
}
