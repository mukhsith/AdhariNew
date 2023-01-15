
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Areas.Backend.Factories;
using API.Areas.Backend.Helpers; 
using Services.Backend.Content;
using Services.Backend.Content.Interface;
using Services.Backend.CouponPromotion.Interface;
using Services.Backend.CustomerManagement;
using Services.Backend.DeliveryManagement.Interface;
using Services.Backend.Locations;
using Services.Backend.Locations.Interface;
using Services.Backend.ProductManagement.Interface;
using Services.Backend.SystemUserManagement; 
using Services.Backend.Template.Interface;
using Services.Backend.PushNotification;
using Services.Backend.Sales;
using Services.Backend.Shop; 

namespace API.Extensions
{
public class BackendServiceExtensions
{
        public static void RegisterService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //service 
            serviceCollection.AddScoped<IBannerService, BannerService>(); 
            serviceCollection.AddScoped<ISystemUserService, SystemUserService>();
            
            serviceCollection.AddScoped<ISiteContentService, SiteContentService>();
            serviceCollection.AddScoped<IContactDetailService, ContactDetailService>();
            serviceCollection.AddScoped<ICustomerFeedbackService, CustomerFeedbackService>();
            serviceCollection.AddScoped<ISocialMediaLinkService,  SocialMediaLinkService>();
            serviceCollection.AddScoped<IDisplayWebControlService, DisplayWebControlService>(); 
            serviceCollection.AddScoped<IPaymentMethodService, PaymentMethodService>();
            serviceCollection.AddScoped<IDeliveryBlockedDateService, DeliveryBlockedDateService>();
            serviceCollection.AddScoped<IDeliveryTimeSlotService, DeliveryTimeSlotService>();

            //Quick Payment Links
            serviceCollection.AddScoped<IQuickPaymentService, QuickPaymentService>();

            // Coupon Service
            serviceCollection.AddScoped<ICouponService, CouponService>();
            serviceCollection.AddScoped<IPromotionService, PromotionService>();

            //Customer Service
            serviceCollection.AddScoped<ICustomerService, CustomerService>();

            //Location Service
            serviceCollection.AddScoped<IAreaService, AreaService>();
            serviceCollection.AddScoped<ICountryService, CountryService>();
            serviceCollection.AddScoped<IGovernorateService, GovernorateService>();

            //products
            serviceCollection.AddScoped<ICategoryService, CategoryService>();
            serviceCollection.AddScoped<IItemSizeService, ItemSizeService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<IProductStockHistoryService, ProductStockHistoryService>();

            //Push Servicve
            serviceCollection.AddScoped<INotificationService, NotificationService>();

            //Sales  Service
            serviceCollection.AddScoped<IOrderService, OrderService>();
            
            //Subscription  Service
            serviceCollection.AddScoped<ISubscriptionService, SubscriptionService>();

            //Shop  Service
            serviceCollection.AddScoped<ICartService, CartService>();

            //SMS Service
            serviceCollection.AddScoped<INotificationTemplateService, NotificationTemplateService>();

            //Wallet Package Cards
            serviceCollection.AddScoped<IWalletPackageService, WalletPackageService>();

            //factories
            serviceCollection.AddScoped<ICustomerModelFactory, CustomerModelFactory>();
            serviceCollection.AddScoped<ICartModelFactory, CartModelFactory>();
            serviceCollection.AddScoped<IOrderModelFactory, OrderModelFactory>();
            serviceCollection.AddScoped<IProductModelFactory, ProductModelFactory>();
            serviceCollection.AddScoped<ISubscriptionModelFactory,  SubscriptionModelFactory>();
            
            //helpers
            serviceCollection.AddScoped<IModelHelper, ModelHelper>();
        }
    }
}
