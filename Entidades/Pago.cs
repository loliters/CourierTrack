using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.Entidades
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public int PedidoId { get; set; }
        public int MetodoPagoId { get; set; }
        public int EstadoPagoId { get; set; }

        [MaxLength(50, ErrorMessage = "El número de transacción no puede superar los 50 caracteres")]
        public string NumeroTransaccion { get; set; }

        [MaxLength(50, ErrorMessage = "El número de cuenta bancaria no puede superar los 50 caracteres")]
        public string CuentaBancaria { get; set; }

        [MaxLength(50, ErrorMessage = "El nombre del banco no puede superar los 50 caracteres")]
        public string Banco { get; set; }
        //propiedades de navegacion

        public MetodoPago MetodoPago { get; set; } = null!;
        public Pedido Pedido { get; set; } = null!;
    }
}
