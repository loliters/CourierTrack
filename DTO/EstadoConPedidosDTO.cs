using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.DTO
{
    public class EstadoConPedidosDTO : EstadoDTO
    {
        public List<PedidoDTO> Pedidos { get; set; }
    }
}
