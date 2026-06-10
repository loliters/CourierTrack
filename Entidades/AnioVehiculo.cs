using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class AnioVehiculo
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        [Range(2013, 2026, ErrorMessage = "El año debe estar entre 2013 y 2026")]
        public int Anio { get; set; }
    }
}
