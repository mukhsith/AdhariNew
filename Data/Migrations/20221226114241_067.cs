using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _067 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FirstOrder",
                table: "SubscriptionOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "QuickPayments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(6816));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7093));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9160));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9176));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9190));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 322, DateTimeKind.Local).AddTicks(8668));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 322, DateTimeKind.Local).AddTicks(8969));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 322, DateTimeKind.Local).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 26, 14, 42, 40, 330, DateTimeKind.Local).AddTicks(5537), new Guid("1112e2ed-5c5b-42ef-b2ed-3fdd7107442f") });

            migrationBuilder.CreateIndex(
                name: "IX_QuickPayments_CustomerId",
                table: "QuickPayments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments");

            migrationBuilder.DropIndex(
                name: "IX_QuickPayments_CustomerId",
                table: "QuickPayments");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "FirstOrder",
                table: "SubscriptionOrders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "QuickPayments");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8314));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8321));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8565));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8571));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8622));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(9238));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(36));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(59));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(89));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 991, DateTimeKind.Local).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 991, DateTimeKind.Local).AddTicks(2957));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 991, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 25, 17, 0, 3, 999, DateTimeKind.Local).AddTicks(6646), new Guid("7935fc16-09b9-47ff-88bf-ad8900463e80") });
        }
    }
}
