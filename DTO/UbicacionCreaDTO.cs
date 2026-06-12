using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.DTO
{
    public class UbicacionCreaDTO
    {
        [Required]
        [Column(TypeName = "decimal(9,6)")]//evita que EF use por defecto
        [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
        public decimal Latitud { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,6)")]//evita que EF use por defecto
        [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
        public decimal Longitud { get; set; }
    }
}
