
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.Entidades;
namespace WebAppCourierTrack
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //tablas
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<ExtensionCI> ExtensionCI { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<EstadoPago> EstadoPagos { get; set; }

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



        }
    }
}
