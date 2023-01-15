using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _073 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashbackAmount",
                table: "SubscriptionAttributes");

            migrationBuilder.DropColumn(
                name: "WalletUsedAmount",
                table: "SubscriptionAttributes");

            migrationBuilder.AddColumn<bool>(
                name: "UseWalletAmount",
                table: "SubscriptionAttributes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(3620));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(3629));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(3861));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(3878));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(3881));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(4453));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(5234));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(5255));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 852, DateTimeKind.Local).AddTicks(5283));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 850, DateTimeKind.Local).AddTicks(4370));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 850, DateTimeKind.Local).AddTicks(4891));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 10, 34, 33, 850, DateTimeKind.Local).AddTicks(4895));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 29, 10, 34, 33, 858, DateTimeKind.Local).AddTicks(9198), new Guid("b4155f67-fabd-4398-a779-381d345e719d") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseWalletAmount",
                table: "SubscriptionAttributes");

            migrationBuilder.AddColumn<decimal>(
                name: "CashbackAmount",
                table: "SubscriptionAttributes",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WalletUsedAmount",
                table: "SubscriptionAttributes",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(887));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(896));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(1146));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(1709));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(2502));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(2538));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 660, DateTimeKind.Local).AddTicks(2552));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 658, DateTimeKind.Local).AddTicks(8345));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 658, DateTimeKind.Local).AddTicks(8579));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 9, 49, 37, 658, DateTimeKind.Local).AddTicks(8582));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 29, 9, 49, 37, 665, DateTimeKind.Local).AddTicks(6323), new Guid("0b9b5f09-7b33-4337-a892-fd7be9ed35ee") });
        }
    }
}
