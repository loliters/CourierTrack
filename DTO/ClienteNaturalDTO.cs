using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class ClienteNaturalDTO
    {
        
        public int Id { get; set; }

        public DateTime FechaNac { get; set; }
        //mostrar relacion
        public ClienteDTO Cliente { get; set; }
    }
}
