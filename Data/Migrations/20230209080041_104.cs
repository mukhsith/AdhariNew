using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrandTotal",
                table: "Subscriptions",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrandTotal",
                table: "Orders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(6708));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(6717));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(6990));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(7010));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(7014));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(8129));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(8959));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(9002));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 916, DateTimeKind.Local).AddTicks(9017));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 914, DateTimeKind.Local).AddTicks(4675));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 914, DateTimeKind.Local).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 11, 0, 40, 914, DateTimeKind.Local).AddTicks(5400));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 9, 11, 0, 40, 923, DateTimeKind.Local).AddTicks(8174), new Guid("7c66d06d-4daf-4593-b19b-ef4586164a56") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrandTotal",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "GrandTotal",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(5784));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(5792));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6025));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6031));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7415));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7439));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 921, DateTimeKind.Local).AddTicks(7476));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 920, DateTimeKind.Local).AddTicks(3486));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 920, DateTimeKind.Local).AddTicks(3722));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 2, 9, 9, 21, 36, 920, DateTimeKind.Local).AddTicks(3724));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 2, 9, 9, 21, 36, 927, DateTimeKind.Local).AddTicks(416), new Guid("321e1c12-8d7b-4f0f-a691-1a55bb6de6ad") });
        }
    }
}
