using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class TipoLicenciaCreaDTO
    {
        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "La categoría debe ser un solo carácter")]
        public string Categoria { get; set; }
    }
}
