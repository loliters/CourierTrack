using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class TipoLicencia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "La categoría debe ser un solo carácter")]
        public string Categoria { get; set; } 
    }
}
