using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class ClienteJuridico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La razón social es obligatoria")]
        [StringLength(100, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El NIT es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string Nit { get; set; }

        // FK Cliente
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }

        // Propiedad de navegación
        public Cliente Cliente { get; set; }
    }
}