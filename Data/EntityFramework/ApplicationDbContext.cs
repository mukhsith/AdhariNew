using Data.Content;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.DeliveryManagement;
using Data.EmailManagement;
using Data.Locations;
using Data.NotifyTemplate;
using Data.ProductManagement;
using Data.PushNotification;
using Data.Sales;
using Data.Shop;
using Data.SMS;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Data();
            base.OnModelCreating(builder);
        }

        #region User management
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<SystemUserPermission> SystemUserPermissions { get; set; }
        public virtual DbSet<SystemUserRole> SystemUserRoles { get; set; }
        public virtual DbSet<SystemUserRolePermission> SystemUserRolePermissions { get; set; }
        public virtual DbSet<SystemUserHistory> SystemUserHistories { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        #endregion

        #region Admin Content
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<SiteContent> SiteContents { get; set; }
        public virtual DbSet<ContactDetail> ContactDetails { get; set; }
        public virtual DbSet<SocialMediaLink> SocialMediaLinks { get; set; }
        public virtual DbSet<CompanySetting> CompanySettings { get; set; }
        public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public virtual DbSet<DisplayWebControl> DisplayWebControls { get; set; } //show specific menu on website
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        #endregion

        #region Product
        public virtual DbSet<Category> Categories { get; set; } //show specific menu on website
        public virtual DbSet<ItemSize> ItemSizes { get; set; } //show specific menu on website
        public virtual DbSet<Product> Products { get; set; } //show specific menu on website
        public virtual DbSet<ProductDetail> ProductDetails { get; set; } //show specific menu on website
        public virtual DbSet<ProductStockHistory> ProductStockHistories { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<ProductAvailabilityNotifyRequest> ProductAvailabilityNotifyRequests { get; set; }
        public virtual DbSet<SubscriptionDuration> SubscriptionDurations { get; set; }
        public virtual DbSet<SubscriptionDeliveryDate> SubscriptionDeliveryDates { get; set; }
        #endregion

        #region Locations  
        public virtual DbSet<Governorate> Governorates { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        #endregion

        #region Delivery Management
        public virtual DbSet<DeliveryBlockedDate> DeliveryBlockedDates { get; set; }
        public virtual DbSet<DeliveryTimeSlot> DeliveryTimeSlots { get; set; }

        #endregion

        #region Coupon & Promotion Management
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<WalletPackage> WalletPackages { get; set; }
        #endregion

        #region Customer Management
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<CustomerRegisterRequest> CustomerRegisterRequests { get; set; }
        public virtual DbSet<WalletTransaction> WalletTransactions { get; set; }
        #endregion

        #region Admin + User Notification Templates
        public virtual DbSet<AdminNotificationTemplate> AdminNotificationTemplates { get; set; }
        public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        //public virtual DbSet<SMSTemplate>  SMSTemplates { get; set; }
        #endregion

        #region Push notifications
        public virtual DbSet<DeviceToken> DeviceTokens { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationReader> NotificationReaders { get; set; }
        public virtual DbSet<PushNotificationLog> PushNotificationLogs { get; set; }
        #endregion

        #region  Sales
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderItemDetail> OrderItemDetails { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionItemDetail> SubscriptionItemDetails { get; set; }
        public virtual DbSet<SubscriptionOrder> SubscriptionOrders { get; set; }
        public virtual DbSet<WalletPackageOrder> WalletPackageOrders { get; set; }
        public virtual DbSet<QuickPayment> QuickPayments { get; set; }
        #endregion

        #region  Cart
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<CartAttribute> CartAttributes { get; set; }
        public virtual DbSet<SubscriptionAttribute> SubscriptionAttributes { get; set; }
        public virtual DbSet<SubscriptionHolding> SubscriptionHoldings { get; set; }
        #endregion

        #region  SMS
        public virtual DbSet<Company_SenderID> Company_SenderIDs { get; set; }
        public virtual DbSet<OTPDetail> OTPDetails { get; set; }
        public virtual DbSet<SMS_Push> SMS_Pushes { get; set; }
        public virtual DbSet<QueuedEmail> QueuedEmails { get; set; }

        #endregion
    }
}
