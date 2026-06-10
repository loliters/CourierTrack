using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourierTrack.Entidades
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        private string nombre;

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
            ErrorMessage = "Solo se permiten letras")]
        public string Nombre
        {
            get => nombre;
            set => nombre = value?.Trim().ToUpper();
        }
    }
}