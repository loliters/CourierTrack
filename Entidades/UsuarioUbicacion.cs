using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class UsuarioUbicacion
    {
        [Key]
        public int Id { get; set; }

        public bool EsPrincipal { get; set; }

        public int UsuarioId { get; set; }
        public int UbicacionId { get; set; }
        //Propeidades de navegación
        public Ubicacion Ubicacion { get; set; } 

        public Usuario Usuario { get; set; }
    }
}
