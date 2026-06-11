using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class PedidoDTO
    {
        
        public int Id { get; set; }
        public bool Fragil { get; set; }
        public decimal PesoKg { get; set; }

        public decimal DistanciaKm { get; set; }
        public decimal CostoTotal { get; set; }

    }
}
