using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _027 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SignupCashbackValue",
                table: "Promotions",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "CashbackValueToDeduct",
                table: "Promotions",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "CashbackRedeemMinWalletAmount",
                table: "Promotions",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "CashbackRedeemMinOrderAmount",
                table: "Promotions",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "CashbackOnPurchaseValue",
                table: "Promotions",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "CashbackOnPurchaseMinOrderAmount",
                table: "Promotions",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "IOSVersion",
                table: "CompanySettings",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AndroidVersion",
                table: "CompanySettings",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    WalletTypeId = table.Column<int>(type: "int", nullable: false),
                    RelatedEntityTypeId = table.Column<int>(type: "int", nullable: false),
                    RelatedEntityId = table.Column<int>(type: "int", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Customers_CustomerId",
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
                name: "IX_WalletTransactions_CustomerId",
                table: "WalletTransactions",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.AlterColumn<float>(
                name: "SignupCashbackValue",
                table: "Promotions",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<float>(
                name: "CashbackValueToDeduct",
                table: "Promotions",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<float>(
                name: "CashbackRedeemMinWalletAmount",
                table: "Promotions",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<float>(
                name: "CashbackRedeemMinOrderAmount",
                table: "Promotions",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<float>(
                name: "CashbackOnPurchaseValue",
                table: "Promotions",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<float>(
                name: "CashbackOnPurchaseMinOrderAmount",
                table: "Promotions",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "IOSVersion",
                table: "CompanySettings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AndroidVersion",
                table: "CompanySettings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 819, DateTimeKind.Local).AddTicks(9646));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 819, DateTimeKind.Local).AddTicks(9659));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(118));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(129));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(133));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(1158));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(2702));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(2749));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(2783));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 820, DateTimeKind.Local).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 817, DateTimeKind.Local).AddTicks(9862));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 818, DateTimeKind.Local).AddTicks(330));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 30, 17, 59, 39, 818, DateTimeKind.Local).AddTicks(334));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 30, 17, 59, 39, 828, DateTimeKind.Local).AddTicks(4617), new Guid("b13e21d6-e9bf-497a-b3b8-c1d7356b8740") });
        }
    }
}
