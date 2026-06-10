using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class Confe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosPedidos_Estados_EstadoId",
                table: "EstadosPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_EstadosPedidos_Pedidos_PedidoId",
                table: "EstadosPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosUbicaciones_Ubicaciones_UbicacionId",
                table: "UsuariosUbicaciones");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosUbicaciones_UsuarioId_EsPrincipal",
                table: "UsuariosUbicaciones");

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EstadosPedidos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosUbicaciones_UsuarioId",
                table: "UsuariosUbicaciones",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosPedidos_Estados_EstadoId",
                table: "EstadosPedidos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosPedidos_Pedidos_PedidoId",
                table: "EstadosPedidos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosUbicaciones_Ubicaciones_UbicacionId",
                table: "UsuariosUbicaciones",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosPedidos_Estados_EstadoId",
                table: "EstadosPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_EstadosPedidos_Pedidos_PedidoId",
                table: "EstadosPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosUbicaciones_Ubicaciones_UbicacionId",
                table: "UsuariosUbicaciones");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosUbicaciones_UsuarioId",
                table: "UsuariosUbicaciones");

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
                table: "UsuariosUbicaciones",
                columns: new[] { "Id", "EsPrincipal", "UbicacionId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 1, 2 },
                    { 3, false, 2, 2 }
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
                name: "IX_UsuariosUbicaciones_UsuarioId_EsPrincipal",
                table: "UsuariosUbicaciones",
                columns: new[] { "UsuarioId", "EsPrincipal" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosPedidos_Estados_EstadoId",
                table: "EstadosPedidos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosPedidos_Pedidos_PedidoId",
                table: "EstadosPedidos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosUbicaciones_Ubicaciones_UbicacionId",
                table: "UsuariosUbicaciones",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
