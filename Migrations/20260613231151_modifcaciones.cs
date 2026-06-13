using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class modifcaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacions_Usuarios_UsuarioId",
                table: "Calificacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Calificacions_CalificacionId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "CalificacionId",
                table: "Pedidos",
                newName: "ConductorId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_CalificacionId",
                table: "Pedidos",
                newName: "IX_Pedidos_ConductorId");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Calificacions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Calificacions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConductorId",
                table: "Calificacions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Calificacions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClienteId", "ConductorId", "PedidoId", "UsuarioId" },
                values: new object[] { 1, 1, 1, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "UsuarioId" },
                values: new object[] { 2, "Muy buen servicio", 2, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "Puntuacion", "UsuarioId" },
                values: new object[] { 3, "Servicio aceptable", 3, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "Puntuacion", "UsuarioId" },
                values: new object[] { 4, "Buen trato", 4, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "Puntuacion", "UsuarioId" },
                values: new object[] { 5, "Entrega rápida", 5, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "Puntuacion", "UsuarioId" },
                values: new object[] { 6, "Podría mejorar", 6, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "Puntuacion", "UsuarioId" },
                values: new object[] { 7, "Muy satisfecho", 7, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 5, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "Puntuacion", "UsuarioId" },
                values: new object[] { 8, "Servicio regular", 8, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "UsuarioId" },
                values: new object[] { 9, "Buen conductor", 9, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ClienteId", "Comentario", "ConductorId", "Fecha", "PedidoId", "Puntuacion", "UsuarioId" },
                values: new object[] { 10, "Excelente atención", 10, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 5, null });

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5503));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5510));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5511));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5512));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5516));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5517));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 11, 50, 161, DateTimeKind.Utc).AddTicks(5518));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 21, 50, 162, DateTimeKind.Utc).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 26, 50, 162, DateTimeKind.Utc).AddTicks(76));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 31, 50, 162, DateTimeKind.Utc).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 36, 50, 162, DateTimeKind.Utc).AddTicks(81));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 41, 50, 162, DateTimeKind.Utc).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 46, 50, 162, DateTimeKind.Utc).AddTicks(85));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 51, 50, 162, DateTimeKind.Utc).AddTicks(87));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 22, 56, 50, 162, DateTimeKind.Utc).AddTicks(89));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 1, 50, 162, DateTimeKind.Utc).AddTicks(91));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 13, 23, 6, 50, 162, DateTimeKind.Utc).AddTicks(93));

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConductorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConductorId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Seguimientos_PedidoId",
                table: "Seguimientos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PedidoId",
                table: "Pagos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacions_ClienteId",
                table: "Calificacions",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacions_ConductorId",
                table: "Calificacions",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacions_PedidoId",
                table: "Calificacions",
                column: "PedidoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacions_Clientes_ClienteId",
                table: "Calificacions",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacions_Conductores_ConductorId",
                table: "Calificacions",
                column: "ConductorId",
                principalTable: "Conductores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacions_Pedidos_PedidoId",
                table: "Calificacions",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacions_Usuarios_UsuarioId",
                table: "Calificacions",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId",
                table: "Pagos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Conductores_ConductorId",
                table: "Pedidos",
                column: "ConductorId",
                principalTable: "Conductores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seguimientos_Pedidos_PedidoId",
                table: "Seguimientos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacions_Clientes_ClienteId",
                table: "Calificacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificacions_Conductores_ConductorId",
                table: "Calificacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificacions_Pedidos_PedidoId",
                table: "Calificacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificacions_Usuarios_UsuarioId",
                table: "Calificacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Conductores_ConductorId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Seguimientos_Pedidos_PedidoId",
                table: "Seguimientos");

            migrationBuilder.DropIndex(
                name: "IX_Seguimientos_PedidoId",
                table: "Seguimientos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_PedidoId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Calificacions_ClienteId",
                table: "Calificacions");

            migrationBuilder.DropIndex(
                name: "IX_Calificacions_ConductorId",
                table: "Calificacions");

            migrationBuilder.DropIndex(
                name: "IX_Calificacions_PedidoId",
                table: "Calificacions");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Calificacions");

            migrationBuilder.DropColumn(
                name: "ConductorId",
                table: "Calificacions");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Calificacions");

            migrationBuilder.RenameColumn(
                name: "ConductorId",
                table: "Pedidos",
                newName: "CalificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ConductorId",
                table: "Pedidos",
                newName: "IX_Pedidos_CalificacionId");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Calificacions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 1,
                column: "UsuarioId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Comentario", "Fecha", "UsuarioId" },
                values: new object[] { "Entrega rápida y segura", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[] { "Buen trato del conductor", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 5 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[] { "El pedido llegó tarde", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), 3, 6 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[] { "Servicio aceptable", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), 4, 7 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[] { "El paquete llegó en buen estado", new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), 5, 8 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[] { "Faltó comunicación durante la entrega", new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), 3, 9 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[] { "Muy recomendado", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), 5, 10 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Comentario", "Fecha", "UsuarioId" },
                values: new object[] { "Buen servicio empresarial", new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), 11 });

            migrationBuilder.UpdateData(
                table: "Calificacions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Comentario", "Fecha", "Puntuacion", "UsuarioId" },
                values: new object[] { "Entrega satisfactoria", new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), 4, 12 });

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8248));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8251));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8253));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8254));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8255));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8256));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8257));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8258));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8259));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 12, 47, 868, DateTimeKind.Utc).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 22, 47, 868, DateTimeKind.Utc).AddTicks(9763));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 27, 47, 868, DateTimeKind.Utc).AddTicks(9770));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 32, 47, 868, DateTimeKind.Utc).AddTicks(9772));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 37, 47, 868, DateTimeKind.Utc).AddTicks(9774));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 42, 47, 868, DateTimeKind.Utc).AddTicks(9776));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 47, 47, 868, DateTimeKind.Utc).AddTicks(9778));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 52, 47, 868, DateTimeKind.Utc).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 57, 47, 868, DateTimeKind.Utc).AddTicks(9781));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 2, 47, 868, DateTimeKind.Utc).AddTicks(9783));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 7, 47, 868, DateTimeKind.Utc).AddTicks(9785));

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CalificacionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CalificacionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CalificacionId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CalificacionId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CalificacionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "CalificacionId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "CalificacionId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "CalificacionId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "CalificacionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "CalificacionId",
                value: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacions_Usuarios_UsuarioId",
                table: "Calificacions",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Calificacions_CalificacionId",
                table: "Pedidos",
                column: "CalificacionId",
                principalTable: "Calificacions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
