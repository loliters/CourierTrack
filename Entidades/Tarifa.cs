using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.Entidades
{
    public class Tarifa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0", "999999.99",ErrorMessage = "El precio por kg debe ser mayor o igual a 0")]
        public decimal PrecioKg { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0", "999999.99",ErrorMessage = "El precio por km debe ser mayor o igual a 0")]
        public decimal PrecioKm { get; set; }

        [Required]
        public int TipoVehiculoId { get; set; }

        // Propiedades de navegación
        public TipoVehiculo TipoVehiculo { get; set; } 
    }
}
