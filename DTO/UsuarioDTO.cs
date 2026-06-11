using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ApPat { get; set; }

        public string ApMat { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Password { get; set; }

    }
}
