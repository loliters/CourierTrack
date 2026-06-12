using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class NotificacionCreaDTO
    {
        [Required]
        [MaxLength(40, ErrorMessage = "El título no puede superar los 40 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string Mensaje { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }
        public bool Leida { get; set; }

        // FK Usuario
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        // FK Pedido
        [Required(ErrorMessage = "El pedido es obligatorio")]
        public int PedidoId { get; set; }
    }
}
