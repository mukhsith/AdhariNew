using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _99 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsAppNumber",
                table: "ContactDetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4277));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4645));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(5556));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6796));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6841));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6869));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 7, DateTimeKind.Local).AddTicks(6916));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 5, DateTimeKind.Local).AddTicks(4508));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 5, DateTimeKind.Local).AddTicks(4904));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 2, 15, 40, 56, 5, DateTimeKind.Local).AddTicks(4908));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 2, 15, 40, 56, 15, DateTimeKind.Local).AddTicks(777), new Guid("d6c42749-acbd-49d4-aeca-2c14ae93b120") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsAppNumber",
                table: "ContactDetails");

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
    }
}
