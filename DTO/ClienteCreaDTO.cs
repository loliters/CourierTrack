using System.ComponentModel.DataAnnotations;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.DTO
{
    public class ClienteCreaDTO
    {


        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string NroDocumento { get; set; }

        // FK TipoDocumento
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int TipoDocumentoId { get; set; }

        // FK ExtensionCI 
        public int? ExtensionCIId { get; set; }

        // FK única (1:1 con Usuario)
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        // FK TipoCliente
        [Required(ErrorMessage = "El tipo de cliente es obligatorio")]
        public int TipoClienteId { get; set; }
    }
}
