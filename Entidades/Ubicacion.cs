using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace WebAppCourierTrack.Entidades
{
    public class Ubicacion
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        [Column(TypeName = "decimal(9,6)")]//evita que EF use por defecto
        [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
        public decimal Latitud { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,6)")]//evita que EF use por defecto
        [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
        public decimal Longitud { get; set; }

        //relacion 1:N direccion origen
        public List<DireccionOrigen> DireccionesOrigenes { get; set; }

        //relacion 1:N destino
        public List<DireccionDestino> DireccionesDestinos { get; set; }

        public List<UsuarioUbicacion> UsuariosUbicaciones { get; set; }
    }
}
