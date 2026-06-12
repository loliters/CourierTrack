using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.DTO
{
    public class TarifaCreaDTO
    {
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0", "999999.99", ErrorMessage = "El precio por kg debe ser mayor o igual a 0")]
        public decimal PrecioKg { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0", "999999.99", ErrorMessage = "El precio por km debe ser mayor o igual a 0")]
        public decimal PrecioKm { get; set; }

        // FK TipoVehiculo
        [Required(ErrorMessage = "El tipo de vehículo es obligatorio")]
        public int TipoVehiculoId { get; set; }
    }
}
