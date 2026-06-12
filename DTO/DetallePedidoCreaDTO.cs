using System.ComponentModel.DataAnnotations;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.DTO
{
    public class DetallePedidoCreaDTO
    {
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [MaxLength(255, ErrorMessage = "La descripción no puede superar los 255 caracteres")]
        public string Descripcion { get; set; }

        // FK DireccionOrigen
        [Required(ErrorMessage = "La dirección origen es obligatoria")]
        public int DireccionOrigenId { get; set; }

        // FK DireccionDestino
        [Required(ErrorMessage = "La dirección destino es obligatoria")]
        public int DireccionDestinoId { get; set; }
    }
}
