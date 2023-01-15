using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _064 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOrders_PaymentMethods_PaymentMethodId",
                table: "SubscriptionOrders");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentStatusId",
                table: "SubscriptionOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "SubscriptionOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 828, DateTimeKind.Local).AddTicks(9097));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 828, DateTimeKind.Local).AddTicks(9105));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 828, DateTimeKind.Local).AddTicks(9338));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 828, DateTimeKind.Local).AddTicks(9344));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 828, DateTimeKind.Local).AddTicks(9346));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 828, DateTimeKind.Local).AddTicks(9955));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 829, DateTimeKind.Local).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 829, DateTimeKind.Local).AddTicks(752));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 829, DateTimeKind.Local).AddTicks(766));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 829, DateTimeKind.Local).AddTicks(779));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 826, DateTimeKind.Local).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 826, DateTimeKind.Local).AddTicks(9675));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 13, 23, 5, 826, DateTimeKind.Local).AddTicks(9678));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 25, 13, 23, 5, 834, DateTimeKind.Local).AddTicks(3331), new Guid("8f185501-f58a-4d51-8061-73211cdedffe") });

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOrders_PaymentMethods_PaymentMethodId",
                table: "SubscriptionOrders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOrders_PaymentMethods_PaymentMethodId",
                table: "SubscriptionOrders");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentStatusId",
                table: "SubscriptionOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "SubscriptionOrders",
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
                value: new DateTime(2022, 12, 22, 16, 57, 9, 105, DateTimeKind.Local).AddTicks(9750));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 105, DateTimeKind.Local).AddTicks(9759));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(4));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(10));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(607));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(1409));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 106, DateTimeKind.Local).AddTicks(1439));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 104, DateTimeKind.Local).AddTicks(2798));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 104, DateTimeKind.Local).AddTicks(3028));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 22, 16, 57, 9, 104, DateTimeKind.Local).AddTicks(3030));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 22, 16, 57, 9, 111, DateTimeKind.Local).AddTicks(9173), new Guid("9f11a7d9-7f5c-480f-b4fe-9c08a72bf742") });

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOrders_PaymentMethods_PaymentMethodId",
                table: "SubscriptionOrders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
