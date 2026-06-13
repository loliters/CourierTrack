using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        // TABLAS INDEPENDIENTES
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<ExtensionCI> ExtensionCI { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<EstadoPago> EstadoPagos { get; set; }

        // DBSET REPO - INDEPENDIENTES
        public DbSet<TipoLicencia> TipoLicencias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<AnioVehiculo> AnioVehiculos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }

        // TABLAS DEPENDIENTES
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteNatural> ClientesNatural { get; set; }
        public DbSet<ClienteJuridico> ClientesJuridicos { get; set; }
        public DbSet<DireccionOrigen> DireccionesOrigenes { get; set; }
        public DbSet<DireccionDestino> DireccionesDestinos { get; set; }
        public DbSet<Calificacion> Calificacions { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        // DBSET REPO - DEPENDIENTES
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Seguimiento> Seguimientos { get; set; }
        public DbSet<HistorialUbicacion> HistorialUbicaciones { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        // PIVOTES
        public DbSet<UsuarioUbicacion> UsuariosUbicaciones { get; set; }
        public DbSet<EstadoPedido> EstadosPedidos { get; set; }

        // DATA SEEDING
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // INDEPENDIENTES

            // tabla rol
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Administrador" },
                new Rol { Id = 2, Nombre = "Conductor" },
                new Rol { Id = 3, Nombre = "Cliente" }
            );
            modelBuilder.Entity<Rol>().HasIndex(x => x.Nombre).IsUnique();

            // tabla Genero
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nombre = "Femenino" },
                new Genero { Id = 2, Nombre = "Masculino" },
                new Genero { Id = 3, Nombre = "Otro" }
            );
            modelBuilder.Entity<Genero>().HasIndex(x => x.Nombre).IsUnique();

            // tabla TipoCliente
            modelBuilder.Entity<TipoCliente>().HasData(
                new TipoCliente { Id = 1, Nombre = "Juridico" },
                new TipoCliente { Id = 2, Nombre = "Natural" }
            );
            modelBuilder.Entity<TipoCliente>().HasIndex(x => x.Nombre).IsUnique();

            // tabla TipoDocumento
            modelBuilder.Entity<TipoDocumento>().HasData(
                new TipoDocumento { Id = 1, Nombre = "Cedula de identidad" },
                new TipoDocumento { Id = 2, Nombre = "NIT" }
            );
            modelBuilder.Entity<TipoDocumento>().HasIndex(x => x.Nombre).IsUnique();

            // tabla ExtensionCI
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
            modelBuilder.Entity<ExtensionCI>().HasIndex(x => x.Nombre).IsUnique();

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

            // tabla EstadoPagos
            modelBuilder.Entity<EstadoPago>().HasData(
                new EstadoPago { Id = 1, Nombre = "Pagado" },
                new EstadoPago { Id = 2, Nombre = "Pendiente" },
                new EstadoPago { Id = 3, Nombre = "Rechazado" }
            );
            modelBuilder.Entity<EstadoPago>().HasIndex(x => x.Nombre).IsUnique();

            // tabla TipoLicencia
            modelBuilder.Entity<TipoLicencia>().HasData(
                new TipoLicencia { Id = 1, Categoria = "M" },
                new TipoLicencia { Id = 2, Categoria = "P" },
                new TipoLicencia { Id = 3, Categoria = "C" }
            );
            modelBuilder.Entity<TipoLicencia>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_TipoLicencia_categoria",
                    "\"Categoria\" IN ('M','P','C')"));

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

            // tabla Color
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

            // DEPENDIENTES

            // tabla Conductor
            modelBuilder.Entity<Conductor>().HasData(
                new Conductor { Id = 1, NroLicencia = "8765432SC", UsuarioId = 3, TipoLicenciaId = 3 },
                new Conductor { Id = 2, NroLicencia = "5432109CB", UsuarioId = 5, TipoLicenciaId = 2 }
            );
            modelBuilder.Entity<Conductor>().HasIndex(c => c.NroLicencia).IsUnique();
            modelBuilder.Entity<Conductor>().HasIndex(c => c.UsuarioId).IsUnique();
            modelBuilder.Entity<Conductor>()
                .HasOne(c => c.TipoLicencia)
                .WithMany()
                .HasForeignKey(c => c.TipoLicenciaId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla Modelo
            modelBuilder.Entity<Modelo>().HasData(
                new Modelo { Id = 1, Nombre = "Corolla", MarcaId = 1, TipoVehiculoId = 2 },
                new Modelo { Id = 2, Nombre = "F-150", MarcaId = 2, TipoVehiculoId = 4 },
                new Modelo { Id = 3, Nombre = "Sprinter", MarcaId = 4, TipoVehiculoId = 3 },
                new Modelo { Id = 4, Nombre = "Civic", MarcaId = 1, TipoVehiculoId = 2 },
                new Modelo { Id = 5, Nombre = "Carga 5000", MarcaId = 5, TipoVehiculoId = 5 }
            );
            modelBuilder.Entity<Modelo>()
                .HasIndex(m => new { m.Nombre, m.MarcaId }).IsUnique();
            modelBuilder.Entity<Modelo>()
                .HasOne(m => m.Marca)
                .WithMany()
                .HasForeignKey(m => m.MarcaId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Modelo>()
                .HasOne(m => m.TipoVehiculo)
                .WithMany()
                .HasForeignKey(m => m.TipoVehiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla Vehiculo
            modelBuilder.Entity<Vehiculo>().HasData(
                new Vehiculo { Id = 1, Placa = "1852-PHD", ModeloId = 1, ColorId = 1, AnioVehiculoId = 6, ConductorId = 1 },
                new Vehiculo { Id = 2, Placa = "4511-GAD", ModeloId = 2, ColorId = 4, AnioVehiculoId = 8, ConductorId = 2 }
            );
            modelBuilder.Entity<Vehiculo>().HasIndex(v => v.Placa).IsUnique();
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
                .HasForeignKey(v => v.ConductorId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla Tarifa
            modelBuilder.Entity<Tarifa>().HasData(
                new Tarifa { Id = 1, PrecioKg = 1.50m, PrecioKm = 2.00m, TipoVehiculoId = 1 },
                new Tarifa { Id = 2, PrecioKg = 2.00m, PrecioKm = 2.50m, TipoVehiculoId = 2 },
                new Tarifa { Id = 3, PrecioKg = 2.50m, PrecioKm = 3.00m, TipoVehiculoId = 3 },
                new Tarifa { Id = 4, PrecioKg = 3.00m, PrecioKm = 4.00m, TipoVehiculoId = 4 },
                new Tarifa { Id = 5, PrecioKg = 4.00m, PrecioKm = 5.00m, TipoVehiculoId = 5 }
            );
            modelBuilder.Entity<Tarifa>().HasIndex(t => t.TipoVehiculoId).IsUnique();
            modelBuilder.Entity<Tarifa>()
                .HasOne(t => t.TipoVehiculo)
                .WithMany()
                .HasForeignKey(t => t.TipoVehiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla Pago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.MetodoPago)
                .WithMany()
                .HasForeignKey(p => p.MetodoPagoId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla Seguimiento
            modelBuilder.Entity<Seguimiento>()
                .HasOne(s => s.Conductor)
                .WithMany()
                .HasForeignKey(s => s.ConductorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Seguimiento>()
                .HasOne(s => s.Vehiculo)
                .WithMany()
                .HasForeignKey(s => s.VehiculoId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Seguimiento>()
                .HasOne(s => s.Ubicacion)
                .WithMany()
                .HasForeignKey(s => s.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla HistorialUbicacion
            modelBuilder.Entity<HistorialUbicacion>()
                .HasOne(h => h.Ubicacion)
                .WithMany()
                .HasForeignKey(h => h.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HistorialUbicacion>()
                .HasOne(h => h.Seguimiento)
                .WithMany()
                .HasForeignKey(h => h.SeguimientoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usuario y Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

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
                new Usuario { Id = 10, Nombre = "Valeria", ApPat = "Mendez", ApMat = "Cruz", Correo = "valeria@courier.com", Telefono = "70234567", Password = "User123*", RolId = 3 },
                new Usuario { Id = 11, Nombre = "Empresa", ApPat = "Uno", ApMat = "SRL", Correo = "empresa1@courier.com", Telefono = "70111111", Password = "User123*", RolId = 3 },
                new Usuario { Id = 12, Nombre = "Empresa", ApPat = "Dos", ApMat = "SA", Correo = "empresa2@courier.com", Telefono = "70222222", Password = "User123*", RolId = 3 }
            );

            // Cliente (base)
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
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

            // ClienteNatural
            modelBuilder.Entity<ClienteNatural>()
                .HasOne(cn => cn.Cliente)
                .WithOne(c => c.ClienteNatural)
                .HasForeignKey<ClienteNatural>(cn => cn.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ClienteNatural>()
                .HasOne(cn => cn.Genero)
                .WithMany(g => g.ClientesNatural)
                .HasForeignKey(cn => cn.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteNatural>().HasData(
                new ClienteNatural { Id = 1, FechaNac = new DateTime(1995, 3, 15, 0, 0, 0, DateTimeKind.Utc), GeneroId = 1, ClienteId = 1 },
                new ClienteNatural { Id = 2, FechaNac = new DateTime(1998, 7, 22, 0, 0, 0, DateTimeKind.Utc), GeneroId = 2, ClienteId = 2 },
                new ClienteNatural { Id = 3, FechaNac = new DateTime(1992, 11, 8, 0, 0, 0, DateTimeKind.Utc), GeneroId = 1, ClienteId = 3 },
                new ClienteNatural { Id = 4, FechaNac = new DateTime(2000, 1, 30, 0, 0, 0, DateTimeKind.Utc), GeneroId = 2, ClienteId = 4 },
                new ClienteNatural { Id = 5, FechaNac = new DateTime(1997, 9, 14, 0, 0, 0, DateTimeKind.Utc), GeneroId = 1, ClienteId = 5 }
            );

            // ClienteJuridico
            modelBuilder.Entity<ClienteJuridico>()
                .HasOne(cj => cj.Cliente)
                .WithOne(c => c.ClienteJuridico)
                .HasForeignKey<ClienteJuridico>(cj => cj.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteJuridico>().HasData(
                new ClienteJuridico { Id = 1, RazonSocial = "Courier Express SRL", Nit = "1020304011", ClienteId = 8 },
                new ClienteJuridico { Id = 2, RazonSocial = "Logistica Andina SA", Nit = "2040506070", ClienteId = 9 },
                new ClienteJuridico { Id = 3, RazonSocial = "Distribuciones Bolivia LTDA", Nit = "3098765432", ClienteId = 10 }
            );

            // DireccionOrigen
            modelBuilder.Entity<DireccionOrigen>()
                .HasOne(d => d.Ubicacion)
                .WithMany(u => u.DireccionesOrigenes)
                .HasForeignKey(d => d.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DireccionOrigen>().HasData(
                new DireccionOrigen { Id = 1, Referencia = "Av. Arce, frente al Multicine", UbicacionId = 1 },
                new DireccionOrigen { Id = 2, Referencia = "Zona Sopocachi, cerca de Plaza Abaroa", UbicacionId = 2 },
                new DireccionOrigen { Id = 3, Referencia = "Av. Busch, frente al mercado", UbicacionId = 3 },
                new DireccionOrigen { Id = 4, Referencia = "Calle Comercio, al lado del banco", UbicacionId = 4 },
                new DireccionOrigen { Id = 5, Referencia = "Zona Sur, cerca del Megacenter", UbicacionId = 5 },
                new DireccionOrigen { Id = 6, Referencia = "Av. Ballivián, esquina semáforo", UbicacionId = 1 },
                new DireccionOrigen { Id = 7, Referencia = "Terminal de buses, ingreso principal", UbicacionId = 2 },
                new DireccionOrigen { Id = 8, Referencia = "Av. Camacho, edificio empresarial", UbicacionId = 3 },
                new DireccionOrigen { Id = 9, Referencia = "Zona Miraflores, frente al estadio", UbicacionId = 4 },
                new DireccionOrigen { Id = 10, Referencia = "Calle 21 de Calacoto, esquina farmacia", UbicacionId = 5 }
            );

            // DireccionDestino
            modelBuilder.Entity<DireccionDestino>()
                .HasOne(d => d.Ubicacion)
                .WithMany(u => u.DireccionesDestinos)
                .HasForeignKey(d => d.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DireccionDestino>().HasData(
                new DireccionDestino { Id = 1, Referencia = "Av. 6 de Agosto, frente al supermercado", UbicacionId = 1 },
                new DireccionDestino { Id = 2, Referencia = "Zona San Miguel, cerca de la plaza", UbicacionId = 2 },
                new DireccionDestino { Id = 3, Referencia = "Av. Montenegro, edificio empresarial", UbicacionId = 3 },
                new DireccionDestino { Id = 4, Referencia = "Calle Mercado, al lado de la farmacia", UbicacionId = 4 },
                new DireccionDestino { Id = 5, Referencia = "Zona Obrajes, frente a la estación", UbicacionId = 5 }
            );

            // Calificacion
            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Calificaciones)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Conductor)
                .WithMany(c => c.Calificaciones)
                .HasForeignKey(c => c.ConductorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Pedido)
                .WithOne(p => p.Calificacion)
                .HasForeignKey<Calificacion>(c => c.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>().HasData(
                new Calificacion { Id = 1, Comentario = "Excelente servicio", Puntuacion = 5, Fecha = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc), PedidoId = 1, ClienteId = 1, ConductorId = 1 },
                new Calificacion { Id = 2, Comentario = "Muy buen servicio", Puntuacion = 4, Fecha = new DateTime(2025, 1, 12, 0, 0, 0, DateTimeKind.Utc), PedidoId = 2, ClienteId = 2, ConductorId = 2 },
                new Calificacion { Id = 3, Comentario = "Servicio aceptable", Puntuacion = 3, Fecha = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc), PedidoId = 3, ClienteId = 3, ConductorId = 3 },
                new Calificacion { Id = 4, Comentario = "Buen trato", Puntuacion = 4, Fecha = new DateTime(2025, 1, 18, 0, 0, 0, DateTimeKind.Utc), PedidoId = 4, ClienteId = 4, ConductorId = 4 },
                new Calificacion { Id = 5, Comentario = "Entrega rápida", Puntuacion = 5, Fecha = new DateTime(2025, 1, 20, 0, 0, 0, DateTimeKind.Utc), PedidoId = 5, ClienteId = 5, ConductorId = 5 },
                new Calificacion { Id = 6, Comentario = "Podría mejorar", Puntuacion = 3, Fecha = new DateTime(2025, 1, 22, 0, 0, 0, DateTimeKind.Utc), PedidoId = 6, ClienteId = 6, ConductorId = 6 },
                new Calificacion { Id = 7, Comentario = "Muy satisfecho", Puntuacion = 5, Fecha = new DateTime(2025, 1, 24, 0, 0, 0, DateTimeKind.Utc), PedidoId = 7, ClienteId = 7, ConductorId = 7 },
                new Calificacion { Id = 8, Comentario = "Servicio regular", Puntuacion = 2, Fecha = new DateTime(2025, 1, 26, 0, 0, 0, DateTimeKind.Utc), PedidoId = 8, ClienteId = 8, ConductorId = 8 },
                new Calificacion { Id = 9, Comentario = "Buen conductor", Puntuacion = 4, Fecha = new DateTime(2025, 1, 28, 0, 0, 0, DateTimeKind.Utc), PedidoId = 9, ClienteId = 9, ConductorId = 9 },
                new Calificacion { Id = 10, Comentario = "Excelente atención", Puntuacion = 5, Fecha = new DateTime(2025, 1, 30, 0, 0, 0, DateTimeKind.Utc), PedidoId = 10, ClienteId = 10, ConductorId = 10 }
            );

            // DetallePedido 
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.DireccionOrigen)
                .WithMany(d => d.DetallesPedido)
                .HasForeignKey(dp => dp.DireccionOrigenId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.DireccionDestino)
                .WithMany(d => d.DetallesPedido)
                .HasForeignKey(dp => dp.DireccionDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla DetallePedido corregida (Establece DateTime.UtcNow o fechas válidas para evitar el '-infinity')
            modelBuilder.Entity<DetallePedido>().HasData(
                new DetallePedido { Id = 1, Fecha = DateTime.UtcNow, Descripcion = "Documentos importantes", DireccionOrigenId = 1, DireccionDestinoId = 1 },
                new DetallePedido { Id = 2, Fecha = DateTime.UtcNow, Descripcion = "Caja mediana", DireccionOrigenId = 2, DireccionDestinoId = 2 },
                new DetallePedido { Id = 3, Fecha = DateTime.UtcNow, Descripcion = "Paquete pequeño frágil", DireccionOrigenId = 3, DireccionDestinoId = 3 },
                new DetallePedido { Id = 4, Fecha = DateTime.UtcNow, Descripcion = "Electrodoméstico", DireccionOrigenId = 4, DireccionDestinoId = 4 },
                new DetallePedido { Id = 5, Fecha = DateTime.UtcNow, Descripcion = "Equipos de oficina", DireccionOrigenId = 5, DireccionDestinoId = 5 },
                new DetallePedido { Id = 6, Fecha = DateTime.UtcNow, Descripcion = "Repuestos mecánicos", DireccionOrigenId = 6, DireccionDestinoId = 1 },
                new DetallePedido { Id = 7, Fecha = DateTime.UtcNow, Descripcion = "Medicamentos", DireccionOrigenId = 7, DireccionDestinoId = 2 },
                new DetallePedido { Id = 8, Fecha = DateTime.UtcNow, Descripcion = "Muebles pequeños", DireccionOrigenId = 8, DireccionDestinoId = 3 },
                new DetallePedido { Id = 9, Fecha = DateTime.UtcNow, Descripcion = "Papelería", DireccionOrigenId = 9, DireccionDestinoId = 4 },
                new DetallePedido { Id = 10, Fecha = DateTime.UtcNow, Descripcion = "Material electrónico", DireccionOrigenId = 10, DireccionDestinoId = 5 }
            );

            // Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.TipoVehiculo)
                .WithMany(tv => tv.Pedidos)
                .HasForeignKey(p => p.TipoVehiculoId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Conductor)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ConductorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.DetallePedido)
                .WithMany(dp => dp.Pedidos)
                .HasForeignKey(p => p.DetallePedidoId)
                .OnDelete(DeleteBehavior.Restrict);

            // tabla Pedido 
            modelBuilder.Entity<Pedido>().HasData(
                new Pedido { Id = 1, Fragil = true, PesoKg = 2.50m, DistanciaKm = 8.50m, CostoTotal = 45.00m, TipoVehiculoId = 1, ClienteId = 1, DetallePedidoId = 1},
                new Pedido { Id = 2, Fragil = false, PesoKg = 5.20m, DistanciaKm = 12.00m, CostoTotal = 70.00m, TipoVehiculoId = 2, ClienteId = 2, DetallePedidoId = 2},
                new Pedido { Id = 3, Fragil = true, PesoKg = 1.80m, DistanciaKm = 4.50m, CostoTotal = 30.00m, TipoVehiculoId = 1, ClienteId = 3, DetallePedidoId = 3},
                new Pedido { Id = 4, Fragil = false, PesoKg = 8.00m, DistanciaKm = 20.00m, CostoTotal = 120.00m, TipoVehiculoId = 3, ClienteId = 4, DetallePedidoId = 4},
                new Pedido { Id = 5, Fragil = true, PesoKg = 3.40m, DistanciaKm = 15.50m, CostoTotal = 95.00m, TipoVehiculoId = 2, ClienteId = 5, DetallePedidoId = 5},
                new Pedido { Id = 6, Fragil = false, PesoKg = 6.80m, DistanciaKm = 18.00m, CostoTotal = 110.00m, TipoVehiculoId = 3, ClienteId = 1, DetallePedidoId = 6},
                new Pedido { Id = 7, Fragil = true, PesoKg = 0.90m, DistanciaKm = 3.50m, CostoTotal = 22.00m, TipoVehiculoId = 1, ClienteId = 2, DetallePedidoId = 7},
                new Pedido { Id = 8, Fragil = false, PesoKg = 12.00m, DistanciaKm = 28.00m, CostoTotal = 175.00m, TipoVehiculoId = 3, ClienteId = 3, DetallePedidoId = 8},
                new Pedido { Id = 9, Fragil = true, PesoKg = 4.60m, DistanciaKm = 9.20m, CostoTotal = 58.00m, TipoVehiculoId = 2, ClienteId = 4, DetallePedidoId = 9},
                new Pedido { Id = 10, Fragil = false, PesoKg = 7.30m, DistanciaKm = 14.00m, CostoTotal = 88.00m, TipoVehiculoId = 2, ClienteId = 5, DetallePedidoId = 10}
            );
            // tabla Pago
            modelBuilder.Entity<Pago>().HasData(
                new Pago { Id = 1, Monto = 45.00m, Fecha = DateTime.UtcNow.AddMinutes(-50), MetodoPagoId = 1, EstadoPagoId = 1, PedidoId = 1, Banco = "No Aplica", CuentaBancaria = "No Aplica", NumeroTransaccion = "No Aplica" },
                new Pago { Id = 2, Monto = 70.00m, Fecha = DateTime.UtcNow.AddMinutes(-45), MetodoPagoId = 2, EstadoPagoId = 1, PedidoId = 2, Banco = "Banco Unión", CuentaBancaria = "10000045218", NumeroTransaccion = "TXN-85214" },
                new Pago { Id = 3, Monto = 30.00m, Fecha = DateTime.UtcNow.AddMinutes(-40), MetodoPagoId = 1, EstadoPagoId = 1, PedidoId = 3, Banco = "No Aplica", CuentaBancaria = "No Aplica", NumeroTransaccion = "No Aplica" },
                new Pago { Id = 4, Monto = 120.00m, Fecha = DateTime.UtcNow.AddMinutes(-35), MetodoPagoId = 2, EstadoPagoId = 1, PedidoId = 4, Banco = "Banco Nacional de Bolivia", CuentaBancaria = "201-514789", NumeroTransaccion = "TXN-96325" },
                new Pago { Id = 5, Monto = 95.00m, Fecha = DateTime.UtcNow.AddMinutes(-30), MetodoPagoId = 1, EstadoPagoId = 2, PedidoId = 5, Banco = "No Aplica", CuentaBancaria = "No Aplica", NumeroTransaccion = "No Aplica" },
                new Pago { Id = 6, Monto = 110.00m, Fecha = DateTime.UtcNow.AddMinutes(-25), MetodoPagoId = 2, EstadoPagoId = 1, PedidoId = 6, Banco = "Banco Mercantil Santa Cruz", CuentaBancaria = "402-369852", NumeroTransaccion = "TXN-14785" },
                new Pago { Id = 7, Monto = 22.00m, Fecha = DateTime.UtcNow.AddMinutes(-20), MetodoPagoId = 1, EstadoPagoId = 1, PedidoId = 7, Banco = "No Aplica", CuentaBancaria = "No Aplica", NumeroTransaccion = "No Aplica" },
                new Pago { Id = 8, Monto = 175.00m, Fecha = DateTime.UtcNow.AddMinutes(-15), MetodoPagoId = 2, EstadoPagoId = 3, PedidoId = 8, Banco = "Banco de Crédito BCP", CuentaBancaria = "305-784125", NumeroTransaccion = "TXN-36985" },
                new Pago { Id = 9, Monto = 58.00m, Fecha = DateTime.UtcNow.AddMinutes(-10), MetodoPagoId = 1, EstadoPagoId = 1, PedidoId = 9, Banco = "No Aplica", CuentaBancaria = "No Aplica", NumeroTransaccion = "No Aplica" },
                new Pago { Id = 10, Monto = 88.00m, Fecha = DateTime.UtcNow.AddMinutes(-5), MetodoPagoId = 2, EstadoPagoId = 2, PedidoId = 10, Banco = "Banco Económico", CuentaBancaria = "501-963258", NumeroTransaccion = "TXN-25814" }
            );
            // NOTIFICACION
            modelBuilder.Entity<Notificacion>().HasData(
                new Notificacion
                {
                    Id = 1,
                    Titulo = "Pedido Registrado",
                    Mensaje = "Su pedido fue registrado correctamente.",
                    Fecha = new DateTime(2025, 1, 10, 8, 5, 0, DateTimeKind.Utc),
                    Leida = true,
                    UsuarioId = 6,
                    PedidoId = 1
                },
                new Notificacion
                {
                    Id = 2,
                    Titulo = "Pedido Asignado",
                    Mensaje = "Se asignó un conductor a su pedido.",
                    Fecha = new DateTime(2025, 1, 11, 9, 5, 0, DateTimeKind.Utc),
                    Leida = false,
                    UsuarioId = 7,
                    PedidoId = 2
                },
                new Notificacion
                {
                    Id = 3,
                    Titulo = "En Camino",
                    Mensaje = "Su pedido está siendo transportado.",
                    Fecha = new DateTime(2025, 1, 12, 10, 5, 0, DateTimeKind.Utc),
                    Leida = false,
                    UsuarioId = 8,
                    PedidoId = 3
                },
                new Notificacion
                {
                    Id = 4,
                    Titulo = "Pedido Entregado",
                    Mensaje = "Su pedido fue entregado exitosamente.",
                    Fecha = new DateTime(2025, 1, 13, 11, 5, 0, DateTimeKind.Utc),
                    Leida = true,
                    UsuarioId = 9,
                    PedidoId = 4
                },
                new Notificacion
                {
                    Id = 5,
                    Titulo = "Pago Pendiente",
                    Mensaje = "Tiene un pago pendiente por realizar.",
                    Fecha = new DateTime(2025, 1, 14, 12, 5, 0, DateTimeKind.Utc),
                    Leida = false,
                    UsuarioId = 10,
                    PedidoId = 5
                },
                new Notificacion
                {
                    Id = 6,
                    Titulo = "Pago Confirmado",
                    Mensaje = "Su pago fue confirmado correctamente.",
                    Fecha = new DateTime(2025, 1, 15, 13, 5, 0, DateTimeKind.Utc),
                    Leida = true,
                    UsuarioId = 11,
                    PedidoId = 6
                },
                new Notificacion
                {
                    Id = 7,
                    Titulo = "Actualización",
                    Mensaje = "El conductor está próximo a llegar.",
                    Fecha = new DateTime(2025, 1, 16, 14, 5, 0, DateTimeKind.Utc),
                    Leida = false,
                    UsuarioId = 12,
                    PedidoId = 7
                },
                new Notificacion
                {
                    Id = 8,
                    Titulo = "Retraso",
                    Mensaje = "Se registró un retraso en la entrega.",
                    Fecha = new DateTime(2025, 1, 17, 15, 5, 0, DateTimeKind.Utc),
                    Leida = false,
                    UsuarioId = 6,
                    PedidoId = 8
                },
                new Notificacion
                {
                    Id = 9,
                    Titulo = "Entrega Exitosa",
                    Mensaje = "Gracias por utilizar CourierTrack.",
                    Fecha = new DateTime(2025, 1, 18, 16, 5, 0, DateTimeKind.Utc),
                    Leida = true,
                    UsuarioId = 7,
                    PedidoId = 9
                },
                new Notificacion
                {
                    Id = 10,
                    Titulo = "Pedido Finalizado",
                    Mensaje = "El pedido fue completado satisfactoriamente.",
                    Fecha = new DateTime(2025, 1, 19, 17, 5, 0, DateTimeKind.Utc),
                    Leida = true,
                    UsuarioId = 8,
                    PedidoId = 10
                }
            );
            // SEGUIMIENTO
            modelBuilder.Entity<Seguimiento>().HasData(
                new Seguimiento
                {
                    Id = 1,
                    Fecha = new DateTime(2025, 1, 10, 8, 0, 0, DateTimeKind.Utc),
                    Observacion = "Pedido registrado",
                    PedidoId = 1,
                    ConductorId = 1,
                    VehiculoId = 1,
                    UbicacionId = 1
                },
                new Seguimiento
                {
                    Id = 2,
                    Fecha = new DateTime(2025, 1, 11, 9, 0, 0, DateTimeKind.Utc),
                    Observacion = "Pedido asignado",
                    PedidoId = 2,
                    ConductorId = 2,
                    VehiculoId = 2,
                    UbicacionId = 2
                },
                new Seguimiento
                {
                    Id = 3,
                    Fecha = new DateTime(2025, 1, 12, 10, 0, 0, DateTimeKind.Utc),
                    Observacion = "En camino",
                    PedidoId = 3,
                    ConductorId = 1,
                    VehiculoId = 1,
                    UbicacionId = 3
                },
                new Seguimiento
                {
                    Id = 4,
                    Fecha = new DateTime(2025, 1, 13, 11, 0, 0, DateTimeKind.Utc),
                    Observacion = "Entregado",
                    PedidoId = 4,
                    ConductorId = 2,
                    VehiculoId = 2,
                    UbicacionId = 4
                },
                new Seguimiento
                {
                    Id = 5,
                    Fecha = new DateTime(2025, 1, 14, 12, 0, 0, DateTimeKind.Utc),
                    Observacion = "Confirmado",
                    PedidoId = 5,
                    ConductorId = 1,
                    VehiculoId = 1,
                    UbicacionId = 5
                },
                new Seguimiento
                {
                    Id = 6,
                    Fecha = new DateTime(2025, 1, 15, 13, 0, 0, DateTimeKind.Utc),
                    Observacion = "Pendiente de entrega",
                    PedidoId = 6,
                    ConductorId = 2,
                    VehiculoId = 2,
                    UbicacionId = 1
                },
                new Seguimiento
                {
                    Id = 7,
                    Fecha = new DateTime(2025, 1, 16, 14, 0, 0, DateTimeKind.Utc),
                    Observacion = "Retraso por tráfico",
                    PedidoId = 7,
                    ConductorId = 1,
                    VehiculoId = 1,
                    UbicacionId = 2
                },
                new Seguimiento
                {
                    Id = 8,
                    Fecha = new DateTime(2025, 1, 17, 15, 0, 0, DateTimeKind.Utc),
                    Observacion = "En reparto",
                    PedidoId = 8,
                    ConductorId = 2,
                    VehiculoId = 2,
                    UbicacionId = 3
                },
                new Seguimiento
                {
                    Id = 9,
                    Fecha = new DateTime(2025, 1, 18, 16, 0, 0, DateTimeKind.Utc),
                    Observacion = "Llegó al destino",
                    PedidoId = 9,
                    ConductorId = 1,
                    VehiculoId = 1,
                    UbicacionId = 4
                },
                new Seguimiento
                {
                    Id = 10,
                    Fecha = new DateTime(2025, 1, 19, 17, 0, 0, DateTimeKind.Utc),
                    Observacion = "Proceso finalizado",
                    PedidoId = 10,
                    ConductorId = 2,
                    VehiculoId = 2,
                    UbicacionId = 5
                }
            );
            //Pivote
            //
            modelBuilder.Entity<UsuarioUbicacion>().HasData(
                new UsuarioUbicacion { Id = 1, UsuarioId = 3, UbicacionId = 1, EsPrincipal = true },
                new UsuarioUbicacion { Id = 2, UsuarioId = 4, UbicacionId = 2, EsPrincipal = true },
                new UsuarioUbicacion { Id = 3, UsuarioId = 5, UbicacionId = 3, EsPrincipal = true },
                new UsuarioUbicacion { Id = 4, UsuarioId = 6, UbicacionId = 4, EsPrincipal = true },
                new UsuarioUbicacion { Id = 5, UsuarioId = 7, UbicacionId = 5, EsPrincipal = true },

                new UsuarioUbicacion { Id = 6, UsuarioId = 8, UbicacionId = 1, EsPrincipal = true },
                new UsuarioUbicacion { Id = 7, UsuarioId = 9, UbicacionId = 2, EsPrincipal = true },
                new UsuarioUbicacion { Id = 8, UsuarioId = 10, UbicacionId = 3, EsPrincipal = true },
                new UsuarioUbicacion { Id = 9, UsuarioId = 11, UbicacionId = 4, EsPrincipal = true },
                new UsuarioUbicacion { Id = 10, UsuarioId = 12, UbicacionId = 5, EsPrincipal = true }
            );
            modelBuilder.Entity<EstadoPedido>().HasData(
                new EstadoPedido
                {
                    Id = 1,
                    HoraCambio = new DateTime(2025, 1, 10, 8, 0, 0, DateTimeKind.Utc),
                    PedidoId = 1,
                    EstadoId = 4 
                },
                new EstadoPedido
                {
                    Id = 2,
                    HoraCambio = new DateTime(2025, 1, 11, 9, 0, 0, DateTimeKind.Utc),
                    PedidoId = 2,
                    EstadoId = 3 
                },
                new EstadoPedido
                {
                    Id = 3,
                    HoraCambio = new DateTime(2025, 1, 12, 10, 0, 0, DateTimeKind.Utc),
                    PedidoId = 3,
                    EstadoId = 4 
                },
                new EstadoPedido
                {
                    Id = 4,
                    HoraCambio = new DateTime(2025, 1, 13, 11, 0, 0, DateTimeKind.Utc),
                    PedidoId = 4,
                    EstadoId = 2 
                },
                new EstadoPedido
                {
                    Id = 5,
                    HoraCambio = new DateTime(2025, 1, 14, 12, 0, 0, DateTimeKind.Utc),
                    PedidoId = 5,
                    EstadoId = 1 
                },
                new EstadoPedido
                {
                    Id = 6,
                    HoraCambio = new DateTime(2025, 1, 15, 13, 0, 0, DateTimeKind.Utc),
                    PedidoId = 6,
                    EstadoId = 4 
                },
                new EstadoPedido
                {
                    Id = 7,
                    HoraCambio = new DateTime(2025, 1, 16, 14, 0, 0, DateTimeKind.Utc),
                    PedidoId = 7,
                    EstadoId = 3 
                },
                new EstadoPedido
                {
                    Id = 8,
                    HoraCambio = new DateTime(2025, 1, 17, 15, 0, 0, DateTimeKind.Utc),
                    PedidoId = 8,
                    EstadoId = 5 
                },
                new EstadoPedido
                {
                    Id = 9,
                    HoraCambio = new DateTime(2025, 1, 18, 16, 0, 0, DateTimeKind.Utc),
                    PedidoId = 9,
                    EstadoId = 4 
                },
                new EstadoPedido
                {
                    Id = 10,
                    HoraCambio = new DateTime(2025, 1, 19, 17, 0, 0, DateTimeKind.Utc),
                    PedidoId = 10,
                    EstadoId = 10 
                }
            );
        }
    }
}