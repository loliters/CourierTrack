using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class prueba11todoNuevabd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnioVehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Anio = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnioVehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPagos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtensionCI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionCI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Leida = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    PedidoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoClientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoLicencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Categoria = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLicencias", x => x.Id);
                    table.CheckConstraint("CK_TipoLicencia_categoria", "\"Categoria\" IN ('M','P','C')");
                });

            migrationBuilder.CreateTable(
                name: "TipoVehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitud = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitud = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Monto = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PedidoId = table.Column<int>(type: "integer", nullable: false),
                    MetodoPagoId = table.Column<int>(type: "integer", nullable: false),
                    EstadoPagoId = table.Column<int>(type: "integer", nullable: false),
                    NumeroTransaccion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CuentaBancaria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Banco = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_MetodoPagos_MetodoPagoId",
                        column: x => x.MetodoPagoId,
                        principalTable: "MetodoPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ApPat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ApMat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MarcaId = table.Column<int>(type: "integer", nullable: false),
                    TipoVehiculoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modelos_TipoVehiculos_TipoVehiculoId",
                        column: x => x.TipoVehiculoId,
                        principalTable: "TipoVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tarifas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrecioKg = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    PrecioKm = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    TipoVehiculoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarifas_TipoVehiculos_TipoVehiculoId",
                        column: x => x.TipoVehiculoId,
                        principalTable: "TipoVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DireccionesDestinos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Referencia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UbicacionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionesDestinos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DireccionesDestinos_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DireccionesOrigenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Referencia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UbicacionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionesOrigenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DireccionesOrigenes_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calificacions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comentario = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Puntuacion = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificacions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificacions_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NroDocumento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "integer", nullable: false),
                    ExtensionCIId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    TipoClienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_ExtensionCI_ExtensionCIId",
                        column: x => x.ExtensionCIId,
                        principalTable: "ExtensionCI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_TipoClientes_TipoClienteId",
                        column: x => x.TipoClienteId,
                        principalTable: "TipoClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conductores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NroLicencia = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    TipoLicenciaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conductores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conductores_TipoLicencias_TipoLicenciaId",
                        column: x => x.TipoLicenciaId,
                        principalTable: "TipoLicencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conductores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosUbicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    UbicacionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosUbicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosUbicaciones_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosUbicaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DireccionOrigenId = table.Column<int>(type: "integer", nullable: false),
                    DireccionDestinoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_DireccionesDestinos_DireccionDestinoId",
                        column: x => x.DireccionDestinoId,
                        principalTable: "DireccionesDestinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_DireccionesOrigenes_DireccionOrigenId",
                        column: x => x.DireccionOrigenId,
                        principalTable: "DireccionesOrigenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientesJuridicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RazonSocial = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nit = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesJuridicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesJuridicos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientesNatural",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaNac = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeneroId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesNatural", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesNatural_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientesNatural_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Placa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ModeloId = table.Column<int>(type: "integer", nullable: false),
                    ColorId = table.Column<int>(type: "integer", nullable: false),
                    AnioVehiculoId = table.Column<int>(type: "integer", nullable: false),
                    ConductorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_AnioVehiculos_AnioVehiculoId",
                        column: x => x.AnioVehiculoId,
                        principalTable: "AnioVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Conductores_ConductorId",
                        column: x => x.ConductorId,
                        principalTable: "Conductores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fragil = table.Column<bool>(type: "boolean", nullable: false),
                    PesoKg = table.Column<decimal>(type: "numeric", nullable: false),
                    DistanciaKm = table.Column<decimal>(type: "numeric", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    TipoVehiculoId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    CalificacionId = table.Column<int>(type: "integer", nullable: true),
                    DetallePedidoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Calificacions_CalificacionId",
                        column: x => x.CalificacionId,
                        principalTable: "Calificacions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_DetallePedidos_DetallePedidoId",
                        column: x => x.DetallePedidoId,
                        principalTable: "DetallePedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_TipoVehiculos_TipoVehiculoId",
                        column: x => x.TipoVehiculoId,
                        principalTable: "TipoVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seguimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PedidoId = table.Column<int>(type: "integer", nullable: false),
                    ConductorId = table.Column<int>(type: "integer", nullable: false),
                    VehiculoId = table.Column<int>(type: "integer", nullable: false),
                    UbicacionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seguimientos_Conductores_ConductorId",
                        column: x => x.ConductorId,
                        principalTable: "Conductores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seguimientos_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seguimientos_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EstadosPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoraCambio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PedidoId = table.Column<int>(type: "integer", nullable: false),
                    EstadoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadosPedidos_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstadosPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistorialUbicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UbicacionId = table.Column<int>(type: "integer", nullable: false),
                    SeguimientoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialUbicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialUbicaciones_Seguimientos_SeguimientoId",
                        column: x => x.SeguimientoId,
                        principalTable: "Seguimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialUbicaciones_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AnioVehiculos",
                columns: new[] { "Id", "Anio" },
                values: new object[,]
                {
                    { 1, 2013 },
                    { 2, 2014 },
                    { 3, 2015 },
                    { 4, 2016 },
                    { 5, 2017 },
                    { 6, 2018 },
                    { 7, 2019 },
                    { 8, 2020 },
                    { 9, 2021 },
                    { 10, 2022 },
                    { 11, 2023 },
                    { 12, 2024 },
                    { 13, 2025 },
                    { 14, 2026 }
                });

            migrationBuilder.InsertData(
                table: "Colores",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Blanco" },
                    { 2, "Negro" },
                    { 3, "Rojo" },
                    { 4, "Azul" },
                    { 5, "Gris" },
                    { 6, "Plateado" },
                    { 7, "Verde" },
                    { 8, "Amarillo" },
                    { 9, "Naranja" },
                    { 10, "Morado" }
                });

            migrationBuilder.InsertData(
                table: "EstadoPagos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Pagado" },
                    { 2, "Pendiente" },
                    { 3, "Rechazado" }
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Asignado" },
                    { 3, "En camino" },
                    { 4, "Entregado" },
                    { 5, "Cancelado" },
                    { 6, "Confirmado" },
                    { 7, "En espera" },
                    { 8, "Retrasado" },
                    { 9, "Devuelto" },
                    { 10, "Completado" }
                });

            migrationBuilder.InsertData(
                table: "ExtensionCI",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "LP" },
                    { 2, "CB" },
                    { 3, "SC" },
                    { 4, "OR" },
                    { 5, "PT" },
                    { 6, "CH" },
                    { 7, "TJ" },
                    { 8, "BN" },
                    { 9, "PD" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "FEMENINO" },
                    { 2, "MASCULINO" },
                    { 3, "OTRO" }
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Ford" },
                    { 3, "Chevrolet" },
                    { 4, "Mercedes Benz" },
                    { 5, "Volvo" },
                    { 6, "Hino" },
                    { 7, "Nissan" },
                    { 8, "Hyundai" },
                    { 9, "Kia" },
                    { 10, "Mitsubishi" }
                });

            migrationBuilder.InsertData(
                table: "MetodoPagos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Efectivo" },
                    { 2, "Transferencia" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "ADMINISTRADOR" },
                    { 2, "CONDUCTOR" },
                    { 3, "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "TipoClientes",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "JURIDICO" },
                    { 2, "NATURAL" }
                });

            migrationBuilder.InsertData(
                table: "TipoDocumentos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "CEDULA DE IDENTIDAD" },
                    { 2, "NIT" }
                });

            migrationBuilder.InsertData(
                table: "TipoLicencias",
                columns: new[] { "Id", "Categoria" },
                values: new object[,]
                {
                    { 1, "M" },
                    { 2, "P" },
                    { 3, "C" }
                });

            migrationBuilder.InsertData(
                table: "TipoVehiculos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Moto" },
                    { 2, "Automóvil" },
                    { 3, "Furgoneta" },
                    { 4, "Camión pequeño" },
                    { 5, "Camión grande" },
                    { 6, "Camioneta" },
                    { 7, "Minibús" },
                    { 8, "Bus" },
                    { 9, "Triciclo" },
                    { 10, "Bicicleta" }
                });

            migrationBuilder.InsertData(
                table: "Ubicaciones",
                columns: new[] { "Id", "Latitud", "Longitud" },
                values: new object[,]
                {
                    { 1, -17.389577m, -66.157607m },
                    { 2, -17.393531m, -66.157001m },
                    { 3, -17.378240m, -66.161950m },
                    { 4, -17.401470m, -66.155790m },
                    { 5, -17.356478m, -66.145554m }
                });

            migrationBuilder.InsertData(
                table: "DireccionesDestinos",
                columns: new[] { "Id", "Referencia", "UbicacionId" },
                values: new object[,]
                {
                    { 1, "Av. 6 de Agosto, frente al supermercado", 1 },
                    { 2, "Zona San Miguel, cerca de la plaza", 2 },
                    { 3, "Av. Montenegro, edificio empresarial", 3 },
                    { 4, "Calle Mercado, al lado de la farmacia", 4 },
                    { 5, "Zona Obrajes, frente a la estación", 5 }
                });

            migrationBuilder.InsertData(
                table: "DireccionesOrigenes",
                columns: new[] { "Id", "Referencia", "UbicacionId" },
                values: new object[,]
                {
                    { 1, "Av. Arce, frente al Multicine", 1 },
                    { 2, "Zona Sopocachi, cerca de Plaza Abaroa", 2 },
                    { 3, "Av. Busch, frente al mercado", 3 },
                    { 4, "Calle Comercio, al lado del banco", 4 },
                    { 5, "Zona Sur, cerca del Megacenter", 5 },
                    { 6, "Av. Ballivián, esquina semáforo", 1 },
                    { 7, "Terminal de buses, ingreso principal", 2 },
                    { 8, "Av. Camacho, edificio empresarial", 3 },
                    { 9, "Zona Miraflores, frente al estadio", 4 },
                    { 10, "Calle 21 de Calacoto, esquina farmacia", 5 }
                });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "Id", "MarcaId", "Nombre", "TipoVehiculoId" },
                values: new object[,]
                {
                    { 1, 1, "Corolla", 2 },
                    { 2, 2, "F-150", 4 },
                    { 3, 4, "Sprinter", 3 },
                    { 4, 1, "Civic", 2 },
                    { 5, 5, "Carga 5000", 5 }
                });

            migrationBuilder.InsertData(
                table: "Tarifas",
                columns: new[] { "Id", "PrecioKg", "PrecioKm", "TipoVehiculoId" },
                values: new object[,]
                {
                    { 1, 1.50m, 2.00m, 1 },
                    { 2, 2.00m, 2.50m, 2 },
                    { 3, 2.50m, 3.00m, 3 },
                    { 4, 3.00m, 4.00m, 4 },
                    { 5, 4.00m, 5.00m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "ApMat", "ApPat", "Correo", "Nombre", "Password", "RolId", "Telefono" },
                values: new object[,]
                {
                    { 1, "Rojas", "Mendoza", "admin1@courier.com", "Carlos", "Admin123*", 1, "71234567" },
                    { 2, "Perez", "Lopez", "admin2@courier.com", "Andrea", "Admin456*", 1, "72345678" },
                    { 3, "Soto", "Flores", "juan@courier.com", "Juan", "User123*", 2, "73456789" },
                    { 4, "Mamani", "Gutierrez", "maria@courier.com", "Maria", "User123*", 2, "74567891" },
                    { 5, "Rivera", "Choque", "pedro@courier.com", "Pedro", "User123*", 2, "75678912" },
                    { 6, "Fernandez", "Torrez", "lucia@courier.com", "Lucia", "User123*", 3, "76789123" },
                    { 7, "Suarez", "Ramos", "miguel@courier.com", "Miguel", "User123*", 3, "77891234" },
                    { 8, "Castro", "Vargas", "paola@courier.com", "Paola", "User123*", 3, "78912345" },
                    { 9, "Quispe", "Salazar", "fernando@courier.com", "Fernando", "User123*", 3, "79123456" },
                    { 10, "Cruz", "Mendez", "valeria@courier.com", "Valeria", "User123*", 3, "70234567" },
                    { 11, "SRL", "Uno", "empresa1@courier.com", "Empresa", "User123*", 3, "70111111" },
                    { 12, "SA", "Dos", "empresa2@courier.com", "Empresa", "User123*", 3, "70222222" }
                });

            migrationBuilder.InsertData(
                table: "Calificacions",
                columns: new[] { "Id", "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Excelente servicio", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5, 3 },
                    { 2, "Entrega rápida y segura", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4, 4 },
                    { 3, "Buen trato del conductor", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 5 },
                    { 4, "El pedido llegó tarde", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), 3, 6 },
                    { 5, "Servicio aceptable", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), 4, 7 },
                    { 6, "El paquete llegó en buen estado", new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), 5, 8 },
                    { 7, "Faltó comunicación durante la entrega", new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), 3, 9 },
                    { 8, "Muy recomendado", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), 5, 10 },
                    { 9, "Buen servicio empresarial", new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), 4, 11 },
                    { 10, "Entrega satisfactoria", new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), 4, 12 }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "ExtensionCIId", "NroDocumento", "TipoClienteId", "TipoDocumentoId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, "1234567", 1, 1, 3 },
                    { 2, 2, "2345678", 1, 1, 4 },
                    { 3, 3, "3456789", 1, 1, 5 },
                    { 4, 4, "4567891", 1, 1, 6 },
                    { 5, 5, "5678912", 1, 1, 7 },
                    { 6, 6, "6789123", 1, 1, 8 },
                    { 7, 7, "7891234", 1, 1, 9 },
                    { 8, null, "1020304011", 2, 2, 10 },
                    { 9, null, "2040506070", 2, 2, 11 },
                    { 10, null, "3098765432", 2, 2, 12 }
                });

            migrationBuilder.InsertData(
                table: "Conductores",
                columns: new[] { "Id", "NroLicencia", "TipoLicenciaId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "8765432SC", 3, 3 },
                    { 2, "5432109CB", 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "DetallePedidos",
                columns: new[] { "Id", "Descripcion", "DireccionDestinoId", "DireccionOrigenId", "Fecha" },
                values: new object[,]
                {
                    { 1, "Documentos importantes", 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Caja mediana", 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Paquete pequeño frágil", 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Electrodoméstico", 4, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Equipos de oficina", 5, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Repuestos mecánicos", 1, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Medicamentos", 2, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Muebles pequeños", 3, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Papelería", 4, 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Material electrónico", 5, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UsuariosUbicaciones",
                columns: new[] { "Id", "EsPrincipal", "UbicacionId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 1, 2 },
                    { 3, false, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ClientesJuridicos",
                columns: new[] { "Id", "ClienteId", "Nit", "RazonSocial" },
                values: new object[,]
                {
                    { 1, 8, "1020304011", "Courier Express SRL" },
                    { 2, 9, "2040506070", "Logistica Andina SA" },
                    { 3, 10, "3098765432", "Distribuciones Bolivia LTDA" }
                });

            migrationBuilder.InsertData(
                table: "ClientesNatural",
                columns: new[] { "Id", "ClienteId", "FechaNac", "GeneroId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 2, new DateTime(1998, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, 3, new DateTime(1992, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, 4, new DateTime(2000, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 5, 5, new DateTime(1997, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "CalificacionId", "ClienteId", "CostoTotal", "DetallePedidoId", "DistanciaKm", "Fragil", "PesoKg", "TipoVehiculoId" },
                values: new object[,]
                {
                    { 1, 1, 1, 45.00m, 1, 8.50m, true, 2.50m, 1 },
                    { 2, 2, 2, 70.00m, 2, 12.00m, false, 5.20m, 2 },
                    { 3, 3, 3, 30.00m, 3, 4.50m, true, 1.80m, 1 },
                    { 4, 4, 4, 120.00m, 4, 20.00m, false, 8.00m, 3 },
                    { 5, 5, 5, 80.00m, 5, 15.50m, true, 3.40m, 2 },
                    { 6, null, 6, 95.00m, 6, 18.00m, false, 6.80m, 3 },
                    { 7, 7, 7, 22.00m, 7, 3.50m, true, 0.90m, 1 },
                    { 8, 8, 8, 175.00m, 8, 28.00m, false, 12.00m, 3 },
                    { 9, null, 9, 58.00m, 9, 9.20m, true, 4.60m, 2 },
                    { 10, 10, 10, 88.00m, 10, 14.00m, false, 7.30m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Id", "AnioVehiculoId", "ColorId", "ConductorId", "ModeloId", "Placa" },
                values: new object[,]
                {
                    { 1, 6, 1, 1, 1, "1852-PHD" },
                    { 2, 8, 4, 2, 2, "4511-GAD" }
                });

            migrationBuilder.InsertData(
                table: "EstadosPedidos",
                columns: new[] { "Id", "EstadoId", "HoraCambio", "PedidoId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 2, new DateTime(2025, 1, 10, 10, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, 3, new DateTime(2025, 1, 10, 13, 15, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, 1, new DateTime(2025, 1, 12, 9, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 5, 2, new DateTime(2025, 1, 12, 12, 20, 0, 0, DateTimeKind.Utc), 2 },
                    { 6, 3, new DateTime(2025, 1, 12, 15, 45, 0, 0, DateTimeKind.Utc), 2 },
                    { 7, 1, new DateTime(2025, 1, 15, 8, 45, 0, 0, DateTimeKind.Utc), 3 },
                    { 8, 4, new DateTime(2025, 1, 15, 11, 15, 0, 0, DateTimeKind.Utc), 3 },
                    { 9, 1, new DateTime(2025, 1, 18, 7, 50, 0, 0, DateTimeKind.Utc), 4 },
                    { 10, 2, new DateTime(2025, 1, 18, 11, 40, 0, 0, DateTimeKind.Utc), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnioVehiculos_Anio",
                table: "AnioVehiculos",
                column: "Anio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calificacions_UsuarioId",
                table: "Calificacions",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ExtensionCIId",
                table: "Clientes",
                column: "ExtensionCIId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoClienteId",
                table: "Clientes",
                column: "TipoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoDocumentoId",
                table: "Clientes",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesJuridicos_ClienteId",
                table: "ClientesJuridicos",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesNatural_ClienteId",
                table: "ClientesNatural",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesNatural_GeneroId",
                table: "ClientesNatural",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Colores_Nombre",
                table: "Colores",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conductores_NroLicencia",
                table: "Conductores",
                column: "NroLicencia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conductores_TipoLicenciaId",
                table: "Conductores",
                column: "TipoLicenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Conductores_UsuarioId",
                table: "Conductores",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_DireccionDestinoId",
                table: "DetallePedidos",
                column: "DireccionDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_DireccionOrigenId",
                table: "DetallePedidos",
                column: "DireccionOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesDestinos_UbicacionId",
                table: "DireccionesDestinos",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesOrigenes_UbicacionId",
                table: "DireccionesOrigenes",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoPagos_Nombre",
                table: "EstadoPagos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstadosPedidos_EstadoId",
                table: "EstadosPedidos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosPedidos_PedidoId",
                table: "EstadosPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionCI_Nombre",
                table: "ExtensionCI",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Generos_Nombre",
                table: "Generos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorialUbicaciones_SeguimientoId",
                table: "HistorialUbicaciones",
                column: "SeguimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialUbicaciones_UbicacionId",
                table: "HistorialUbicaciones",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_Nombre",
                table: "Marcas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetodoPagos_Nombre",
                table: "MetodoPagos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaId",
                table: "Modelos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_Nombre_MarcaId",
                table: "Modelos",
                columns: new[] { "Nombre", "MarcaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_TipoVehiculoId",
                table: "Modelos",
                column: "TipoVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_MetodoPagoId",
                table: "Pagos",
                column: "MetodoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CalificacionId",
                table: "Pedidos",
                column: "CalificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DetallePedidoId",
                table: "Pedidos",
                column: "DetallePedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_TipoVehiculoId",
                table: "Pedidos",
                column: "TipoVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Nombre",
                table: "Roles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seguimientos_ConductorId",
                table: "Seguimientos",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimientos_UbicacionId",
                table: "Seguimientos",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimientos_VehiculoId",
                table: "Seguimientos",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifas_TipoVehiculoId",
                table: "Tarifas",
                column: "TipoVehiculoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoClientes_Nombre",
                table: "TipoClientes",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoDocumentos_Nombre",
                table: "TipoDocumentos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoVehiculos_Nombre",
                table: "TipoVehiculos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosUbicaciones_UbicacionId",
                table: "UsuariosUbicaciones",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosUbicaciones_UsuarioId_EsPrincipal",
                table: "UsuariosUbicaciones",
                columns: new[] { "UsuarioId", "EsPrincipal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_AnioVehiculoId",
                table: "Vehiculos",
                column: "AnioVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ColorId",
                table: "Vehiculos",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ConductorId",
                table: "Vehiculos",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ModeloId",
                table: "Vehiculos",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Placa",
                table: "Vehiculos",
                column: "Placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesJuridicos");

            migrationBuilder.DropTable(
                name: "ClientesNatural");

            migrationBuilder.DropTable(
                name: "EstadoPagos");

            migrationBuilder.DropTable(
                name: "EstadosPedidos");

            migrationBuilder.DropTable(
                name: "HistorialUbicaciones");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Tarifas");

            migrationBuilder.DropTable(
                name: "UsuariosUbicaciones");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Seguimientos");

            migrationBuilder.DropTable(
                name: "MetodoPagos");

            migrationBuilder.DropTable(
                name: "Calificacions");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "DetallePedidos");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "ExtensionCI");

            migrationBuilder.DropTable(
                name: "TipoClientes");

            migrationBuilder.DropTable(
                name: "TipoDocumentos");

            migrationBuilder.DropTable(
                name: "DireccionesDestinos");

            migrationBuilder.DropTable(
                name: "DireccionesOrigenes");

            migrationBuilder.DropTable(
                name: "AnioVehiculos");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Conductores");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "TipoLicencias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "TipoVehiculos");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
