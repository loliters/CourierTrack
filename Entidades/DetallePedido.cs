using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class DetallePedido
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [MaxLength(255, ErrorMessage = "La descripción no puede superar los 255 caracteres")]
        public string Descripcion { get; set; } 
        //dependencia
        public int DireccionOrigenId { get; set; }
        public int DireccionDestinoId { get; set; }
    }
}
