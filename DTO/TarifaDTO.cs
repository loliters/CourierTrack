using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.DTO
{
    public class TarifaDTO
    {
        public int Id { get; set; }
        public decimal PrecioKg { get; set; }
        public decimal PrecioKm { get; set; }
        public string NombreVehiculo { get; set; }
    }
}
