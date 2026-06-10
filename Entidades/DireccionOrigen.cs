using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class DireccionOrigen
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La referencia es obligatoria")]
        [StringLength(150, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string Referencia { get; set; }

        // FK Ubicacion
        [Required(ErrorMessage = "La ubicación es obligatoria")]
        public int UbicacionId { get; set; }

        // Propiedad de navegación
        public Ubicacion Ubicacion { get; set; }

        public List<DetallePedido> DetallesPedido { get; set; }
    }
}