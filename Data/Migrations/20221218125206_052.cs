using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _052 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_Countries_CountryId",
            //    table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CountryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AramexTransactionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CouponAppliedOnItems",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerGuidValue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeAr",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeEn",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MyFatoorahInvoiceId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryTimeSlotId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryTimeSlotId",
                table: "Orders",
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
                value: new DateTime(2022, 12, 18, 15, 52, 5, 545, DateTimeKind.Local).AddTicks(9636));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 545, DateTimeKind.Local).AddTicks(9647));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(113));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(122));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(125));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(2233));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(2266));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(2289));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 546, DateTimeKind.Local).AddTicks(2311));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 544, DateTimeKind.Local).AddTicks(4077));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 544, DateTimeKind.Local).AddTicks(4317));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 52, 5, 544, DateTimeKind.Local).AddTicks(4320));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 18, 15, 52, 5, 553, DateTimeKind.Local).AddTicks(3404), new Guid("11891db3-9f81-4090-8414-ebab447abca4") });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DeliveryTimeSlotId",
                table: "Subscriptions",
                column: "DeliveryTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DriverId",
                table: "Subscriptions",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ProductId",
                table: "Subscriptions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Orders",
                column: "DeliveryTimeSlotId",
                principalTable: "DeliveryTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Subscriptions",
                column: "DeliveryTimeSlotId",
                principalTable: "DeliveryTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Products_ProductId",
                table: "Subscriptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SystemUsers_DriverId",
                table: "Subscriptions",
                column: "DriverId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Products_ProductId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SystemUsers_DriverId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_DeliveryTimeSlotId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_DriverId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ProductId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryTimeSlotId",
                table: "Subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryTimeSlotId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AramexTransactionId",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CouponAppliedOnItems",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomerGuidValue",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTimeAr",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTimeEn",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyFatoorahInvoiceId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(4202));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(4208));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(4211));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(4752));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(5495));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(5516));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(5531));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 719, DateTimeKind.Local).AddTicks(5546));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 718, DateTimeKind.Local).AddTicks(1712));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 718, DateTimeKind.Local).AddTicks(1944));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 15, 43, 30, 718, DateTimeKind.Local).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 18, 15, 43, 30, 725, DateTimeKind.Local).AddTicks(4432), new Guid("30f1fe6c-b5e1-4120-80c8-1532652c889f") });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CountryId",
                table: "Orders",
                column: "CountryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_Countries_CountryId",
            //    table: "Orders",
            //    column: "CountryId",
            //    principalTable: "Countries",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Orders",
                column: "DeliveryTimeSlotId",
                principalTable: "DeliveryTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
