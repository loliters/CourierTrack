using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.DTO
{
    public class UbicacionCreaDTO
    {
        [Required]
        [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
        public decimal Latitud { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
        public decimal Longitud { get; set; }
    }
}
