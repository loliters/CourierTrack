using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class AparicionSeguimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Seguimientos",
                columns: new[] { "Id", "ConductorId", "Fecha", "Observacion", "PedidoId", "UbicacionId", "VehiculoId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), "Pedido registrado", 1, 1, 1 },
                    { 2, 2, new DateTime(2025, 1, 11, 9, 0, 0, 0, DateTimeKind.Utc), "Pedido asignado", 2, 2, 2 },
                    { 3, 1, new DateTime(2025, 1, 12, 10, 0, 0, 0, DateTimeKind.Utc), "En camino", 3, 3, 1 },
                    { 4, 2, new DateTime(2025, 1, 13, 11, 0, 0, 0, DateTimeKind.Utc), "Entregado", 4, 4, 2 },
                    { 5, 1, new DateTime(2025, 1, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Confirmado", 5, 5, 1 },
                    { 6, 2, new DateTime(2025, 1, 15, 13, 0, 0, 0, DateTimeKind.Utc), "Pendiente de entrega", 6, 1, 2 },
                    { 7, 1, new DateTime(2025, 1, 16, 14, 0, 0, 0, DateTimeKind.Utc), "Retraso por tráfico", 7, 2, 1 },
                    { 8, 2, new DateTime(2025, 1, 17, 15, 0, 0, 0, DateTimeKind.Utc), "En reparto", 8, 3, 2 },
                    { 9, 1, new DateTime(2025, 1, 18, 16, 0, 0, 0, DateTimeKind.Utc), "Llegó al destino", 9, 4, 1 },
                    { 10, 2, new DateTime(2025, 1, 19, 17, 0, 0, 0, DateTimeKind.Utc), "Proceso finalizado", 10, 5, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Seguimientos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1518));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1522));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1523));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1524));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1526));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1527));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1528));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1529));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 54, 34, 968, DateTimeKind.Utc).AddTicks(1531));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 4, 34, 968, DateTimeKind.Utc).AddTicks(3029));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 9, 34, 968, DateTimeKind.Utc).AddTicks(3036));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 14, 34, 968, DateTimeKind.Utc).AddTicks(3038));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 19, 34, 968, DateTimeKind.Utc).AddTicks(3040));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 24, 34, 968, DateTimeKind.Utc).AddTicks(3042));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 29, 34, 968, DateTimeKind.Utc).AddTicks(3044));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 34, 34, 968, DateTimeKind.Utc).AddTicks(3045));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 39, 34, 968, DateTimeKind.Utc).AddTicks(3047));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 44, 34, 968, DateTimeKind.Utc).AddTicks(3049));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 34, 968, DateTimeKind.Utc).AddTicks(3051));
        }
    }
}
