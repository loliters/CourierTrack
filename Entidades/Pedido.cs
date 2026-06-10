using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.Entidades
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

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
        public TipoVehiculo TipoVehiculo { get; set; }

        // FK Cliente
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // FK Calificacion (opcional)
        public int? CalificacionId { get; set; }
        public Calificacion? Calificacion { get; set; }

        // FK DetallePedido
        [Required(ErrorMessage = "El detalle del pedido es obligatorio")]
        public int DetallePedidoId { get; set; }
        public DetallePedido DetallePedido { get; set; }

        //nvegacion N:M, adición para el uso de tabla pivote
        public List<EstadoPedido> EstadosPedidos { get; set; }
    }
}