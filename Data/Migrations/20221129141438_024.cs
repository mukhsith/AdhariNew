using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageNormalIconName",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSelectedIconName",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(4899));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(4912));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(5305));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(5316));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(5319));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7813));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7854));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 922, DateTimeKind.Local).AddTicks(5821));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 922, DateTimeKind.Local).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 922, DateTimeKind.Local).AddTicks(6239));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 29, 17, 14, 36, 934, DateTimeKind.Local).AddTicks(998), new Guid("33865cdf-6afb-41ad-8ffe-d89f9ed1a0db") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageNormalIconName",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageSelectedIconName",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 160, DateTimeKind.Local).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 160, DateTimeKind.Local).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local).AddTicks(11));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local).AddTicks(2642));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local).AddTicks(2683));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local).AddTicks(2715));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 161, DateTimeKind.Local).AddTicks(2745));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 158, DateTimeKind.Local).AddTicks(7872));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 158, DateTimeKind.Local).AddTicks(8298));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 13, 10, 3, 158, DateTimeKind.Local).AddTicks(8302));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 29, 13, 10, 3, 168, DateTimeKind.Local).AddTicks(3392), new Guid("0e1ba94d-8eb7-4569-952c-969c8fd55698") });
        }
    }
}
