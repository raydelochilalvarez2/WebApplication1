using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class AgregandoDatosAlaTablaVilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImageUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[] { 1, "", "Detalles de la Villa....", new DateTime(2023, 9, 20, 4, 46, 1, 410, DateTimeKind.Local).AddTicks(2305), new DateTime(2023, 9, 20, 4, 46, 1, 410, DateTimeKind.Local).AddTicks(2255), "", 10, "VillaReal", 10, 25.43 });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImageUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[] { 2, "", "Detalles de la Villa2....", new DateTime(2023, 9, 20, 4, 46, 1, 410, DateTimeKind.Local).AddTicks(2322), new DateTime(2023, 9, 20, 4, 46, 1, 410, DateTimeKind.Local).AddTicks(2316), "", 20, "VillaReal2", 20, 60.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
