namespace WebAppCourierTrack.DTO
{
    public class PedidoConEstadosDTO :PedidoDTO
    {
        public List<EstadoDTO> Estados { get; set; }
        public string ClienteNombre { get; set; }     // ← nuevo
        public string ConductorNombre { get; set; }   // ← nuevo
    }
}
