using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class Modelo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; } 

        public int MarcaId { get; set; }
        public int TipoVehiculoId { get; set; }
        //Propiedades de navegación
        public Marca Marca { get; set; } 
        public TipoVehiculo TipoVehiculo { get; set; } 
    }
}
