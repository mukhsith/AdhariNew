using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SnapchatLink",
                table: "SocialMediaLinks",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiktokLink",
                table: "SocialMediaLinks",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppLink",
                table: "SocialMediaLinks",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3478));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3496));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3978));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(5259));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7198));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7275));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 685, DateTimeKind.Local).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 682, DateTimeKind.Local).AddTicks(6622));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 682, DateTimeKind.Local).AddTicks(6925));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 21, 15, 4, 5, 682, DateTimeKind.Local).AddTicks(6928));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 4, 5, 695, DateTimeKind.Local).AddTicks(1384), new Guid("d0a11920-7e28-49a6-9e3e-af3c468e2289") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SnapchatLink",
                table: "SocialMediaLinks");

            migrationBuilder.DropColumn(
                name: "TiktokLink",
                table: "SocialMediaLinks");

            migrationBuilder.DropColumn(
                name: "WhatsAppLink",
                table: "SocialMediaLinks");

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(5958));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(5971));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(6396));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(6408));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(6411));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(7379));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(8822));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(8980));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(9025));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 843, DateTimeKind.Local).AddTicks(9059));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 841, DateTimeKind.Local).AddTicks(6244));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 841, DateTimeKind.Local).AddTicks(6679));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 8, 11, 15, 27, 841, DateTimeKind.Local).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 8, 11, 15, 27, 854, DateTimeKind.Local).AddTicks(848), new Guid("02533ee5-d66a-44be-bfc3-d63ae5d12d84") });
        }
    }
}
