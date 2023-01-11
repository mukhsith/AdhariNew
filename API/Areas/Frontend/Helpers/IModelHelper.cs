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
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.Shop;

namespace API.Areas.Frontend.Helpers
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
        PaymentMethodModel PreparePaymentMethodModel(PaymentMethod paymentMethod, bool isEnglish, bool details = false);
        Task<WalletPackageModel> PrepareWalletPackageModel(WalletPackage walletPackage, bool isEnglish);
        Task<QuickPaymentModel> PrepareQuickPaymentModel(QuickPayment quickPayment, bool isEnglish);
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
        Task<WalletPackageOrderModel> PrepareWalletPackageOrderModel(WalletPackageOrder walletPackageOrder, bool isEnglish, bool loadDetails = false);
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
        #endregion

        #region Subscription
        Task<SubscriptionSummaryModel> PrepareSubscriptionSummaryModel(bool isEnglish, Customer customer, bool app = true);
        Task<SubscriptionCheckOutModel> PrepareSubscriptionCheckOutModel(bool isEnglish, Customer customer, bool app = true);
        Task<SubscriptionModel> PrepareSubscriptionModel(Subscription subscription, bool isEnglish, bool loadDetails = false);
        Task<SubscriptionAdminModel> PrepareSubscriptionModel1(Subscription subscription, bool isEnglish, bool loadDetails = false);
        #endregion
    }
}
