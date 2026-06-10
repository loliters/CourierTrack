
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.Entidades;
namespace WebAppCourierTrack
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //TABLAS INDEPENDIENTES
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<ExtensionCI> ExtensionCI { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<EstadoPago> EstadoPagos { get; set; }
        //TABLAS DEPENDIENTES
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteNatural> ClientesNatural { get; set; }
        public DbSet<ClienteJuridico> ClientesJuridicos { get; set; }
        public DbSet<DireccionOrigen> DireccionesOrigenes { get; set; }

        //DATA SEEDING
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //independientes
            //tabla rol
            modelBuilder.Entity<Rol>().HasData(
                new Rol{Id = 1, Nombre = "Administrador"},
                new Rol{Id = 2, Nombre = "Conductor"},
                new Rol{Id = 3, Nombre = "Cliente"}
            );
            // Roles unicos
            modelBuilder.Entity<Rol>()
                .HasIndex(x => x.Nombre)
                .IsUnique();
            //tabla Genero
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nombre = "Femenino" },
                new Genero { Id = 2, Nombre = "Masculino" },
                new Genero { Id = 3, Nombre = "Otro" }
            );
            // Generos unicos
            modelBuilder.Entity<Genero>()
                .HasIndex(x => x.Nombre)
                .IsUnique();
            //tabla TipoCliente
            modelBuilder.Entity<TipoCliente>().HasData(
                new TipoCliente { Id = 1, Nombre = "Juridico" },
                new TipoCliente { Id = 2, Nombre = "Natural" }
            );
            // tipo de cliente unicos
            modelBuilder.Entity<TipoCliente>()
                .HasIndex(x => x.Nombre)
                .IsUnique();
            //tabla TipoDocumento
            modelBuilder.Entity<TipoDocumento>().HasData(
                new TipoDocumento { Id = 1, Nombre = "Cedula de identidad" },
                new TipoDocumento { Id = 2, Nombre = "NIT" }
            );
            // tipo de cliente unicos
            modelBuilder.Entity<TipoDocumento>()
                .HasIndex(x => x.Nombre)
                .IsUnique();

            //tabla ExtensionCI
            modelBuilder.Entity<ExtensionCI>().HasData(
                new ExtensionCI { Id = 1, Nombre = "LP" },
                new ExtensionCI { Id = 2, Nombre = "CB" },
                new ExtensionCI { Id = 3, Nombre = "SC" },
                new ExtensionCI { Id = 4, Nombre = "OR" },
                new ExtensionCI { Id = 5, Nombre = "PT" },
                new ExtensionCI { Id = 6, Nombre = "CH" },
                new ExtensionCI { Id = 7, Nombre = "TJ" },
                new ExtensionCI { Id = 8, Nombre = "BN" },
                new ExtensionCI { Id = 9, Nombre = "PD" }
            );
            // unicos
            modelBuilder.Entity<ExtensionCI>()
                .HasIndex(x => x.Nombre)
                .IsUnique();
            
            // tabla Estados
            modelBuilder.Entity<Estado>().HasData(
                new Estado { Id = 1, Nombre = "Pendiente" },
                new Estado { Id = 2, Nombre = "Asignado" },
                new Estado { Id = 3, Nombre = "En camino" },
                new Estado { Id = 4, Nombre = "Entregado" },
                new Estado { Id = 5, Nombre = "Cancelado" },
                new Estado { Id = 6, Nombre = "Confirmado" },
                new Estado { Id = 7, Nombre = "En espera" },
                new Estado { Id = 8, Nombre = "Retrasado" },
                new Estado { Id = 9, Nombre = "Devuelto" },
                new Estado { Id = 10, Nombre = "Completado" }
            );
            //tabla EstadoPagos
            modelBuilder.Entity<EstadoPago>().HasData(
                new EstadoPago { Id = 1, Nombre = "Pagado" },
                new EstadoPago { Id = 2, Nombre = "Pendiente" },
                new EstadoPago { Id = 3, Nombre = "Rechazado" }
            );
            // tipo de cliente unicos
            modelBuilder.Entity<EstadoPago>()
                .HasIndex(x => x.Nombre)
                .IsUnique();
            //TABLA DEPENDIENTE USUARIOS
            // Relación Usuario y Rol 1:N
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos de usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Carlos", ApPat = "Mendoza", ApMat = "Rojas", Correo = "admin1@courier.com", Telefono = "71234567", Password = "Admin123*", RolId = 1 },
                new Usuario { Id = 2, Nombre = "Andrea", ApPat = "Lopez", ApMat = "Perez", Correo = "admin2@courier.com", Telefono = "72345678", Password = "Admin456*", RolId = 1 },
                new Usuario { Id = 3, Nombre = "Juan", ApPat = "Flores", ApMat = "Soto", Correo = "juan@courier.com", Telefono = "73456789", Password = "User123*", RolId = 2 },
                new Usuario { Id = 4, Nombre = "Maria", ApPat = "Gutierrez", ApMat = "Mamani", Correo = "maria@courier.com", Telefono = "74567891", Password = "User123*", RolId = 2 },
                new Usuario { Id = 5, Nombre = "Pedro", ApPat = "Choque", ApMat = "Rivera", Correo = "pedro@courier.com", Telefono = "75678912", Password = "User123*", RolId = 2 },
                new Usuario { Id = 6, Nombre = "Lucia", ApPat = "Torrez", ApMat = "Fernandez", Correo = "lucia@courier.com", Telefono = "76789123", Password = "User123*", RolId = 3 },
                new Usuario { Id = 7, Nombre = "Miguel", ApPat = "Ramos", ApMat = "Suarez", Correo = "miguel@courier.com", Telefono = "77891234", Password = "User123*", RolId = 3 },
                new Usuario { Id = 8, Nombre = "Paola", ApPat = "Vargas", ApMat = "Castro", Correo = "paola@courier.com", Telefono = "78912345", Password = "User123*", RolId = 3 },
                new Usuario { Id = 9, Nombre = "Fernando", ApPat = "Salazar", ApMat = "Quispe", Correo = "fernando@courier.com", Telefono = "79123456", Password = "User123*", RolId = 3 },
                new Usuario { Id = 10, Nombre = "Valeria", ApPat = "Mendez", ApMat = "Cruz", Correo = "valeria@courier.com", Telefono = "70234567", Password = "User123*", RolId = 3 }
            );
            //TABLA DEPENDIENTE CLIENTES
            // usuario cliente 1:1 por especializacion
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            //claves foraneas
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.TipoDocumento)
                .WithMany(td => td.Clientes)
                .HasForeignKey(c => c.TipoDocumentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.ExtensionCI)
                .WithMany(e => e.Clientes)
                .HasForeignKey(c => c.ExtensionCIId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.TipoCliente)
                .WithMany(tc => tc.Clientes)
                .HasForeignKey(c => c.TipoClienteId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos de cliente
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, NroDocumento = "1234567", TipoDocumentoId = 1, ExtensionCIId = 1, UsuarioId = 3, TipoClienteId = 1 },
                new Cliente { Id = 2, NroDocumento = "2345678", TipoDocumentoId = 1, ExtensionCIId = 2, UsuarioId = 4, TipoClienteId = 1 },
                new Cliente { Id = 3, NroDocumento = "3456789", TipoDocumentoId = 1, ExtensionCIId = 3, UsuarioId = 5, TipoClienteId = 1 },
                new Cliente { Id = 4, NroDocumento = "4567891", TipoDocumentoId = 1, ExtensionCIId = 4, UsuarioId = 6, TipoClienteId = 1 },
                new Cliente { Id = 5, NroDocumento = "5678912", TipoDocumentoId = 1, ExtensionCIId = 5, UsuarioId = 7, TipoClienteId = 1 },
                new Cliente { Id = 6, NroDocumento = "6789123", TipoDocumentoId = 1, ExtensionCIId = 6, UsuarioId = 8, TipoClienteId = 1 },
                new Cliente { Id = 7, NroDocumento = "7891234", TipoDocumentoId = 1, ExtensionCIId = 7, UsuarioId = 9, TipoClienteId = 1 },

                new Cliente { Id = 8, NroDocumento = "1020304011", TipoDocumentoId = 2, ExtensionCIId = null, UsuarioId = 10, TipoClienteId = 2 },
                new Cliente { Id = 9, NroDocumento = "2040506070", TipoDocumentoId = 2, ExtensionCIId = null, UsuarioId = 11, TipoClienteId = 2 },
                new Cliente { Id = 10, NroDocumento = "3098765432", TipoDocumentoId = 2, ExtensionCIId = null, UsuarioId = 12, TipoClienteId = 2 }
            );
            //TABLA DEPENDIENTE CLIENTENATURAL
            //cliente y cliente natural 1:1
            modelBuilder.Entity<ClienteNatural>()
                .HasOne(cn => cn.Cliente)
                .WithOne(c => c.ClienteNatural)
                .HasForeignKey<ClienteNatural>(cn => cn.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
            //clientenatural y genero N:1
            modelBuilder.Entity<ClienteNatural>()
                .HasOne(cn => cn.Genero)
                .WithMany(g => g.ClientesNatural)
                .HasForeignKey(cn => cn.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, NroDocumento = "1234567", TipoDocumentoId = 1, ExtensionCIId = 1, UsuarioId = 3, TipoClienteId = 1 },
                new Cliente { Id = 2, NroDocumento = "2345678", TipoDocumentoId = 1, ExtensionCIId = 2, UsuarioId = 4, TipoClienteId = 1 },
                new Cliente { Id = 3, NroDocumento = "3456789", TipoDocumentoId = 1, ExtensionCIId = 3, UsuarioId = 5, TipoClienteId = 1 },
                new Cliente { Id = 4, NroDocumento = "4567891", TipoDocumentoId = 1, ExtensionCIId = 4, UsuarioId = 6, TipoClienteId = 1 },
                new Cliente { Id = 5, NroDocumento = "5678912", TipoDocumentoId = 1, ExtensionCIId = 5, UsuarioId = 7, TipoClienteId = 1 }
            );
            //TABLA DEPENDIENTE CLIENTEJURIDICO
            modelBuilder.Entity<ClienteJuridico>()
                .HasOne(cj => cj.Cliente)
                .WithOne(c => c.ClienteJuridico)
                .HasForeignKey<ClienteJuridico>(cj => cj.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos
            modelBuilder.Entity<ClienteJuridico>().HasData(
                new ClienteJuridico { Id = 1, RazonSocial = "Courier Express SRL", Nit = "1020304011", ClienteId = 8 },
                new ClienteJuridico { Id = 2, RazonSocial = "Logistica Andina SA", Nit = "2040506070", ClienteId = 9 },
                new ClienteJuridico { Id = 3, RazonSocial = "Distribuciones Bolivia LTDA", Nit = "3098765432", ClienteId = 10 }
            );
            //TABLA DEPENDIENTE DIRECCIONORIGEN

        }
    }
}
