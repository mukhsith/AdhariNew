using Data.Content;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.Locations;
using Data.ProductManagement;
using Data.PushNotification;
using Data.Sales;
using Data.Shop;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.ProductManagement; 
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.Shop; 

namespace API.Areas.Backend.Helpers
{
    public interface IModelHelper
    {
        #region Common
        SocialMediaLinkModel PrepareSocialMediaLinkModel(SocialMediaLink model, bool isEnglish);
        BannerModel PrepareBannerModel(Banner banner, bool isEnglish);
        PageFooterContentModel PrepareSiteContentModel(IList<SiteContent> siteContent, bool isEnglish);
        SiteContentModel PrepareSiteContentModel(SiteContent siteContent, bool isEnglish);
        ContactDetailModel PrepareContactDetailModel(ContactDetail contactDetail, bool isEnglish);
        GovernorateModel PrepareGovernorateModel(Governorate governorate, bool isEnglish);
        AreaModel PrepareAreaModel(Area area, bool isEnglish);
        PaymentMethodModel PreparePaymentMethodModel(PaymentMethod paymentMethod, bool isEnglish);
        Task<WalletPackageModel> PrepareWalletPackageModel(WalletPackage walletPackage, bool isEnglish);
        Task<Utility.Models.Frontend.CustomizedModel.QuickPaymentModel> PrepareQuickPaymentModel(QuickPayment quickPayment, bool isEnglish);
        #endregion

        #region Category
        IList<CategoryModel> PrepareCategoryModels(IEnumerable<Category> models, bool isEnglish);
        CategoryModel PrepareCategoryModel(Category model, bool isEnglish);
        #endregion

        #region Customer
        CustomerModel PrepareCustomerModel(Customer customer, bool isEnglish);
        Task<AddressModel> PrepareAddressModel(Address address, bool isEnglish);
        NotificationModel PrepareNotificationModel(Notification notification, bool isEnglish);
        Task<WalletTransactionModel> PrepareWalletTransactionModel(WalletTransaction walletTransaction, bool isEnglish);
        Task<Utility.Models.Admin.Sales.WalletPackageOrderModel> PrepareWalletPackageOrderModel(WalletPackageOrder walletPackageOrder, bool isEnglish, bool loadDetails = false);
        #endregion

        #region Product
        Task<ProductModel> PrepareProductModel(Product product, bool isEnglish, string customerGuidValue = "", Customer customer = null,
        bool loadDescription = false, bool loadPrice = false, bool calculateStock = false, bool loadCategory = false,
        bool loadSubscriptionAttributes = false, bool loadSubscriptionPackTitle = false, bool loadCartQuantity = false);
        #endregion

        #region Cart
        Task<CartItemModel> PrepareCartItemModel(CartItem cartItem, bool isEnglish, Customer customer = null);
        Task<CartModel> PrepareCartModel(IList<CartItem> cartItems, bool isEnglish);
        Task<CartSummaryModel> PrepareCartSummaryModel(bool isEnglish, int customerId, List<CartItemModel> cartItemModels);
        Task<CheckOutModel> PrepareCheckOutModel(bool isEnglish, int customerId);
        #endregion

        #region Order
        Task<OrderModel> PrepareOrderModel(Order order, bool isEnglish, bool loadDetails = false);
        Task<AdminOrderSummaryModel> PrepareOrderSummary(Order order, bool isEnglish);
        #endregion

        #region Subscription
        Task<SubscriptionModel> PrepareSubscriptionModel(Subscription subscription, bool isEnglish, bool loadDetails = false);
        #endregion
        //    #region Common
        //    SocialMediaLinkModel PrepareSocialMediaLinkModel(SocialMediaLink model, bool isEnglish);
        //    BannerModel PrepareBannerModel(Banner banner, bool isEnglish);
        //    PageFooterContentModel PrepareSiteContentModel(IList<SiteContent> siteContent, bool isEnglish);
        //    SiteContentModel PrepareSiteContentModel(SiteContent siteContent, bool isEnglish);
        //    ContactDetailModel PrepareContactDetailModel(ContactDetail contactDetail, bool isEnglish);
        //    GovernorateModel PrepareGovernorateModel(Governorate governorate, bool isEnglish);
        //    AreaModel PrepareAreaModel(Area area, bool isEnglish);
        //    PaymentMethodModel PreparePaymentMethodModel(PaymentMethod paymentMethod, bool isEnglish);
        //   Task<AdminOrderSummaryModel>   PrepareOrderSummary(Order order, bool isEnglish);
        //#endregion

        ////#region Category
        //IList<CategoryModel> PrepareCategoryModels(IEnumerable<Category> models, bool isEnglish);
        //CategoryModel PrepareCategoryModel(Category model, bool isEnglish);
        ////#endregion 

        //#region Customer
        //CustomerModel PrepareCustomerModel(Customer customer, bool isEnglish);
        //    Task<AddressModel> PrepareAddressModel(Address address, bool isEnglish);
        //    NotificationModel PrepareNotificationModel(Notification notification, bool isEnglish);
        //    Task<WalletTransactionModel> PrepareWalletTransactionModel(WalletTransaction walletTransaction, bool isEnglish);
        //    #endregion

        //    #region Product
        //    Task<ProductModel> PrepareProductModel(Product product, bool isEnglish, string customerGuidValue = "", Customer customer = null,
        //    bool loadDescription = false, bool loadPrice = false, bool calculateStock = false, bool loadCategory = false);
        //#endregion

        //#region Cart
        //Task<CartItemModel> PrepareCartItemModel(CartItem cartItem, bool isEnglish);
        //Task<CartModel> PrepareCartModel(IList<CartItem> cartItems, bool isEnglish);
        //Task<CartSummaryModel> PrepareCartSummaryModel(bool isEnglish, int customerId, List<CartItemModel> cartItemModels);
        //Task<CheckOutModel> PrepareCheckOutModel(bool isEnglish, int customerId);
        //#endregion

        //#region Order
        //Task<OrderModel> PrepareOrderModel(Order order, bool isEnglish, bool loadDetails = false);
        ////Task<AdminOrderModel> PrepareOrderModelAdmin(Order order,  bool isEnglish, bool loadDetails = false);

        //Task<SubscriptionModel> PrepareSubscriptionModel(Subscription subscription, bool isEnglish, bool loadDetails = false);
        //#endregion



    }


}
