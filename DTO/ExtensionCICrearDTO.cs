using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.DTO
{
    public class ExtensionCICrearDTO
    {


        private string nombre;

        [Required(ErrorMessage = "La extensión de CI es obligatoria")]
        [StringLength(2, ErrorMessage = "La extensión debe tener como máximo 2 caracteres")]
        [RegularExpression(@"^[A-Za-z]{1,2}$",
            ErrorMessage = "Solo se permiten letras y un máximo de 2 caracteres")]
        public string Nombre
        {
            get => nombre;
            set => nombre = value?.Trim().ToUpper();
        }

    }
}
