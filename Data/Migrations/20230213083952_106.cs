using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SystemUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(6734));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(6741));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(6743));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(7428));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(8242));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(8264));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 80, DateTimeKind.Local).AddTicks(8294));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 79, DateTimeKind.Local).AddTicks(3909));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 79, DateTimeKind.Local).AddTicks(4147));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 11, 39, 52, 79, DateTimeKind.Local).AddTicks(4150));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 13, 11, 39, 52, 85, DateTimeKind.Local).AddTicks(7951), new Guid("3c7079a9-dd5d-4d13-b035-8cd3fec83967") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SystemUsers");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 865, DateTimeKind.Local).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 865, DateTimeKind.Local).AddTicks(8698));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 865, DateTimeKind.Local).AddTicks(8929));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 865, DateTimeKind.Local).AddTicks(8936));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 865, DateTimeKind.Local).AddTicks(8939));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 865, DateTimeKind.Local).AddTicks(9561));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 866, DateTimeKind.Local).AddTicks(336));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 866, DateTimeKind.Local).AddTicks(359));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 866, DateTimeKind.Local).AddTicks(375));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 866, DateTimeKind.Local).AddTicks(392));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 864, DateTimeKind.Local).AddTicks(6326));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 864, DateTimeKind.Local).AddTicks(6559));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 13, 9, 41, 20, 864, DateTimeKind.Local).AddTicks(6562));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 13, 9, 41, 20, 870, DateTimeKind.Local).AddTicks(8880), new Guid("0b0ebf1c-7d45-4a4c-aca4-7edcf8c95995") });
        }
    }
}
