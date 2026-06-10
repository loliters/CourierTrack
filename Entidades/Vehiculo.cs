using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria")]
        [MaxLength(10, ErrorMessage = "La placa no puede superar los 10 caracteres")]
        public string Placa { get; set; } 

        public int ModeloId { get; set; }
        public int ColorId { get; set; }
        public int AnioVehiculoId { get; set; }
        public int ConductorId { get; set; } 
        //Propiedades de navegación
        public Modelo Modelo { get; set; } 
        public Color Color { get; set; } 
        public AnioVehiculo AnioVehiculo { get; set; } 
        public Conductor Conductor { get; set; } 
    }
}
