using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class UsuarioUbicacion
    {
        [Key]
        public int Id { get; set; }

        public bool EsPrincipal { get; set; }

        public int IdUsuario { get; set; }
        public int IdUbicacion { get; set; }
        //Propeidades de navegación
        public Ubicacion Ubicacion { get; set; } 

        public Usuario Usuario { get; set; }
    }
}
