using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class AparicionPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "Banco", "CuentaBancaria", "EstadoPagoId", "Fecha", "MetodoPagoId", "Monto", "NumeroTransaccion", "PedidoId" },
                values: new object[,]
                {
                    { 1, "No Aplica", "No Aplica", 1, new DateTime(2026, 6, 10, 18, 4, 34, 968, DateTimeKind.Utc).AddTicks(3029), 1, 45.00m, "No Aplica", 1 },
                    { 2, "Banco Unión", "10000045218", 1, new DateTime(2026, 6, 10, 18, 9, 34, 968, DateTimeKind.Utc).AddTicks(3036), 2, 70.00m, "TXN-85214", 2 },
                    { 3, "No Aplica", "No Aplica", 1, new DateTime(2026, 6, 10, 18, 14, 34, 968, DateTimeKind.Utc).AddTicks(3038), 1, 30.00m, "No Aplica", 3 },
                    { 4, "Banco Nacional de Bolivia", "201-514789", 1, new DateTime(2026, 6, 10, 18, 19, 34, 968, DateTimeKind.Utc).AddTicks(3040), 2, 120.00m, "TXN-96325", 4 },
                    { 5, "No Aplica", "No Aplica", 2, new DateTime(2026, 6, 10, 18, 24, 34, 968, DateTimeKind.Utc).AddTicks(3042), 1, 95.00m, "No Aplica", 5 },
                    { 6, "Banco Mercantil Santa Cruz", "402-369852", 1, new DateTime(2026, 6, 10, 18, 29, 34, 968, DateTimeKind.Utc).AddTicks(3044), 2, 110.00m, "TXN-14785", 6 },
                    { 7, "No Aplica", "No Aplica", 1, new DateTime(2026, 6, 10, 18, 34, 34, 968, DateTimeKind.Utc).AddTicks(3045), 1, 22.00m, "No Aplica", 7 },
                    { 8, "Banco de Crédito BCP", "305-784125", 3, new DateTime(2026, 6, 10, 18, 39, 34, 968, DateTimeKind.Utc).AddTicks(3047), 2, 175.00m, "TXN-36985", 8 },
                    { 9, "No Aplica", "No Aplica", 1, new DateTime(2026, 6, 10, 18, 44, 34, 968, DateTimeKind.Utc).AddTicks(3049), 1, 58.00m, "No Aplica", 9 },
                    { 10, "Banco Económico", "501-963258", 2, new DateTime(2026, 6, 10, 18, 49, 34, 968, DateTimeKind.Utc).AddTicks(3051), 2, 88.00m, "TXN-25814", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6845));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6847));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6849));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6850));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6851));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6852));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6854));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6855));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6856));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 49, 51, 189, DateTimeKind.Utc).AddTicks(6857));
        }
    }
}
