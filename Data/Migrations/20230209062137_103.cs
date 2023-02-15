using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceTypeId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(5784));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(5792));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6025));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6031));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7415));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7439));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7476));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 920, DateTimeKind.Local).AddTicks(3486));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 920, DateTimeKind.Local).AddTicks(3722));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 920, DateTimeKind.Local).AddTicks(3724));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 9, 9, 21, 36, 927, DateTimeKind.Local).AddTicks(416), new Guid("321e1c12-8d7b-4f0f-a691-1a55bb6de6ad") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceTypeId",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(5803));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(5811));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(6045));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(6052));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(6054));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(6721));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(7504));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(7567));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(7582));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 980, DateTimeKind.Local).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 979, DateTimeKind.Local).AddTicks(3261));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 979, DateTimeKind.Local).AddTicks(3498));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 8, 10, 26, 21, 979, DateTimeKind.Local).AddTicks(3501));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 8, 10, 26, 21, 986, DateTimeKind.Local).AddTicks(6558), new Guid("3b225e7d-9bb0-4226-b940-12487645c3e1") });
        }
    }
}
