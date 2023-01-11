using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _077 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullPayment",
                table: "Products",
                newName: "SpecialPackage");

            migrationBuilder.AddColumn<bool>(
                name: "SpecialPackage",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionDurationId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6002));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6011));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6266));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(6269));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(7102));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8100));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8138));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 718, DateTimeKind.Local).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 717, DateTimeKind.Local).AddTicks(522));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 717, DateTimeKind.Local).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 12, 26, 32, 717, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 2, 12, 26, 32, 724, DateTimeKind.Local).AddTicks(3624), new Guid("e28c961d-893c-4451-8730-de62618ab99a") });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubscriptionDurationId",
                table: "Products",
                column: "SubscriptionDurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubscriptionDurations_SubscriptionDurationId",
                table: "Products",
                column: "SubscriptionDurationId",
                principalTable: "SubscriptionDurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubscriptionDurations_SubscriptionDurationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubscriptionDurationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SpecialPackage",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionDurationId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SpecialPackage",
                table: "Products",
                newName: "FullPayment");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(2729));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(2738));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(2968));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(2975));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(2978));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(3542));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(4317));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(4338));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 179, DateTimeKind.Local).AddTicks(4366));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 178, DateTimeKind.Local).AddTicks(389));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 178, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 2, 9, 17, 8, 178, DateTimeKind.Local).AddTicks(623));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2023, 1, 2, 9, 17, 8, 198, DateTimeKind.Local).AddTicks(6346), new Guid("1e0a6151-0403-4abc-b764-a438c184f4ed") });
        }
    }
}
