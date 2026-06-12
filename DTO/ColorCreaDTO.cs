using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class ColorCreaDTO
    {
        [Required]
        [MaxLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres")]
        public string Nombre { get; set; }
    }
}
