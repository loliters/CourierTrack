using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class Calificacion
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "El comentario no puede exceder los 255 caracteres")]
        public string? Comentario { get; set; }

        [Required(ErrorMessage = "La puntuación es obligatoria")]
        [Range(1, 5, ErrorMessage = "La puntuación debe estar entre 1 y 5")]
        public int Puntuacion { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;//cambio


        // Pedido calificado
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        // Cliente que calificó
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Conductor calificado
        public int ConductorId { get; set; }
        public Conductor Conductor { get; set; }
    }
}