using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Areas.Frontend.Factories;
using API.Areas.Frontend.Helpers;
using Services.Frontend.Content;
using Services.Frontend.CouponPromotion;
using Services.Frontend.CustomerManagement;
using Services.Frontend.Locations;
using Services.Frontend.ProductManagement;
using Services.Frontend.Sales;
using Services.Frontend.EmailManagement;
using Services.Frontend.SMS;
using Services.Frontend.Shop;
using Services.Frontend.PushNotification;
using Services.Frontend.DeliveryManagement;

namespace API.Extensions
{
    public class FrontendServiceExtensions
    {
        public static void RegisterService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //Content service             
            serviceCollection.AddScoped<IBannerService, BannerService>();
            serviceCollection.AddScoped<ISiteContentService, SiteContentService>();
            serviceCollection.AddScoped<IContactDetailService, ContactDetailService>();
            serviceCollection.AddScoped<ICustomerFeedbackService, CustomerFeedbackService>();
            serviceCollection.AddScoped<ISocialMediaLinkService, SocialMediaLinkService>();
            serviceCollection.AddScoped<ICompanySettingService, CompanySettingService>();
            serviceCollection.AddScoped<IPaymentMethodService, PaymentMethodService>();
            serviceCollection.AddScoped<IDeliveryBlockedDateService, DeliveryBlockedDateService>();
            serviceCollection.AddScoped<IDeliveryTimeSlotService, DeliveryTimeSlotService>();
            // Coupon Service
            serviceCollection.AddScoped<ICouponService, CouponService>();
            serviceCollection.AddScoped<IPromotionService, PromotionService>();
            serviceCollection.AddScoped<IWalletPackageService, WalletPackageService>();

            //Customer Service
            serviceCollection.AddScoped<ICustomerService, CustomerService>();

            //Email Service
            serviceCollection.AddScoped<IQueuedEmailService, QueuedEmailService>();

            //Location Service
            serviceCollection.AddScoped<IAreaService, AreaService>();
            serviceCollection.AddScoped<IGovernorateService, GovernorateService>();

            //products Service
            serviceCollection.AddScoped<ICategoryService, CategoryService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            
            //Push Servicve
            serviceCollection.AddScoped<INotificationService, NotificationService>();

            //Sales  Service
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<ISubscriptionService, SubscriptionService>();
            serviceCollection.AddScoped<IQuickPaymentService, QuickPaymentService>();

            //Shop  Service
            serviceCollection.AddScoped<ICartService, CartService>();

            //SMS Service
            serviceCollection.AddScoped<INotificationTemplateService, NotificationTemplateService>();

            //factories
            serviceCollection.AddScoped<IAppContentModelFactory, AppContentModelFactory>();
            serviceCollection.AddScoped<ICustomerModelFactory, CustomerModelFactory>();
            serviceCollection.AddScoped<ICartModelFactory, CartModelFactory>();
            serviceCollection.AddScoped<IOrderModelFactory, OrderModelFactory>();
            serviceCollection.AddScoped<IProductModelFactory, ProductModelFactory>();
            serviceCollection.AddScoped<ISubscriptionModelFactory, SubscriptionModelFactory>();

            //helpers
            serviceCollection.AddScoped<IModelHelper, ModelHelper>();
        }
    }
}
