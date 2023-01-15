using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _030 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "CartAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6397));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6408));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6711));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6719));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8778));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8812));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8837));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8859));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 399, DateTimeKind.Local).AddTicks(9226));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 399, DateTimeKind.Local).AddTicks(9534));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 399, DateTimeKind.Local).AddTicks(9538));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 8, 9, 6, 43, 408, DateTimeKind.Local).AddTicks(7750), new Guid("d10d01b1-f18a-461c-9707-0d24fe475d40") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "CartAttributes");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(7528));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(7537));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(7768));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(7775));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(7778));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(8470));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(9247));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(9270));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(9286));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 760, DateTimeKind.Local).AddTicks(9302));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 759, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 759, DateTimeKind.Local).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 15, 2, 34, 759, DateTimeKind.Local).AddTicks(2995));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 7, 15, 2, 34, 766, DateTimeKind.Local).AddTicks(1642), new Guid("c26ca886-e8fc-4602-8209-850d9f7e28ac") });
        }
    }
}
