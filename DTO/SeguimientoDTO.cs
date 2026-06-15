using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class SeguimientoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public int PedidoId { get; set; }
        public string PedidoInfo { get; set; }           // ej. "ID: 1 - Cliente: Juan"
        public int ConductorId { get; set; }
        public string ConductorNombre { get; set; }      // nombre del conductor
        public int VehiculoId { get; set; }
        public string VehiculoPlaca { get; set; }
        public int UbicacionId { get; set; }
        public string UbicacionCoords { get; set; }      // "lat, lng"
    }
}
