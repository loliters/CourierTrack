using System.ComponentModel.DataAnnotations;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.DTO
{
    public class DetallePedidoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
    }
}
