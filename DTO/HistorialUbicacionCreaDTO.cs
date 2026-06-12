using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class HistorialUbicacionCreaDTO
    {
        [Required]//(ErrorMessage = "La fecha es obligatoria")
        public DateTime Fecha { get; set; }

        // FK Ubicacion
        [Required(ErrorMessage = "La ubicación es obligatoria")]
        public int UbicacionId { get; set; }

        // FK Seguimiento
        [Required(ErrorMessage = "El seguimiento es obligatorio")]
        public int SeguimientoId { get; set; }
    }
}
