using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class ExtensionCI
    {
        [Key]
        public int Id { get; set; }

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

        // Relación con Cliente
        public List<Cliente> Clientes { get; set; }
    }
}
