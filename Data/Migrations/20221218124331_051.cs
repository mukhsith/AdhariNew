using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeSlotId",
                table: "Orders",
                newName: "DeliveryTimeSlotId");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrderAddressId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionStatusId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    B2BPrice = table.Column<bool>(type: "bit", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountValueApplied = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CouponDiscountType = table.Column<int>(type: "int", nullable: false),
                    CouponDiscountValueApplied = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: true),
                    CouponDiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    FreeDelivery = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    WalletUsedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CashbackAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    ReceivedCashbackAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CustomerLanguageId = table.Column<int>(type: "int", nullable: false),
                    CustomerIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentResult = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentAuth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentRefId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentTrackId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentTransId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditCardType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentError = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaidCurrencyValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankServiceCharge = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryTimeSlotId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Subscriptions_OrderAddresses_OrderAddressId",
                        column: x => x.OrderAddressId,
                        principalTable: "OrderAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Subscriptions_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionItemDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ChildProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionItemDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionItemDetails_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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
                name: "IX_Orders_DeliveryTimeSlotId",
                table: "Orders",
                column: "DeliveryTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DriverId",
                table: "Orders",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionItemDetails_SubscriptionId",
                table: "SubscriptionItemDetails",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CouponId",
                table: "Subscriptions",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CustomerId",
                table: "Subscriptions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_OrderAddressId",
                table: "Subscriptions",
                column: "OrderAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PaymentMethodId",
                table: "Subscriptions",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Orders",
                column: "DeliveryTimeSlotId",
                principalTable: "DeliveryTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SystemUsers_DriverId",
                table: "Orders",
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
                name: "FK_Orders_SystemUsers_DriverId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "SubscriptionItemDetails");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryTimeSlotId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DriverId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryTimeSlotId",
                table: "Orders",
                newName: "TimeSlotId");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(3357));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(3371));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(3816));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(3828));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(4997));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(6509));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(6551));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(6599));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 692, DateTimeKind.Local).AddTicks(6649));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 690, DateTimeKind.Local).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 690, DateTimeKind.Local).AddTicks(4053));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 18, 12, 22, 11, 690, DateTimeKind.Local).AddTicks(4056));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 18, 12, 22, 11, 698, DateTimeKind.Local).AddTicks(4630), new Guid("2a395a33-fb2e-40b1-86c9-fcfe9afd08d3") });
        }
    }
}
