using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class Conductor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de licencia es obligatorio")]
        [MaxLength(13, ErrorMessage = "El número de licencia no puede superar los 13 caracteres")]
        public string NroLicencia { get; set; }

        public int UsuarioId { get; set; }

        public int TipoLicenciaId { get; set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
        public TipoLicencia TipoLicencia { get; set; } 
    }
}
