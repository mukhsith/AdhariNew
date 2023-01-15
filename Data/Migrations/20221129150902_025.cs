using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressDetails",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "IDNumber",
                table: "Addresses",
                newName: "SchoolName");

            migrationBuilder.AddColumn<string>(
                name: "Avenue",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GovernmentEntity",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MosqueName",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(600));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(613));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(1022));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(1036));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(2134));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(3523));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(3560));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(3589));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 574, DateTimeKind.Local).AddTicks(3615));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 572, DateTimeKind.Local).AddTicks(1917));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 572, DateTimeKind.Local).AddTicks(2365));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 18, 9, 1, 572, DateTimeKind.Local).AddTicks(2369));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 29, 18, 9, 1, 581, DateTimeKind.Local).AddTicks(2661), new Guid("4f6fd2fb-0137-4e92-ac02-432dfef467ac") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avenue",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "GovernmentEntity",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "MosqueName",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "SchoolName",
                table: "Addresses",
                newName: "IDNumber");

            migrationBuilder.AddColumn<string>(
                name: "AddressDetails",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(4899));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(4912));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(5305));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(5316));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(5319));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7813));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7854));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 924, DateTimeKind.Local).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 922, DateTimeKind.Local).AddTicks(5821));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 922, DateTimeKind.Local).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 29, 17, 14, 36, 922, DateTimeKind.Local).AddTicks(6239));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 29, 17, 14, 36, 934, DateTimeKind.Local).AddTicks(998), new Guid("33865cdf-6afb-41ad-8ffe-d89f9ed1a0db") });
        }
    }
}
