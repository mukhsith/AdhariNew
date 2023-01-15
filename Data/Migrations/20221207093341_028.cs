using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartAttributes_Coupons_CouponId",
                table: "CartAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_CartAttributes_Customers_CustomerId",
                table: "CartAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_CartAttributes_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "CartAttributes");

            migrationBuilder.DropIndex(
                name: "IX_CartAttributes_CouponId",
                table: "CartAttributes");

            migrationBuilder.DropIndex(
                name: "IX_CartAttributes_CustomerId",
                table: "CartAttributes");

            migrationBuilder.DropIndex(
                name: "IX_CartAttributes_DeliveryTimeSlotId",
                table: "CartAttributes");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "CartAttributes");

            migrationBuilder.DropColumn(
                name: "CustomerGuidValue",
                table: "CartAttributes");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "CartAttributes");

            migrationBuilder.DropColumn(
                name: "DeliveryOptionId",
                table: "CartAttributes");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeSlotId",
                table: "CartAttributes");

            migrationBuilder.RenameColumn(
                name: "DeliveryTypeId",
                table: "CartAttributes",
                newName: "AddressId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CartAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WalletUsedAmount",
                table: "CartAttributes",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(3987));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(3999));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(4390));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(4403));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(4406));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(5383));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(6814));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(6860));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(6887));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 761, DateTimeKind.Local).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 759, DateTimeKind.Local).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 759, DateTimeKind.Local).AddTicks(8154));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 7, 12, 33, 40, 759, DateTimeKind.Local).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 7, 12, 33, 40, 768, DateTimeKind.Local).AddTicks(941), new Guid("314e01b4-94fe-4ef3-a475-76615bccfe8a") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletUsedAmount",
                table: "CartAttributes");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "CartAttributes",
                newName: "DeliveryTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CartAttributes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "CartAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerGuidValue",
                table: "CartAttributes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "CartAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryOptionId",
                table: "CartAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTimeSlotId",
                table: "CartAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(4387));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(4396));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(5241));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(5988));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(6008));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(6023));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 302, DateTimeKind.Local).AddTicks(6037));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 301, DateTimeKind.Local).AddTicks(1931));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 301, DateTimeKind.Local).AddTicks(2159));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 6, 15, 45, 40, 301, DateTimeKind.Local).AddTicks(2162));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 6, 15, 45, 40, 307, DateTimeKind.Local).AddTicks(4687), new Guid("55663b29-0208-428b-a904-310b47a91dd1") });

            migrationBuilder.CreateIndex(
                name: "IX_CartAttributes_CouponId",
                table: "CartAttributes",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAttributes_CustomerId",
                table: "CartAttributes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAttributes_DeliveryTimeSlotId",
                table: "CartAttributes",
                column: "DeliveryTimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartAttributes_Coupons_CouponId",
                table: "CartAttributes",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartAttributes_Customers_CustomerId",
                table: "CartAttributes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartAttributes_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "CartAttributes",
                column: "DeliveryTimeSlotId",
                principalTable: "DeliveryTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
