using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_BlockedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBlockedDates_BlockedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropColumn(
                name: "BlockedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropColumn(
                name: "BlockedOn",
                table: "DeliveryBlockedDates");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(8665));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(9090));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(9094));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(175));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1493));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1531));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 792, DateTimeKind.Local).AddTicks(8827));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 792, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 792, DateTimeKind.Local).AddTicks(9344));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 6, 17, 21, 59, 802, DateTimeKind.Local).AddTicks(1228), new Guid("292dc631-c1e1-4792-8035-a01457ce7e6c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockedBy",
                table: "DeliveryBlockedDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedOn",
                table: "DeliveryBlockedDates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(3843));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(4259));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(4271));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(4274));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(6805));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(6940));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 211, DateTimeKind.Local).AddTicks(7003));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 209, DateTimeKind.Local).AddTicks(3676));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 209, DateTimeKind.Local).AddTicks(4105));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 50, 36, 209, DateTimeKind.Local).AddTicks(4109));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 6, 16, 50, 36, 219, DateTimeKind.Local).AddTicks(3341), new Guid("81ea4386-9ba1-4268-9dc7-c3c687cc788e") });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBlockedDates_BlockedBy",
                table: "DeliveryBlockedDates",
                column: "BlockedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_BlockedBy",
                table: "DeliveryBlockedDates",
                column: "BlockedBy",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
