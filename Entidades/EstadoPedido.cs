using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class EstadoPedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La hora de cambio es obligatoria")]
        public DateTime HoraCambio { get; set; }

        // FK Pedido
        public int PedidoId { get; set; }

        // FK Estado
        public int EstadoId { get; set; }

        //propiedades de navegacion
        public Pedido Pedido { get; set; }
        public Estado Estado { get; set; }
    }
}