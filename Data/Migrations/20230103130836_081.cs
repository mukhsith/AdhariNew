using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _081 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "SystemUserPermissions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 920, DateTimeKind.Local).AddTicks(9521));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 920, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 920, DateTimeKind.Local).AddTicks(9761));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 920, DateTimeKind.Local).AddTicks(9768));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 920, DateTimeKind.Local).AddTicks(9771));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 921, DateTimeKind.Local).AddTicks(341));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 921, DateTimeKind.Local).AddTicks(1229));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 921, DateTimeKind.Local).AddTicks(1253));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 921, DateTimeKind.Local).AddTicks(1269));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 921, DateTimeKind.Local).AddTicks(1284));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 919, DateTimeKind.Local).AddTicks(7218));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 919, DateTimeKind.Local).AddTicks(7454));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 16, 8, 35, 919, DateTimeKind.Local).AddTicks(7457));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 3, 16, 8, 35, 926, DateTimeKind.Local).AddTicks(1598), new Guid("ba22f688-8f35-42e5-9fd4-e712ffba6e35") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "SystemUserPermissions");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(7141));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(7398));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(7405));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(7408));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(8062));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(8851));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(8873));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(8888));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 158, DateTimeKind.Local).AddTicks(8921));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 156, DateTimeKind.Local).AddTicks(9830));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 157, DateTimeKind.Local).AddTicks(296));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 3, 14, 42, 0, 157, DateTimeKind.Local).AddTicks(299));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 3, 14, 42, 0, 165, DateTimeKind.Local).AddTicks(8145), new Guid("7e1e622f-ad0c-4216-90f9-eebfc7e68f7a") });
        }
    }
}
