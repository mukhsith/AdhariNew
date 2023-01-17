using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _85 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtherPaymentMethodId",
                table: "SubscriptionAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OtherPaymentMethodId",
                table: "CartAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(7448));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(7690));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(8298));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(9100));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(9116));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 469, DateTimeKind.Local).AddTicks(9129));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 468, DateTimeKind.Local).AddTicks(4797));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 468, DateTimeKind.Local).AddTicks(5032));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 16, 9, 29, 34, 468, DateTimeKind.Local).AddTicks(5035));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 16, 9, 29, 34, 475, DateTimeKind.Local).AddTicks(1341), new Guid("5feda43c-6521-4f95-86f2-61e25806206c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherPaymentMethodId",
                table: "SubscriptionAttributes");

            migrationBuilder.DropColumn(
                name: "OtherPaymentMethodId",
                table: "CartAttributes");

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
    }
}
