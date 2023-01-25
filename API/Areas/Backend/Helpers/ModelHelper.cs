using API.Helpers;
using AutoMapper;
using Data.Content;
using Data.CustomerManagement;
using Data.Locations;
using Data.ProductManagement;
using Data.PushNotification;
using Data.Sales;
using Data.Shop;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Services.Backend.CouponPromotion.Interface;
using Services.Backend.CustomerManagement;
using Services.Backend.Locations.Interface;
using Services.Backend.ProductManagement.Interface;
using Services.Backend.Shop;
using Services.Backend.Content.Interface;
using Services.Backend.Sales; 
//using Utility.Models.Admin.ProductManagement;
//using Utility.Models.Admin.Sales;
//using Utility.Models.Admin.CustomizedModel;
//using Utility.Models.Admin.Content;
//using Utility.Models.Admin.Locations;
//using Utility.Models.Admin.CustomerManagement;
//using Utility.Models.Admin.Shop; 
using Data.CouponPromotion;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.Shop;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.Frontend.Sales;
//using Utility.Models.Admin.Sales;
using Services.Backend.SystemUserManagement;
//using Utility.Models.Admin.CouponPromotion;

namespace API.Areas.Backend.Helpers
{
    public class ModelHelper : IModelHelper
    {
        private readonly IMapper _mapper;
        private readonly AppSettingsModel _appSettings;
        private readonly ICommonHelper _commonHelper;
        private readonly ISystemUserService _systemUserService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        private readonly IAreaService _areaService;
        private readonly ICustomerService _customerService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IOrderService _orderService;
        private readonly ISubscriptionService _subscriptionService; 
        public ModelHelper(IMapper mapper,
           IOptions<AppSettingsModel> options,
           ICommonHelper commonHelper,
           ISystemUserService systemUserService,
           IProductService productService,
           ICartService cartService,
           ICouponService couponService,
           IAreaService areaService,
           ICustomerService customerService,
           IPaymentMethodService paymentMethodService,

           IOrderService orderService,
           ISubscriptionService subscriptionService )
        {
            _mapper = mapper;
            _appSettings = options.Value;
            _commonHelper = commonHelper;
            _systemUserService = systemUserService;
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
            _areaService = areaService;
            _customerService = customerService;
            _paymentMethodService = paymentMethodService;
            _orderService = orderService;
            _subscriptionService = subscriptionService; 
        }

        #region Common
        public SocialMediaLinkModel PrepareSocialMediaLinkModel(SocialMediaLink model, bool isEnglish)
        {
            var itemModel = _mapper.Map<SocialMediaLinkModel>(model);
            return itemModel;
        }
        public BannerModel PrepareBannerModel(Banner banner, bool isEnglish)
        {
            var bannerModel = _mapper.Map<BannerModel>(banner);

            if (isEnglish)
            {
                if (string.IsNullOrEmpty(banner.ImageNameEn))
                    bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerDefault;
                else
                    bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerResized + banner.ImageNameEn;
            }
            else
            {
                if (string.IsNullOrEmpty(banner.ImageNameAr))
                    bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerDefault;
                else
                    bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerResized + banner.ImageNameAr;
            }

            return bannerModel;

        }
        public PageFooterContentModel PrepareSiteContentModel(IList<SiteContent> items, bool isEnglish)
        {
            PageFooterContentModel model = new();

            foreach (var item in items)
            {
                if (item.AppContentType == AppContentType.TermsCondition)
                {
                    model.TermsConditions = isEnglish ? item.ContentEn : item.ContentAr;
                }
                else if (item.AppContentType == AppContentType.PrivacyPolicy)
                {
                    model.PrivacyPolicy = isEnglish ? item.ContentEn : item.ContentAr;
                }
                else if (item.AppContentType == AppContentType.RefundPolicy)
                {
                    model.RefundPolicy = isEnglish ? item.ContentEn : item.ContentAr;
                }
            }

            return model;
        }
        public SiteContentModel PrepareSiteContentModel(SiteContent siteContent, bool isEnglish)
        {
            var siteContentModel = _mapper.Map<SiteContentModel>(siteContent);
            siteContentModel.Content = isEnglish ? siteContent.ContentEn : siteContent.ContentAr;
            if (siteContent.AppContentType == AppContentType.AboutUs && siteContent.ImageName != null)
            {
                siteContentModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageSiteContent + siteContent.ImageName;
            }
            return siteContentModel;
        }
        public ContactDetailModel PrepareContactDetailModel(ContactDetail contactDetail, bool isEnglish)
        {
            var contactDetailModel = _mapper.Map<ContactDetailModel>(contactDetail);
            contactDetailModel.Address = isEnglish ? contactDetail.AddressEn : contactDetail.AddressAr;
            return contactDetailModel;
        }
        public GovernorateModel PrepareGovernorateModel(Governorate governorate, bool isEnglish)
        {
            var governorateModel = _mapper.Map<GovernorateModel>(governorate);
            governorateModel.Name = isEnglish ? governorate.NameEn : governorate.NameAr;
            return governorateModel;
        }
        public AreaModel PrepareAreaModel(Area area, bool isEnglish)
        {
            var areaModel = _mapper.Map<AreaModel>(area);
            areaModel.Name = isEnglish ? area.NameEn : area.NameAr;
            return areaModel;
        }
        public PaymentMethodModel PreparePaymentMethodModel(Data.Content.PaymentMethod paymentMethod, bool isEnglish)
        {
            var paymentMethodModel = new PaymentMethodModel();
            paymentMethodModel.Id = paymentMethod.Id;
            paymentMethodModel.Name = isEnglish ? paymentMethod.NameEn : paymentMethod.NameAr;

            if (string.IsNullOrEmpty(paymentMethod.ImageName))
                paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodDefault;
            else
                paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodResized + paymentMethod.ImageName;

            return paymentMethodModel;
        }
        public async Task<WalletPackageModel> PrepareWalletPackageModel(WalletPackage walletPackage, bool isEnglish)
        {
            var walletPackageModel = _mapper.Map<WalletPackageModel>(walletPackage);

            walletPackageModel.Title = isEnglish ? walletPackage.NameEn : walletPackage.NameAr;
            walletPackageModel.Description = isEnglish ? walletPackage.DescriptionEn : walletPackage.DescriptionAr;
            walletPackageModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: walletPackage.Amount, isEnglish: isEnglish,
                countryId: 0, includeZero: true);

            return walletPackageModel;
        }
        public async Task<Utility.Models.Frontend.CustomizedModel.QuickPaymentModel> PrepareQuickPaymentModel(QuickPayment quickPayment, bool isEnglish)
        {
            Utility.Models.Frontend.CustomizedModel.QuickPaymentModel quickPayModel = new();

            quickPayModel.QuickPayNumber = quickPayment.PaymentNumber;
            quickPayModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: quickPayment.Amount, isEnglish: isEnglish);

            var customer = await _customerService.GetCustomerById(quickPayment.CustomerId);
            if (customer != null)
            {
                CustomerModel customerModel = new();
                customerModel.Name = customer.Name;
                customerModel.MobileNumber = customer.MobileNumber;
                customerModel.EmailAddress = customer.EmailAddress;
                quickPayModel.Customer = customerModel;
            }

            var paymentMethods = await _paymentMethodService.GetAllPaymentMethod(PaymentRequestType.QuickPay);
            foreach (var paymentMethod in paymentMethods)
            {
                var paymentMethodModel = PreparePaymentMethodModel(paymentMethod: paymentMethod, isEnglish: isEnglish);
                quickPayModel.PaymentMethods.Add(paymentMethodModel);
            }

            return quickPayModel;
        }
        //public async Task<QuickPaymentModel> PrepareQuickPaymentModel(QuickPayment quickPayment, bool isEnglish)
        //{
        //    Utility.Models.Admin.Sales.QuickPaymentModel quickPayModel = new();

        //    quickPayModel.PaymentNumber = quickPayment.PaymentNumber;
        //    quickPayModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: quickPayment.Amount, isEnglish: isEnglish);

        //    var customer = await _customerService.GetCustomerById(quickPayment.CustomerId);
        //    if (customer != null)
        //    {
        //        CustomerModel customerModel = new();
        //        customerModel.Name = customer.Name;
        //        customerModel.MobileNumber = customer.MobileNumber;
        //        customerModel.EmailAddress = customer.EmailAddress;
        //        quickPayModel.Customer = customerModel;
        //    }

        //    var paymentMethods = await _paymentMethodService.GetAllPaymentMethod(PaymentRequestType.QuickPay);
        //    foreach (var paymentMethod in paymentMethods)
        //    {
        //        var paymentMethodModel = PreparePaymentMethodModel(paymentMethod: paymentMethod, isEnglish: isEnglish);
        //        quickPayModel.PaymentMethods.Add(paymentMethodModel);
        //    }

        //    return quickPayModel;
        //}
        #endregion

        #region Category
        public IList<CategoryModel> PrepareCategoryModels(IEnumerable<Category> models, bool isEnglish)
        {
            List<CategoryModel> items = new();
            return items;
        }
        public CategoryModel PrepareCategoryModel(Category model, bool isEnglish)
        {
            var itemModel = _mapper.Map<CategoryModel>(model);
            itemModel.Title = isEnglish ? model.NameEn : model.NameAr;
            return itemModel;

        }
        #endregion

        #region Customer
        public CustomerModel PrepareCustomerModel(Customer customer, bool isEnglish)
        {
            var customerModel = _mapper.Map<CustomerModel>(customer);

            if (customer.Country != null)
            {
                customerModel.FormattedMobile = customer.Country.MobileCode + " " + customer.MobileNumber;
            }

            return customerModel;
        }
        public async Task<AddressModel> PrepareAddressModel(Address address, bool isEnglish)
        {
            var addressModel = _mapper.Map<AddressModel>(address);

            var governorate = address.Area.Governorate;
            if (governorate != null)
            {
                addressModel.GovernorateId = governorate.Id;
                addressModel.GovernorateName = isEnglish ? governorate.NameEn : governorate.NameAr;
            }

            var area = address.Area;
            if (area != null)
            {
                addressModel.AreaName = isEnglish ? area.NameEn : area.NameAr;
            }

            string addressText = await _commonHelper.PrepareAddressText(address: address, isEnglish: isEnglish);
            addressModel.AddressText = addressText;

            return addressModel;
        }
        public NotificationModel PrepareNotificationModel(Notification notification, bool isEnglish)
        {
            var notificationModel = new NotificationModel();

            notificationModel.Id = notification.Id;
            if (!string.IsNullOrEmpty(notification.TitleEn))
                notificationModel.Title = notification.TitleEn;
            else
                notificationModel.Title = notification.TitleAr;

            if (!string.IsNullOrEmpty(notification.MessageEn))
                notificationModel.Message = notification.MessageEn;
            else
                notificationModel.Message = notification.MessageAr;

            notificationModel.TimeAgo = _commonHelper.GetTimeAgo(notification.CreatedOn, isEnglish);
            notificationModel.FormattedDate = notification.CreatedOn.ToString("dd'/'MM'/'yyyy - hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            return notificationModel;
        }
        public async Task<WalletTransactionModel> PrepareWalletTransactionModel(WalletTransaction walletTransaction, bool isEnglish)
        {
            var walletTransactionModel = new WalletTransactionModel();

            if (walletTransaction.Credit > 0)
            {
                walletTransactionModel.Title = isEnglish ? Messages.Credit : MessagesAr.Credit;
                walletTransactionModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: walletTransaction.Credit, isEnglish: isEnglish);
                walletTransactionModel.ColorCode = "#00aeef";
                walletTransactionModel.CreditTransaction = true;
            }
            else
            {
                walletTransactionModel.Title = isEnglish ? Messages.Debit : MessagesAr.Debit;
                walletTransactionModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: walletTransaction.Debit, isEnglish: isEnglish);
                walletTransactionModel.ColorCode = "#850b5a";
            }

            if (walletTransaction.WalletTypeId == WalletType.Wallet)
            {
                walletTransactionModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCommonResized + "wallet-icon.png";
                walletTransactionModel.WalletType = isEnglish ? Messages.Wallet : MessagesAr.Wallet;
            }
            else
            {
                walletTransactionModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCommonResized + "cashback-icon.png";
                walletTransactionModel.WalletType = isEnglish ? Messages.Cashback : MessagesAr.Cashback;
            }

            List<KeyValuPairModel> walletDetails = new();
            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.TransactionId : MessagesAr.TransactionId,
                Value = walletTransaction.TransactionNo,
                DisplayOrder = 0
            });

            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.TransactionDate : MessagesAr.TransactionDate,
                Value = walletTransaction.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW")),
                DisplayOrder = 1
            });

            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.TransactionTime : MessagesAr.TransactionTime,
                Value = walletTransaction.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW")),
                DisplayOrder = 2
            });

            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.TransactionAmount : MessagesAr.TransactionAmount,
                Value = walletTransactionModel.FormattedAmount,
                DisplayOrder = 3
            });

            if (walletTransaction.ExpiryDate.HasValue)
            {
                walletDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.ValidTill : MessagesAr.ValidTill,
                    Value = walletTransaction.ExpiryDate.Value.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW")),
                    DisplayOrder = 4
                });
            }

            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.TransactionType : MessagesAr.TransactionType,
                Value = walletTransactionModel.Title,
                DisplayOrder = 5
            });

            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.WalletType : MessagesAr.WalletType,
                Value = walletTransactionModel.WalletType,
                DisplayOrder = 6
            });

            if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.SignUpPromotion)
            {
                walletTransactionModel.Description = isEnglish ? Messages.SignUpBonus : MessagesAr.SignUpBonus;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.CashbackOnPurchase)
            {
                walletTransactionModel.Description = isEnglish ? Messages.PurchaseBonus : MessagesAr.PurchaseBonus;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.CashbackRedeem)
            {
                walletTransactionModel.Description = isEnglish ? Messages.CashbackRedeemOnPurchase : MessagesAr.CashbackRedeemOnPurchase;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.UseWalletAmount)
            {
                walletTransactionModel.Description = isEnglish ? Messages.WalletRedeemOnPurchase : MessagesAr.WalletRedeemOnPurchase;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.CashbackExpiry)
            {
                walletTransactionModel.Description = isEnglish ? Messages.Expired : MessagesAr.Expired;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.RefundWalletAmount)
            {
                //walletTransactionModel.Description = isEnglish ? Messages.RefundWalletAmount : MessagesAr.RefundWalletAmount;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.RefundCashbackOnPurchase)
            {
                //walletTransactionModel.Description = isEnglish ? Messages.RefundCashbackOnPurchase : MessagesAr.RefundCashbackOnPurchase;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.RefundOrderAmount)
            {
                //walletTransactionModel.Description = isEnglish ? Messages.RefundOrderAmount : MessagesAr.RefundOrderAmount;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.RefundSubscriptionAmount)
            {
                //walletTransactionModel.Description = isEnglish ? Messages.RefundOrderAmount : MessagesAr.RefundOrderAmount;
            }

            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.Method : MessagesAr.Method,
                Value = walletTransactionModel.Description,
                DisplayOrder = 7
            });

            if (walletTransaction.RelatedEntityTypeId == RelatedEntityType.Order)
            {
                var order = await _orderService.GetOrderById(walletTransaction.RelatedEntityId);
                if (order != null)
                {
                    walletDetails.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.OrderNumber : MessagesAr.OrderNumber,
                        Value = order.OrderNumber,
                        DisplayOrder = 7
                    });
                }
            }

            walletTransactionModel.PaymentSummary = walletDetails.OrderBy(a => a.DisplayOrder).ToList();

            return walletTransactionModel;
        }
        public async Task<Utility.Models.Admin.Sales.WalletPackageOrderModel> PrepareWalletPackageOrderModel(WalletPackageOrder walletPackageOrder, bool isEnglish, bool loadDetails = false)
        {
            var walletPackageOrderModel = _mapper.Map<Utility.Models.Admin.Sales.WalletPackageOrderModel>(walletPackageOrder);

            walletPackageOrderModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(walletPackageOrder.Amount, isEnglish, includeZero: true);
            walletPackageOrderModel.FormattedWalletAmount = await _commonHelper.ConvertDecimalToString(walletPackageOrder.WalletAmount, isEnglish, includeZero: true);
            walletPackageOrderModel.FormattedDate = walletPackageOrder.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
            walletPackageOrderModel.FormattedTime = walletPackageOrder.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            walletPackageOrderModel.PaymentResult = _commonHelper.GetPaymentResultTitle(walletPackageOrder.PaymentStatusId, isEnglish: isEnglish);

            var paymentMethod = await _paymentMethodService.GetPaymentMethodById(walletPackageOrder.PaymentMethodId);
            if (paymentMethod != null)
            {
                walletPackageOrderModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod);
            }

            if (loadDetails)
            {
                if (walletPackageOrder.Customer != null)
                {
                    walletPackageOrderModel.Customer = PrepareCustomerModel(customer: walletPackageOrder.Customer, isEnglish: isEnglish);
                }

                if (walletPackageOrder.WalletPackage != null)
                {
                    walletPackageOrderModel.WalletPackage = await PrepareWalletPackageModel(walletPackage: walletPackageOrder.WalletPackage, isEnglish: isEnglish);
                }

                //payment summary
                List<KeyValuPairModel> patmentSummary = new();
                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentDate : MessagesAr.PaymentDate,
                    Value = walletPackageOrderModel.FormattedDate,
                    DisplayOrder = 1
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentTime : MessagesAr.PaymentTime,
                    Value = walletPackageOrderModel.FormattedTime,
                    DisplayOrder = 2
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.TopUpAmount : MessagesAr.TopUpAmount,
                    Value = walletPackageOrderModel.FormattedAmount,
                    DisplayOrder = 3
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
                    Value = walletPackageOrderModel.FormattedWalletAmount,
                    DisplayOrder = 4
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentMethod : MessagesAr.PaymentMethod,
                    Value = walletPackageOrderModel.PaymentMethod.Name,
                    DisplayOrder = 5
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentResult : MessagesAr.PaymentResult,
                    Value = walletPackageOrderModel.PaymentResult,
                    DisplayOrder = 6
                });

                if (!string.IsNullOrEmpty(walletPackageOrderModel.PaymentId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentId : MessagesAr.PaymentId,
                        Value = walletPackageOrderModel.PaymentId,
                        DisplayOrder = 7
                    });
                }

                if (!string.IsNullOrEmpty(walletPackageOrderModel.PaymentRefId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentReference : MessagesAr.PaymentReference,
                        Value = walletPackageOrderModel.PaymentRefId,
                        DisplayOrder = 8
                    });
                }

                if (!string.IsNullOrEmpty(walletPackageOrderModel.PaymentTrackId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.TrackId : MessagesAr.TrackId,
                        Value = walletPackageOrderModel.PaymentTrackId,
                        DisplayOrder = 9
                    });
                }

                walletPackageOrderModel.PaymentSummary = patmentSummary.OrderBy(a => a.DisplayOrder).ToList();
            }

            return walletPackageOrderModel;
        }
        #endregion

        #region Product
        public async Task<ProductModel> PrepareProductModel(Product product, bool isEnglish, string customerGuidValue = "", Customer customer = null,
            bool loadDescription = false, bool loadPrice = false, bool calculateStock = false, bool loadCategory = false, bool loadSubscriptionAttributes = false,
            bool loadSubscriptionPackTitle = false, bool loadCartQuantity = false)
        {
            var productModel = _mapper.Map<ProductModel>(product);

            var title = isEnglish ? product.NameEn : product.NameAr;

            productModel.Title = title;

            if (loadDescription)
            {
                productModel.Description = isEnglish ? product.DescriptionEn : product.DescriptionAr;
            }

            if (loadCategory)
            {
                var categoryModel = PrepareCategoryModel(model: product.Category, isEnglish: isEnglish);
                productModel.CategoryName = categoryModel.Title;
            }

            if (product.ItemSize != null)
            {
                productModel.ItemSize.Id = product.ItemSize.Id;
                productModel.ItemSize.Title = isEnglish ? product.ItemSize.NameEn : product.ItemSize.NameAr;
            }

            bool b2bCustomer = false;
            if (customer != null)
            {
                var favorite = (await _productService.GetAllFavorite(customerId: customer.Id, productId: product.Id)).FirstOrDefault();
                if (favorite != null)
                {
                    productModel.Favorite = true;
                }

                var productAvailabilityNotifyRequest = await _productService.GetProductAvailabilityNotifyRequest(customerId: customer.Id, productId: product.Id);
                if (productAvailabilityNotifyRequest != null)
                {
                    productModel.Notified = true;
                }

                if (product.B2BPriceEnabled && customer.B2B)
                {
                    b2bCustomer = true;
                }
            }

            decimal price = product.Price;
            decimal discountedPrice = 0;

            if (loadPrice)
            {
                price = product.GetPriceFrontend(b2bCustomer);
                discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);
            }

            productModel.Price = price;
            productModel.DiscountedPrice = discountedPrice;
            productModel.FormattedPrice = await _commonHelper.ConvertDecimalToString(productModel.Price, isEnglish, 0);
            productModel.FormattedDiscountedPrice = await _commonHelper.ConvertDecimalToString(productModel.DiscountedPrice, isEnglish, 0);

            if (string.IsNullOrEmpty(product.ImageName))
                productModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageProductDefault;
            else
                productModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageProductResized + product.ImageName;

            if (calculateStock)
            {
                if (product.ProductType == ProductType.BaseProduct)
                {
                    var productStockQuantity = await _productService.GetAvailableStockQuantity(productId: product.Id,
                    customerId: customer?.Id, customerGuidValue: customerGuidValue);
                    if (productStockQuantity < 0)
                        productStockQuantity = 0;

                    productModel.StockQuantity = productStockQuantity;
                }
                else if (product.ProductType == ProductType.BundledProduct)
                {
                    int lowStockProduct = 0;
                    List<int> childProductStocks = new();
                    var productDetails = product.ProductDetails.ToList();
                    foreach (var productDetail in productDetails)
                    {
                        var productStockQuantity = await _productService.GetAvailableStockQuantity(productId: productDetail.ChildProductId,
                    customerId: customer?.Id, customerGuidValue: customerGuidValue);
                        if (productStockQuantity <= 0)
                            lowStockProduct++;

                        childProductStocks.Add(productStockQuantity / productDetail.Quantity);
                    }

                    if (lowStockProduct > 0)
                        productModel.StockQuantity = 0;
                    else
                        productModel.StockQuantity = childProductStocks.Min();
                }
                else if (product.ProductType == ProductType.SubscriptionProduct)
                {
                    int lowStockProduct = 0;
                    List<int> childProductStocks = new();
                    var productDetails = product.ProductDetails.ToList();
                    foreach (var productDetail in productDetails)
                    {
                        var productStockQuantity = await _productService.GetAvailableStockQuantity(productId: productDetail.ChildProductId,
                    customerId: customer?.Id, customerGuidValue: customerGuidValue);
                        if (productStockQuantity <= 0)
                            lowStockProduct++;

                        childProductStocks.Add(productStockQuantity / productDetail.Quantity);
                    }

                    if (lowStockProduct > 0)
                        productModel.StockQuantity = 0;
                    else
                        productModel.StockQuantity = childProductStocks.Min();
                }
            }

            productModel.DetailsUrl = _appSettings.WebsiteUrl + "product/" + productModel.CategoryName + "/" + product.SeoName;

            if (loadCartQuantity)
            {
                var existingCart = (await _cartService.GetAllCartItem(productDetailId: product.Id, customerId: customer?.Id, customerGuidValue: customerGuidValue)).FirstOrDefault();
                productModel.CartQuantity = existingCart != null ? existingCart.Quantity : 0;

                int availableQuantity = productModel.StockQuantity - productModel.CartQuantity;
                if (availableQuantity < 0)
                    availableQuantity = 0;

                productModel.StockQuantity = availableQuantity;
            }

            if (product.ProductType == ProductType.SubscriptionProduct)
            {
                if (loadSubscriptionAttributes)
                {
                    var subscriptionDurations = await _productService.GetAllSubscriptionDuration();
                    foreach (var subscriptionDuration in subscriptionDurations)
                    {
                        productModel.SubscriptionDurations.Add(new SubscriptionDurationModel
                        {
                            Id = subscriptionDuration.Id,
                            Title = isEnglish ? subscriptionDuration.NameEn : subscriptionDuration.NameAr
                        });
                    }

                    var subscriptionDeliveryDates = await _productService.GetAllSubscriptionDeliveryDate();
                    foreach (var subscriptionDeliveryDate in subscriptionDeliveryDates)
                    {
                        productModel.SubscriptionDeliveryDates.Add(new SubscriptionDeliveryDateModel
                        {
                            Id = subscriptionDeliveryDate.Id,
                            Title = isEnglish ? subscriptionDeliveryDate.NameEn : subscriptionDeliveryDate.NameAr
                        });
                    }
                }

                if (loadSubscriptionPackTitle)
                {
                    var productDetails = product.ProductDetails.ToList();
                    foreach (var productDetail in productDetails)
                    {
                        var childProduct = await _productService.GetById(productDetail.ChildProductId);
                        if (childProduct != null)
                        {
                            productModel.SubscriptionPackTitles.Add(new KeyValuPairModel
                            {
                                Title = (isEnglish ? childProduct.NameEn : childProduct.NameAr) + " x " + productDetail.Quantity,
                                Value = childProduct.Id.ToString()
                            });
                        }
                    }
                }
            }

            return productModel;
        }
        #endregion

        #region Cart
        public async Task<CartItemModel> PrepareCartItemModel(CartItem cartItem, bool isEnglish, Customer customer = null)
        {
            var cartItemModel = _mapper.Map<CartItemModel>(cartItem);
            decimal total = 0;
            decimal shortTotal = 0;

            var product = await _productService.GetById(cartItem.ProductId);
            if (product != null)
            {
                var productModel = await PrepareProductModel(product: product, isEnglish: isEnglish, loadPrice: true, calculateStock: true, customer: customer);
                cartItemModel.Product = productModel;

                if (productModel.DiscountedPrice > 0)
                {
                    total = productModel.DiscountedPrice * cartItem.Quantity;
                    shortTotal = productModel.DiscountedPrice * cartItem.Quantity;
                }
                else
                {
                    total = productModel.Price * cartItem.Quantity;
                    shortTotal = productModel.Price * cartItem.Quantity;
                }
            }

            cartItemModel.Total = total;
            cartItemModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(total, isEnglish, countryId: 0, includeZero: true);

            cartItemModel.ShortTotal = shortTotal;
            cartItemModel.FormattedShortTotal = await _commonHelper.ConvertDecimalToString(shortTotal, isEnglish, countryId: 0, includeZero: true);

            return cartItemModel;
        }
        public async Task<CartModel> PrepareCartModel(IList<CartItem> cartItems, bool isEnglish)
        {
            var cartModel = new CartModel();

            Customer customer = null;
            var cartItem = cartItems.Where(a => a.CustomerId.HasValue).FirstOrDefault();
            if (cartItem != null)
            {
                customer = await _customerService.GetCustomerById(cartItem.CustomerId.Value);
            }

            foreach (var item in cartItems)
            {
                var cartItemModel = await PrepareCartItemModel(cartItem: item, isEnglish: isEnglish, customer: customer);
                cartModel.CartItems.Add(cartItemModel);
            }

            cartModel.SubTotal = cartModel.CartItems.Sum(a => a.Total);

            if (cartItems.Count > 0)
            {
                cartModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(value: cartModel.SubTotal, isEnglish: isEnglish,
                    countryId: 0);
            }
            else
            {
                cartModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(value: 0, isEnglish: isEnglish,
                      countryId: 0, includeZero: true);
            }

            return cartModel;
        }
        //public async Task<CartSummaryModel> PrepareCartSummaryModel(bool isEnglish, int customerId, List<CartItemModel> cartItemModels)
        //{
        //    var cartSummaryModel = new CartSummaryModel();
        //    decimal subTotal = cartItemModels.Sum(a => a.Total);
        //    decimal deliveryFee = 0;
        //    decimal discountAmount = 0;
        //    decimal cashbackAmount = 0;
        //    decimal walletUsedAmount = 0;

        //    var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
        //    //if (cartAttribute == null)
        //    //{
        //    //    return null;
        //    //}

        //    List<KeyValuPairModel> AmountSplitUps = new();
        //    AmountSplitUps.Add(new KeyValuPairModel
        //    {
        //        Title = isEnglish ? Messages.Items : MessagesAr.Items,
        //        Value = await _commonHelper.ConvertDecimalToString(value: subTotal, isEnglish: isEnglish, includeZero: true),
        //        DisplayOrder = 0
        //    });

        //    var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
        //    cartSummaryModel.WalletBalanceAmount = walletBalance;
        //    cartSummaryModel.FormattedWalletBalanceAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletBalanceAmount, isEnglish);

        //    if (cartAttribute != null)
        //    {
        //        if (cartAttribute.CouponId.HasValue)
        //        {
        //            var coupon = await _couponService.GetById(cartAttribute.CouponId.Value);
        //            if (coupon != null)
        //            {
        //                discountAmount = coupon.ApplyCouponDiscount2(subTotal);
        //                cartSummaryModel.CouponCode = coupon.CouponCode;

        //                AmountSplitUps.Add(new KeyValuPairModel
        //                {
        //                    Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
        //                    Value = "-" + await _commonHelper.ConvertDecimalToString(value: discountAmount, isEnglish: isEnglish),
        //                    DisplayOrder = 2
        //                });
        //            }
        //            else
        //            {
        //                cartAttribute.CouponId = null;
        //                await _cartService.UpdateCartAttribute(cartAttribute);
        //            }
        //        }

        //        if (cartAttribute.AddressId.HasValue)
        //        {
        //            var address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
        //            if (address != null)
        //            {
        //                var area = await _areaService.GetById(address.AreaId);
        //                if (area != null)
        //                {
        //                    deliveryFee = await _commonHelper.GetDeliveryFeeByAreaId(areaId: area.Id);
        //                    AmountSplitUps.Add(new KeyValuPairModel
        //                    {
        //                        Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
        //                        Value = await _commonHelper.ConvertDecimalToString(value: deliveryFee, isEnglish: isEnglish),
        //                        DisplayOrder = 1
        //                    });
        //                }
        //            }
        //        }

        //        if (cartAttribute.UseWalletAmount && walletBalance > 0)
        //        {
        //            walletUsedAmount = walletBalance;

        //            decimal grossTotal = subTotal - discountAmount + deliveryFee - cashbackAmount;
        //            if (walletUsedAmount > grossTotal)
        //            {
        //                walletUsedAmount = grossTotal;

        //                cartAttribute.PaymentMethodId = (int)Utility.Enum.PaymentMethod.Wallet;
        //                await _cartService.UpdateCartAttribute(cartAttribute);

        //                cartSummaryModel.SkipPaymentMethodSelection = true;
        //            }
        //            else
        //            {
        //                if (cartAttribute.PaymentMethodId == (int)Utility.Enum.PaymentMethod.Wallet)
        //                {
        //                    cartAttribute.PaymentMethodId = null;
        //                    await _cartService.UpdateCartAttribute(cartAttribute);
        //                }
        //            }

        //            AmountSplitUps.Add(new KeyValuPairModel
        //            {
        //                Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
        //                Value = "-" + await _commonHelper.ConvertDecimalToString(value: walletUsedAmount, isEnglish: isEnglish),
        //                DisplayOrder = 4
        //            });

        //            cartSummaryModel.WalletUsedAmount = walletUsedAmount;
        //            cartSummaryModel.FormattedWalletUsedAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletUsedAmount, isEnglish);
        //        }
        //    }
        //    cashbackAmount = await _commonHelper.GetCashbackAmount(customerId: customerId, amount: subTotal - discountAmount);
        //    if (cashbackAmount > 0)
        //    {
        //        AmountSplitUps.Add(new KeyValuPairModel
        //        {
        //            Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
        //            Value = "-" + await _commonHelper.ConvertDecimalToString(value: cashbackAmount, isEnglish: isEnglish),
        //            DisplayOrder = 3
        //        });
        //    }



        //    cartSummaryModel.Total = subTotal + deliveryFee - discountAmount - cashbackAmount - walletUsedAmount;
        //    cartSummaryModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(cartSummaryModel.Total, isEnglish, includeZero: true);

        //    cartSummaryModel.AmountSplitUps = AmountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

        //    return cartSummaryModel;
        //}

        public async Task<CartSummaryModel> PrepareCartSummaryModel(bool isEnglish, int customerId, List<CartItemModel> cartItemModels)
        {
            var cartSummaryModel = new CartSummaryModel();
            decimal subTotal = cartItemModels.Sum(a => a.Total);
            decimal deliveryFee = 0;
            decimal discountAmount = 0;
            decimal cashbackAmount = 0;
            decimal walletUsedAmount = 0;

            var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
            if (cartAttribute == null)
            {
                return null;
            }

            List<KeyValuPairModel> AmountSplitUps = new();
            AmountSplitUps.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.SubTotal : MessagesAr.SubTotal,
                Value = await _commonHelper.ConvertDecimalToString(value: subTotal, isEnglish: isEnglish, includeZero: true),
                DisplayOrder = 0
            });

            var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
            cartSummaryModel.WalletBalanceAmount = walletBalance;
            cartSummaryModel.FormattedWalletBalanceAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletBalanceAmount, isEnglish: isEnglish);

            if (cartAttribute.CouponId.HasValue)
            {
                var coupon = await _couponService.GetById(cartAttribute.CouponId.Value);
                if (coupon != null)
                {
                    discountAmount = coupon.ApplyCouponDiscount2(subTotal);
                    cartSummaryModel.CouponCode = coupon.CouponCode;

                    AmountSplitUps.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
                        Value = "-" + await _commonHelper.ConvertDecimalToString(value: discountAmount, isEnglish: isEnglish),
                        DisplayOrder = 2
                    });
                }
                else
                {
                    cartAttribute.CouponId = null;
                    await _cartService.UpdateCartAttribute(cartAttribute);
                }
            }

            cashbackAmount = await _commonHelper.GetCashbackAmount(customerId: customerId, amount: subTotal - discountAmount);
            if (cashbackAmount > 0)
            {
                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
                    Value = "-" + await _commonHelper.ConvertDecimalToString(value: cashbackAmount, isEnglish: isEnglish),
                    DisplayOrder = 3
                });
            }

            if (cartAttribute.AddressId.HasValue)
            {
                var address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
                if (address != null)
                {
                    var area = await _areaService.GetById(address.AreaId);
                    if (area != null)
                    {
                        deliveryFee = await _commonHelper.GetDeliveryFeeByAreaId(areaId: area.Id);
                        AmountSplitUps.Add(new KeyValuPairModel
                        {
                            Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
                            Value = await _commonHelper.ConvertDecimalToString(value: deliveryFee, isEnglish: isEnglish),
                            DisplayOrder = 1
                        });
                    }
                }
            }

            if (cartAttribute.UseWalletAmount && walletBalance > 0)
            {
                walletUsedAmount = walletBalance;

                decimal grossTotal = subTotal - discountAmount + deliveryFee - cashbackAmount;
                if (walletUsedAmount > grossTotal)
                {
                    walletUsedAmount = grossTotal;

                    if (cartAttribute.PaymentMethodId != (int)Utility.Enum.PaymentMethod.Wallet)
                        cartAttribute.OtherPaymentMethodId = cartAttribute.PaymentMethodId;
                    cartAttribute.PaymentMethodId = (int)Utility.Enum.PaymentMethod.Wallet;
                    await _cartService.UpdateCartAttribute(cartAttribute);

                    cartSummaryModel.SkipPaymentMethodSelection = true;
                }
                else
                {
                    if (cartAttribute.PaymentMethodId == (int)Utility.Enum.PaymentMethod.Wallet)
                    {
                        cartAttribute.PaymentMethodId = cartAttribute.OtherPaymentMethodId;
                        cartAttribute.OtherPaymentMethodId = null;
                        await _cartService.UpdateCartAttribute(cartAttribute);
                    }
                }

                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
                    Value = "-" + await _commonHelper.ConvertDecimalToString(value: walletUsedAmount, isEnglish: isEnglish),
                    DisplayOrder = 4
                });

                cartSummaryModel.WalletUsedAmount = walletUsedAmount;
                cartSummaryModel.FormattedWalletUsedAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletUsedAmount, isEnglish: isEnglish);
            }

            cartSummaryModel.Total = subTotal + deliveryFee - discountAmount - cashbackAmount - walletUsedAmount;
            cartSummaryModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(cartSummaryModel.Total, isEnglish: isEnglish, includeZero: true);

            cartSummaryModel.AmountSplitUps = AmountSplitUps.OrderBy(a => a.DisplayOrder).ToList();
            cartSummaryModel.Notes = cartAttribute.Notes;

            return cartSummaryModel;
        }

        public async Task<CheckOutModel> PrepareCheckOutModel(bool isEnglish, int customerId)
        {
            var checkOutModel = new CheckOutModel();

            var cartItems = await _cartService.GetAllCartItem(customerId: customerId);
            if (cartItems.Count == 0)
            {
                return null;
            }

            var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
            if (cartAttribute == null)
            {
                return null;
            }

            if (!cartAttribute.AddressId.HasValue)
            {
                return null;
            }

            if (!cartAttribute.PaymentMethodId.HasValue)
            {
                return null;
            }

            var customer = await _customerService.GetCustomerById(customerId);
            if (customer == null)
            {
                return null;
            }

            foreach (var cartItem in cartItems)
            {
                var cartItemModel = await PrepareCartItemModel(cartItem: cartItem, isEnglish: isEnglish, customer: customer);
                checkOutModel.CartItems.Add(cartItemModel);
            }

            checkOutModel.CartSummary = await PrepareCartSummaryModel(isEnglish: isEnglish, customerId: customerId, cartItemModels: checkOutModel.CartItems.ToList());

            var address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
            if (address == null)
            {
                return null;
            }

            string addressText = await _commonHelper.PrepareAddressText(address: address, isEnglish: isEnglish);
            checkOutModel.AddressText = addressText;

            var paymentMethod = await _paymentMethodService.GetPaymentMethodById(cartAttribute.PaymentMethodId.Value);
            if (paymentMethod == null)
            {
                return null;
            }

            checkOutModel.PaymentMethod = PreparePaymentMethodModel(paymentMethod: paymentMethod, isEnglish: isEnglish);

            var dateAndSlot = await _commonHelper.GetAvailableDeliveryDateAndSlot();
            var deliveryDays = (dateAndSlot.Item1.Date - DateTime.Now.Date).TotalDays;
            checkOutModel.EstimatedDelivery = (isEnglish ? Messages.EstimatedDelivery : MessagesAr.EstimatedDelivery) + ": " + (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);

            return checkOutModel;
        }
        #endregion

        #region Order
        public async Task<OrderItemModel> PrepareOrderItemModel(OrderItem orderItem, bool isEnglish)
        {
            var orderItemModel = _mapper.Map<OrderItemModel>(orderItem);

            decimal finalUnitPrice = orderItem.UnitPrice - orderItem.DiscountAmount;
            orderItemModel.UnitPrice = finalUnitPrice;
            orderItemModel.FormattedUnitPrice = await _commonHelper.ConvertDecimalToString(orderItemModel.UnitPrice, isEnglish);
            orderItemModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(orderItem.Total, isEnglish);

            if (orderItem.Product != null)
            {
                orderItemModel.Product = await PrepareProductModel(product: orderItem.Product, isEnglish: isEnglish);
            }

            return orderItemModel;
        }
        public async Task<OrderModel> PrepareOrderModel(Order order, bool isEnglish, bool loadDetails = false)
        {
            var orderModel = _mapper.Map<OrderModel>(order);

            orderModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(order.Total, isEnglish, includeZero: true);
            orderModel.FormattedDate = order.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
            orderModel.FormattedTime = order.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            orderModel.PaymentResult = _commonHelper.GetPaymentResultTitle(order.PaymentStatusId, isEnglish: isEnglish);

            var OrderStatusDetails = _commonHelper.GetOrderStatusNameAndColorCode(order.OrderStatusId, isEnglish);
            orderModel.OrderStatusName = OrderStatusDetails.Item1;
            orderModel.OrderStatusColor = OrderStatusDetails.Item2;

            var paymentMethod = await _paymentMethodService.GetPaymentMethodById(order.PaymentMethodId);
            if (paymentMethod != null)
            {
                orderModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod);
            }

            //order summary
            List<KeyValuPairModel> amountSplitUps = new();
            orderModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(value: order.SubTotal, isEnglish: isEnglish, includeZero: true);
            amountSplitUps.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.Items : MessagesAr.Items,
                Value = orderModel.FormattedSubTotal,
                DisplayOrder = 0
            });

            if (order.DeliveryFee > 0)
            {
                orderModel.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(value: order.DeliveryFee, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
                    Value = orderModel.FormattedDeliveryFee,
                    DisplayOrder = 1
                });
            }

            if (order.CouponDiscountAmount > 0)
            {
                orderModel.FormattedCouponDiscountAmount = "-" + await _commonHelper.ConvertDecimalToString(value: order.CouponDiscountAmount, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
                    Value = orderModel.FormattedCouponDiscountAmount,
                    DisplayOrder = 2
                });
            }

            if (order.CashbackAmount > 0)
            {
                orderModel.FormattedCashbackAmount = "-" + await _commonHelper.ConvertDecimalToString(value: order.CashbackAmount, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
                    Value = orderModel.FormattedCashbackAmount,
                    DisplayOrder = 3
                });
            }

            if (order.WalletUsedAmount > 0)
            {
                orderModel.FormattedWalletUsedAmount = "-" + await _commonHelper.ConvertDecimalToString(value: order.WalletUsedAmount, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
                    Value = orderModel.FormattedWalletUsedAmount,
                    DisplayOrder = 4
                });
            }

            orderModel.AmountSplitUps = amountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

            if (loadDetails)
            {
                orderModel.FormattedItemCount = order.OrderItems.Count + " " + (isEnglish ? Messages.Items : MessagesAr.Items);

                if (order.Address != null)
                    orderModel.Address = await PrepareAddressModel(isEnglish: isEnglish, address: order.Address);

                var orderItems = order.OrderItems;
                foreach (var orderItem in orderItems)
                {
                    var orderItemModel = await PrepareOrderItemModel(orderItem: orderItem, isEnglish: isEnglish);
                    orderModel.OrderItems.Add(orderItemModel);
                }

                if (order.Customer != null)
                {
                    orderModel.Customer = PrepareCustomerModel(customer: order.Customer, isEnglish: isEnglish);
                }

                //payment summary
                List<KeyValuPairModel> patmentSummary = new();
                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderNumber : MessagesAr.OrderNumber,
                    Value = orderModel.OrderNumber,
                    DisplayOrder = 0
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderDate : MessagesAr.OrderDate,
                    Value = orderModel.FormattedDate,
                    DisplayOrder = 1
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderTime : MessagesAr.OrderTime,
                    Value = orderModel.FormattedTime,
                    DisplayOrder = 2
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderAmount : MessagesAr.OrderAmount,
                    Value = orderModel.FormattedTotal,
                    DisplayOrder = 3
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentMethod : MessagesAr.PaymentMethod,
                    Value = orderModel.PaymentMethod.Name,
                    DisplayOrder = 4
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentResult : MessagesAr.PaymentResult,
                    Value = orderModel.PaymentResult,
                    DisplayOrder = 5
                });

                if (!string.IsNullOrEmpty(orderModel.PaymentId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentId : MessagesAr.PaymentId,
                        Value = orderModel.PaymentId,
                        DisplayOrder = 6
                    });
                }

                if (!string.IsNullOrEmpty(orderModel.PaymentRefId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentReference : MessagesAr.PaymentReference,
                        Value = orderModel.PaymentRefId,
                        DisplayOrder = 7
                    });
                }

                if (!string.IsNullOrEmpty(orderModel.PaymentTrackId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.TrackId : MessagesAr.TrackId,
                        Value = orderModel.PaymentTrackId,
                        DisplayOrder = 8
                    });
                }

                orderModel.PaymentSummary = patmentSummary.OrderBy(a => a.DisplayOrder).ToList();

                var deliveryDays = (order.DeliveryDate.Date - DateTime.Now.Date).TotalDays;
                if (deliveryDays >= 0)
                {
                    orderModel.EstimatedDelivery = (isEnglish ? Messages.EstimatedDelivery : MessagesAr.EstimatedDelivery) + ": " + (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);
                    orderModel.EstimatedDeliveryWithoutHeading = (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);
                }
                else
                {
                    if (orderModel.Address != null)
                    {
                        orderModel.EstimatedDelivery = orderModel.Address.Name;
                    }
                }
            }
            else
            {
                var orderItems = await _orderService.GetAllOrderItem(order.Id);
                orderModel.FormattedItemCount = orderItems.Count + " " + (isEnglish ? Messages.Items : MessagesAr.Items);
            }
                    orderModel.OrderSummary = await PrepareOrderSummary(order, isEnglish);
            return orderModel;
        }
        #endregion

        #region Subscription
        public async Task<SubscriptionModel> PrepareSubscriptionModel(Subscription subscription, bool isEnglish, bool loadDetails = false)
        {
            var subscriptionModel = _mapper.Map<SubscriptionModel>(subscription);
            if(subscription.Product != null)
            {
                subscriptionModel.SubscriptionTitle = isEnglish ? subscription.Product.NameEn : subscription.Product.NameAr;
            }
            subscriptionModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(subscription.SubTotal, isEnglish, includeZero: true);
            subscriptionModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(subscription.Total, isEnglish, includeZero: true);
            subscriptionModel.FormattedDate = subscription.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
            subscriptionModel.FormattedTime = subscription.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            var subscriptionStatusDetails = _commonHelper.GetSubscriptionStatusNameAndColorCode(subscription.SubscriptionStatusId, isEnglish);
            subscriptionModel.SubscriptionStatusName = subscriptionStatusDetails.Item1;
            subscriptionModel.SubscriptionStatusColor = subscriptionStatusDetails.Item2;
            if(subscription.SubscriptionOrders != null) { 
                var subscriptionOrder = subscription.SubscriptionOrders.OrderBy(a => a.Id).FirstOrDefault();
                if (subscriptionOrder != null)
                {
                    if (subscriptionOrder.PaymentStatusId.HasValue)
                        subscriptionModel.PaymentResult = _commonHelper.GetPaymentResultTitle(subscriptionOrder.PaymentStatusId.Value, isEnglish: isEnglish);

                    if (subscriptionOrder.PaymentMethodId.HasValue)
                    {
                        var paymentMethod = await _paymentMethodService.GetPaymentMethodById(subscriptionOrder.PaymentMethodId.Value);
                        if (paymentMethod != null)
                        {
                            subscriptionModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod);
                        }
                    }

                    subscriptionModel.PaymentResult = subscriptionOrder.PaymentResult;
                    subscriptionModel.PaymentId = subscriptionOrder.PaymentId;
                    subscriptionModel.PaymentRefId = subscriptionOrder.PaymentRefId;
                    subscriptionModel.PaymentTrackId = subscriptionOrder.PaymentTrackId;
                }
            }  
            //order summary
            List<KeyValuPairModel> amountSplitUps = new();

            amountSplitUps.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.Quantity : MessagesAr.Quantity,
                Value = subscriptionModel.Quantity.ToString(),
                DisplayOrder = 0
            });

            var subscriptionDuration = await _productService.GetSubscriptionDurationById(subscription.DurationId);
            subscriptionModel.Duration = isEnglish ? subscriptionDuration?.NameEn : subscriptionDuration?.NameAr;
            amountSplitUps.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.Duration : MessagesAr.Duration,
                Value = subscriptionModel.Duration,
                DisplayOrder = 1
            });

            var subscriptionDeliveryDate = await _productService.GetSubscriptionDeliveryDateById(subscription.DeliveryDateId);
            subscriptionModel.DeliveryDate = isEnglish ? subscriptionDeliveryDate?.NameEn : subscriptionDeliveryDate?.NameAr;
            amountSplitUps.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.DeliveryDay : MessagesAr.DeliveryDay,
                Value = subscriptionModel.DeliveryDate,
                DisplayOrder = 2
            });

            amountSplitUps.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.SubscriptionPrice : MessagesAr.SubscriptionPrice,
                Value = subscriptionModel.FormattedSubTotal,
                DisplayOrder = 3
            });

            if (subscription.DeliveryFee > 0)
            {
                subscriptionModel.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(value: subscription.DeliveryFee, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
                    Value = subscriptionModel.FormattedDeliveryFee,
                    DisplayOrder = 4
                });
            }

            if (subscription.CouponDiscountAmount > 0)
            {
                subscriptionModel.FormattedCouponDiscountAmount = "-" + await _commonHelper.ConvertDecimalToString(value: subscription.CouponDiscountAmount, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
                    Value = subscriptionModel.FormattedCouponDiscountAmount,
                    DisplayOrder = 5
                });
            }

            if (subscription.CashbackAmount > 0)
            {
                subscriptionModel.FormattedCashbackAmount = "-" + await _commonHelper.ConvertDecimalToString(value: subscription.CashbackAmount, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
                    Value = subscriptionModel.FormattedCashbackAmount,
                    DisplayOrder = 6
                });
            }

            if (subscription.WalletUsedAmount > 0)
            {
                subscriptionModel.FormattedWalletUsedAmount = "-" + await _commonHelper.ConvertDecimalToString(value: subscription.WalletUsedAmount, isEnglish: isEnglish);
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
                    Value = subscriptionModel.FormattedWalletUsedAmount,
                    DisplayOrder = 7
                });
            }

            subscriptionModel.AmountSplitUps = amountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

            if (loadDetails)
            {
                if (subscription.Address != null)
                    subscriptionModel.Address = await PrepareAddressModel(isEnglish: isEnglish, address: subscription.Address);

                if (subscription.Customer != null)
                {
                    subscriptionModel.Customer = PrepareCustomerModel(customer: subscription.Customer, isEnglish: isEnglish);
                }

                //payment summary
                List<KeyValuPairModel> subscriptionDetails = new();

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Quantity : MessagesAr.Quantity,
                    Value = subscriptionModel.Quantity.ToString(),
                    DisplayOrder = 0
                });

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Duration : MessagesAr.Duration,
                    Value = isEnglish ? subscriptionDuration?.NameEn : subscriptionDuration?.NameAr,
                    DisplayOrder = 1
                });

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.DeliveryDay : MessagesAr.DeliveryDay,
                    Value = isEnglish ? subscriptionDeliveryDate?.NameEn : subscriptionDeliveryDate?.NameAr,
                    DisplayOrder = 2
                });

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.SubscriptionPrice : MessagesAr.SubscriptionPrice,
                    Value = subscriptionModel.FormattedSubTotal,
                    DisplayOrder = 3
                });

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Received : MessagesAr.Received,
                    Value = "1",
                    DisplayOrder = 0
                });

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Remaining : MessagesAr.Remaining,
                    Value = "1",
                    DisplayOrder = 1
                });

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.NextDelivery : MessagesAr.NextDelivery,
                    Value = "1",
                    DisplayOrder = 1
                });

                subscriptionModel.SubscriptionDetails = subscriptionDetails.OrderBy(a => a.DisplayOrder).ToList();

                var productDetails = await _productService.GetAllProductDetail(subscription.ProductId);
                foreach (var productDetail in productDetails)
                {
                    var childProduct = await _productService.GetById(productDetail.ChildProductId);
                    if (childProduct != null)
                    {
                        subscriptionModel.SubscriptionPackTitles.Add(new KeyValuPairModel
                        {
                            Title = (isEnglish ? childProduct.NameEn : childProduct.NameAr) + " x " + productDetail.Quantity,
                            Value = childProduct.Id.ToString()
                        });
                    }
                }

                //subscriptionModel.QuickPayments = .
            }

            return subscriptionModel;
        }
        public async Task<Utility.Models.Admin.Sales.AdminOrderSummaryModel> PrepareOrderSummary(Order order, bool isEnglish)
        {
            Utility.Models.Admin.Sales.AdminOrderSummaryModel model = new();
            if (order.OrderTypeId == OrderType.Online)
                model.OrderModeText = isEnglish ? Messages.Online : MessagesAr.Online;
            else
                model.OrderModeText = isEnglish ? Messages.Offline : MessagesAr.Offline;


            if (order.OrderStatusId == OrderStatus.Cancelled)
                model.OrderStatusText = isEnglish ? Messages.Cancelled : MessagesAr.Cancelled;
            else if (order.OrderStatusId == OrderStatus.CancelledByCustomer)
                model.OrderStatusText = isEnglish ? Messages.CancelledByCustomer : MessagesAr.CancelledByCustomer;
            else if (order.OrderStatusId == OrderStatus.Confirmed)
                model.OrderStatusText = isEnglish ? Messages.Confirmed : MessagesAr.Confirmed;
            else if (order.OrderStatusId == OrderStatus.Delivered)
                model.OrderStatusText = isEnglish ? Messages.Delivered : MessagesAr.Delivered;
            else if (order.OrderStatusId == OrderStatus.Discarded)
                model.OrderStatusText = isEnglish ? Messages.Discarded : MessagesAr.Discarded;
            else if (order.OrderStatusId == OrderStatus.Failed)
                model.OrderStatusText = isEnglish ? Messages.Failed : MessagesAr.Failed;
            else if (order.OrderStatusId == OrderStatus.OnTheWay)
                model.OrderStatusText = isEnglish ? Messages.OnTheWay : MessagesAr.OnTheWay;
            else if (order.OrderStatusId == OrderStatus.Pending)
                model.OrderStatusText = isEnglish ? Messages.Pending : MessagesAr.Pending;
            else if (order.OrderStatusId == OrderStatus.Received)
                model.OrderStatusText = isEnglish ? Messages.Received : MessagesAr.Received;
            else if (order.OrderStatusId == OrderStatus.Returned)
                model.OrderStatusText = isEnglish ? Messages.Returned : MessagesAr.Returned;


            //get order created by and driver info
            if (order.CreatedBy is not null)
            {
                int salesManagerId = (int)order.CreatedBy;
                model.SalesmanName = await _systemUserService.GetUserName(salesManagerId);
            }
            if (order.DriverId is not null)
            {
                int driverId = (int)order.DriverId;
                model.DriverName = await _systemUserService.GetUserName(driverId);
            }
            return model;
        }

        //private QuickPaymentModel PrepareQuickPayment(QuickPayment quickPayment, bool isEnglish)
        //{
        //    QuickPaymentModel paymentModel = _mapper.Map<QuickPaymentModel>(quickPayment);

        //    return paymentModel;
        //}
        #endregion
    }
    //public class ModelHelper : IModelHelper
    //{
    //    private readonly IMapper _mapper;
    //    private readonly AppSettingsModel _appSettings;
    //    private readonly ICommonHelper _commonHelper;
    //    private readonly ISystemUserService _systemUserService;
    //    private readonly IProductService _productService;
    //    private readonly ICartService _cartService;
    //    private readonly ICouponService _couponService;
    //    private readonly IAreaService _areaService;
    //    private readonly ICustomerService _customerService;
    //    private readonly IPromotionService _promotionService;
    //    private readonly IPaymentMethodService _paymentMethodService;
    //    private readonly IOrderService _orderService;
    //    public ModelHelper(IMapper mapper,
    //       IOptions<AppSettingsModel> options,
    //       ICommonHelper commonHelper,
    //       ISystemUserService systemUserService,
    //       IProductService productService,
    //       ICartService cartService,
    //       ICouponService couponService,
    //       IAreaService areaService,
    //       ICustomerService customerService,
    //       IPromotionService promotionService,
    //       IPaymentMethodService paymentMethodService,
    //       IOrderService orderService)
    //    {
    //        _mapper = mapper;
    //        _appSettings = options.Value;
    //        _commonHelper = commonHelper;
    //        _systemUserService = systemUserService;
    //        _productService = productService;
    //        _cartService = cartService;
    //        _couponService = couponService;
    //        _areaService = areaService;
    //        _customerService = customerService;
    //        _promotionService = promotionService;
    //        _paymentMethodService = paymentMethodService;
    //        _orderService = orderService;
    //    }

    //    #region Common
    //    public SocialMediaLinkModel PrepareSocialMediaLinkModel(SocialMediaLink model, bool isEnglish)
    //    {
    //        var itemModel = _mapper.Map<SocialMediaLinkModel>(model);
    //        return itemModel;
    //    }
    //    public BannerModel PrepareBannerModel(Banner banner, bool isEnglish)
    //    {
    //        var bannerModel = _mapper.Map<BannerModel>(banner);

    //        if (isEnglish)
    //        {
    //            if (string.IsNullOrEmpty(banner.ImageNameEn))
    //                bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerDefault;
    //            else
    //                bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerResized + banner.ImageNameEn;
    //        }
    //        else
    //        {
    //            if (string.IsNullOrEmpty(banner.ImageNameAr))
    //                bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerDefault;
    //            else
    //                bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerResized + banner.ImageNameAr;
    //        }

    //        return bannerModel;

    //    }
    //    public PageFooterContentModel PrepareSiteContentModel(IList<SiteContent> items, bool isEnglish)
    //    {
    //        PageFooterContentModel model = new();

    //        foreach (var item in items)
    //        {
    //            if (item.AppContentType == AppContentType.TermsCondition)
    //            {
    //                model.TermsConditions = isEnglish ? item.ContentEn : item.ContentAr;
    //            }
    //            else if (item.AppContentType == AppContentType.PrivacyPolicy)
    //            {
    //                model.PrivacyPolicy = isEnglish ? item.ContentEn : item.ContentAr;
    //            }
    //            else if (item.AppContentType == AppContentType.RefundPolicy)
    //            {
    //                model.RefundPolicy = isEnglish ? item.ContentEn : item.ContentAr;
    //            }
    //        }

    //        return model;
    //    }
    //    public SiteContentModel PrepareSiteContentModel(SiteContent siteContent, bool isEnglish)
    //    {
    //        var siteContentModel = _mapper.Map<SiteContentModel>(siteContent);
    //        siteContentModel.Content = isEnglish ? siteContent.ContentEn : siteContent.ContentAr;
    //        if (siteContent.AppContentType == AppContentType.AboutUs && siteContent.ImageName != null)
    //        {
    //            siteContentModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageSiteContent + siteContent.ImageName;
    //        }
    //        return siteContentModel;
    //    }
    //    public ContactDetailModel PrepareContactDetailModel(ContactDetail contactDetail, bool isEnglish)
    //    {
    //        var contactDetailModel = _mapper.Map<ContactDetailModel>(contactDetail);
    //        contactDetailModel.Address = isEnglish ? contactDetail.AddressEn : contactDetail.AddressAr;
    //        return contactDetailModel;
    //    }
    //    public GovernorateModel PrepareGovernorateModel(Governorate governorate, bool isEnglish)
    //    {
    //        var governorateModel = _mapper.Map<GovernorateModel>(governorate);
    //        governorateModel.Name = isEnglish ? governorate.NameEn : governorate.NameAr;
    //        return governorateModel;
    //    }
    //    public AreaModel PrepareAreaModel(Area area, bool isEnglish)
    //    {
    //        var areaModel = _mapper.Map<AreaModel>(area);
    //        areaModel.Name = isEnglish ? area.NameEn : area.NameAr;
    //        return areaModel;
    //    }
    //    public PaymentMethodModel PreparePaymentMethodModel(Data.Content.PaymentMethod paymentMethod, bool isEnglish)
    //    {
    //        var paymentMethodModel = new PaymentMethodModel();
    //        paymentMethodModel.Id = paymentMethod.Id;
    //        paymentMethodModel.Name = isEnglish ? paymentMethod.NameEn : paymentMethod.NameAr;

    //        if (string.IsNullOrEmpty(paymentMethod.ImageName))
    //            paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodDefault;
    //        else
    //            paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodResized + paymentMethod.ImageName;

    //        return paymentMethodModel;
    //    }


    //    #endregion

    //    #region Category
    //    public IList<CategoryModel> PrepareCategoryModels(IEnumerable<Category> models, bool isEnglish)
    //    {
    //        List<CategoryModel> items = new();
    //        return items;
    //    }
    //    public CategoryModel PrepareCategoryModel(Category model, bool isEnglish)
    //    {
    //        var itemModel = _mapper.Map<CategoryModel>(model);
    //        itemModel.Title = isEnglish ? model.NameEn : model.NameAr;
    //        return itemModel;

    //    }


    //    #endregion

    //    #region Customer
    //    public CustomerModel PrepareCustomerModel(Customer customer, bool isEnglish)
    //    {
    //        var customerModel = _mapper.Map<CustomerModel>(customer);

    //        if (customer.Country != null)
    //        {
    //            customerModel.FormattedMobile = customer.Country.MobileCode + " " + customer.MobileNumber;
    //        }

    //        return customerModel;
    //    }
    //    public async Task<AddressModel> PrepareAddressModel(Address address, bool isEnglish)
    //    {
    //        var addressModel = _mapper.Map<AddressModel>(address);

    //        var governorate = address.Area.Governorate;
    //        if (governorate != null)
    //        {
    //            addressModel.GovernorateId = governorate.Id;
    //            addressModel.GovernorateName = isEnglish ? governorate.NameEn : governorate.NameAr;
    //        }

    //        var area = address.Area;
    //        if (area != null)
    //        {
    //            addressModel.AreaName = isEnglish ? area.NameEn : area.NameAr;
    //        }

    //        string addressText = await _commonHelper.PrepareAddressText(address: address, isEnglish: isEnglish);
    //        addressModel.AddressText = addressText;

    //        return addressModel;
    //    }
    //    public NotificationModel PrepareNotificationModel(Notification notification, bool isEnglish)
    //    {
    //        var notificationModel = new NotificationModel();

    //        notificationModel.Id = notification.Id;
    //        if (!string.IsNullOrEmpty(notification.TitleEn))
    //            notificationModel.Title = notification.TitleEn;
    //        else
    //            notificationModel.Title = notification.TitleAr;

    //        if (!string.IsNullOrEmpty(notification.MessageEn))
    //            notificationModel.Message = notification.MessageEn;
    //        else
    //            notificationModel.Message = notification.MessageAr;

    //        notificationModel.TimeAgo = _commonHelper.GetTimeAgo(notification.CreatedOn, isEnglish);
    //        notificationModel.FormattedDate = notification.CreatedOn.ToString("dd'/'MM'/'yyyy - hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

    //        return notificationModel;
    //    }
    //    public async Task<WalletTransactionModel> PrepareWalletTransactionModel(WalletTransaction walletTransaction, bool isEnglish)
    //    {
    //        var walletTransactionModel = new WalletTransactionModel();

    //        if (walletTransaction.Credit > 0)
    //        {
    //            walletTransactionModel.Title = isEnglish ? Messages.Credit : MessagesAr.Credit;
    //            walletTransactionModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: walletTransaction.Credit, isEnglish: isEnglish);
    //            walletTransactionModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCommonResized + "credit-icon.png";
    //            walletTransactionModel.ColorCode = "#00aeef";
    //        }
    //        else
    //        {
    //            walletTransactionModel.Title = isEnglish ? Messages.Debit : MessagesAr.Debit;
    //            walletTransactionModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: walletTransaction.Debit, isEnglish: isEnglish);
    //            walletTransactionModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCommonResized + "debit-icon.png";
    //            walletTransactionModel.ColorCode = "#850b5a";
    //        }

    //        if (walletTransaction.WalletTypeId == WalletType.Wallet)
    //        {
    //            walletTransactionModel.WalletType = isEnglish ? Messages.Wallet : MessagesAr.Wallet;
    //        }
    //        else
    //        {
    //            walletTransactionModel.WalletType = isEnglish ? Messages.Cashback : MessagesAr.Cashback;
    //        }

    //        List<KeyValuPairModel> walletDetails = new();
    //        walletDetails.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.TransactionId : MessagesAr.TransactionId,
    //            Value = walletTransaction.TransactionNo,
    //            DisplayOrder = 0
    //        });

    //        walletDetails.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.TransactionDate : MessagesAr.TransactionDate,
    //            Value = walletTransaction.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW")),
    //            DisplayOrder = 1
    //        });

    //        walletDetails.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.TransactionTime : MessagesAr.TransactionTime,
    //            Value = walletTransaction.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW")),
    //            DisplayOrder = 2
    //        });

    //        walletDetails.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.TransactionAmount : MessagesAr.TransactionAmount,
    //            Value = walletTransactionModel.FormattedAmount,
    //            DisplayOrder = 3
    //        });

    //        walletDetails.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.TransactionType : MessagesAr.TransactionType,
    //            Value = walletTransactionModel.Title,
    //            DisplayOrder = 4
    //        });

    //        walletDetails.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.WalletType : MessagesAr.WalletType,
    //            Value = walletTransactionModel.WalletType,
    //            DisplayOrder = 5
    //        });

    //        if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.SignUpPromotion)
    //        {
    //            walletTransactionModel.Description = isEnglish ? Messages.SignUpBonus : MessagesAr.SignUpBonus;
    //        }
    //        else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.CashbackOnPurchase)
    //        {
    //            walletTransactionModel.Description = isEnglish ? Messages.PurchaseBonus : MessagesAr.PurchaseBonus;
    //        }
    //        else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.CashbackRedeem)
    //        {
    //            walletTransactionModel.Description = isEnglish ? Messages.CashbackRedeemOnPurchase : MessagesAr.CashbackRedeemOnPurchase;
    //        }
    //        else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.UseWalletAmount)
    //        {
    //            walletTransactionModel.Description = isEnglish ? Messages.WalletRedeemOnPurchase : MessagesAr.WalletRedeemOnPurchase;
    //        }

    //        walletDetails.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.Method : MessagesAr.Method,
    //            Value = walletTransactionModel.Description,
    //            DisplayOrder = 6
    //        });

    //        if (walletTransaction.RelatedEntityTypeId == RelatedEntityType.Order)
    //        {
    //            var order = await _orderService.GetOrderById(walletTransaction.RelatedEntityId);
    //            if (order != null)
    //            {
    //                walletDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.LinkedToOrder : MessagesAr.LinkedToOrder,
    //                    Value = order.OrderNumber,
    //                    DisplayOrder = 7
    //                });
    //            }
    //        }

    //        walletTransactionModel.PaymentSummary = walletDetails.OrderBy(a => a.DisplayOrder).ToList();

    //        return walletTransactionModel;
    //    }
    //    #endregion

    //    #region Product
    //    public async Task<ProductModel> PrepareProductModel(Product product, bool isEnglish, string customerGuidValue = "", Customer customer = null,
    //        bool loadDescription = false, bool loadPrice = false, bool calculateStock = false, bool loadCategory = false)
    //    {
    //        var productModel = _mapper.Map<ProductModel>(product);

    //        var title = isEnglish ? product.NameEn : product.NameAr;

    //        productModel.Title = title;

    //        if (loadDescription)
    //        {
    //            productModel.Description = isEnglish ? product.DescriptionEn : product.DescriptionAr;
    //        }

    //        if (loadCategory)
    //        {
    //            var categoryModel = PrepareCategoryModel(model: product.Category, isEnglish: isEnglish);
    //            productModel.CategoryName = categoryModel.Title;
    //        }

    //        if (product.ItemSize != null)
    //        {
    //            productModel.ItemSize.Id = product.ItemSize.Id;
    //            productModel.ItemSize.Title = isEnglish ? product.ItemSize.NameEn : product.ItemSize.NameAr;
    //        }

    //        bool b2bCustomer = false;
    //        if (customer != null)
    //        {
    //            var favorite = (await _productService.GetAllFavorite(customerId: customer.Id, productId: product.Id)).FirstOrDefault();
    //            if (favorite != null)
    //            {
    //                productModel.Favorite = true;
    //            }

    //            var productAvailabilityNotifyRequest = await _productService.GetProductAvailabilityNotifyRequest(customerId: customer.Id, productId: product.Id);
    //            if (productAvailabilityNotifyRequest != null)
    //            {
    //                productModel.Notified = true;
    //            }

    //            if (product.B2BPriceEnabled && customer.B2B)
    //            {
    //                b2bCustomer = true;
    //            }
    //        }

    //        decimal price = product.Price;
    //        decimal discountedPrice = 0;

    //        if (loadPrice)
    //        {
    //            price = product.GetPriceFrontend(b2bCustomer);
    //            discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);
    //        }

    //        productModel.Price = price;
    //        productModel.DiscountedPrice = discountedPrice;
    //        productModel.FormattedPrice = await _commonHelper.ConvertDecimalToString(productModel.Price, isEnglish, 0);
    //        productModel.FormattedDiscountedPrice = await _commonHelper.ConvertDecimalToString(productModel.DiscountedPrice, isEnglish, 0);

    //        if (string.IsNullOrEmpty(product.ImageName))
    //            productModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageProductDefault;
    //        else
    //            productModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageProductResized + product.ImageName;

    //        if (calculateStock)
    //        {
    //            if (product.ProductType == ProductType.BaseProduct)
    //            {
    //                var productStockQuantity = await _productService.GetAvailableStockQuantity(productId: product.Id,
    //                customerId: customer?.Id, customerGuidValue: customerGuidValue);
    //                if (productStockQuantity < 0)
    //                    productStockQuantity = 0;

    //                productModel.StockQuantity = productStockQuantity;
    //            }
    //            else if (product.ProductType == ProductType.BundledProduct)
    //            {
    //                int lowStockProduct = 0;
    //                List<int> childProductStocks = new();
    //                var productDetails = product.ProductDetails.ToList();
    //                foreach (var productDetail in productDetails)
    //                {
    //                    var productStockQuantity = await _productService.GetAvailableStockQuantity(productId: productDetail.ChildProductId,
    //                customerId: customer?.Id, customerGuidValue: customerGuidValue);
    //                    if (productStockQuantity <= 0)
    //                        lowStockProduct++;

    //                    childProductStocks.Add(productStockQuantity / productDetail.Quantity);
    //                }

    //                if (lowStockProduct > 0)
    //                    productModel.StockQuantity = 0;
    //                else
    //                    productModel.StockQuantity = childProductStocks.Min();
    //            }
    //            else if (product.ProductType == ProductType.SubscriptionProduct)
    //            {
    //                int lowStockProduct = 0;
    //                List<int> childProductStocks = new();
    //                var productDetails = product.ProductDetails.ToList();
    //                foreach (var productDetail in productDetails)
    //                {
    //                    var productStockQuantity = await _productService.GetAvailableStockQuantity(productId: productDetail.ChildProductId,
    //                customerId: customer?.Id, customerGuidValue: customerGuidValue);
    //                    if (productStockQuantity <= 0)
    //                        lowStockProduct++;

    //                    childProductStocks.Add(productStockQuantity / productDetail.Quantity);
    //                }

    //                if (lowStockProduct > 0)
    //                    productModel.StockQuantity = 0;
    //                else
    //                    productModel.StockQuantity = childProductStocks.Min();
    //            }
    //        }

    //        productModel.DetailsUrl = _appSettings.WebsiteUrl + "product/" + productModel.CategoryName + "/" + product.SeoName;

    //        return productModel;
    //    }
    //    #endregion

    //    #region Cart
    //    public async Task<CartItemModel> PrepareCartItemModel(CartItem cartItem, bool isEnglish)
    //    {
    //        var cartItemModel = _mapper.Map<CartItemModel>(cartItem);
    //        decimal total = 0;
    //        decimal shortTotal = 0;

    //        var product = await _productService.GetById(cartItem.ProductId);
    //        if (product != null)
    //        {
    //            var productModel = await PrepareProductModel(product: product, isEnglish: isEnglish, loadPrice: true, calculateStock: true);
    //            cartItemModel.Product = productModel;

    //            if (productModel.DiscountedPrice > 0)
    //            {
    //                total = productModel.DiscountedPrice * cartItem.Quantity;
    //                shortTotal = productModel.DiscountedPrice * cartItem.Quantity;
    //            }
    //            else
    //            {
    //                total = productModel.Price * cartItem.Quantity;
    //                shortTotal = productModel.Price * cartItem.Quantity;
    //            }
    //        }

    //        cartItemModel.Total = total;
    //        cartItemModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(total, isEnglish, countryId: 0, includeZero: true);

    //        cartItemModel.ShortTotal = shortTotal;
    //        cartItemModel.FormattedShortTotal = await _commonHelper.ConvertDecimalToString(shortTotal, isEnglish, countryId: 0, includeZero: true);

    //        return cartItemModel;
    //    }
    //    public async Task<CartModel> PrepareCartModel(IList<CartItem> cartItems, bool isEnglish)
    //    {
    //        var cartModel = new CartModel();

    //        foreach (var item in cartItems)
    //        {
    //            var cartItemModel = await PrepareCartItemModel(cartItem: item, isEnglish: isEnglish);
    //            cartModel.CartItems.Add(cartItemModel);
    //        }

    //        cartModel.SubTotal = cartModel.CartItems.Sum(a => a.Total);

    //        if (cartItems.Count > 0)
    //        {
    //            cartModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(value: cartModel.SubTotal, isEnglish: isEnglish,
    //                countryId: 0);
    //        }
    //        else
    //        {
    //            cartModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(value: 0, isEnglish: isEnglish,
    //                  countryId: 0, includeZero: true);
    //        }

    //        return cartModel;
    //    }
    //    public async Task<CartSummaryModel> PrepareCartSummaryModel(bool isEnglish, int customerId, List<CartItemModel> cartItemModels)
    //    {
    //        var cartSummaryModel = new CartSummaryModel();
    //        decimal subTotal = cartItemModels.Sum(a => a.Total);
    //        decimal deliveryFee = 0;
    //        decimal discountAmount = 0;
    //        decimal cashbackAmount = 0;
    //        decimal walletUsedAmount = 0;

    //        List<KeyValuPairModel> AmountSplitUps = new();

    //        AmountSplitUps.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.Items : MessagesAr.Items,
    //            Value = await _commonHelper.ConvertDecimalToString(value: subTotal, isEnglish: isEnglish, includeZero: true),
    //            DisplayOrder = 0
    //        });

    //        var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
    //        if (walletBalance > 0)
    //        {
    //            cartSummaryModel.WalletBalanceAmount = walletBalance;
    //            cartSummaryModel.FormattedWalletBalanceAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletBalanceAmount, isEnglish, includeZero: true);
    //        }

    //        var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
    //        if (cartAttribute != null)
    //        {
    //            if (cartAttribute.CashbackAmount > 0)
    //            {
    //                cashbackAmount = cartAttribute.CashbackAmount;
    //                AmountSplitUps.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
    //                    Value = "-" + await _commonHelper.ConvertDecimalToString(value: cartAttribute.CashbackAmount, isEnglish: isEnglish),
    //                    DisplayOrder = 3
    //                });
    //            }

    //            if (cartAttribute.WalletUsedAmount > 0)
    //            {
    //                walletUsedAmount = cartAttribute.WalletUsedAmount;
    //                AmountSplitUps.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
    //                    Value = "-" + await _commonHelper.ConvertDecimalToString(value: walletUsedAmount, isEnglish: isEnglish),
    //                    DisplayOrder = 4
    //                });

    //                cartSummaryModel.WalletUsedAmount = walletUsedAmount;
    //                cartSummaryModel.FormattedWalletUsedAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletUsedAmount, isEnglish);
    //            }

    //            if (cartAttribute.AddressId.HasValue)
    //            {
    //                var address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
    //                if (address != null)
    //                {
    //                    var area = await _areaService.GetById(address.AreaId);
    //                    if (area != null)
    //                    {
    //                        deliveryFee = await _commonHelper.GetDeliveryFeeByAreaId(areaId: area.Id);
    //                        AmountSplitUps.Add(new KeyValuPairModel
    //                        {
    //                            Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
    //                            Value = await _commonHelper.ConvertDecimalToString(value: deliveryFee, isEnglish: isEnglish),
    //                            DisplayOrder = 1
    //                        });
    //                    }
    //                }
    //            }

    //            if (cartAttribute.CouponId.HasValue)
    //            {
    //                var coupon = await _couponService.GetById(cartAttribute.CouponId.Value);
    //                if (coupon != null)
    //                {
    //                    discountAmount = coupon.ApplyCouponDiscount2(subTotal);
    //                    cartSummaryModel.CouponCode = coupon.CouponCode;

    //                    AmountSplitUps.Add(new KeyValuPairModel
    //                    {
    //                        Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
    //                        Value = "-" + await _commonHelper.ConvertDecimalToString(value: discountAmount, isEnglish: isEnglish),
    //                        DisplayOrder = 2
    //                    });
    //                }
    //                else
    //                {
    //                    cartAttribute.CouponId = null;
    //                    await _cartService.UpdateCartAttribute(cartAttribute);
    //                }
    //            }

    //            cartSummaryModel.SkipPaymentMethodSelection = (cartAttribute.PaymentMethodId ?? 0) == (int)Utility.Enum.PaymentMethod.Wallet;
    //        }

    //        cartSummaryModel.Total = subTotal + deliveryFee - discountAmount - cashbackAmount - walletUsedAmount;
    //        cartSummaryModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(cartSummaryModel.Total, isEnglish, includeZero: true);

    //        cartSummaryModel.AmountSplitUps = AmountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

    //        return cartSummaryModel;
    //    }
    //    public async Task<CheckOutModel> PrepareCheckOutModel(bool isEnglish, int customerId)
    //    {
    //        var checkOutModel = new CheckOutModel();

    //        var cartItems = await _cartService.GetAllCartItem(customerId: customerId);
    //        if (cartItems.Count == 0)
    //        {
    //            return null;
    //        }

    //        var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
    //        if (cartAttribute == null)
    //        {
    //            return null;
    //        }

    //        if (!cartAttribute.AddressId.HasValue)
    //        {
    //            return null;
    //        }

    //        if (!cartAttribute.PaymentMethodId.HasValue)
    //        {
    //            return null;
    //        }

    //        foreach (var cartItem in cartItems)
    //        {
    //            var cartItemModel = await PrepareCartItemModel(cartItem: cartItem, isEnglish: isEnglish);
    //            checkOutModel.CartItems.Add(cartItemModel);
    //        }

    //        checkOutModel.CartSummary = await PrepareCartSummaryModel(isEnglish: isEnglish, customerId: customerId, cartItemModels: checkOutModel.CartItems.ToList());

    //        var address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
    //        if (address == null)
    //        {
    //            return null;
    //        }

    //        string addressText = await _commonHelper.PrepareAddressText(address: address, isEnglish: isEnglish);
    //        checkOutModel.AddressText = addressText;

    //        var paymentMethod = await _paymentMethodService.GetPaymentMethodById(cartAttribute.PaymentMethodId.Value);
    //        if (paymentMethod == null)
    //        {
    //            return null;
    //        }

    //        checkOutModel.PaymentMethod = PreparePaymentMethodModel(paymentMethod: paymentMethod, isEnglish: isEnglish);

    //        var dateAndSlot = await _commonHelper.GetAvailableDeliveryDateAndSlot();
    //        var deliveryDays = (dateAndSlot.Item1.Date - DateTime.Now.Date).TotalDays;
    //        checkOutModel.EstimatedDelivery = (isEnglish ? Messages.EstimatedDelivery : MessagesAr.EstimatedDelivery) + ": " + (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);

    //        return checkOutModel;
    //    }
    //    #endregion

    //    #region Order
    //    public async Task<OrderItemModel> PrepareOrderItemModel(OrderItem orderItem, bool isEnglish)
    //    {
    //        var orderItemModel = _mapper.Map<OrderItemModel>(orderItem);

    //        decimal finalUnitPrice = orderItem.UnitPrice - orderItem.DiscountAmount;
    //        orderItemModel.FormattedUnitPrice = await _commonHelper.ConvertDecimalToString(finalUnitPrice, isEnglish);
    //        orderItemModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(orderItem.Total, isEnglish);

    //        if (orderItem.Product != null)
    //        {
    //            orderItemModel.Product = await PrepareProductModel(product: orderItem.Product, isEnglish: isEnglish);
    //        }

    //        return orderItemModel;
    //    }

    //    public async Task<OrderModel> PrepareOrderModel(Order order, bool isEnglish, bool loadDetails = false)
    //    {
    //        var orderModel = _mapper.Map<OrderModel>(order);

    //        if (order.OrderTypeId == OrderType.Online)
    //            orderModel.OrderTypeText = isEnglish ? Messages.Online : MessagesAr.Online;
    //        else if (order.OrderTypeId == OrderType.Offline)
    //            orderModel.OrderTypeText = isEnglish ? Messages.Offline: MessagesAr.Offline;

    //        orderModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(order.Total, isEnglish, includeZero: true);
    //        orderModel.FormattedDate = order.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
    //        orderModel.FormattedTime = order.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

    //        orderModel.PaymentResult = _commonHelper.GetPaymentResultTitle(order.PaymentStatusId, isEnglish: isEnglish);

    //        var OrderStatusDetails = _commonHelper.GetOrderStatusNameAndColorCode(order.OrderStatusId, isEnglish);
    //        orderModel.OrderStatusName = OrderStatusDetails.Item1;
    //        orderModel.OrderStatusColor = OrderStatusDetails.Item2;

    //        var paymentMethod = await _paymentMethodService.GetPaymentMethodById(order.PaymentMethodId);
    //        if (paymentMethod != null)
    //        {
    //            orderModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod);
    //        }

    //        //order summary
    //        List<KeyValuPairModel> amountSplitUps = new();
    //        orderModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(value: order.SubTotal, isEnglish: isEnglish, includeZero: true);
    //        amountSplitUps.Add(new KeyValuPairModel
    //        {
    //            Title = isEnglish ? Messages.Items : MessagesAr.Items,
    //            Value = orderModel.FormattedSubTotal,
    //            DisplayOrder = 0
    //        });

    //        if (order.DeliveryFee > 0)
    //        {
    //            orderModel.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(value: order.DeliveryFee, isEnglish: isEnglish);
    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
    //                Value = orderModel.FormattedDeliveryFee,
    //                DisplayOrder = 1
    //            });
    //        }

    //        if (order.CouponDiscountAmount > 0)
    //        {
    //            orderModel.FormattedCouponDiscountAmount = "-" + await _commonHelper.ConvertDecimalToString(value: order.CouponDiscountAmount, isEnglish: isEnglish);
    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
    //                Value = orderModel.FormattedCouponDiscountAmount,
    //                DisplayOrder = 2
    //            });
    //        }

    //        if (order.CashbackAmount > 0)
    //        {
    //            orderModel.FormattedCashbackAmount = "-" + await _commonHelper.ConvertDecimalToString(value: order.CashbackAmount, isEnglish: isEnglish);
    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
    //                Value = orderModel.FormattedCashbackAmount,
    //                DisplayOrder = 3
    //            });
    //        }

    //        if (order.WalletUsedAmount > 0)
    //        {
    //            orderModel.FormattedWalletUsedAmount = "-" + await _commonHelper.ConvertDecimalToString(value: order.WalletUsedAmount, isEnglish: isEnglish);
    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
    //                Value = orderModel.FormattedWalletUsedAmount,
    //                DisplayOrder = 4
    //            });
    //        }

    //        orderModel.AmountSplitUps = amountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

    //        if (loadDetails)
    //        {
    //            orderModel.FormattedItemCount = order.OrderItems.Count + " " + (isEnglish ? Messages.Items : MessagesAr.Items);

    //            if (order.Address != null)
    //                orderModel.Address = await PrepareAddressModel(isEnglish: isEnglish, address: order.Address);

    //            var orderItems = order.OrderItems;
    //            foreach (var orderItem in orderItems)
    //            {
    //                var orderItemModel = await PrepareOrderItemModel(orderItem: orderItem, isEnglish: isEnglish);
    //                orderModel.OrderItems.Add(orderItemModel);
    //            }

    //            if (order.Customer != null)
    //            {
    //                orderModel.Customer = PrepareCustomerModel(customer: order.Customer, isEnglish: isEnglish);
    //            }

    //            //payment summary
    //            List<KeyValuPairModel> patmentSummary = new();
    //            patmentSummary.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.OrderNumber : MessagesAr.OrderNumber,
    //                Value = orderModel.OrderNumber,
    //                DisplayOrder = 0
    //            });

    //            patmentSummary.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.OrderDate : MessagesAr.OrderDate,
    //                Value = orderModel.FormattedDate,
    //                DisplayOrder = 1
    //            });

    //            patmentSummary.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.OrderTime : MessagesAr.OrderTime,
    //                Value = orderModel.FormattedTime,
    //                DisplayOrder = 2
    //            });

    //            patmentSummary.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.OrderAmount : MessagesAr.OrderAmount,
    //                Value = orderModel.FormattedTotal,
    //                DisplayOrder = 3
    //            });

    //            patmentSummary.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.PaymentMethod : MessagesAr.PaymentMethod,
    //                Value = orderModel.PaymentMethod.Name,
    //                DisplayOrder = 4
    //            });

    //            patmentSummary.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.PaymentResult : MessagesAr.PaymentResult,
    //                Value = orderModel.PaymentResult,
    //                DisplayOrder = 5
    //            });

    //            if (!string.IsNullOrEmpty(orderModel.PaymentId))
    //            {
    //                patmentSummary.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.PaymentId : MessagesAr.PaymentId,
    //                    Value = orderModel.PaymentId,
    //                    DisplayOrder = 6
    //                });
    //            }

    //            if (!string.IsNullOrEmpty(orderModel.PaymentRefId))
    //            {
    //                patmentSummary.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.PaymentReference : MessagesAr.PaymentReference,
    //                    Value = orderModel.PaymentRefId,
    //                    DisplayOrder = 7
    //                });
    //            }

    //            if (!string.IsNullOrEmpty(orderModel.PaymentTrackId))
    //            {
    //                patmentSummary.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.TrackId : MessagesAr.TrackId,
    //                    Value = orderModel.PaymentTrackId,
    //                    DisplayOrder = 8
    //                });
    //            }

    //            orderModel.PaymentSummary = patmentSummary.OrderBy(a => a.DisplayOrder).ToList();

    //            var deliveryDays = (order.DeliveryDate.Date - DateTime.Now.Date).TotalDays;
    //            if (deliveryDays >= 0)
    //            {
    //                orderModel.EstimatedDelivery = (isEnglish ? Messages.EstimatedDelivery : MessagesAr.EstimatedDelivery) + ": " + (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);
    //                orderModel.EstimatedDeliveryWithoutHeading = (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);
    //            }
    //            else
    //            {
    //                if (orderModel.Address != null)
    //                {
    //                    orderModel.EstimatedDelivery = orderModel.Address.Name;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            var orderItems = await _orderService.GetAllOrderItem(order.Id);
    //            orderModel.FormattedItemCount = orderItems.Count + " " + (isEnglish ? Messages.Items : MessagesAr.Items);
    //        }
    //        orderModel.OrderSummary = await PrepareOrderSummary(order, isEnglish);
    //        return orderModel;
    //    }
    //    //public async Task<AdminOrderModel> PrepareOrderModelAdmin(Order order, bool isEnglish, bool loadDetails = false)
    //    //{
    //    //    var orderModel = _mapper.Map<AdminOrderModel>(order);

    //    //    var deliveryDays = (order.DeliveryDate.Date - DateTime.Now.Date).TotalDays;
    //    //    orderModel.EstimatedDelivery = (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);

    //    //    orderModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(order.Total, isEnglish);
    //    //    orderModel.FormattedDate = order.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
    //    //    orderModel.FormattedTime = order.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

    //    //    orderModel.PaymentResult = _commonHelper.GetPaymentResultTitle(order.PaymentStatusId, isEnglish: isEnglish);

    //    //    var paymentMethod = await _paymentMethodService.GetPaymentMethodById(order.PaymentMethodId);
    //    //    if (paymentMethod != null)
    //    //    {
    //    //        orderModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod);
    //    //    }

    //    //    //order summary
    //    //    List<KeyValuPairModel> amountSplitUps = new();
    //    //    amountSplitUps.Add(new KeyValuPairModel
    //    //    {
    //    //        Title = isEnglish ? Messages.Items : MessagesAr.Items,
    //    //        Value = await _commonHelper.ConvertDecimalToString(value: order.SubTotal, isEnglish: isEnglish, includeZero: true),
    //    //        DisplayOrder = 0
    //    //    });

    //    //    if (order.DeliveryFee > 0)
    //    //    {
    //    //        amountSplitUps.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
    //    //            Value = await _commonHelper.ConvertDecimalToString(value: order.DeliveryFee, isEnglish: isEnglish),
    //    //            DisplayOrder = 1
    //    //        });
    //    //    }

    //    //    if (order.CouponDiscountAmount > 0)
    //    //    {
    //    //        amountSplitUps.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
    //    //            Value = await _commonHelper.ConvertDecimalToString(value: order.CouponDiscountAmount, isEnglish: isEnglish),
    //    //            DisplayOrder = 2
    //    //        });
    //    //    }

    //    //    if (order.CashbackAmount > 0)
    //    //    {
    //    //        amountSplitUps.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
    //    //            Value = await _commonHelper.ConvertDecimalToString(value: order.CashbackAmount, isEnglish: isEnglish),
    //    //            DisplayOrder = 3
    //    //        });
    //    //    }

    //    //    if (order.WalletUsedAmount > 0)
    //    //    {
    //    //        amountSplitUps.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
    //    //            Value = await _commonHelper.ConvertDecimalToString(value: order.WalletUsedAmount, isEnglish: isEnglish),
    //    //            DisplayOrder = 4
    //    //        });
    //    //    }

    //    //    orderModel.AmountSplitUps = amountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

    //    //    if (loadDetails)
    //    //    {
    //    //        orderModel.FormattedItemCount = order.OrderItems.Count + " " + (isEnglish ? Messages.Items : MessagesAr.Items);

    //    //        if (order.Address != null)
    //    //            orderModel.Address = await PrepareAddressModel(isEnglish: isEnglish, address: order.Address);

    //    //        var orderItems = order.OrderItems;
    //    //        foreach (var orderItem in orderItems)
    //    //        {
    //    //            var orderItemModel = await PrepareOrderItemModel(orderItem: orderItem, isEnglish: isEnglish);
    //    //            orderModel.OrderItems.Add(orderItemModel);
    //    //        }

    //    //        if (order.Customer != null)
    //    //        {
    //    //            orderModel.Customer = PrepareCustomerModel(customer: order.Customer, isEnglish: isEnglish);
    //    //        }

    //    //        //payment summary
    //    //        List<KeyValuPairModel> patmentSummary = new();
    //    //        patmentSummary.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.OrderNumber : MessagesAr.OrderNumber,
    //    //            Value = orderModel.OrderNumber,
    //    //            DisplayOrder = 0
    //    //        });

    //    //        patmentSummary.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.OrderDate : MessagesAr.OrderDate,
    //    //            Value = orderModel.FormattedDate,
    //    //            DisplayOrder = 1
    //    //        });

    //    //        patmentSummary.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.OrderTime : MessagesAr.OrderTime,
    //    //            Value = orderModel.FormattedTime,
    //    //            DisplayOrder = 2
    //    //        });

    //    //        patmentSummary.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.OrderAmount : MessagesAr.OrderAmount,
    //    //            Value = orderModel.FormattedTotal,
    //    //            DisplayOrder = 3
    //    //        });

    //    //        patmentSummary.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.PaymentMethod : MessagesAr.PaymentMethod,
    //    //            Value = orderModel.PaymentMethod.Name,
    //    //            DisplayOrder = 4
    //    //        });

    //    //        patmentSummary.Add(new KeyValuPairModel
    //    //        {
    //    //            Title = isEnglish ? Messages.PaymentResult : MessagesAr.PaymentResult,
    //    //            Value = orderModel.PaymentResult,
    //    //            DisplayOrder = 5
    //    //        });

    //    //        if (!string.IsNullOrEmpty(orderModel.PaymentId))
    //    //        {
    //    //            patmentSummary.Add(new KeyValuPairModel
    //    //            {
    //    //                Title = isEnglish ? Messages.PaymentId : MessagesAr.PaymentId,
    //    //                Value = orderModel.PaymentId,
    //    //                DisplayOrder = 6
    //    //            });
    //    //        }

    //    //        if (!string.IsNullOrEmpty(orderModel.PaymentRefId))
    //    //        {
    //    //            patmentSummary.Add(new KeyValuPairModel
    //    //            {
    //    //                Title = isEnglish ? Messages.PaymentReference : MessagesAr.PaymentReference,
    //    //                Value = orderModel.PaymentRefId,
    //    //                DisplayOrder = 7
    //    //            });
    //    //        }

    //    //        if (!string.IsNullOrEmpty(orderModel.PaymentTrackId))
    //    //        {
    //    //            patmentSummary.Add(new KeyValuPairModel
    //    //            {
    //    //                Title = isEnglish ? Messages.TrackId : MessagesAr.TrackId,
    //    //                Value = orderModel.PaymentTrackId,
    //    //                DisplayOrder = 8
    //    //            });
    //    //        }

    //    //        orderModel.PaymentSummary = patmentSummary.OrderBy(a => a.DisplayOrder).ToList();

    //    //    }
    //    //    else
    //    //    {
    //    //        var orderItems = await _orderService.GetAllOrderItem(order.Id);
    //    //        orderModel.FormattedItemCount = orderItems.Count + " " + (isEnglish ? Messages.Items : MessagesAr.Items);
    //    //    }
    //    //    orderModel.OrderSummary = await PrepareOrderSummary(order, isEnglish);
    //    //    return orderModel;
    //    //}

    //    public async Task<Utility.Models.Frontend.Sales.SubscriptionModel> PrepareSubscriptionModel(Subscription subscription, bool isEnglish, bool loadDetails = false)
    //    {
    //           var subscriptionModel = _mapper.Map<Utility.Models.Frontend.Sales.SubscriptionModel>(subscription);

    //            subscriptionModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(subscription.SubTotal, isEnglish, includeZero: true);
    //            subscriptionModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(subscription.Total, isEnglish, includeZero: true);
    //            subscriptionModel.FormattedDate = subscription.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
    //            subscriptionModel.FormattedTime = subscription.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

    //            var subscriptionStatusDetails = _commonHelper.GetSubscriptionStatusNameAndColorCode(subscription.SubscriptionStatusId, isEnglish);
    //            subscriptionModel.SubscriptionStatusName = subscriptionStatusDetails.Item1;
    //            subscriptionModel.SubscriptionStatusColor = subscriptionStatusDetails.Item2;

    //            var subscriptionOrder = subscription.SubscriptionOrders.OrderBy(a => a.Id).FirstOrDefault();
    //            if (subscriptionOrder != null)
    //            {
    //                if (subscriptionOrder.PaymentStatusId.HasValue)
    //                    subscriptionModel.PaymentResult = _commonHelper.GetPaymentResultTitle(subscriptionOrder.PaymentStatusId.Value, isEnglish: isEnglish);

    //                if (subscriptionOrder.PaymentMethodId.HasValue)
    //                {
    //                    var paymentMethod = await _paymentMethodService.GetPaymentMethodById(subscriptionOrder.PaymentMethodId.Value);
    //                    if (paymentMethod != null)
    //                    {
    //                        subscriptionModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod);
    //                    }
    //                }

    //                subscriptionModel.PaymentResult = subscriptionOrder.PaymentResult;
    //                subscriptionModel.PaymentId = subscriptionOrder.PaymentId;
    //                subscriptionModel.PaymentRefId = subscriptionOrder.PaymentRefId;
    //                subscriptionModel.PaymentTrackId = subscriptionOrder.PaymentTrackId;
    //            }

    //            //order summary
    //            List<KeyValuPairModel> amountSplitUps = new();

    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.Quantity : MessagesAr.Quantity,
    //                Value = subscriptionModel.Quantity.ToString(),
    //                DisplayOrder = 0
    //            });

    //            var subscriptionDuration = await _productService.GetSubscriptionDurationById(subscription.DurationId);
    //            subscriptionModel.Duration = isEnglish ? subscriptionDuration?.NameEn : subscriptionDuration?.NameAr;
    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.Duration : MessagesAr.Duration,
    //                Value = subscriptionModel.Duration,
    //                DisplayOrder = 1
    //            });

    //            var subscriptionDeliveryDate = await _productService.GetSubscriptionDeliveryDateById(subscription.DeliveryDateId);
    //            subscriptionModel.DeliveryDate = isEnglish ? subscriptionDeliveryDate?.NameEn : subscriptionDeliveryDate?.NameAr;
    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.DeliveryDay : MessagesAr.DeliveryDay,
    //                Value = subscriptionModel.DeliveryDate,
    //                DisplayOrder = 2
    //            });

    //            amountSplitUps.Add(new KeyValuPairModel
    //            {
    //                Title = isEnglish ? Messages.SubscriptionPrice : MessagesAr.SubscriptionPrice,
    //                Value = subscriptionModel.FormattedSubTotal,
    //                DisplayOrder = 3
    //            });

    //            if (subscription.DeliveryFee > 0)
    //            {
    //                subscriptionModel.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(value: subscription.DeliveryFee, isEnglish: isEnglish);
    //                amountSplitUps.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
    //                    Value = subscriptionModel.FormattedDeliveryFee,
    //                    DisplayOrder = 4
    //                });
    //            }

    //            if (subscription.CouponDiscountAmount > 0)
    //            {
    //                subscriptionModel.FormattedCouponDiscountAmount = "-" + await _commonHelper.ConvertDecimalToString(value: subscription.CouponDiscountAmount, isEnglish: isEnglish);
    //                amountSplitUps.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
    //                    Value = subscriptionModel.FormattedCouponDiscountAmount,
    //                    DisplayOrder = 5
    //                });
    //            }

    //            if (subscription.CashbackAmount > 0)
    //            {
    //                subscriptionModel.FormattedCashbackAmount = "-" + await _commonHelper.ConvertDecimalToString(value: subscription.CashbackAmount, isEnglish: isEnglish);
    //                amountSplitUps.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
    //                    Value = subscriptionModel.FormattedCashbackAmount,
    //                    DisplayOrder = 6
    //                });
    //            }

    //            if (subscription.WalletUsedAmount > 0)
    //            {
    //                subscriptionModel.FormattedWalletUsedAmount = "-" + await _commonHelper.ConvertDecimalToString(value: subscription.WalletUsedAmount, isEnglish: isEnglish);
    //                amountSplitUps.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
    //                    Value = subscriptionModel.FormattedWalletUsedAmount,
    //                    DisplayOrder = 7
    //                });
    //            }

    //            subscriptionModel.AmountSplitUps = amountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

    //            if (loadDetails)
    //            {
    //                if (subscription.Address != null)
    //                    subscriptionModel.Address = await PrepareAddressModel(isEnglish: isEnglish, address: subscription.Address);

    //                if (subscription.Customer != null)
    //                {
    //                    subscriptionModel.Customer = PrepareCustomerModel(customer: subscription.Customer, isEnglish: isEnglish);
    //                }

    //                //payment summary
    //                List<KeyValuPairModel> subscriptionDetails = new();

    //                subscriptionDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.Quantity : MessagesAr.Quantity,
    //                    Value = subscriptionModel.Quantity.ToString(),
    //                    DisplayOrder = 0
    //                });

    //                subscriptionDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.Duration : MessagesAr.Duration,
    //                    Value = isEnglish ? subscriptionDuration?.NameEn : subscriptionDuration?.NameAr,
    //                    DisplayOrder = 1
    //                });

    //                subscriptionDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.DeliveryDay : MessagesAr.DeliveryDay,
    //                    Value = isEnglish ? subscriptionDeliveryDate?.NameEn : subscriptionDeliveryDate?.NameAr,
    //                    DisplayOrder = 2
    //                });

    //                subscriptionDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.SubscriptionPrice : MessagesAr.SubscriptionPrice,
    //                    Value = subscriptionModel.FormattedSubTotal,
    //                    DisplayOrder = 3
    //                });

    //                subscriptionDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.Received : MessagesAr.Received,
    //                    Value = "1",
    //                    DisplayOrder = 0
    //                });

    //                subscriptionDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.Remaining : MessagesAr.Remaining,
    //                    Value = "1",
    //                    DisplayOrder = 1
    //                });

    //                subscriptionDetails.Add(new KeyValuPairModel
    //                {
    //                    Title = isEnglish ? Messages.NextDelivery : MessagesAr.NextDelivery,
    //                    Value = "1",
    //                    DisplayOrder = 1
    //                });

    //                subscriptionModel.SubscriptionDetails = subscriptionDetails.OrderBy(a => a.DisplayOrder).ToList();

    //                var productDetails = await _productService.GetAllProductDetail(subscription.ProductId);
    //                foreach (var productDetail in productDetails)
    //                {
    //                    var childProduct = await _productService.GetById(productDetail.ChildProductId);
    //                    if (childProduct != null)
    //                    {
    //                        subscriptionModel.SubscriptionPackTitles.Add(new KeyValuPairModel
    //                        {
    //                            Title = (isEnglish ? childProduct.NameEn : childProduct.NameAr) + " x " + productDetail.Quantity,
    //                            Value = childProduct.Id.ToString()
    //                        });
    //                    }
    //                }
    //            }

    //            return subscriptionModel;
    //        }


    //        #endregion


    //    }

}
