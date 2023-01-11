using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _053 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_OrderAddresses_OrderAddressId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "OrderAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_OrderAddressId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "OrderAddressId",
                table: "Subscriptions",
                newName: "DurationId");

            migrationBuilder.RenameColumn(
                name: "OrderAddressId",
                table: "Orders",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderAddressId",
                table: "Orders",
                newName: "IX_Orders_AddressId");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryDateId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubscriptionHoldings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    HoldUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionHoldings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionHoldings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SubscriptionHoldings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(1));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(10));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(255));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(263));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(1619));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(2691));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(2719));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(2741));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 59, DateTimeKind.Local).AddTicks(2762));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 57, DateTimeKind.Local).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 57, DateTimeKind.Local).AddTicks(3319));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 19, 11, 31, 59, 57, DateTimeKind.Local).AddTicks(3323));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 19, 11, 31, 59, 66, DateTimeKind.Local).AddTicks(1381), new Guid("5307ea2f-4be0-4409-b5c9-48c114251f81") });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AddressId",
                table: "Subscriptions",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionHoldings_CustomerId",
                table: "SubscriptionHoldings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionHoldings_ProductId",
                table: "SubscriptionHoldings",
                column: "ProductId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_Addresses_AddressId",
            //    table: "Orders",
            //    column: "AddressId",
            //    principalTable: "Addresses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Addresses_AddressId",
                table: "Subscriptions",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_Addresses_AddressId",
            //    table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Addresses_AddressId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionHoldings");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_AddressId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeliveryDateId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "DurationId",
                table: "Subscriptions",
                newName: "OrderAddressId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Orders",
                newName: "OrderAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                newName: "IX_Orders_OrderAddressId");

            migrationBuilder.CreateTable(
                name: "OrderAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Avenue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Block = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FlatNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FloorNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GovernmentEntity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MosqueName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAddresses_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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
                name: "IX_Subscriptions_OrderAddressId",
                table: "Subscriptions",
                column: "OrderAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_AreaId",
                table: "OrderAddresses",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_CustomerId",
                table: "OrderAddresses",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressId",
                table: "Orders",
                column: "OrderAddressId",
                principalTable: "OrderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_OrderAddresses_OrderAddressId",
                table: "Subscriptions",
                column: "OrderAddressId",
                principalTable: "OrderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
