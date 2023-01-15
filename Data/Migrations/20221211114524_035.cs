using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _035 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(3344));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(3353));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(3587));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(3593));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(3596));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(4275));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(5192));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 15, DateTimeKind.Local).AddTicks(5222));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 14, DateTimeKind.Local).AddTicks(734));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 14, DateTimeKind.Local).AddTicks(970));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 14, 45, 24, 14, DateTimeKind.Local).AddTicks(973));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 11, 14, 45, 24, 20, DateTimeKind.Local).AddTicks(7471), new Guid("2eb8ddfa-14a2-44d4-b341-5c8391128c70") });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(4414));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(4440));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(4687));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(4693));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(4696));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(5337));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(6156));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(6179));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(6196));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 522, DateTimeKind.Local).AddTicks(6212));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 520, DateTimeKind.Local).AddTicks(7004));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 520, DateTimeKind.Local).AddTicks(7499));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 11, 11, 26, 49, 520, DateTimeKind.Local).AddTicks(7505));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 11, 11, 26, 49, 528, DateTimeKind.Local).AddTicks(5971), new Guid("5f48dcac-ed9a-4fdb-87f8-1ad62b153c69") });
        }
    }
}
