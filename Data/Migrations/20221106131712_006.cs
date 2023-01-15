using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_SystemUserId",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBlockedDates_SystemUserId",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropColumn(
                name: "BlockedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropColumn(
                name: "SystemUserId",
                table: "DeliveryBlockedDates");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "DeliveryBlockedDates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBlockedDates_CreatedBy",
                table: "DeliveryBlockedDates",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBlockedDates_ModifiedBy",
                table: "DeliveryBlockedDates",
                column: "ModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_CreatedBy",
                table: "DeliveryBlockedDates",
                column: "CreatedBy",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_ModifiedBy",
                table: "DeliveryBlockedDates",
                column: "ModifiedBy",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_CreatedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_ModifiedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBlockedDates_CreatedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBlockedDates_ModifiedBy",
                table: "DeliveryBlockedDates");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "DeliveryBlockedDates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "BlockedBy",
                table: "DeliveryBlockedDates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemUserId",
                table: "DeliveryBlockedDates",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(174));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(187));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(674));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3327));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3369));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3401));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3433));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 907, DateTimeKind.Local).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 907, DateTimeKind.Local).AddTicks(8397));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 907, DateTimeKind.Local).AddTicks(8401));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 3, 17, 38, 56, 917, DateTimeKind.Local).AddTicks(5796), new Guid("d6bddd8d-343a-4a0d-92f3-8487b9a51476") });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBlockedDates_SystemUserId",
                table: "DeliveryBlockedDates",
                column: "SystemUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBlockedDates_SystemUsers_SystemUserId",
                table: "DeliveryBlockedDates",
                column: "SystemUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
