using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSTemplates");

            migrationBuilder.DropColumn(
                name: "NewEmailAddress",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "SMSMessage",
                table: "NotificationTemplates",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "MobileNotificationTitle",
                table: "NotificationTemplates",
                newName: "SMSMessageEn");

            migrationBuilder.RenameColumn(
                name: "MobileNotificationMessage",
                table: "NotificationTemplates",
                newName: "SMSMessageAr");

            migrationBuilder.AddColumn<bool>(
                name: "EmailEnabled",
                table: "NotificationTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmailMessageAr",
                table: "NotificationTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailMessageEn",
                table: "NotificationTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PushEnabled",
                table: "NotificationTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PushMessageAr",
                table: "NotificationTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PushMessageEn",
                table: "NotificationTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SMSEnabled",
                table: "NotificationTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "NotificationTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NewMobileNumber",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailEnabled",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "EmailMessageAr",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "EmailMessageEn",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "PushEnabled",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "PushMessageAr",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "PushMessageEn",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "SMSEnabled",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "NewMobileNumber",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "NotificationTemplates",
                newName: "SMSMessage");

            migrationBuilder.RenameColumn(
                name: "SMSMessageEn",
                table: "NotificationTemplates",
                newName: "MobileNotificationTitle");

            migrationBuilder.RenameColumn(
                name: "SMSMessageAr",
                table: "NotificationTemplates",
                newName: "MobileNotificationMessage");

            migrationBuilder.AddColumn<string>(
                name: "NewEmailAddress",
                table: "Customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SMSTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SMSNotificationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SMSNotificationMessageAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSNotificationMessageEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSOrderConfirmationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SMSOrderConfirmationMessageAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSOrderConfirmationMessageEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSPaymentFailedEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SMSPaymentFailedMessageAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSPaymentFailedMessageEn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSTemplates", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(8665));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(9090));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 794, DateTimeKind.Local).AddTicks(9094));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(175));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1493));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1531));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 795, DateTimeKind.Local).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 792, DateTimeKind.Local).AddTicks(8827));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 792, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 17, 21, 59, 792, DateTimeKind.Local).AddTicks(9344));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 6, 17, 21, 59, 802, DateTimeKind.Local).AddTicks(1228), new Guid("292dc631-c1e1-4792-8035-a01457ce7e6c") });
        }
    }
}
