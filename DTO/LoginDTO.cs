using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
