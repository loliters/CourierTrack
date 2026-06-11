using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class CalificacionDTO
    {
        
        public int Id { get; set; }

        public string? Comentario { get; set; }

        public int Puntuacion { get; set; }

        public DateTime Fecha { get; set; }

    }
}
