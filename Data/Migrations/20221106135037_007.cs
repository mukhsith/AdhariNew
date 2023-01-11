using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(1958));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(2492));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(2505));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(2508));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(3552));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(5042));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(5081));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 846, DateTimeKind.Local).AddTicks(5138));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 843, DateTimeKind.Local).AddTicks(3662));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 843, DateTimeKind.Local).AddTicks(4245));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 16, 17, 11, 843, DateTimeKind.Local).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 6, 16, 17, 11, 855, DateTimeKind.Local).AddTicks(9781), new Guid("3923a0de-46ec-4264-be47-efdcd0b98176") });
        }
    }
}
