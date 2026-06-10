using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppCourierTrack.Migrations
{
    /// <inheritdoc />
    public partial class AparicionUsuarioUbiPivote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "UsuariosUbicaciones",
                columns: new[] { "Id", "EsPrincipal", "UbicacionId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, true, 1, 3 },
                    { 2, true, 2, 4 },
                    { 3, true, 3, 5 },
                    { 4, true, 4, 6 },
                    { 5, true, 5, 7 },
                    { 6, true, 1, 8 },
                    { 7, true, 2, 9 },
                    { 8, true, 3, 10 },
                    { 9, true, 4, 11 },
                    { 10, true, 5, 12 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UsuariosUbicaciones",
                keyColumn: "Id",
                keyValue: 10);

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
    }
}
