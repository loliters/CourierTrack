using System.ComponentModel.DataAnnotations;
namespace WebAppCourierTrack.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string NroDocumento { get; set; }

        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }      // ← nombre completo del usuario
        public string UsuarioCorreo { get; set; }      // ← correo del usuario
        public string TipoClienteNombre { get; set; }  // ← "NATURAL" o "JURÍDICO"
    }
}
