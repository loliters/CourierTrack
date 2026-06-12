namespace WebAppCourierTrack.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public int UsuarioId { get; set; }
    }
}
