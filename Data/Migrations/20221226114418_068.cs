using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _068 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "QuickPayments",
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
                value: new DateTime(2022, 12, 26, 14, 44, 17, 943, DateTimeKind.Local).AddTicks(9042));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 943, DateTimeKind.Local).AddTicks(9051));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 943, DateTimeKind.Local).AddTicks(9278));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 943, DateTimeKind.Local).AddTicks(9284));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 943, DateTimeKind.Local).AddTicks(9287));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 943, DateTimeKind.Local).AddTicks(9850));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 944, DateTimeKind.Local).AddTicks(614));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 944, DateTimeKind.Local).AddTicks(636));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 944, DateTimeKind.Local).AddTicks(651));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 944, DateTimeKind.Local).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 942, DateTimeKind.Local).AddTicks(6682));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 942, DateTimeKind.Local).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 44, 17, 942, DateTimeKind.Local).AddTicks(6916));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 26, 14, 44, 17, 949, DateTimeKind.Local).AddTicks(89), new Guid("f446a5d4-f59c-46c3-82d4-0b8b8882815a") });

            migrationBuilder.AddForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "QuickPayments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(6816));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7093));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9160));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9176));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 324, DateTimeKind.Local).AddTicks(9190));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 322, DateTimeKind.Local).AddTicks(8668));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 322, DateTimeKind.Local).AddTicks(8969));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 26, 14, 42, 40, 322, DateTimeKind.Local).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 26, 14, 42, 40, 330, DateTimeKind.Local).AddTicks(5537), new Guid("1112e2ed-5c5b-42ef-b2ed-3fdd7107442f") });

            migrationBuilder.AddForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
