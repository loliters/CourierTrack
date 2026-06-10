using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class AparicionPedido2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6073));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6075));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6076));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6077));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6079));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6080));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6081));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6082));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6083));

            migrationBuilder.UpdateData(
                table: "DetallePedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Fecha",
                value: new DateTime(2026, 6, 10, 18, 46, 35, 738, DateTimeKind.Utc).AddTicks(6084));

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 9,
                column: "CalificacionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 10,
                column: "CalificacionId",
                value: null);
        }
    }
}
