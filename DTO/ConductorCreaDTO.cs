using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class ConductorCreaDTO
    {
        [Required(ErrorMessage = "El número de licencia es obligatorio")]
        [MaxLength(13, ErrorMessage = "El número de licencia no puede superar los 13 caracteres")]
        public string NroLicencia { get; set; }
        // FK Usuario
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }
        // FK TipoLicencia
        [Required(ErrorMessage = "El tipo de licencia es obligatorio")]
        public int TipoLicenciaId { get; set; }
    }
}
