using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CartItems_Countries_CountryId",
            //    table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderAddresses_Addresses_AddressId",
                table: "OrderAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderAddresses_AddressId",
                table: "OrderAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CountryId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "AddressDetails",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "IDNumber",
                table: "OrderAddresses",
                newName: "SchoolName");

            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityId",
                table: "ProductStockHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityTypeId",
                table: "ProductStockHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "BankServiceCharge",
                table: "Orders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashbackAmount",
                table: "Orders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DeviceTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "WalletUsedAmount",
                table: "Orders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avenue",
                table: "OrderAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "OrderAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GovernmentEntity",
                table: "OrderAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MosqueName",
                table: "OrderAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
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
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(6718));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(6726));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(6961));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(6968));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(6970));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(7693));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(8523));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(8539));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 219, DateTimeKind.Local).AddTicks(8553));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 218, DateTimeKind.Local).AddTicks(4019));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 218, DateTimeKind.Local).AddTicks(4255));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 14, 47, 36, 218, DateTimeKind.Local).AddTicks(4258));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 8, 14, 47, 36, 224, DateTimeKind.Local).AddTicks(8830), new Guid("b2056ac0-b162-4c8d-b9db-793ca18d65be") });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_CustomerId",
                table: "OrderAddresses",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAddresses_Customers_CustomerId",
                table: "OrderAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderAddresses_Customers_CustomerId",
                table: "OrderAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderAddresses_CustomerId",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                table: "ProductStockHistories");

            migrationBuilder.DropColumn(
                name: "RelatedEntityTypeId",
                table: "ProductStockHistories");

            migrationBuilder.DropColumn(
                name: "BankServiceCharge",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CashbackAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeviceTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WalletUsedAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Avenue",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "GovernmentEntity",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "MosqueName",
                table: "OrderAddresses");

            migrationBuilder.RenameColumn(
                name: "SchoolName",
                table: "OrderAddresses",
                newName: "IDNumber");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressDetails",
                table: "OrderAddresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6397));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6408));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6711));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6719));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8778));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8812));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8837));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 401, DateTimeKind.Local).AddTicks(8859));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 399, DateTimeKind.Local).AddTicks(9226));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 399, DateTimeKind.Local).AddTicks(9534));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 8, 9, 6, 43, 399, DateTimeKind.Local).AddTicks(9538));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 8, 9, 6, 43, 408, DateTimeKind.Local).AddTicks(7750), new Guid("d10d01b1-f18a-461c-9707-0d24fe475d40") });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_AddressId",
                table: "OrderAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CountryId",
                table: "CartItems",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Countries_CountryId",
                table: "CartItems",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAddresses_Addresses_AddressId",
                table: "OrderAddresses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
