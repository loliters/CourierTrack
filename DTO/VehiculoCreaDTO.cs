using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class VehiculoCreaDTO
    {
        [Required(ErrorMessage = "La placa es obligatoria")]
        [MaxLength(10, ErrorMessage = "La placa no puede superar los 10 caracteres")]
        public string Placa { get; set; }
        // FK Modelo
        [Required(ErrorMessage = "El modelo es obligatorio")]
        public int ModeloId { get; set; }
        // FK Color
        [Required(ErrorMessage = "El color es obligatorio")]
        public int ColorId { get; set; }
        // FK AnioVehiculo
        [Required(ErrorMessage = "El año del vehículo es obligatorio")]
        public int AnioVehiculoId { get; set; }
        // FK Conductor
        [Required(ErrorMessage = "El conductor es obligatorio")]
        public int ConductorId { get; set; }
    }
}
