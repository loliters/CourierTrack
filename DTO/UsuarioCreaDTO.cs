using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class UsuarioCreaDTO
    {
  

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
            ErrorMessage = "Solo se permiten letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
            ErrorMessage = "Solo se permiten letras")]
        public string ApPat { get; set; }

        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
            ErrorMessage = "Solo se permiten letras")]
        public string ApMat { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
        [StringLength(100, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^\d{8}$",
            ErrorMessage = "El teléfono debe tener exactamente 8 dígitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_\-]).{8,}$",
            ErrorMessage = "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial")]
        public string Password { get; set; }
        //rol

        [Required(ErrorMessage = "El rol es obligatorio")]
        public int RolId { get; set; }
        public List<int> UbicacionesIds { get; set; } = new List<int>();


    }
}
