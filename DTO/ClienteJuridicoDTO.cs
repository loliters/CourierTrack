using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class ClienteJuridicoDTO
    {
        
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        //mostrar relacion
        public ClienteDTO Cliente { get; set; }
    }
}
