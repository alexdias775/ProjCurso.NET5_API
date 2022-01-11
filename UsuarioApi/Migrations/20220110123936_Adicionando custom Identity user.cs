using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuarioApi.Migrations
{
    public partial class AdicionandocustomIdentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "2df02f51-40cf-4476-9bcb-31d766a5da1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "e39eae37-d921-40ad-b234-6225ddf049e1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2619adec-775f-49aa-b62c-827c0c2a476e", "AQAAAAEAACcQAAAAEGz1sMSPV/++drTvhBbOT5B8EFLY8ztlUdQBGNopw2s2HJmS4LocY/K50MgE/Ky+cw==", "c6cbf33d-c1dc-4d3e-8998-3a58b621a324" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "a3a7ab5a-8e0f-4228-bb17-d871472d3249");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "c8129f83-890c-4223-8d8b-fa95dc2fed4c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "533ee2a6-b073-418b-8b63-3cc07f00a892", "AQAAAAEAACcQAAAAEBXCesS9I19xSyE9AJdl3QgV9v7dO0eoSO5jxz8JIMKQsV5VBgYd62wZVD2Ub1eovw==", "39b6f27c-a630-412d-8c8e-8a364016add0" });
        }
    }
}
