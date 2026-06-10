using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class MetodoPago
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres")]
        public string Nombre { get; set; } 
    }
}
