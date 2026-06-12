using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class SeguimientoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
    }
}
