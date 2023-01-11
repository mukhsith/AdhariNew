using AutoMapper;
using Data.Content;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.Locations;
using Data.ProductManagement;
using Data.PushNotification;
using Data.Sales;
using Data.Shop;
using Utility.Models.Admin.ProductManagement;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.Shop;

namespace API.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            //var configuration = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Product, ProductModel>()
            //      .ForMember(dest => dest.u, opt => opt.Condition(src => (src.Image.Length > 0)));
            //});

            CreateMap<ItemSize, ItemSizeModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<ItemSizeModel, ItemSize>();

            CreateMap<Category, CategoryModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<CategoryModel, Category>();

            CreateMap<Product, ProductModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<ProductModel, Product>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);



            CreateMap<Banner, BannerModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<BannerModel, Banner>();

            CreateMap<Country, CountryModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<CountryModel, Country>();

            CreateMap<Customer, CustomerModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<CustomerModel, Customer>();

            CreateMap<CustomerFeedback, CustomerFeedbackModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<CustomerFeedbackModel, CustomerFeedback>();



            CreateMap<SiteContent, SiteContentModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<SiteContentModel, SiteContent>();

            CreateMap<ContactDetail, ContactDetailModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<ContactDetailModel, ContactDetail>();

            CreateMap<SocialMediaLink, SocialMediaLinkModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<SocialMediaLinkModel, SocialMediaLink>();



            CreateMap<CartItem, CartItemModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<CartItemModel, CartItem>();


            CreateMap<Area, AreaModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<AreaModel, Area>();

            CreateMap<Order, OrderModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
            .ForMember(p => p.OrderItems, opt => opt.Ignore())
            .ForMember(p => p.Address, opt => opt.Ignore());
            CreateMap<OrderModel, Order>()
            .ForMember(p => p.OrderItems, opt => opt.Ignore())
            .ForMember(p => p.Address, opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<OrderItemModel, OrderItem>();

            CreateMap<Coupon, CouponModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<CouponModel, Coupon>();

            CreateMap<Promotion, PromotionModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<PromotionModel, Promotion>();

            CreateMap<Address, AddressModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<AddressModel, Address>();

            CreateMap<Governorate, GovernorateModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<GovernorateModel, Governorate>();

            CreateMap<Area, AreaModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<AreaModel, Area>();

            CreateMap<DeviceToken, DeviceTokenModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<DeviceTokenModel, DeviceToken>();

            CreateMap<PaymentMethod, PaymentMethodModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<PaymentMethodModel, PaymentMethod>();

            CreateMap<Subscription, SubscriptionModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<SubscriptionModel, Subscription>();

            CreateMap<WalletPackage, WalletPackageModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<WalletPackageModel, WalletPackage>();

            CreateMap<WalletPackageOrder, Utility.Models.Frontend.Sales.WalletPackageOrderModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<Utility.Models.Frontend.Sales.WalletPackageOrderModel, WalletPackageOrder>();

            #region for Backend Admin
            CreateMap<Order, AdminOrderModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
          .ForMember(p => p.OrderItems, opt => opt.Ignore())
          .ForMember(p => p.Address, opt => opt.Ignore());
            CreateMap<AdminOrderModel, Order>()
            .ForMember(p => p.OrderItems, opt => opt.Ignore())
            .ForMember(p => p.Address, opt => opt.Ignore());

            CreateMap<OrderItem, AdminOrderItemModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<AdminOrderItemModel, OrderItem>();

            CreateMap<Subscription, SubscriptionAdminModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(p => p.SubscriptionOrders, opt => opt.Ignore());
            CreateMap<SubscriptionAdminModel, Subscription>()
                .ForMember(p => p.SubscriptionOrders, opt => opt.Ignore());

            CreateMap<SubscriptionOrder, SubscriptionOrderAdminModel>().AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);
            CreateMap<SubscriptionOrderAdminModel, SubscriptionOrder>();
            #endregion
        }
    }
}
