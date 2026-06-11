using System.ComponentModel.DataAnnotations;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.DTO
{
    public class TipoDocumentoCreaDTO
    {
  

        private string nombre;

        [Required(ErrorMessage = "El nombre del tipo de documento es obligatorio")]
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
