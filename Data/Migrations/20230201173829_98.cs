using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _98 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MinOrderAmount",
                table: "CompanySettings",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(6047));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(6056));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(6319));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(6326));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(6329));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(6966));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 904, DateTimeKind.Local).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 903, DateTimeKind.Local).AddTicks(250));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 903, DateTimeKind.Local).AddTicks(498));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 20, 38, 28, 903, DateTimeKind.Local).AddTicks(500));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 1, 20, 38, 28, 911, DateTimeKind.Local).AddTicks(5036), new Guid("0a8cafd9-d9d7-4b99-b245-670e17ec3b00") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinOrderAmount",
                table: "CompanySettings");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(4715));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(5097));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(5104));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(5107));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(6507));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 542, DateTimeKind.Local).AddTicks(6537));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 540, DateTimeKind.Local).AddTicks(9108));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 540, DateTimeKind.Local).AddTicks(9347));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 16, 52, 42, 540, DateTimeKind.Local).AddTicks(9350));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 1, 16, 52, 42, 548, DateTimeKind.Local).AddTicks(8545), new Guid("247663d2-1582-4c34-9e46-5ba8fba907c4") });
        }
    }
}
