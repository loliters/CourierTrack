using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class NotificacionDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public bool Leida { get; set; }
    }
}
