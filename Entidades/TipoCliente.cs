using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class TipoCliente
    {

        [Key]
        public int Id { get; set; }

        private string nombre;

        [Required(ErrorMessage = "El nombre del tipo de cliente es obligatorio")]
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
