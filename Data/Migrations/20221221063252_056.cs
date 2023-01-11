using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _056 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WalletPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    WalletAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletPackageOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    WalletPackageId = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletPackageOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletPackageOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WalletPackageOrders_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WalletPackageOrders_WalletPackages_WalletPackageId",
                        column: x => x.WalletPackageId,
                        principalTable: "WalletPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(3515));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(3521));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(3523));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(4131));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(4899));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(4930));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(4946));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 918, DateTimeKind.Local).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 916, DateTimeKind.Local).AddTicks(8187));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 916, DateTimeKind.Local).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 21, 9, 32, 51, 916, DateTimeKind.Local).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 21, 9, 32, 51, 923, DateTimeKind.Local).AddTicks(2702), new Guid("224f1c13-7301-4b65-89fc-560c166fa7b3") });

            migrationBuilder.CreateIndex(
                name: "IX_WalletPackageOrders_CustomerId",
                table: "WalletPackageOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPackageOrders_PaymentMethodId",
                table: "WalletPackageOrders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPackageOrders_WalletPackageId",
                table: "WalletPackageOrders",
                column: "WalletPackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletPackageOrders");

            migrationBuilder.DropTable(
                name: "WalletPackages");

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
        }
    }
}
