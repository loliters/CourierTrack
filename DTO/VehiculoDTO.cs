using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class VehiculoDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; }

        // Propiedades adicionales para mostrar datos relacionados
        public int ModeloId { get; set; }
        public string ModeloNombre { get; set; }
        public int ColorId { get; set; }
        public string ColorNombre { get; set; }
        public int AnioVehiculoId { get; set; }
        public int Anio { get; set; }   // valor numérico del año
        public int ConductorId { get; set; }
        public string ConductorNroLicencia { get; set; }
        public string ConductorNombre { get; set; }  // si tienes Usuario en Conductor
    }
}
