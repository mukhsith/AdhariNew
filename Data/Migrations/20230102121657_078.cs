using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _078 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "SubscriptionOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(1489));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(2311));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(3084));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(3105));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 675, DateTimeKind.Local).AddTicks(3133));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 673, DateTimeKind.Local).AddTicks(8816));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 673, DateTimeKind.Local).AddTicks(9049));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 15, 16, 56, 673, DateTimeKind.Local).AddTicks(9052));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 2, 15, 16, 56, 680, DateTimeKind.Local).AddTicks(4692), new Guid("d7291c7e-3858-477d-ab36-57069e165334") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "SubscriptionOrders");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6002));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6011));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6266));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6269));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(7102));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8100));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8138));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 717, DateTimeKind.Local).AddTicks(522));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 717, DateTimeKind.Local).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 717, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 2, 12, 26, 32, 724, DateTimeKind.Local).AddTicks(3624), new Guid("e28c961d-893c-4451-8730-de62618ab99a") });
        }
    }
}
