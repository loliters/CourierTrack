using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class HistorialUbicacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public int UbicacionId { get; set; }
        public int SeguimientoId { get; set; }
        //Propiedades de navegación
        public Ubicacion Ubicacion { get; set; }
        public Seguimiento Seguimiento { get; set; } 
    }
}
