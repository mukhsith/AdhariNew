using API.Helpers;
using AutoMapper;
using Data.Content;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.Locations;
using Data.ProductManagement;
using Data.PushNotification;
using Data.Sales;
using Data.Shop;
using Microsoft.Extensions.Options;
using Services.Frontend.Content;
using Services.Frontend.CouponPromotion;
using Services.Frontend.CustomerManagement;
using Services.Frontend.DeliveryManagement;
using Services.Frontend.Locations;
using Services.Frontend.ProductManagement;
using Services.Frontend.Sales;
using Services.Frontend.Shop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
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
    public class ModelHelper : IModelHelper
    {
        private readonly IMapper _mapper;
        private readonly AppSettingsModel _appSettings;
        private readonly ICommonHelper _commonHelper;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        private readonly IAreaService _areaService;
        private readonly ICustomerService _customerService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IOrderService _orderService;
        public ModelHelper(IMapper mapper,
           IOptions<AppSettingsModel> options,
           ICommonHelper commonHelper,
           IProductService productService,
           ICartService cartService,
           ICouponService couponService,
           IAreaService areaService,
           ICustomerService customerService,
           IPaymentMethodService paymentMethodService,
           IOrderService orderService)
        {
            _mapper = mapper;
            _appSettings = options.Value;
            _commonHelper = commonHelper;
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
            _areaService = areaService;
            _customerService = customerService;
            _paymentMethodService = paymentMethodService;
            _orderService = orderService;
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

            var imageName = isEnglish ? banner.ImageNameEn : banner.ImageNameAr;
            if (string.IsNullOrEmpty(imageName))
                bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerDefault;
            else
                bannerModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageBannerResized + imageName;

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
        public PaymentMethodModel PreparePaymentMethodModel(Data.Content.PaymentMethod paymentMethod, bool isEnglish, bool details = false)
        {
            var paymentMethodModel = new PaymentMethodModel();
            paymentMethodModel.Id = paymentMethod.Id;
            paymentMethodModel.Name = isEnglish ? paymentMethod.NameEn : paymentMethod.NameAr;

            if (details)
            {
                if (string.IsNullOrEmpty(paymentMethod.IconName))
                    paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodDefault;
                else
                    paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodResized + paymentMethod.IconName;
            }
            else
            {
                string imageName = isEnglish ? paymentMethod.ImageName : paymentMethod.ImageNameAr;
                if (string.IsNullOrEmpty(imageName))
                    paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodDefault;
                else
                    paymentMethodModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImagePaymentMethodResized + imageName;
            }

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
        public async Task<QuickPaymentModel> PrepareQuickPaymentModel(QuickPayment quickPayment, bool isEnglish)
        {
            QuickPaymentModel quickPayModel = new();

            quickPayModel.QuickPayNumber = quickPayment.PaymentNumber;
            quickPayModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(value: quickPayment.Amount, isEnglish: isEnglish, includeZero: true);

            if (quickPayment.PaymentStatusId.HasValue)
                quickPayModel.PaymentStatusId = (int)quickPayment.PaymentStatusId;

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

            if (quickPayment.Used)
            {
                //payment summary
                var formattedTotal = await _commonHelper.ConvertDecimalToString(quickPayment.Amount, isEnglish, includeZero: true);
                var formattedDate = quickPayment.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
                var formattedTime = quickPayment.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

                string paymentMethodName = string.Empty;
                if (quickPayment.PaymentMethodId.HasValue)
                {
                    var paymentMethod = await _paymentMethodService.GetPaymentMethodById(quickPayment.PaymentMethodId.Value);
                    if (paymentMethod != null)
                    {
                        paymentMethodName = isEnglish ? paymentMethod.NameEn : paymentMethod.NameAr;
                    }
                }

                string paymentResult = string.Empty;
                if (quickPayment.PaymentMethodId.HasValue)
                {
                    paymentResult = _commonHelper.GetPaymentResultTitle(quickPayment.PaymentStatusId.Value, isEnglish: isEnglish);
                }

                List<KeyValuPairModel> patmentSummary = new();
                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderNumber : MessagesAr.OrderNumber,
                    Value = quickPayment.PaymentNumber,
                    DisplayOrder = 0
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderDate : MessagesAr.OrderDate,
                    Value = formattedDate,
                    DisplayOrder = 1
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderTime : MessagesAr.OrderTime,
                    Value = formattedTime,
                    DisplayOrder = 2
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderAmount : MessagesAr.OrderAmount,
                    Value = formattedTotal,
                    DisplayOrder = 3
                });

                if (!string.IsNullOrEmpty(paymentMethodName))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentMethod : MessagesAr.PaymentMethod,
                        Value = paymentMethodName,
                        DisplayOrder = 4
                    });
                }

                if (quickPayment.PaymentStatusId.HasValue)
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentResult : MessagesAr.PaymentResult,
                        Value = _commonHelper.GetPaymentResultTitle(quickPayment.PaymentStatusId.Value, isEnglish),
                        DisplayOrder = 5
                    });
                }

                if (!string.IsNullOrEmpty(quickPayment.PaymentId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentId : MessagesAr.PaymentId,
                        Value = quickPayment.PaymentId,
                        DisplayOrder = 6
                    });
                }

                if (!string.IsNullOrEmpty(quickPayment.PaymentRefId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentReference : MessagesAr.PaymentReference,
                        Value = quickPayment.PaymentRefId,
                        DisplayOrder = 7
                    });
                }

                if (!string.IsNullOrEmpty(quickPayment.PaymentTrackId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.TrackId : MessagesAr.TrackId,
                        Value = quickPayment.PaymentTrackId,
                        DisplayOrder = 8
                    });
                }

                quickPayModel.PaymentSummary = patmentSummary.OrderBy(a => a.DisplayOrder).ToList();
            }

            return quickPayModel;
        }
        #endregion

        #region Category
        public IList<CategoryModel> PrepareCategoryModels(IEnumerable<Category> models, bool isEnglish)
        {
            List<CategoryModel> items = new();
            return items;
        }
        public CategoryModel PrepareCategoryModel(Category category, bool isEnglish)
        {
            var categoryModel = _mapper.Map<CategoryModel>(category);

            categoryModel.Title = isEnglish ? category.NameEn : category.NameAr;

            if (string.IsNullOrEmpty(category.ImageName))
                categoryModel.HoverImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryDefault;
            else
                categoryModel.HoverImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryResized + category.ImageName;

            if (string.IsNullOrEmpty(category.ImageNormalIconName))
                categoryModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryDefault;
            else
                categoryModel.ImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryResized + category.ImageNormalIconName;

            if (string.IsNullOrEmpty(category.ImageSelectedIconName))
                categoryModel.SelectedImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryDefault;
            else
                categoryModel.SelectedImageUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryResized + category.ImageSelectedIconName;

            if (string.IsNullOrEmpty(category.ImageDesktopName))
                categoryModel.ImageDesktopUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryDefault;
            else
                categoryModel.ImageDesktopUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryResized + category.ImageDesktopName;

            if (string.IsNullOrEmpty(category.ImageMobileName))
                categoryModel.ImageMobileUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryDefault;
            else
                categoryModel.ImageMobileUrl = _appSettings.APIBaseUrl + _appSettings.ImageCategoryResized + category.ImageMobileName;

            return categoryModel;

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

            addressModel.TypeTitle = _commonHelper.GetAddressTypeTitle((AddressType)address.TypeId, isEnglish);

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

            walletDetails.Add(new KeyValuPairModel
            {
                Title = isEnglish ? Messages.TransactionType : MessagesAr.TransactionType,
                Value = walletTransactionModel.Title,
                DisplayOrder = 4
            });

            if (walletTransaction.ExpiryDate.HasValue)
            {
                walletDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.ValidTill : MessagesAr.ValidTill,
                    Value = walletTransaction.ExpiryDate.Value.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW")),
                    DisplayOrder = 5
                });
            }

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
                walletTransactionModel.Description = isEnglish ? Messages.RefundWalletAmount : MessagesAr.RefundWalletAmount;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.RefundCashbackOnPurchase)
            {
                walletTransactionModel.Description = isEnglish ? Messages.RefundCashbackOnPurchase : MessagesAr.RefundCashbackOnPurchase;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.RefundOrderAmount)
            {
                walletTransactionModel.Description = isEnglish ? Messages.RefundOrderAmount : MessagesAr.RefundOrderAmount;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.RefundSubscriptionAmount)
            {
                walletTransactionModel.Description = isEnglish ? Messages.RefundSubscriptionAmount : MessagesAr.RefundOrderAmount;
            }
            else if (walletTransaction.WalletTransactionTypeId == WalletTransactionType.TopUp)
            {
                walletTransactionModel.Description = isEnglish ? Messages.TopUp : MessagesAr.TopUp;
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
        public async Task<WalletPackageOrderModel> PrepareWalletPackageOrderModel(WalletPackageOrder walletPackageOrder, bool isEnglish, bool loadDetails = false)
        {
            var walletPackageOrderModel = _mapper.Map<WalletPackageOrderModel>(walletPackageOrder);

            walletPackageOrderModel.FormattedAmount = await _commonHelper.ConvertDecimalToString(walletPackageOrder.Amount, isEnglish, includeZero: true);
            walletPackageOrderModel.FormattedWalletAmount = await _commonHelper.ConvertDecimalToString(walletPackageOrder.WalletAmount, isEnglish, includeZero: true);
            walletPackageOrderModel.FormattedDate = walletPackageOrder.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
            walletPackageOrderModel.FormattedTime = walletPackageOrder.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            walletPackageOrderModel.PaymentResult = _commonHelper.GetPaymentResultTitle(walletPackageOrder.PaymentStatusId, isEnglish: isEnglish);

            var paymentMethod = await _paymentMethodService.GetPaymentMethodById(walletPackageOrder.PaymentMethodId);
            if (paymentMethod != null)
            {
                walletPackageOrderModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod, details: true);
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
                    Value = _commonHelper.GetPaymentResultTitle(walletPackageOrder.PaymentStatusId, isEnglish),
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
                productModel.CategoryName = isEnglish ? product.Category.NameEn : product.Category.NameAr;
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

                //int availableQuantity = productModel.StockQuantity - productModel.CartQuantity;
                //if (availableQuantity < 0)
                //    availableQuantity = 0;

                //productModel.StockQuantity = availableQuantity;
            }

            if (product.ProductType == ProductType.SubscriptionProduct)
            {
                if (loadSubscriptionAttributes)
                {
                    var subscriptionDurations = await _productService.GetAllSubscriptionDuration();
                    if (product.SpecialPackage)
                    {
                        if (product.SubscriptionDurationId.HasValue)
                        {
                            subscriptionDurations = subscriptionDurations.Where(a => a.Id == product.SubscriptionDurationId.Value).ToList();
                        }
                    }

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
                Title = isEnglish ? Messages.Items : MessagesAr.Items,
                Value = await _commonHelper.ConvertDecimalToString(value: subTotal, isEnglish: isEnglish, includeZero: true),
                DisplayOrder = 0
            });

            var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
            cartSummaryModel.WalletBalanceAmount = walletBalance;
            cartSummaryModel.FormattedWalletBalanceAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletBalanceAmount, isEnglish);

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
                cartSummaryModel.FormattedWalletUsedAmount = await _commonHelper.ConvertDecimalToString(cartSummaryModel.WalletUsedAmount, isEnglish);
            }

            cartSummaryModel.Total = subTotal + deliveryFee - discountAmount - cashbackAmount - walletUsedAmount;
            cartSummaryModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(cartSummaryModel.Total, isEnglish, includeZero: true);

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

            checkOutModel.Customer = PrepareCustomerModel(customer, isEnglish);

            checkOutModel.CartSummary = await PrepareCartSummaryModel(isEnglish: isEnglish, customerId: customerId, cartItemModels: checkOutModel.CartItems.ToList());

            var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
            if (cartAttribute != null)
            {
                if (cartAttribute.AddressId.HasValue)
                {
                    var address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
                    if (address != null)
                    {
                        string addressText = await _commonHelper.PrepareAddressText(address: address, isEnglish: isEnglish);
                        checkOutModel.AddressText = addressText;
                    }
                }

                if (cartAttribute.PaymentMethodId.HasValue)
                {
                    var paymentMethod = await _paymentMethodService.GetPaymentMethodById(cartAttribute.PaymentMethodId.Value);
                    if (paymentMethod != null)
                    {
                        checkOutModel.PaymentMethod = PreparePaymentMethodModel(paymentMethod: paymentMethod, isEnglish: isEnglish, details: true);
                    }
                }
            }

            var dateAndSlot = await _commonHelper.GetAvailableDeliveryDateAndSlot();
            var deliveryDays = (dateAndSlot.Item1.Date - DateTime.Now.Date).TotalDays;
            checkOutModel.EstimatedDelivery = (isEnglish ? Messages.EstimatedDelivery : MessagesAr.EstimatedDelivery) + ": " + (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);
            checkOutModel.EstimatedDeliveryValue = (deliveryDays + 1) + " - " + (deliveryDays + 2) + " " + (isEnglish ? Messages.Days : MessagesAr.Days);

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
                orderModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod, details: true);

                if (order.PaymentMethodId != (int)Utility.Enum.PaymentMethod.Wallet && order.WalletUsedAmount > 0)
                {
                    orderModel.PaymentMethod.Name = orderModel.PaymentMethod.Name + " " + (isEnglish ? Messages.And : MessagesAr.And) + " " +
                        (isEnglish ? Messages.Wallet : MessagesAr.Wallet);
                }
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
                    Value = _commonHelper.GetPaymentResultTitle(order.PaymentStatusId, isEnglish),
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

            return orderModel;
        }
        #endregion

        #region Subscription
        public async Task<SubscriptionSummaryModel> PrepareSubscriptionSummaryModel(bool isEnglish, Customer customer, bool app = true)
        {
            var subscriptionSummaryModel = new SubscriptionSummaryModel();
            decimal subscriptionPrice = 0;
            decimal deliveryFee = 0;
            decimal discountAmount = 0;
            decimal cashbackAmount = 0;
            decimal walletUsedAmount = 0;
            bool b2bCustomer = false;
            List<KeyValuPairModel> AmountSplitUps = new();

            if (customer == null)
            {
                return null;
            }

            var subscriptionAttribute = await _cartService.GetSubscriptionAttributeByCustomerId(customer.Id);
            if (subscriptionAttribute == null)
            {
                return null;
            }

            if (!subscriptionAttribute.ProductId.HasValue || !subscriptionAttribute.Quantity.HasValue || !subscriptionAttribute.DurationId.HasValue ||
                !subscriptionAttribute.DeliveryDateId.HasValue)
            {
                return null;
            }

            var product = await _productService.GetById(subscriptionAttribute.ProductId.Value);
            if (product == null)
            {
                return null;
            }

            var subscriptionDuration = await _productService.GetSubscriptionDurationById(subscriptionAttribute.DurationId.Value);
            if (subscriptionDuration == null)
            {
                return null;
            }

            var subscriptionDeliveryDate = await _productService.GetSubscriptionDeliveryDateById(subscriptionAttribute.DeliveryDateId.Value);
            if (subscriptionDeliveryDate == null)
            {
                return null;
            }

            b2bCustomer = customer != null && customer.B2B;

            var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customer.Id, walletTypeId: WalletType.Wallet);
            subscriptionSummaryModel.WalletBalanceAmount = walletBalance;
            subscriptionSummaryModel.FormattedWalletBalanceAmount = await _commonHelper.ConvertDecimalToString(subscriptionSummaryModel.WalletBalanceAmount, isEnglish, includeZero: true);

            decimal price = product.GetPriceFrontend(b2bCustomer);
            decimal discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);
            subscriptionPrice = (discountedPrice > 0 ? discountedPrice : price) * subscriptionAttribute.Quantity.Value;

            bool fullPayment = false;
            if (subscriptionAttribute.PaymentMethodId.HasValue && subscriptionAttribute.PaymentMethodId.Value == (int)Utility.Enum.PaymentMethod.Tabby)
            {
                fullPayment = true;
                if (!product.SpecialPackage)
                    subscriptionPrice *= subscriptionDuration.NumberOfMonths;
            }
            else
            {
                if (product.SpecialPackage)
                    fullPayment = true;
            }

            if (app)
            {
                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Quantity : MessagesAr.Quantity,
                    Value = subscriptionAttribute.Quantity.ToString(),
                    DisplayOrder = 0
                });
            }

            if (app)
            {
                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Duration : MessagesAr.Duration,
                    Value = isEnglish ? subscriptionDuration.NameEn : subscriptionDuration.NameAr,
                    DisplayOrder = 1
                });
            }

            if (app)
            {
                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.DeliveryDay : MessagesAr.DeliveryDay,
                    Value = isEnglish ? subscriptionDeliveryDate.NameEn : subscriptionDeliveryDate.NameAr,
                    DisplayOrder = 2
                });
            }

            if (app)
            {
                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.SubscriptionPrice : MessagesAr.SubscriptionPrice,
                    Value = await _commonHelper.ConvertDecimalToString(value: subscriptionPrice, isEnglish: isEnglish, includeZero: true),
                    DisplayOrder = 3
                });
            }
            else
            {
                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.SubTotal : MessagesAr.SubTotal,
                    Value = await _commonHelper.ConvertDecimalToString(value: subscriptionPrice, isEnglish: isEnglish, includeZero: true),
                    DisplayOrder = 3
                });
            }

            if (subscriptionAttribute.AddressId.HasValue)
            {
                var address = await _customerService.GetAddressById(subscriptionAttribute.AddressId.Value);
                if (address != null)
                {
                    var area = await _areaService.GetById(address.AreaId);
                    if (area != null)
                    {
                        deliveryFee = await _commonHelper.GetDeliveryFeeByAreaId(areaId: area.Id);
                        if (fullPayment)
                            deliveryFee *= subscriptionDuration.NumberOfMonths;

                        AmountSplitUps.Add(new KeyValuPairModel
                        {
                            Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
                            Value = await _commonHelper.ConvertDecimalToString(value: deliveryFee, isEnglish: isEnglish),
                            DisplayOrder = 4
                        });
                    }
                }
            }

            if (subscriptionAttribute.CouponId.HasValue)
            {
                var coupon = await _couponService.GetById(subscriptionAttribute.CouponId.Value);
                if (coupon != null)
                {
                    discountAmount = coupon.ApplyCouponDiscount2(subscriptionPrice);
                    subscriptionSummaryModel.CouponCode = coupon.CouponCode;

                    AmountSplitUps.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
                        Value = "-" + await _commonHelper.ConvertDecimalToString(value: discountAmount, isEnglish: isEnglish),
                        DisplayOrder = 5
                    });
                }
                else
                {
                    subscriptionAttribute.CouponId = null;
                    await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                }
            }

            cashbackAmount = await _commonHelper.GetCashbackAmount(customerId: customer.Id, amount: subscriptionPrice - discountAmount);
            if (cashbackAmount > 0)
            {
                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
                    Value = "-" + await _commonHelper.ConvertDecimalToString(value: cashbackAmount, isEnglish: isEnglish),
                    DisplayOrder = 6
                });
            }

            if (subscriptionAttribute.UseWalletAmount && walletBalance > 0)
            {
                walletUsedAmount = walletBalance;

                decimal grossTotal = subscriptionPrice - discountAmount + deliveryFee - cashbackAmount;
                if (walletUsedAmount > grossTotal)
                {
                    walletUsedAmount = grossTotal;

                    if (subscriptionAttribute.PaymentMethodId != (int)Utility.Enum.PaymentMethod.Wallet)
                        subscriptionAttribute.OtherPaymentMethodId = subscriptionAttribute.PaymentMethodId;
                    subscriptionAttribute.PaymentMethodId = (int)Utility.Enum.PaymentMethod.Wallet;
                    await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);

                    subscriptionSummaryModel.SkipPaymentMethodSelection = true;
                }
                else
                {
                    if (subscriptionAttribute.PaymentMethodId == (int)Utility.Enum.PaymentMethod.Wallet)
                    {
                        subscriptionAttribute.PaymentMethodId = subscriptionAttribute.OtherPaymentMethodId;
                        subscriptionAttribute.OtherPaymentMethodId = null;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                }

                AmountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
                    Value = "-" + await _commonHelper.ConvertDecimalToString(value: walletUsedAmount, isEnglish: isEnglish),
                    DisplayOrder = 7
                });

                subscriptionSummaryModel.WalletUsedAmount = walletUsedAmount;
                subscriptionSummaryModel.FormattedWalletUsedAmount = await _commonHelper.ConvertDecimalToString(subscriptionSummaryModel.WalletUsedAmount, isEnglish);
            }

            subscriptionSummaryModel.Total = subscriptionPrice + deliveryFee - discountAmount - cashbackAmount - walletUsedAmount;
            subscriptionSummaryModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(subscriptionSummaryModel.Total, isEnglish, includeZero: true);
            subscriptionSummaryModel.Notes = subscriptionAttribute.Notes;
            subscriptionSummaryModel.AmountSplitUps = AmountSplitUps.OrderBy(a => a.DisplayOrder).ToList();

            return subscriptionSummaryModel;
        }
        public async Task<SubscriptionCheckOutModel> PrepareSubscriptionCheckOutModel(bool isEnglish, Customer customer, bool app = true)
        {
            var subscriptionCheckOutModel = new SubscriptionCheckOutModel();

            if (customer == null)
            {
                return null;
            }

            var subscriptionAttribute = await _cartService.GetSubscriptionAttributeByCustomerId(customer.Id);
            if (subscriptionAttribute == null)
            {
                return null;
            }

            if (!subscriptionAttribute.ProductId.HasValue || !subscriptionAttribute.Quantity.HasValue ||
                !subscriptionAttribute.DurationId.HasValue || !subscriptionAttribute.DeliveryDateId.HasValue)
            {
                return null;
            }

            subscriptionCheckOutModel.Customer = PrepareCustomerModel(customer, isEnglish);
            subscriptionCheckOutModel.SubscriptionSummary = await PrepareSubscriptionSummaryModel(isEnglish: isEnglish, customer: customer, app: app);

            var subscriptionDuration = await _productService.GetSubscriptionDurationById(subscriptionAttribute.DurationId.Value);
            if (subscriptionDuration != null)
            {
                subscriptionCheckOutModel.Duration = isEnglish ? subscriptionDuration.NameEn : subscriptionDuration.NameAr;
            }

            var subscriptionDeliveryDate = await _productService.GetSubscriptionDeliveryDateById(subscriptionAttribute.DeliveryDateId.Value);
            if (subscriptionDeliveryDate != null)
            {
                subscriptionCheckOutModel.DeliveryDate = isEnglish ? subscriptionDeliveryDate.NameEn : subscriptionDeliveryDate.NameAr;
            }

            subscriptionCheckOutModel.Quantity = subscriptionAttribute.Quantity.ToString();

            if (subscriptionAttribute.AddressId.HasValue)
            {
                var address = await _customerService.GetAddressById(subscriptionAttribute.AddressId.Value);
                if (address != null)
                {
                    string addressText = await _commonHelper.PrepareAddressText(address: address, isEnglish: isEnglish);
                    subscriptionCheckOutModel.AddressText = addressText;
                }
            }

            if (subscriptionAttribute.PaymentMethodId.HasValue)
            {
                var paymentMethod = await _paymentMethodService.GetPaymentMethodById(subscriptionAttribute.PaymentMethodId.Value);
                if (paymentMethod != null)
                {
                    subscriptionCheckOutModel.PaymentMethod = PreparePaymentMethodModel(paymentMethod: paymentMethod, isEnglish: isEnglish, details: true);
                }
            }

            var product = await _productService.GetById(subscriptionAttribute.ProductId.Value);
            if (product != null)
            {
                var b2bCustomer = customer != null && customer.B2B;
                decimal price = product.GetPriceFrontend(b2bCustomer);
                decimal discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);

                var subscriptionPrice = discountedPrice > 0 ? discountedPrice : price;
                subscriptionCheckOutModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(value: subscriptionPrice, isEnglish: isEnglish, includeZero: true);
                subscriptionCheckOutModel.SubscriptionTitle = isEnglish ? product.NameEn : product.NameAr;
                subscriptionCheckOutModel.Product = await PrepareProductModel(product, isEnglish);

                var productDetails = product.ProductDetails;
                if (productDetails != null)
                {
                    foreach (var productDetail in productDetails)
                    {
                        var childProduct = await _productService.GetById(productDetail.ChildProductId);
                        if (childProduct != null)
                        {
                            subscriptionCheckOutModel.SubscriptionPackTitles.Add(new KeyValuPairModel
                            {
                                Title = (isEnglish ? childProduct.NameEn : childProduct.NameAr) + " x " + productDetail.Quantity,
                                Value = childProduct.Id.ToString()
                            });
                        }
                    }
                }
            }

            return subscriptionCheckOutModel;
        }
        public async Task<SubscriptionModel> PrepareSubscriptionModel(Subscription subscription, bool isEnglish, bool loadDetails = false)
        {
            var subscriptionModel = _mapper.Map<SubscriptionModel>(subscription);

            subscriptionModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(subscription.SubTotal, isEnglish, includeZero: true);
            subscriptionModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(subscription.Total, isEnglish, includeZero: true);
            subscriptionModel.FormattedDate = subscription.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
            subscriptionModel.FormattedTime = subscription.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            var subscriptionStatusDetails = _commonHelper.GetSubscriptionStatusNameAndColorCode(subscription.SubscriptionStatusId, isEnglish);
            subscriptionModel.SubscriptionStatusName = subscriptionStatusDetails.Item1;
            subscriptionModel.SubscriptionStatusColor = subscriptionStatusDetails.Item2;

            var subscriptionOrder = subscription.SubscriptionOrders.Where(a => a.FirstOrder).FirstOrDefault();
            if (subscriptionOrder != null)
            {
                if (subscriptionOrder.PaymentStatusId.HasValue)
                {
                    subscriptionModel.PaymentStatusId = (int)subscriptionOrder.PaymentStatusId;
                    subscriptionModel.PaymentResult = _commonHelper.GetPaymentResultTitle(subscriptionOrder.PaymentStatusId.Value, isEnglish: isEnglish);
                }

                if (subscriptionOrder.PaymentMethodId.HasValue)
                {
                    var paymentMethod = await _paymentMethodService.GetPaymentMethodById(subscriptionOrder.PaymentMethodId.Value);
                    if (paymentMethod != null)
                    {
                        subscriptionModel.PaymentMethod = PreparePaymentMethodModel(isEnglish: isEnglish, paymentMethod: paymentMethod, details: true);

                        if (subscriptionOrder.PaymentMethodId != (int)Utility.Enum.PaymentMethod.Wallet && subscription.WalletUsedAmount > 0)
                        {
                            subscriptionModel.PaymentMethod.Name = subscriptionModel.PaymentMethod.Name + " " + (isEnglish ? Messages.And : MessagesAr.And) + " " +
                                (isEnglish ? Messages.Wallet : MessagesAr.Wallet);
                        }
                    }
                }

                subscriptionModel.PaymentResult = subscriptionOrder.PaymentResult;
                subscriptionModel.PaymentId = subscriptionOrder.PaymentId;
                subscriptionModel.PaymentRefId = subscriptionOrder.PaymentRefId;
                subscriptionModel.PaymentTrackId = subscriptionOrder.PaymentTrackId;
            }

            subscriptionModel.SubscriptionTitle = isEnglish ? subscription.Product.NameEn : subscription.Product.NameAr;

            if (loadDetails)
            {
                if (subscription.Address != null)
                    subscriptionModel.Address = await PrepareAddressModel(isEnglish: isEnglish, address: subscription.Address);

                if (subscription.Customer != null)
                {
                    subscriptionModel.Customer = PrepareCustomerModel(customer: subscription.Customer, isEnglish: isEnglish);
                }

                //payment summary
                List<KeyValuPairModel> patmentSummary = new();
                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.SubscriptionNumber : MessagesAr.SubscriptionNumber,
                    Value = subscription.SubscriptionNumber,
                    DisplayOrder = 0
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderDate : MessagesAr.OrderDate,
                    Value = subscriptionModel.FormattedDate,
                    DisplayOrder = 1
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderTime : MessagesAr.OrderTime,
                    Value = subscriptionModel.FormattedTime,
                    DisplayOrder = 2
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.OrderAmount : MessagesAr.OrderAmount,
                    Value = subscriptionModel.FormattedTotal,
                    DisplayOrder = 3
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentMethod : MessagesAr.PaymentMethod,
                    Value = subscriptionModel.PaymentMethod.Name,
                    DisplayOrder = 4
                });

                patmentSummary.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.PaymentResult : MessagesAr.PaymentResult,
                    Value = _commonHelper.GetPaymentResultTitle((PaymentStatus)subscriptionModel.PaymentStatusId, isEnglish),
                    DisplayOrder = 5
                });

                if (!string.IsNullOrEmpty(subscriptionModel.PaymentId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentId : MessagesAr.PaymentId,
                        Value = subscriptionModel.PaymentId,
                        DisplayOrder = 6
                    });
                }

                if (!string.IsNullOrEmpty(subscriptionModel.PaymentRefId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.PaymentReference : MessagesAr.PaymentReference,
                        Value = subscriptionModel.PaymentRefId,
                        DisplayOrder = 7
                    });
                }

                if (!string.IsNullOrEmpty(subscriptionModel.PaymentTrackId))
                {
                    patmentSummary.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.TrackId : MessagesAr.TrackId,
                        Value = subscriptionModel.PaymentTrackId,
                        DisplayOrder = 8
                    });
                }

                subscriptionModel.PaymentSummary = patmentSummary.OrderBy(a => a.DisplayOrder).ToList();

                //order summary
                List<KeyValuPairModel> amountSplitUps = new();
                amountSplitUps.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.SubscriptionNumber : MessagesAr.SubscriptionNumber,
                    Value = subscription.SubscriptionNumber,
                    DisplayOrder = 0
                });

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

                //subscription details
                List<KeyValuPairModel> subscriptionDetails = new();
                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.SubscriptionNumber : MessagesAr.SubscriptionNumber,
                    Value = subscription.SubscriptionNumber,
                    DisplayOrder = 0
                });

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

                //subscriptionDetails.Add(new KeyValuPairModel
                //{
                //    Title = isEnglish ? Messages.SubscriptionPrice : MessagesAr.SubscriptionPrice,
                //    Value = subscriptionModel.FormattedSubTotal,
                //    DisplayOrder = 3
                //});

                int receivedCount = subscription.SubscriptionOrders.Where(a => a.Delivered).Count();
                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Received : MessagesAr.Received,
                    Value = receivedCount.ToString(),
                    DisplayOrder = 4
                });

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.Remaining : MessagesAr.Remaining,
                    Value = (subscription.NumberOfMonths - receivedCount).ToString(),
                    DisplayOrder = 5
                });

                string NextDelivery = string.Empty;
                if (subscriptionDeliveryDate != null)
                {
                    DateTime nextFromDate = new DateTime(subscription.NextExpectedDelivery.Year, subscription.NextExpectedDelivery.Month, subscriptionDeliveryDate.FromDay);
                    var totalDays = DateTime.DaysInMonth(subscription.NextExpectedDelivery.Year, subscription.NextExpectedDelivery.Month);
                    int toDay = subscriptionDeliveryDate.ToDay;
                    if (toDay > 25)
                        toDay = totalDays;
                    DateTime nextToDate = new DateTime(subscription.NextExpectedDelivery.Year, subscription.NextExpectedDelivery.Month, toDay);
                    NextDelivery = nextFromDate.ToString("dd-MMM", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW")) + " " +
                        (isEnglish ? Messages.To : MessagesAr.To) + " " + nextToDate.ToString("dd-MMM", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

                    subscriptionDetails.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.NextDelivery : MessagesAr.NextDelivery,
                        Value = NextDelivery,
                        DisplayOrder = 6
                    });
                }

                subscriptionDetails.Add(new KeyValuPairModel
                {
                    Title = isEnglish ? Messages.SubTotal : MessagesAr.SubTotal,
                    Value = subscriptionModel.FormattedSubTotal,
                    DisplayOrder = 7
                });

                if (subscription.DeliveryFee > 0)
                {
                    subscriptionDetails.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.DeliveryAmount : MessagesAr.DeliveryAmount,
                        Value = subscriptionModel.FormattedDeliveryFee,
                        DisplayOrder = 8
                    });
                }

                if (subscription.CouponDiscountAmount > 0)
                {
                    subscriptionDetails.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.DiscountAmount : MessagesAr.DiscountAmount,
                        Value = subscriptionModel.FormattedCouponDiscountAmount,
                        DisplayOrder = 9
                    });
                }

                if (subscription.CashbackAmount > 0)
                {
                    subscriptionDetails.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.Cashback : MessagesAr.Cashback,
                        Value = subscriptionModel.FormattedCashbackAmount,
                        DisplayOrder = 10
                    });
                }

                if (subscription.WalletUsedAmount > 0)
                {
                    subscriptionDetails.Add(new KeyValuPairModel
                    {
                        Title = isEnglish ? Messages.WalletAmount : MessagesAr.WalletAmount,
                        Value = subscriptionModel.FormattedWalletUsedAmount,
                        DisplayOrder = 11
                    });
                }

                subscriptionModel.SubscriptionDetails = subscriptionDetails.OrderBy(a => a.DisplayOrder).ToList();

                var product = await _productService.GetById(subscription.ProductId);
                subscriptionModel.Product = await PrepareProductModel(product, isEnglish);
            }

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

            return subscriptionModel;
        }
        public async Task<SubscriptionAdminModel> PrepareSubscriptionModel1(Subscription subscription, bool isEnglish, bool loadDetails = false)
        {
            var subscriptionModel = _mapper.Map<SubscriptionAdminModel>(subscription);

            subscriptionModel.FormattedSubTotal = await _commonHelper.ConvertDecimalToString(subscription.SubTotal, isEnglish, includeZero: true);
            subscriptionModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(subscription.Total, isEnglish, includeZero: true);
            subscriptionModel.FormattedDate = subscription.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
            subscriptionModel.FormattedTime = subscription.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            var subscriptionStatusDetails = _commonHelper.GetSubscriptionStatusNameAndColorCode(subscription.SubscriptionStatusId, isEnglish);
            subscriptionModel.SubscriptionStatusName = subscriptionStatusDetails.Item1;
            subscriptionModel.SubscriptionStatusColor = subscriptionStatusDetails.Item2;

            if (subscription.FullPayment)
            {
                subscriptionModel.FormattedMonthlyAmount = await _commonHelper.ConvertDecimalToString(0, isEnglish, includeZero: true);
            }
            else
            {
                decimal monthlyPaymentAmount = subscription.SubTotal - subscription.CouponDiscountAmount + subscription.DeliveryFee;
                subscriptionModel.FormattedMonthlyAmount = await _commonHelper.ConvertDecimalToString(monthlyPaymentAmount, isEnglish, includeZero: true);
            }

            var subscriptionDuration = await _productService.GetSubscriptionDurationById(subscription.DurationId);
            subscriptionModel.Duration = isEnglish ? subscriptionDuration?.NameEn : subscriptionDuration?.NameAr;

            var subscriptionDeliveryDate = await _productService.GetSubscriptionDeliveryDateById(subscription.DeliveryDateId);
            subscriptionModel.DeliveryDate = isEnglish ? subscriptionDeliveryDate?.NameEn : subscriptionDeliveryDate?.NameAr;
            if (subscription.Product != null)
            {
                subscriptionModel.SubscriptionTitle = isEnglish ? subscription.Product.NameEn : subscription.Product.NameAr;
            }
            subscriptionModel.SubscriptionNumber = subscription.SubscriptionNumber;
            subscriptionModel.CouponCode = subscription.Coupon != null ? subscription.Coupon.CouponCode : "";

            subscriptionModel.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(value: subscription.DeliveryFee, isEnglish: isEnglish);
            subscriptionModel.FormattedCouponDiscountAmount = await _commonHelper.ConvertDecimalToString(value: subscription.CouponDiscountAmount, isEnglish: isEnglish);
            subscriptionModel.FormattedCashbackAmount = await _commonHelper.ConvertDecimalToString(value: subscription.CashbackAmount, isEnglish: isEnglish);
            subscriptionModel.FormattedWalletUsedAmount = await _commonHelper.ConvertDecimalToString(value: subscription.WalletUsedAmount, isEnglish: isEnglish);
            subscriptionModel.FormattedTotal = await _commonHelper.ConvertDecimalToString(subscription.Total, isEnglish, includeZero: true);
            subscriptionModel.FormattedDate = subscription.CreatedOn.ToString("dd MMM yyyy", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));
            subscriptionModel.FormattedTime = subscription.CreatedOn.ToString("hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

            var subscriptionOrders = subscription.SubscriptionOrders.OrderBy(a => a.Id).ToList();
            foreach (var subscriptionOrder in subscriptionOrders)
            {
                SubscriptionOrderAdminModel subscriptionOrderAdminModel = new();
                subscriptionOrderAdminModel.FormattedDate = subscription.CreatedOn.ToString("dd MMM yyyy hh:mm tt", isEnglish ? new CultureInfo("en-US") : new CultureInfo("ar-KW"));

                if (subscriptionOrder.PaymentMethodId.HasValue)
                {
                    var paymentMethod = await _paymentMethodService.GetPaymentMethodById(subscriptionOrder.PaymentMethodId.Value);
                    if (paymentMethod != null)
                    {
                        subscriptionOrderAdminModel.PaymentMethodName = isEnglish ? paymentMethod.NameEn : paymentMethod.NameAr;
                        subscriptionOrderAdminModel.PaymentMethodColor = paymentMethod.ColorCode;
                    }
                }

                if (subscriptionOrder.PaymentStatusId.HasValue)
                {
                    var paymentMethod = _commonHelper.GetPaymentResultNameAndColorCode(subscriptionOrder.PaymentStatusId.Value, isEnglish);
                    subscriptionOrderAdminModel.PaymentStatusName = paymentMethod.Item1;
                    subscriptionOrderAdminModel.PaymentStatusColor = paymentMethod.Item2;
                }

                subscriptionOrderAdminModel.PaymentId = subscriptionOrder.PaymentId;
                subscriptionOrderAdminModel.PaymentRefId = subscriptionOrder.PaymentRefId;

                subscriptionModel.SubscriptionOrders.Add(subscriptionOrderAdminModel);
            }

            if (subscription.Address != null)
                subscriptionModel.Address = await PrepareAddressModel(isEnglish: isEnglish, address: subscription.Address);

            if (subscription.Customer != null)
            {
                subscriptionModel.Customer = PrepareCustomerModel(customer: subscription.Customer, isEnglish: isEnglish);
            }

            if (subscription.Product != null)
            {
                subscriptionModel.Product = await PrepareProductModel(product: subscription.Product, isEnglish: isEnglish);
            }

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

            return subscriptionModel;
        }
        #endregion
    }
}
