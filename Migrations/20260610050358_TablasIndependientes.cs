using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class TablasIndependientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_EstadoPagos_Nombre",
                table: "EstadoPagos",
                column: "Nombre",
                unique: true);

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
                name: "IX_Roles_Nombre",
                table: "Roles",
                column: "Nombre",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoPagos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "ExtensionCI");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TipoClientes");

            migrationBuilder.DropTable(
                name: "TipoDocumentos");
        }
    }
}
