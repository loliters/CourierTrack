using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class AparicionNotificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1967));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1971));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1973));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1974));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1975));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1976));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1978));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1979));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 4, 41, 628, DateTimeKind.Utc).AddTicks(1981));

            migrationBuilder.InsertData(
                table: "Notificaciones",
                columns: new[] { "Id", "Fecha", "Leida", "Mensaje", "PedidoId", "Titulo", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 8, 5, 0, 0, DateTimeKind.Utc), true, "Su pedido fue registrado correctamente.", 1, "Pedido Registrado", 6 },
                    { 2, new DateTime(2025, 1, 11, 9, 5, 0, 0, DateTimeKind.Utc), false, "Se asignó un conductor a su pedido.", 2, "Pedido Asignado", 7 },
                    { 3, new DateTime(2025, 1, 12, 10, 5, 0, 0, DateTimeKind.Utc), false, "Su pedido está siendo transportado.", 3, "En Camino", 8 },
                    { 4, new DateTime(2025, 1, 13, 11, 5, 0, 0, DateTimeKind.Utc), true, "Su pedido fue entregado exitosamente.", 4, "Pedido Entregado", 9 },
                    { 5, new DateTime(2025, 1, 14, 12, 5, 0, 0, DateTimeKind.Utc), false, "Tiene un pago pendiente por realizar.", 5, "Pago Pendiente", 10 },
                    { 6, new DateTime(2025, 1, 15, 13, 5, 0, 0, DateTimeKind.Utc), true, "Su pago fue confirmado correctamente.", 6, "Pago Confirmado", 11 },
                    { 7, new DateTime(2025, 1, 16, 14, 5, 0, 0, DateTimeKind.Utc), false, "El conductor está próximo a llegar.", 7, "Actualización", 12 },
                    { 8, new DateTime(2025, 1, 17, 15, 5, 0, 0, DateTimeKind.Utc), false, "Se registró un retraso en la entrega.", 8, "Retraso", 6 },
                    { 9, new DateTime(2025, 1, 18, 16, 5, 0, 0, DateTimeKind.Utc), true, "Gracias por utilizar CourierTrack.", 9, "Entrega Exitosa", 7 },
                    { 10, new DateTime(2025, 1, 19, 17, 5, 0, 0, DateTimeKind.Utc), true, "El pedido fue completado satisfactoriamente.", 10, "Pedido Finalizado", 8 }
                });

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 14, 41, 628, DateTimeKind.Utc).AddTicks(4374));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 19, 41, 628, DateTimeKind.Utc).AddTicks(4384));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 24, 41, 628, DateTimeKind.Utc).AddTicks(4388));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 29, 41, 628, DateTimeKind.Utc).AddTicks(4390));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 34, 41, 628, DateTimeKind.Utc).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 39, 41, 628, DateTimeKind.Utc).AddTicks(4394));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 44, 41, 628, DateTimeKind.Utc).AddTicks(4396));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 41, 628, DateTimeKind.Utc).AddTicks(4398));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 41, 628, DateTimeKind.Utc).AddTicks(4400));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 59, 41, 628, DateTimeKind.Utc).AddTicks(4403));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Notificaciones",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8321));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8323));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8325));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8326));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8327));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8328));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8329));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8331));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 3, 11, 923, DateTimeKind.Utc).AddTicks(8332));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 13, 11, 923, DateTimeKind.Utc).AddTicks(9719));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 18, 11, 923, DateTimeKind.Utc).AddTicks(9726));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 23, 11, 923, DateTimeKind.Utc).AddTicks(9728));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 28, 11, 923, DateTimeKind.Utc).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 33, 11, 923, DateTimeKind.Utc).AddTicks(9732));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 38, 11, 923, DateTimeKind.Utc).AddTicks(9733));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 43, 11, 923, DateTimeKind.Utc).AddTicks(9735));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 48, 11, 923, DateTimeKind.Utc).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 53, 11, 923, DateTimeKind.Utc).AddTicks(9738));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 58, 11, 923, DateTimeKind.Utc).AddTicks(9740));
        }
    }
}
