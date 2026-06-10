using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        //independientes
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<ExtensionCI> ExtensionCI { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<EstadoPago> EstadoPagos { get; set; }
        //
        public DbSet<TipoLicencia> TipoLicencias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<AnioVehiculo> AnioVehiculos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        //dependientes
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Seguimiento> Seguimientos { get; set; }
        public DbSet<HistorialUbicacion> HistorialUbicaciones { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        //pivotes
        public DbSet<UsuarioUbicacion> UsuariosUbicaciones { get; set; }
 
       
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

            // tabla TipoLicencia
            modelBuilder.Entity<TipoLicencia>().HasData(
                new TipoLicencia { Id = 1, Categoria = "M" },
                new TipoLicencia { Id = 2, Categoria = "P" },
                new TipoLicencia { Id = 3, Categoria = "C" }
            );
            modelBuilder.Entity<TipoLicencia>().HasIndex(tl => tl.Categoria).IsUnique();
            modelBuilder.Entity<TipoLicencia>()
                .ToTable(t => t.HasCheckConstraint("CK_TipoLicencia_categoria", "Categoria IN ('M','P','C')"));

            // tabla Marca
            modelBuilder.Entity<Marca>().HasData(
                new Marca { Id = 1, Nombre = "Toyota" },
                new Marca { Id = 2, Nombre = "Ford" },
                new Marca { Id = 3, Nombre = "Chevrolet" },
                new Marca { Id = 4, Nombre = "Mercedes Benz" },
                new Marca { Id = 5, Nombre = "Volvo" },
                new Marca { Id = 6, Nombre = "Hino" },
                new Marca { Id = 7, Nombre = "Nissan" },
                new Marca { Id = 8, Nombre = "Hyundai" },
                new Marca { Id = 9, Nombre = "Kia" },
                new Marca { Id = 10, Nombre = "Mitsubishi" }
            );
            modelBuilder.Entity<Marca>().HasIndex(m => m.Nombre).IsUnique();

            // tabla TipoVehiculo
            modelBuilder.Entity<TipoVehiculo>().HasData(
                new TipoVehiculo { Id = 1, Nombre = "Moto" },
                new TipoVehiculo { Id = 2, Nombre = "Automóvil" },
                new TipoVehiculo { Id = 3, Nombre = "Furgoneta" },
                new TipoVehiculo { Id = 4, Nombre = "Camión pequeño" },
                new TipoVehiculo { Id = 5, Nombre = "Camión grande" },
                new TipoVehiculo { Id = 6, Nombre = "Camioneta" },
                new TipoVehiculo { Id = 7, Nombre = "Minibús" },
                new TipoVehiculo { Id = 8, Nombre = "Bus" },
                new TipoVehiculo { Id = 9, Nombre = "Triciclo" },
                new TipoVehiculo { Id = 10, Nombre = "Bicicleta" }
            );
            modelBuilder.Entity<TipoVehiculo>().HasIndex(tv => tv.Nombre).IsUnique();

            // tabla MetodoPago
            modelBuilder.Entity<MetodoPago>().HasData(
                new MetodoPago { Id = 1, Nombre = "Efectivo" },
                new MetodoPago { Id = 2, Nombre = "Transferencia" }
            );
            modelBuilder.Entity<MetodoPago>().HasIndex(mp => mp.Nombre).IsUnique();

            //tabla Color
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Nombre = "Blanco" },
                new Color { Id = 2, Nombre = "Negro" },
                new Color { Id = 3, Nombre = "Rojo" },
                new Color { Id = 4, Nombre = "Azul" },
                new Color { Id = 5, Nombre = "Gris" },
                new Color { Id = 6, Nombre = "Plateado" },
                new Color { Id = 7, Nombre = "Verde" },
                new Color { Id = 8, Nombre = "Amarillo" },
                new Color { Id = 9, Nombre = "Naranja" },
                new Color { Id = 10, Nombre = "Morado" }
            );
            modelBuilder.Entity<Color>().HasIndex(c => c.Nombre).IsUnique();

            // tabla AnioVehiculo (2013-2026) 
            var anios = new List<AnioVehiculo>();
            for (int i = 2013; i <= 2026; i++)
                anios.Add(new AnioVehiculo { Id = i - 2012, Anio = i });
            modelBuilder.Entity<AnioVehiculo>().HasData(anios);
            modelBuilder.Entity<AnioVehiculo>().HasIndex(av => av.Anio).IsUnique();

            // tabla Ubicacion
            modelBuilder.Entity<Ubicacion>().HasData(
                new Ubicacion { Id = 1, Latitud = -17.389577m, Longitud = -66.157607m },
                new Ubicacion { Id = 2, Latitud = -17.393531m, Longitud = -66.157001m },
                new Ubicacion { Id = 3, Latitud = -17.378240m, Longitud = -66.161950m },
                new Ubicacion { Id = 4, Latitud = -17.401470m, Longitud = -66.155790m },
                new Ubicacion { Id = 5, Latitud = -17.356478m, Longitud = -66.145554m }
            );

            //Dependientes

            //tabla Conductor
            modelBuilder.Entity<Conductor>().HasData(
                new Conductor { Id = 1, NroLicencia = "8765432SC", IdUsuario = 3, IdTipoLicencia = 3 },
                new Conductor { Id = 2, NroLicencia = "5432109CB", IdUsuario = 5, IdTipoLicencia = 2 }
            );
            // Un usuario solo puede ser un conductor
            // Conductor con TipoLicencia
            modelBuilder.Entity<Conductor>()
                .HasIndex(c => c.NroLicencia).IsUnique();
            modelBuilder.Entity<Conductor>()
                .HasIndex(c => c.IdUsuario).IsUnique();
            modelBuilder.Entity<Conductor>()
                .HasOne(c => c.TipoLicencia)
                .WithMany()
                .HasForeignKey(c => c.IdTipoLicencia)
                .OnDelete(DeleteBehavior.Restrict);

            //tabla Modelo
            modelBuilder.Entity<Modelo>().HasData(
                new Modelo { Id = 1, Nombre = "Corolla", IdMarca = 1, IdTipoVehiculo = 2 },
                new Modelo { Id = 2, Nombre = "F-150", IdMarca = 2, IdTipoVehiculo = 4 },
                new Modelo { Id = 3, Nombre = "Sprinter", IdMarca = 4, IdTipoVehiculo = 3 },
                new Modelo { Id = 4, Nombre = "Civic", IdMarca = 1, IdTipoVehiculo = 2 },
                new Modelo { Id = 5, Nombre = "Carga 5000", IdMarca = 5, IdTipoVehiculo = 5 }
            );
            // No puede haber dos modelos con mismo nombre y mar
            // Modelo con Marca
            // Modelo con TipoVehiculo
            modelBuilder.Entity<Modelo>()
                .HasIndex(m => new { m.Nombre, m.IdMarca }).IsUnique();
            modelBuilder.Entity<Modelo>()
                .HasOne(m => m.Marca)
                .WithMany()
                .HasForeignKey(m => m.IdMarca)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Modelo>()
                .HasOne(m => m.TipoVehiculo)
                .WithMany()
                .HasForeignKey(m => m.IdTipoVehiculo)
                .OnDelete(DeleteBehavior.Restrict);

            //tabla Vehiculo
            modelBuilder.Entity<Vehiculo>().HasData(
                new Vehiculo { Id = 1, Placa = "1852-PHD", ModeloId = 1, ColorId = 1, AnioVehiculoId = 6, IdConductor = 1 },
                new Vehiculo { Id = 2, Placa = "4511-GAD", ModeloId = 2, ColorId = 4, AnioVehiculoId = 8, IdConductor = 2 }
            );

            // Vehiculo con Modelo
            // Vehiculo con Color
            // Vehiculo con Año
            // Vehiculo con Conductor
            modelBuilder.Entity<Vehiculo>()
                .HasIndex(v => v.Placa).IsUnique();                
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Modelo)                             
                .WithMany()
                .HasForeignKey(v => v.ModeloId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Color)                              
                .WithMany()
                .HasForeignKey(v => v.ColorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.AnioVehiculo)                       
                .WithMany()
                .HasForeignKey(v => v.AnioVehiculoId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Conductor)                          
                .WithMany()
                .HasForeignKey(v => v.IdConductor)
                .OnDelete(DeleteBehavior.Restrict);                


            //tabla Tarifa
            modelBuilder.Entity<Tarifa>().HasData(
                new Tarifa { Id = 1, PrecioKg = 1.50m, PrecioKm = 2.00m, IdTipoVehiculo = 1 },
                new Tarifa { Id = 2, PrecioKg = 2.00m, PrecioKm = 2.50m, IdTipoVehiculo = 2 },
                new Tarifa { Id = 3, PrecioKg = 2.50m, PrecioKm = 3.00m, IdTipoVehiculo = 3 },
                new Tarifa { Id = 4, PrecioKg = 3.00m, PrecioKm = 4.00m, IdTipoVehiculo = 4 },
                new Tarifa { Id = 5, PrecioKg = 4.00m, PrecioKm = 5.00m, IdTipoVehiculo = 5 }
            );
            // Una tarifa por tipo de vehículo
            // Tarifa con TipoVehiculo
            modelBuilder.Entity<Tarifa>()
                .HasIndex(t => t.IdTipoVehiculo).IsUnique();       
                        modelBuilder.Entity<Tarifa>()
                            .HasOne(t => t.TipoVehiculo)                       
                            .WithMany()
                            .HasForeignKey(t => t.IdTipoVehiculo)
                            .OnDelete(DeleteBehavior.Restrict);
            //relaciones
            //tabla Pago
            modelBuilder.Entity<Pago>()
              .HasOne(p => p.MetodoPago)
              .WithMany()
              .HasForeignKey(p => p.MetodoPagoId)
              .OnDelete(DeleteBehavior.Restrict);

            //tabla Seguimiento
            modelBuilder.Entity<Seguimiento>()
            .HasOne(s => s.Conductor)
            .WithMany()
            .HasForeignKey(s => s.IdConductor)
            .OnDelete(DeleteBehavior.Restrict);
                    modelBuilder.Entity<Seguimiento>()
                        .HasOne(s => s.Vehiculo)
                        .WithMany()
                        .HasForeignKey(s => s.IdVehiculo)
                        .OnDelete(DeleteBehavior.SetNull); 
                    modelBuilder.Entity<Seguimiento>()
                        .HasOne(s => s.Ubicacion)
                        .WithMany()
                        .HasForeignKey(s => s.IdUbicacion)
                        .OnDelete(DeleteBehavior.Restrict);

            // tabla HistorialUbicacion
            modelBuilder.Entity<HistorialUbicacion>()
                .HasOne(h => h.Ubicacion)
                .WithMany()
                .HasForeignKey(h => h.IdUbicacion)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HistorialUbicacion>()
                .HasOne(h => h.Seguimiento)
                .WithMany()
                .HasForeignKey(h => h.IdSeguimiento)
                .OnDelete(DeleteBehavior.Cascade);
            //PIVOTES

            //tabla UsuarioUbicacion
            modelBuilder.Entity<UsuarioUbicacion>().HasData(
                new UsuarioUbicacion { Id = 1, EsPrincipal = true, IdUsuario = 1, IdUbicacion = 1 },
                new UsuarioUbicacion { Id = 2, EsPrincipal = true, IdUsuario = 2, IdUbicacion = 1 },
                new UsuarioUbicacion { Id = 3, EsPrincipal = false, IdUsuario = 2, IdUbicacion = 2 }
            );
            // Un usuario sólo una ubicación principal
            modelBuilder.Entity<UsuarioUbicacion>()
                .HasIndex(uu => new { uu.IdUsuario, uu.EsPrincipal })
                .IsUnique();   
            modelBuilder.Entity<UsuarioUbicacion>()
                .HasOne(uu => uu.Ubicacion)
                .WithMany()
                .HasForeignKey(uu => uu.IdUbicacion)
                .OnDelete(DeleteBehavior.Restrict);

            //Precisiones decimales

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.Property(e => e.Latitud).HasPrecision(9, 6);
                entity.Property(e => e.Longitud).HasPrecision(9, 6);
            });
            modelBuilder.Entity<Tarifa>(entity =>
            {
                entity.Property(e => e.PrecioKg).HasPrecision(10, 2);
                entity.Property(e => e.PrecioKm).HasPrecision(10, 2);
            });
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(e => e.Monto).HasPrecision(10, 2);
            });

        }
    }
}
