using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class MarcaCreaDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; }
    }
}
