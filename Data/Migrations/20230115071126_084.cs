using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _084 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                table: "QueuedEmails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(1637));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(1644));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(1646));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(2191));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(2945));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(2975));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 350, DateTimeKind.Local).AddTicks(3004));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 348, DateTimeKind.Local).AddTicks(8986));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 348, DateTimeKind.Local).AddTicks(9365));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 15, 10, 11, 25, 348, DateTimeKind.Local).AddTicks(9368));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 15, 10, 11, 25, 355, DateTimeKind.Local).AddTicks(1740), new Guid("38b44efc-33ca-43b3-9013-077cf42ff96b") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                table: "QueuedEmails");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(1320));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(1329));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(1676));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(1683));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(1686));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(3137));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(3161));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(3176));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 167, DateTimeKind.Local).AddTicks(3192));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 165, DateTimeKind.Local).AddTicks(8591));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 165, DateTimeKind.Local).AddTicks(8838));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 8, 15, 21, 3, 165, DateTimeKind.Local).AddTicks(8841));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 8, 15, 21, 3, 172, DateTimeKind.Local).AddTicks(8266), new Guid("f5349fc2-2df4-4880-8c99-4d886da77cd5") });
        }
    }
}
