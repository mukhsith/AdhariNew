using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _079 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyPaymentAmount",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "SubscriptionOrders",
                newName: "Total");

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryFee",
                table: "SubscriptionOrders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "SubscriptionOrders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(6916));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(6925));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(7155));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(7161));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(7164));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(8504));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(8524));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(8538));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 304, DateTimeKind.Local).AddTicks(8552));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 303, DateTimeKind.Local).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 303, DateTimeKind.Local).AddTicks(4098));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 13, 17, 7, 303, DateTimeKind.Local).AddTicks(4101));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 3, 13, 17, 7, 309, DateTimeKind.Local).AddTicks(8254), new Guid("7e08f3bf-6db6-4760-b4b8-c6a3b8eed686") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                table: "SubscriptionOrders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "SubscriptionOrders");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "SubscriptionOrders",
                newName: "Amount");

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyPaymentAmount",
                table: "Subscriptions",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

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
    }
}
