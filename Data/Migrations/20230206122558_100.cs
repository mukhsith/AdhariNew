using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DotMatrixPrinted",
                table: "SubscriptionOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotificationTunePlayed",
                table: "SubscriptionOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTries",
                table: "OTPDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DotMatrixPrinted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotificationTunePlayed",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 62, DateTimeKind.Local).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 62, DateTimeKind.Local).AddTicks(9540));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 62, DateTimeKind.Local).AddTicks(9772));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 62, DateTimeKind.Local).AddTicks(9778));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 62, DateTimeKind.Local).AddTicks(9781));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 63, DateTimeKind.Local).AddTicks(431));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 63, DateTimeKind.Local).AddTicks(1231));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 63, DateTimeKind.Local).AddTicks(1251));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 63, DateTimeKind.Local).AddTicks(1266));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 63, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 61, DateTimeKind.Local).AddTicks(6677));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 61, DateTimeKind.Local).AddTicks(6924));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 6, 15, 25, 57, 61, DateTimeKind.Local).AddTicks(6927));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 6, 15, 25, 57, 68, DateTimeKind.Local).AddTicks(3389), new Guid("e47a0227-7c0d-48ca-9e35-7cacb54305f6") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DotMatrixPrinted",
                table: "SubscriptionOrders");

            migrationBuilder.DropColumn(
                name: "NotificationTunePlayed",
                table: "SubscriptionOrders");

            migrationBuilder.DropColumn(
                name: "NumberOfTries",
                table: "OTPDetails");

            migrationBuilder.DropColumn(
                name: "DotMatrixPrinted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NotificationTunePlayed",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4277));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4645));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(5556));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6796));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6841));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6869));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6916));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 5, DateTimeKind.Local).AddTicks(4508));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 5, DateTimeKind.Local).AddTicks(4904));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 5, DateTimeKind.Local).AddTicks(4908));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 2, 15, 40, 56, 15, DateTimeKind.Local).AddTicks(777), new Guid("d6c42749-acbd-49d4-aeca-2c14ae93b120") });
        }
    }
}
