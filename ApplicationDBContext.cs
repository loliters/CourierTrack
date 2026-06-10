using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
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

        //DBSET REPO - INDEPENDIENTES
        public DbSet<TipoLicencia> TipoLicencias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<AnioVehiculo> AnioVehiculos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }

        //TABLAS DEPENDIENTES
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteNatural> ClientesNatural { get; set; }
        public DbSet<ClienteJuridico> ClientesJuridicos { get; set; }
        public DbSet<DireccionOrigen> DireccionesOrigenes { get; set; }
        public DbSet<DireccionDestino> DireccionesDestinos {  get; set; }
        public DbSet<Calificacion> Calificacions { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        //DBSET REPO - DEPENDIENTES
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Seguimiento> Seguimientos { get; set; }
        public DbSet<HistorialUbicacion> HistorialUbicaciones { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        //PIVOTES
        public DbSet<UsuarioUbicacion> UsuariosUbicaciones { get; set; }
        //
        public DbSet<EstadoPedido> EstadosPedidos { get; set; }


        //DATA SEEDING
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //independientes
            //tabla rol
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Administrador" },
                new Rol { Id = 2, Nombre = "Conductor" },
                new Rol { Id = 3, Nombre = "Cliente" }
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
            // ClienteNatural y Genero N:1
            modelBuilder.Entity<ClienteNatural>()
                .HasOne(cn => cn.Genero)
                .WithMany(g => g.ClientesNatural)
                .HasForeignKey(cn => cn.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

            // Datos ClienteNatural
            modelBuilder.Entity<ClienteNatural>().HasData(
                new ClienteNatural { Id = 1, FechaNac = new DateTime(1995, 3, 15), GeneroId = 1, ClienteId = 1 },
                new ClienteNatural { Id = 2, FechaNac = new DateTime(1998, 7, 22), GeneroId = 2, ClienteId = 2 },
                new ClienteNatural { Id = 3, FechaNac = new DateTime(1992, 11, 8), GeneroId = 1, ClienteId = 3 },
                new ClienteNatural { Id = 4, FechaNac = new DateTime(2000, 1, 30), GeneroId = 2, ClienteId = 4 },
                new ClienteNatural { Id = 5, FechaNac = new DateTime(1997, 9, 14), GeneroId = 1, ClienteId = 5 }
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
            modelBuilder.Entity<DireccionOrigen>()
                .HasOne(d => d.Ubicacion)
                .WithMany(u => u.DireccionesOrigenes)
                .HasForeignKey(d => d.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos semilla
            modelBuilder.Entity<DireccionOrigen>().HasData(
                new DireccionOrigen { Id = 1, Referencia = "Av. Arce, frente al Multicine", UbicacionId = 1 },
                new DireccionOrigen { Id = 2, Referencia = "Zona Sopocachi, cerca de Plaza Abaroa", UbicacionId = 2 },
                new DireccionOrigen { Id = 3, Referencia = "Av. Busch, frente al mercado", UbicacionId = 3 },
                new DireccionOrigen { Id = 4, Referencia = "Calle Comercio, al lado del banco", UbicacionId = 4 },
                new DireccionOrigen { Id = 5, Referencia = "Zona Sur, cerca del Megacenter", UbicacionId = 5 },
                new DireccionOrigen { Id = 6, Referencia = "Av. Ballivián, esquina semáforo", UbicacionId = 6 },
                new DireccionOrigen { Id = 7, Referencia = "Terminal de buses, ingreso principal", UbicacionId = 7 },
                new DireccionOrigen { Id = 8, Referencia = "Av. Camacho, edificio empresarial", UbicacionId = 8 },
                new DireccionOrigen { Id = 9, Referencia = "Zona Miraflores, frente al estadio", UbicacionId = 9 },
                new DireccionOrigen { Id = 10, Referencia = "Calle 21 de Calacoto, esquina farmacia", UbicacionId = 10 }
            );
            //TABLA DEPENDIENTE DIRECCION DESTINO
            // DireccionDestino y Ubicacion N:1
            modelBuilder.Entity<DireccionDestino>()
                .HasOne(d => d.Ubicacion)
                .WithMany(u => u.DireccionesDestinos)
                .HasForeignKey(d => d.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos
            modelBuilder.Entity<DireccionDestino>().HasData(
                new DireccionDestino { Id = 1, Referencia = "Av. 6 de Agosto, frente al supermercado", UbicacionId = 1 },
                new DireccionDestino { Id = 2, Referencia = "Zona San Miguel, cerca de la plaza", UbicacionId = 2 },
                new DireccionDestino { Id = 3, Referencia = "Av. Montenegro, edificio empresarial", UbicacionId = 3 },
                new DireccionDestino { Id = 4, Referencia = "Calle Mercado, al lado de la farmacia", UbicacionId = 4 },
                new DireccionDestino { Id = 5, Referencia = "Zona Obrajes, frente a la estación", UbicacionId = 5 }
            );
            //TABLA DEPENDIENTE CALIFICACION
            // Calificacion y Usuario N:1
            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Calificaciones)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos semilla
            modelBuilder.Entity<Calificacion>().HasData(
                new Calificacion { Id = 1, Comentario = "Excelente servicio", Puntuacion = 5, Fecha = new DateTime(2025, 1, 10), UsuarioId = 3 },
                new Calificacion { Id = 2, Comentario = "Entrega rápida y segura", Puntuacion = 4, Fecha = new DateTime(2025, 1, 12), UsuarioId = 4 },
                new Calificacion { Id = 3, Comentario = "Buen trato del conductor", Puntuacion = 5, Fecha = new DateTime(2025, 1, 15), UsuarioId = 5 },
                new Calificacion { Id = 4, Comentario = "El pedido llegó tarde", Puntuacion = 3, Fecha = new DateTime(2025, 1, 18), UsuarioId = 6 },
                new Calificacion { Id = 5, Comentario = "Servicio aceptable", Puntuacion = 4, Fecha = new DateTime(2025, 1, 20), UsuarioId = 7 },
                new Calificacion { Id = 6, Comentario = "El paquete llegó en buen estado", Puntuacion = 5, Fecha = new DateTime(2025, 1, 22), UsuarioId = 8 },
                new Calificacion { Id = 7, Comentario = "Faltó comunicación durante la entrega", Puntuacion = 3, Fecha = new DateTime(2025, 1, 24), UsuarioId = 9 },
                new Calificacion { Id = 8, Comentario = "Muy recomendado", Puntuacion = 5, Fecha = new DateTime(2025, 1, 26), UsuarioId = 10 },
                new Calificacion { Id = 9, Comentario = "Buen servicio empresarial", Puntuacion = 4, Fecha = new DateTime(2025, 1, 28), UsuarioId = 11 },
                new Calificacion { Id = 10, Comentario = "Entrega satisfactoria", Puntuacion = 4, Fecha = new DateTime(2025, 1, 30), UsuarioId = 12 }
            );
            //TABLA DEPENDIENTE PEDIDO
            //Pedido - TipoVehiculo N:1
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.TipoVehiculo)
                .WithMany(tv => tv.Pedidos)
                .HasForeignKey(p => p.TipoVehiculoId)
                .OnDelete(DeleteBehavior.Restrict);
            //Pedido - TipoVehiculo N:1
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.TipoVehiculo)
                .WithMany(tv => tv.Pedidos)
                .HasForeignKey(p => p.TipoVehiculoId)
                .OnDelete(DeleteBehavior.Restrict);
            //Pedido - Calificacion N:1 
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Calificacion)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.CalificacionId)
                .OnDelete(DeleteBehavior.Restrict);
            //Pedido - DetallePedido N:1
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.DetallePedido)
                .WithMany(dp => dp.Pedidos)
                .HasForeignKey(p => p.DetallePedidoId)
                .OnDelete(DeleteBehavior.Restrict);
            //DETALLEPEDIDO
            //DetallePedido - DireccionOrigen N:1
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.DireccionOrigen)
                .WithMany(d => d.DetallesPedido)
                .HasForeignKey(dp => dp.DireccionOrigenId)
                .OnDelete(DeleteBehavior.Restrict);
            //DetallePedido - DireccionDestino N:1
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.DireccionDestino)
                .WithMany(d => d.DetallesPedido)
                .HasForeignKey(dp => dp.DireccionDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            //datos
            modelBuilder.Entity<Pedido>().HasData(
                new Pedido { Id = 1, Fragil = true, PesoKg = 2.50m, DistanciaKm = 8.50m, CostoTotal = 45.00m, TipoVehiculoId = 1, ClienteId = 1, CalificacionId = 1, DetallePedidoId = 1 },
                new Pedido { Id = 2, Fragil = false, PesoKg = 5.20m, DistanciaKm = 12.00m, CostoTotal = 70.00m, TipoVehiculoId = 2, ClienteId = 2, CalificacionId = 2, DetallePedidoId = 2 },
                new Pedido { Id = 3, Fragil = true, PesoKg = 1.80m, DistanciaKm = 4.50m, CostoTotal = 30.00m, TipoVehiculoId = 1, ClienteId = 3, CalificacionId = 3, DetallePedidoId = 3 },
                new Pedido { Id = 4, Fragil = false, PesoKg = 8.00m, DistanciaKm = 20.00m, CostoTotal = 120.00m, TipoVehiculoId = 3, ClienteId = 4, CalificacionId = 4, DetallePedidoId = 4 },
                new Pedido { Id = 5, Fragil = true, PesoKg = 3.40m, DistanciaKm = 15.50m, CostoTotal = 80.00m, TipoVehiculoId = 2, ClienteId = 5, CalificacionId = 5, DetallePedidoId = 5 },
                new Pedido { Id = 6, Fragil = false, PesoKg = 6.80m, DistanciaKm = 18.00m, CostoTotal = 95.00m, TipoVehiculoId = 3, ClienteId = 6, CalificacionId = null, DetallePedidoId = 6 },
                new Pedido { Id = 7, Fragil = true, PesoKg = 0.90m, DistanciaKm = 3.50m, CostoTotal = 22.00m, TipoVehiculoId = 1, ClienteId = 7, CalificacionId = 7, DetallePedidoId = 7 },
                new Pedido { Id = 8, Fragil = false, PesoKg = 12.00m, DistanciaKm = 28.00m, CostoTotal = 175.00m, TipoVehiculoId = 3, ClienteId = 8, CalificacionId = 8, DetallePedidoId = 8 },
                new Pedido { Id = 9, Fragil = true, PesoKg = 4.60m, DistanciaKm = 9.20m, CostoTotal = 58.00m, TipoVehiculoId = 2, ClienteId = 9, CalificacionId = null, DetallePedidoId = 9 },
                new Pedido { Id = 10, Fragil = false, PesoKg = 7.30m, DistanciaKm = 14.00m, CostoTotal = 88.00m, TipoVehiculoId = 2, ClienteId = 10, CalificacionId = 10, DetallePedidoId = 10 }
            );
            //TABLA PIVOTE ESTADO PEDIDO
            // EstadoPedido - Pedido
            modelBuilder.Entity<EstadoPedido>()
                .HasOne(ep => ep.Pedido)
                .WithMany(p => p.EstadosPedidos)
                .HasForeignKey(ep => ep.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);

            // EstadoPedido - Estado
            modelBuilder.Entity<EstadoPedido>()
                .HasOne(ep => ep.Estado)
                .WithMany(e => e.EstadosPedidos)
                .HasForeignKey(ep => ep.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);
            //datos
            modelBuilder.Entity<EstadoPedido>().HasData(
                new EstadoPedido { Id = 1, HoraCambio = new DateTime(2025, 1, 10, 8, 0, 0), PedidoId = 1, EstadoId = 1 },
                new EstadoPedido { Id = 2, HoraCambio = new DateTime(2025, 1, 10, 10, 30, 0), PedidoId = 1, EstadoId = 2 },
                new EstadoPedido { Id = 3, HoraCambio = new DateTime(2025, 1, 10, 13, 15, 0), PedidoId = 1, EstadoId = 3 },
                new EstadoPedido { Id = 4, HoraCambio = new DateTime(2025, 1, 12, 9, 0, 0), PedidoId = 2, EstadoId = 1 },
                new EstadoPedido { Id = 5, HoraCambio = new DateTime(2025, 1, 12, 12, 20, 0), PedidoId = 2, EstadoId = 2 },
                new EstadoPedido { Id = 6, HoraCambio = new DateTime(2025, 1, 12, 15, 45, 0), PedidoId = 2, EstadoId = 3 },
                new EstadoPedido { Id = 7, HoraCambio = new DateTime(2025, 1, 15, 8, 45, 0), PedidoId = 3, EstadoId = 1 },
                new EstadoPedido { Id = 8, HoraCambio = new DateTime(2025, 1, 15, 11, 15, 0), PedidoId = 3, EstadoId = 4 },
                new EstadoPedido { Id = 9, HoraCambio = new DateTime(2025, 1, 18, 7, 50, 0), PedidoId = 4, EstadoId = 1 },
                new EstadoPedido { Id = 10, HoraCambio = new DateTime(2025, 1, 18, 11, 40, 0), PedidoId = 4, EstadoId = 2 }
            );

        }
    }
}
