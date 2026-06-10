using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class EstadoPago
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "El nombre del estado de pago es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
            ErrorMessage = "Solo se permiten letras")]
        public string Nombre { get; set; }
    }
}
