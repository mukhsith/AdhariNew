using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductDetails_ProductDetailId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductDetailId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductDetailId",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(1456));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(1471));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(1892));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(1903));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(2992));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(4407));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(4450));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(4481));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 55, DateTimeKind.Local).AddTicks(4512));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 53, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 53, DateTimeKind.Local).AddTicks(2442));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 44, 44, 53, DateTimeKind.Local).AddTicks(2445));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 44, 44, 63, DateTimeKind.Local).AddTicks(3048), new Guid("fe644e50-df87-40d1-8dca-538a86f3dbac") });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
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

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductDetailId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(3587));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(3603));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(4157));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(4163));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(7255));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(7491));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 740, DateTimeKind.Local).AddTicks(7558));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 737, DateTimeKind.Local).AddTicks(7437));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 737, DateTimeKind.Local).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 24, 16, 39, 44, 737, DateTimeKind.Local).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 39, 44, 749, DateTimeKind.Local).AddTicks(8482), new Guid("7f0d16ca-a4a7-40c9-a1b2-320390b9d342") });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductDetailId",
                table: "CartItems",
                column: "ProductDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductDetails_ProductDetailId",
                table: "CartItems",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
