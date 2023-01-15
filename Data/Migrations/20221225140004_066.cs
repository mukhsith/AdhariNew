using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _066 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentLinks");

            migrationBuilder.AddColumn<bool>(
                name: "ForQuickPay",
                table: "PaymentMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "QuickPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentRequestTypeId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    PaymentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: true),
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
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    CustomerIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuickPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuickPayments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8314));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8321));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8565));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8571));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(8622));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 992, DateTimeKind.Local).AddTicks(9238));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(36));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(59));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 993, DateTimeKind.Local).AddTicks(89));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 991, DateTimeKind.Local).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 991, DateTimeKind.Local).AddTicks(2957));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 17, 0, 3, 991, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 25, 17, 0, 3, 999, DateTimeKind.Local).AddTicks(6646), new Guid("7935fc16-09b9-47ff-88bf-ad8900463e80") });

            migrationBuilder.CreateIndex(
                name: "IX_QuickPayments_PaymentMethodId",
                table: "QuickPayments",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuickPayments");

            migrationBuilder.DropColumn(
                name: "ForQuickPay",
                table: "PaymentMethods");

            migrationBuilder.CreateTable(
                name: "PaymentLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BankServiceCharge = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditCardType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidCurrencyValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentAuth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentError = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    PaymentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentRefId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentRequestTypeId = table.Column<int>(type: "int", nullable: false),
                    PaymentResult = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: true),
                    PaymentTrackId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentTransId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentLinks_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(1005));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(1013));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(1247));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(1270));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(1272));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(1848));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(2625));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(2647));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(2663));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 429, DateTimeKind.Local).AddTicks(2678));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 427, DateTimeKind.Local).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 427, DateTimeKind.Local).AddTicks(8632));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 12, 25, 14, 40, 52, 427, DateTimeKind.Local).AddTicks(8635));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 12, 25, 14, 40, 52, 435, DateTimeKind.Local).AddTicks(3503), new Guid("4cf86db0-ca57-40bc-a6f3-43966311f9dd") });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentLinks_PaymentMethodId",
                table: "PaymentLinks",
                column: "PaymentMethodId");
        }
    }
}
