using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class ModeloCreaDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; }
        // FK Marca
        [Required(ErrorMessage = "La marca es obligatorio")]
        public int MarcaId { get; set; }
        // FK Vehiculo
        [Required(ErrorMessage = "El tipo de vehículo es obligatorio")]
        public int TipoVehiculoId { get; set; }
    }
}
