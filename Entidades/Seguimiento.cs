using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class Seguimiento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(255, ErrorMessage = "La observación no puede superar los 255 caracteres")]
        public string Observacion { get; set; } 

        public int PedidoId { get; set; }
        public int ConductorId { get; set; }
        public int VehiculoId { get; set; }
        public int UbicacionId { get; set; }
        // Propiedeades de navegación

        public Conductor Conductor { get; set; } 
        public Vehiculo  Vehiculo { get; set; }   
        public Ubicacion Ubicacion { get; set; } 
    }
}
