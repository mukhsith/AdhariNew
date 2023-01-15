using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _055 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_PaymentMethods_PaymentMethodId",
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
                name: "IX_Subscriptions_PaymentMethodId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "BankServiceCharge",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CreditCardType",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeSlotId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaidCurrencyValue",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentAuth",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentDateTime",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentError",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentRefId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentResult",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentStatusId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentTrackId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentTransId",
                table: "Subscriptions");

            migrationBuilder.CreateTable(
                name: "SubscriptionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                    DeliveryTimeSlotId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delivered = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_DeliveryTimeSlots_DeliveryTimeSlotId",
                        column: x => x.DeliveryTimeSlotId,
                        principalTable: "DeliveryTimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_SystemUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(3143));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(3438));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(3441));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(4257));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(5352));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(5377));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(5393));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 399, DateTimeKind.Local).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 397, DateTimeKind.Local).AddTicks(563));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 397, DateTimeKind.Local).AddTicks(1108));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 14, 25, 56, 397, DateTimeKind.Local).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 20, 14, 25, 56, 406, DateTimeKind.Local).AddTicks(3830), new Guid("30989c8a-fd1a-4840-a0b6-293f86b4f73f") });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_DeliveryTimeSlotId",
                table: "SubscriptionOrders",
                column: "DeliveryTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_DriverId",
                table: "SubscriptionOrders",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_PaymentMethodId",
                table: "SubscriptionOrders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_SubscriptionId",
                table: "SubscriptionOrders",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionOrders");

            migrationBuilder.AddColumn<decimal>(
                name: "BankServiceCharge",
                table: "Subscriptions",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CreditCardNumber",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCardType",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTimeSlotId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaidCurrencyValue",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentAuth",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDateTime",
                table: "Subscriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentError",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentRefId",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentResult",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatusId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTrackId",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTransId",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(2363));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(2606));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(2613));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(2615));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(3177));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(3939));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(3962));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(3986));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 483, DateTimeKind.Local).AddTicks(4001));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 481, DateTimeKind.Local).AddTicks(9650));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 481, DateTimeKind.Local).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 20, 10, 53, 2, 481, DateTimeKind.Local).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 20, 10, 53, 2, 488, DateTimeKind.Local).AddTicks(4323), new Guid("c46e1457-0b37-4074-ba7b-1d7416fef036") });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DeliveryTimeSlotId",
                table: "Subscriptions",
                column: "DeliveryTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DriverId",
                table: "Subscriptions",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PaymentMethodId",
                table: "Subscriptions",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_DeliveryTimeSlots_DeliveryTimeSlotId",
                table: "Subscriptions",
                column: "DeliveryTimeSlotId",
                principalTable: "DeliveryTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_PaymentMethods_PaymentMethodId",
                table: "Subscriptions",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SystemUsers_DriverId",
                table: "Subscriptions",
                column: "DriverId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
