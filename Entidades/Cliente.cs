using System.ComponentModel.DataAnnotations;

namespace WebAppCourierTrack.Entidades
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string NroDocumento { get; set; }

        // FK TipoDocumento
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int TipoDocumentoId { get; set; }
        public TipoDocumento TipoDocumento { get; set; }

        // FK ExtensionCI 
        public int? ExtensionCIId { get; set; }
        public ExtensionCI ExtensionCI { get; set; }

        // FK única (1:1 con Usuario)
        [Required(ErrorMessage = "El usuario es obligatorio")]
       
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // FK TipoCliente
        [Required(ErrorMessage = "El tipo de cliente es obligatorio")]
        public int TipoClienteId { get; set; }
        public TipoCliente TipoCliente { get; set; }

        //relacion 1:1 con clientenatural
        public ClienteNatural ClienteNatural { get; set; }
        //relacion 1:1 con clientejuridico
        public ClienteJuridico ClienteJuridico { get; set; }

        // Relación con pedidos
        public List<Pedido> Pedidos { get; set; }
    }
}