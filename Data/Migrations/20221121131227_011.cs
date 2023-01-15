using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageDesktopName",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMobileName",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(6103));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(6115));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(6482));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(8422));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(8461));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(8491));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 526, DateTimeKind.Local).AddTicks(8519));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 524, DateTimeKind.Local).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 524, DateTimeKind.Local).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 16, 12, 26, 524, DateTimeKind.Local).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 12, 26, 533, DateTimeKind.Local).AddTicks(9275), new Guid("0d3f5983-1778-4d12-b1ab-d57f6008f04e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageDesktopName",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageMobileName",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3478));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3496));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3978));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(5259));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7198));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7275));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 682, DateTimeKind.Local).AddTicks(6622));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 682, DateTimeKind.Local).AddTicks(6925));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 682, DateTimeKind.Local).AddTicks(6928));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 4, 5, 695, DateTimeKind.Local).AddTicks(1384), new Guid("d0a11920-7e28-49a6-9e3e-af3c468e2289") });
        }
    }
}
