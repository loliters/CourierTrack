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
        public DateTime Fecha { get; set; }

        // FK Usuario
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        // Propiedad de navegación
        public Usuario Usuario { get; set; }

        // Relación con pedidos propedad de navegacion
        public List<Pedido> Pedidos { get; set; }
    }
}