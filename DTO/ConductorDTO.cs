namespace WebAppCourierTrack.DTO
{
    public class ConductorDTO
    {
        public int Id { get; set; }
        public string NroLicencia { get; set; }
        public int UsuarioId { get; set; }       // ← PascalCase, pero se serializará a camelCase
        public int TipoLicenciaId { get; set; } // ← PascalCase
        public string UsuarioNombre { get; set; } // Opcional
        public string TipoLicenciaCategoria { get; set; } // Opcional
    }
}
