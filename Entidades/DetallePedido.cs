using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class DetallePedido
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [MaxLength(255, ErrorMessage = "La descripción no puede superar los 255 caracteres")]
        public string Descripcion { get; set; }
        //dependencia
        // FK DireccionOrigen
        [Required(ErrorMessage = "La dirección origen es obligatoria")]
        public int DireccionOrigenId { get; set; }
        public DireccionOrigen? DireccionOrigen { get; set; }

        // FK DireccionDestino
        [Required(ErrorMessage = "La dirección destino es obligatoria")]
        public int DireccionDestinoId { get; set; }
        public DireccionDestino? DireccionDestino { get; set; }

        // Relación con pedidos, propiedadde navegacion
        public List<Pedido> Pedidos { get; set; }
    }
}
