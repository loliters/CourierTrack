using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class Notificacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "El título no puede superar los 40 caracteres")]
        public string Titulo { get; set; } 

        [Required]
        public string Mensaje { get; set; } 

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public bool Leida { get; set; }
        //Dependencia
        public int UsuarioId { get; set; }
        public int PedidoId { get; set; }

    }
}
