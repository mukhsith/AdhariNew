using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _105 : Migration
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "QuickPayments",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments");

            migrationBuilder.DropColumn(
                name: "Name",
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
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(6708));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(6717));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(6990));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(7010));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(7014));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(8129));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(8959));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(9002));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(9017));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 914, DateTimeKind.Local).AddTicks(4675));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 914, DateTimeKind.Local).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 914, DateTimeKind.Local).AddTicks(5400));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 9, 11, 0, 40, 923, DateTimeKind.Local).AddTicks(8174), new Guid("7c66d06d-4daf-4593-b19b-ef4586164a56") });

            migrationBuilder.AddForeignKey(
                name: "FK_QuickPayments_Customers_CustomerId",
                table: "QuickPayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
