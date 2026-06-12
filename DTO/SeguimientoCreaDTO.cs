using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class SeguimientoCreaDTO
    {
        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(255, ErrorMessage = "La observación no puede superar los 255 caracteres")]
        public string Observacion { get; set; }

        // FK Pedido
        [Required(ErrorMessage = "El pedido es obligatorio")]
        public int PedidoId { get; set; }

        // FK Conductor
        [Required(ErrorMessage = "El conductor es obligatorio")]
        public int ConductorId { get; set; }

        // FK Vehiculo
        [Required(ErrorMessage = "El vehículo es obligatorio")]
        public int VehiculoId { get; set; }

        // FK Ubicacion
        [Required(ErrorMessage = "La ubicación es obligatoria")]
        public int UbicacionId { get; set; }
    }
}
