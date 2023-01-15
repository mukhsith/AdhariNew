using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _070 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TabbyPaymentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TabbySessionId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(4944));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(4958));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(5313));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(5324));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(6032));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(7109));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(7134));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(7149));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 26, DateTimeKind.Local).AddTicks(7163));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 24, DateTimeKind.Local).AddTicks(2001));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 24, DateTimeKind.Local).AddTicks(2515));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 28, 14, 50, 11, 24, DateTimeKind.Local).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 28, 14, 50, 11, 34, DateTimeKind.Local).AddTicks(8792), new Guid("7eec0ca5-c957-45e3-8ab4-baedb82bddb1") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TabbyPaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TabbySessionId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 917, DateTimeKind.Local).AddTicks(9203));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 917, DateTimeKind.Local).AddTicks(9211));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 917, DateTimeKind.Local).AddTicks(9442));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 917, DateTimeKind.Local).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 917, DateTimeKind.Local).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 918, DateTimeKind.Local).AddTicks(254));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 918, DateTimeKind.Local).AddTicks(1637));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 918, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 918, DateTimeKind.Local).AddTicks(1806));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 918, DateTimeKind.Local).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 916, DateTimeKind.Local).AddTicks(6625));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 916, DateTimeKind.Local).AddTicks(6853));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 57, 34, 916, DateTimeKind.Local).AddTicks(6855));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 26, 14, 57, 34, 923, DateTimeKind.Local).AddTicks(6774), new Guid("c86a9af0-13f2-4278-802a-7cd89dd6f44c") });
        }
    }
}
