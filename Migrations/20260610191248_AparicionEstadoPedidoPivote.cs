using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class AparicionEstadoPedidoPivote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "EstadosPedidos",
                columns: new[] { "Id", "EstadoId", "HoraCambio", "PedidoId" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 3, new DateTime(2025, 1, 11, 9, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, 4, new DateTime(2025, 1, 12, 10, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 4, 2, new DateTime(2025, 1, 13, 11, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 5, 1, new DateTime(2025, 1, 14, 12, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 6, 4, new DateTime(2025, 1, 15, 13, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 7, 3, new DateTime(2025, 1, 16, 14, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 8, 5, new DateTime(2025, 1, 17, 15, 0, 0, 0, DateTimeKind.Utc), 8 },
                    { 9, 4, new DateTime(2025, 1, 18, 16, 0, 0, 0, DateTimeKind.Utc), 9 },
                    { 10, 10, new DateTime(2025, 1, 19, 17, 0, 0, 0, DateTimeKind.Utc), 10 }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9701));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9705));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9706));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9708));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9710));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9711));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9712));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9714));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9715));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 10, 32, 122, DateTimeKind.Utc).AddTicks(9716));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 20, 32, 123, DateTimeKind.Utc).AddTicks(1725));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 25, 32, 123, DateTimeKind.Utc).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 30, 32, 123, DateTimeKind.Utc).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 35, 32, 123, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 40, 32, 123, DateTimeKind.Utc).AddTicks(1741));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 45, 32, 123, DateTimeKind.Utc).AddTicks(1743));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 50, 32, 123, DateTimeKind.Utc).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 55, 32, 123, DateTimeKind.Utc).AddTicks(1747));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 0, 32, 123, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 19, 5, 32, 123, DateTimeKind.Utc).AddTicks(1752));
        }
    }
}
