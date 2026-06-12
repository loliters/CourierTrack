using System.ComponentModel.DataAnnotations;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.DTO
{
    public class PedidoCreaDTO
    {

        [Required(ErrorMessage = "Debe indicar si el paquete es frágil")]
        public bool Fragil { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(0.01, 99999999.99, ErrorMessage = "El peso debe ser mayor a 0")]
        public decimal PesoKg { get; set; }

        [Required(ErrorMessage = "La distancia es obligatoria")]
        [Range(0.01, 99999999.99, ErrorMessage = "La distancia debe ser mayor a 0")]
        public decimal DistanciaKm { get; set; }

        [Required(ErrorMessage = "El costo total es obligatorio")]
        [Range(0.01, 99999999.99, ErrorMessage = "El costo total debe ser mayor a 0")]
        public decimal CostoTotal { get; set; }

        // FK TipoVehiculo
        [Required(ErrorMessage = "El tipo de vehículo es obligatorio")]
        public int TipoVehiculoId { get; set; }

        // FK Cliente
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int? ClienteId { get; set; }

        // FK Calificacion (opcional)
        public int? CalificacionId { get; set; }

        // FK DetallePedido
        [Required(ErrorMessage = "El detalle del pedido es obligatorio")]
        public int DetallePedidoId { get; set; }

        //Relacion N:M
        public List<int> EstadoIds { get; set; }
    }
}
