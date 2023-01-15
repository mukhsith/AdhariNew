using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _074 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "WalletTransactions",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(8310));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(8317));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(8319));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(8960));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(9777));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 438, DateTimeKind.Local).AddTicks(9807));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 437, DateTimeKind.Local).AddTicks(5055));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 437, DateTimeKind.Local).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 29, 12, 15, 20, 437, DateTimeKind.Local).AddTicks(5302));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 29, 12, 15, 20, 444, DateTimeKind.Local).AddTicks(5192), new Guid("1eeeb338-f386-45c9-a100-0c89284243de") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "WalletTransactions");

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
    }
}
