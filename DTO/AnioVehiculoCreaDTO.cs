using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class AnioVehiculoCreaDTO
    {
         [Required]
         [Range(2013, 2026, ErrorMessage = "El año debe estar entre 2013 y 2026")]
         public int Anio { get; set; }
    }

}

