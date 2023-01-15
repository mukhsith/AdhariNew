using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeoName",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(1372));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(1385));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(1785));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(1796));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(2749));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(4077));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(4395));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 692, DateTimeKind.Local).AddTicks(4426));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 690, DateTimeKind.Local).AddTicks(2026));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 690, DateTimeKind.Local).AddTicks(2495));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 23, 9, 53, 35, 690, DateTimeKind.Local).AddTicks(2499));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 23, 9, 53, 35, 701, DateTimeKind.Local).AddTicks(4450), new Guid("d434b31f-8330-4114-8af4-6e0d23a3d4fd") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoName",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(6865));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(6878));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(7306));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(7317));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(8406));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(9844));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(9886));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 267, DateTimeKind.Local).AddTicks(9948));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 265, DateTimeKind.Local).AddTicks(6238));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 265, DateTimeKind.Local).AddTicks(6691));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 22, 10, 36, 19, 265, DateTimeKind.Local).AddTicks(6695));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 22, 10, 36, 19, 275, DateTimeKind.Local).AddTicks(4520), new Guid("35bab4d3-6ac1-4fa0-a7e2-1415b2cddfab") });
        }
    }
}
