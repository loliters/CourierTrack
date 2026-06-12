using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class PagoCreaDTO
    {
        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [MaxLength(50, ErrorMessage = "El número de transacción no puede superar los 50 caracteres")]
        public string NumeroTransaccion { get; set; }

        [MaxLength(50, ErrorMessage = "El número de cuenta bancaria no puede superar los 50 caracteres")]
        public string CuentaBancaria { get; set; }

        [MaxLength(50, ErrorMessage = "El nombre del banco no puede superar los 50 caracteres")]
        public string Banco { get; set; }

        // FK Pedido: Id del pedido asociado al pago
        [Required(ErrorMessage = "El pedido es obligatorio")]
        public int PedidoId { get; set; }

        // FK MetodoPago: Id del método de pago utilizado
        [Required(ErrorMessage = "El método de pago es obligatorio")]
        public int MetodoPagoId { get; set; }

        // FK EstadoPago: Id del estado actual del pago
        [Required(ErrorMessage = "El estado del pago es obligatorio")]
        public int EstadoPagoId { get; set; }
    }
}
