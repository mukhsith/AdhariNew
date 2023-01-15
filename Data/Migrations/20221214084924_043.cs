using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _043 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guest",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Guest",
                table: "CustomerRegisterRequests");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 570, DateTimeKind.Local).AddTicks(8184));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 570, DateTimeKind.Local).AddTicks(8195));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 570, DateTimeKind.Local).AddTicks(8549));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 570, DateTimeKind.Local).AddTicks(8559));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 570, DateTimeKind.Local).AddTicks(8562));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 570, DateTimeKind.Local).AddTicks(9540));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 571, DateTimeKind.Local).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 571, DateTimeKind.Local).AddTicks(736));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 571, DateTimeKind.Local).AddTicks(761));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 571, DateTimeKind.Local).AddTicks(785));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 569, DateTimeKind.Local).AddTicks(3583));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 569, DateTimeKind.Local).AddTicks(3819));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 14, 11, 49, 23, 569, DateTimeKind.Local).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 14, 11, 49, 23, 578, DateTimeKind.Local).AddTicks(7205), new Guid("d72d1047-83e0-49d5-8241-08076781db19") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Guest",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Guest",
                table: "CustomerRegisterRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(7866));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(8113));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(8702));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(9533));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(9568));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 954, DateTimeKind.Local).AddTicks(9585));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 953, DateTimeKind.Local).AddTicks(5180));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 953, DateTimeKind.Local).AddTicks(5438));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 13, 15, 24, 25, 953, DateTimeKind.Local).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 13, 15, 24, 25, 961, DateTimeKind.Local).AddTicks(984), new Guid("5f28ba43-2029-4c00-9a3d-270bc86f355c") });
        }
    }
}
