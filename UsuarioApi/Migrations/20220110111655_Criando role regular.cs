using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuarioApi.Migrations
{
    public partial class Criandoroleregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "c8129f83-890c-4223-8d8b-fa95dc2fed4c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "a3a7ab5a-8e0f-4228-bb17-d871472d3249", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "533ee2a6-b073-418b-8b63-3cc07f00a892", "AQAAAAEAACcQAAAAEBXCesS9I19xSyE9AJdl3QgV9v7dO0eoSO5jxz8JIMKQsV5VBgYd62wZVD2Ub1eovw==", "39b6f27c-a630-412d-8c8e-8a364016add0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "96ad5a6f-5f49-4aa1-9f8e-3d5e12b10bb1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6a5909c-6aa5-4963-941a-623abb53256e", "AQAAAAEAACcQAAAAEMkTOK/+bm6kTR9bS3S2qETJhsn6t1GEpD/XMEhH6JpExwhwzXx72aR99XPHiJX6mQ==", "c8d675d0-464f-4e33-8d3e-28c12c8f6074" });
        }
    }
}
