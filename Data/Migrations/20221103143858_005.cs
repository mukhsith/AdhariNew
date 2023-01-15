using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminNotifications");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "MobileNotificationMessage",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "SMSMessage",
                table: "Notifications",
                newName: "MessageEn");

            migrationBuilder.RenameColumn(
                name: "MobileNotificationTitle",
                table: "Notifications",
                newName: "MessageAr");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FromNotificationScreen",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "Notifications",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "Notifications",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdminNotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowStockEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LowStockThresholdQuantity = table.Column<int>(type: "int", nullable: false),
                    LowStockToEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LowStockCCEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewOrderNotificationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    NewOrderNotificationToEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewOrderNotificationCCEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderConfirmationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    OrderConfirmationToEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderConfirmationCCEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminNotificationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerGuidValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CouponId = table.Column<int>(type: "int", nullable: true),
                    DeliveryTypeId = table.Column<int>(type: "int", nullable: true),
                    DeliveryOptionId = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryTimeSlotId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartAttributes_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartAttributes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartAttributes_DeliveryTimeSlots_DeliveryTimeSlotId",
                        column: x => x.DeliveryTimeSlotId,
                        principalTable: "DeliveryTimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CustomerGuidValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ProductDetailId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    HoldUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartItems_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartItems_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Company_SenderIDs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID_ID = table.Column<int>(type: "int", nullable: false),
                    SenderID_CompanyID = table.Column<int>(type: "int", nullable: false),
                    SenderID_Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderID_TotalSMS = table.Column<int>(type: "int", nullable: false),
                    SenderID_UsedSMS = table.Column<int>(type: "int", nullable: false),
                    SenderID_Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_SenderIDs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PushCounter = table.Column<int>(type: "int", nullable: false),
                    Logged = table.Column<bool>(type: "bit", nullable: false),
                    NotificationDisabled = table.Column<bool>(type: "bit", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_DeviceTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTokens_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "NotificationReaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationReaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationReaders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_NotificationReaders_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMSMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNotificationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNotificationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FloorNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FlatNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderAddresses_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OTPDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destination = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OTPValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OTPValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CustomerRegisterRequestId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OTPDetails_CustomerRegisterRequests_CustomerRegisterRequestId",
                        column: x => x.CustomerRegisterRequestId,
                        principalTable: "CustomerRegisterRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OTPDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "QueuedEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyHtml = table.Column<bool>(type: "bit", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentFilePaths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentFileNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuedEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMS_Pushes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Push_ID = table.Column<long>(type: "bigint", nullable: false),
                    Push_MessageID = table.Column<long>(type: "bigint", nullable: false),
                    Push_UserID = table.Column<long>(type: "bigint", nullable: false),
                    Push_ANI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push_DNIS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push_OperatorID = table.Column<byte>(type: "tinyint", nullable: false),
                    Push_Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push_Lang = table.Column<byte>(type: "tinyint", nullable: false),
                    Push_ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Push_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Push_SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Push_Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push_TransactionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push_NetPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push_RejectedNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Push_Retry = table.Column<byte>(type: "tinyint", nullable: false),
                    Push_Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS_Pushes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CustomerGuidValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderAddressId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CouponDiscountType = table.Column<int>(type: "int", nullable: false),
                    CouponDiscountValueApplied = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: true),
                    CouponDiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    FreeDelivery = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                    PaymentError = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaidCurrencyValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSlotId = table.Column<int>(type: "int", nullable: true),
                    DeliveryTimeEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTimeAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyFatoorahInvoiceId = table.Column<int>(type: "int", nullable: false),
                    AramexTransactionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CouponAppliedOnItems = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_OrderAddresses_OrderAddressId",
                        column: x => x.OrderAddressId,
                        principalTable: "OrderAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    B2BPrice = table.Column<bool>(type: "bit", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountValueApplied = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CouponDiscountType = table.Column<int>(type: "int", nullable: false),
                    CouponDiscountValueApplied = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CouponDiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(174));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(187));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.UpdateData(
                table: "SystemUserPermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(674));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3327));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3369));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3401));

            migrationBuilder.UpdateData(
                table: "SystemUserRolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 910, DateTimeKind.Local).AddTicks(3433));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 907, DateTimeKind.Local).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 907, DateTimeKind.Local).AddTicks(8397));

            migrationBuilder.UpdateData(
                table: "SystemUserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 3, 17, 38, 56, 907, DateTimeKind.Local).AddTicks(8401));

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GUID" },
                values: new object[] { new DateTime(2022, 11, 3, 17, 38, 56, 917, DateTimeKind.Local).AddTicks(5796), new Guid("d6bddd8d-343a-4a0d-92f3-8487b9a51476") });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CustomerId",
                table: "Notifications",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_CountryId",
                table: "Areas",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAttributes_CouponId",
                table: "CartAttributes",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAttributes_CustomerId",
                table: "CartAttributes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAttributes_DeliveryTimeSlotId",
                table: "CartAttributes",
                column: "DeliveryTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CountryId",
                table: "CartItems",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CustomerId",
                table: "CartItems",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductDetailId",
                table: "CartItems",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTokens_CustomerId",
                table: "DeviceTokens",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationReaders_CustomerId",
                table: "NotificationReaders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationReaders_NotificationId",
                table: "NotificationReaders",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_AddressId",
                table: "OrderAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_AreaId",
                table: "OrderAddresses",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CountryId",
                table: "Orders",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderAddressId",
                table: "Orders",
                column: "OrderAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OTPDetails_CustomerId",
                table: "OTPDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OTPDetails_CustomerRegisterRequestId",
                table: "OTPDetails",
                column: "CustomerRegisterRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Countries_CountryId",
                table: "Areas",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Customers_CustomerId",
                table: "Notifications",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Countries_CountryId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Customers_CustomerId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "AdminNotificationTemplates");

            migrationBuilder.DropTable(
                name: "CartAttributes");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Company_SenderIDs");

            migrationBuilder.DropTable(
                name: "DeviceTokens");

            migrationBuilder.DropTable(
                name: "NotificationReaders");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OTPDetails");

            migrationBuilder.DropTable(
                name: "QueuedEmails");

            migrationBuilder.DropTable(
                name: "SMS_Pushes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CustomerId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Areas_CountryId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "FromNotificationScreen",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "MessageEn",
                table: "Notifications",
                newName: "SMSMessage");

            migrationBuilder.RenameColumn(
                name: "MessageAr",
                table: "Notifications",
                newName: "MobileNotificationTitle");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Notifications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNotificationMessage",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LowStockCCEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LowStockEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LowStockThresholdQuantity = table.Column<int>(type: "int", nullable: false),
                    LowStockToEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewOrderNotificationCCEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewOrderNotificationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    NewOrderNotificationToEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderConfirmationCCEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderConfirmationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    OrderConfirmationToEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminNotifications", x => x.Id);
                });

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
        }
    }
}
