using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStockHistories_SystemUsers_SystemUserId",
                table: "ProductStockHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductStockHistories_SystemUserId",
                table: "ProductStockHistories");

            migrationBuilder.DropColumn(
                name: "SystemUserId",
                table: "ProductStockHistories");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 493, DateTimeKind.Local).AddTicks(7416));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 493, DateTimeKind.Local).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 493, DateTimeKind.Local).AddTicks(8044));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 493, DateTimeKind.Local).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 493, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 494, DateTimeKind.Local).AddTicks(488));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 494, DateTimeKind.Local).AddTicks(2859));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 494, DateTimeKind.Local).AddTicks(3070));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 494, DateTimeKind.Local).AddTicks(3136));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 494, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 490, DateTimeKind.Local).AddTicks(9177));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 490, DateTimeKind.Local).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 2, 13, 55, 23, 490, DateTimeKind.Local).AddTicks(9798));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 2, 13, 55, 23, 502, DateTimeKind.Local).AddTicks(652), new Guid("80360dad-1ce2-4623-9f10-9be77df97904") });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockHistories_CreatedBy",
                table: "ProductStockHistories",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStockHistories_SystemUsers_CreatedBy",
                table: "ProductStockHistories",
                column: "CreatedBy",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStockHistories_SystemUsers_CreatedBy",
                table: "ProductStockHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductStockHistories_CreatedBy",
                table: "ProductStockHistories");

            migrationBuilder.AddColumn<int>(
                name: "SystemUserId",
                table: "ProductStockHistories",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(2977));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(3405));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(3416));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(4385));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(5791));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(5830));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(5859));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 348, DateTimeKind.Local).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 346, DateTimeKind.Local).AddTicks(3023));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 346, DateTimeKind.Local).AddTicks(3456));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 1, 18, 4, 3, 346, DateTimeKind.Local).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 1, 18, 4, 3, 356, DateTimeKind.Local).AddTicks(9100), new Guid("1bf39188-b5ad-48dd-b5d6-26225ed37f1c") });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockHistories_SystemUserId",
                table: "ProductStockHistories",
                column: "SystemUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStockHistories_SystemUsers_SystemUserId",
                table: "ProductStockHistories",
                column: "SystemUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
