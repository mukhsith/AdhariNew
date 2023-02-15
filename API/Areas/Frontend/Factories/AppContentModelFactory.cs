using API.Areas.Frontend.Helpers;
using API.Helpers;
using AutoMapper;
using Data.Content;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.Locations;
using Data.ProductManagement;
using Data.Sales;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Frontend.Content;
using Services.Frontend.CouponPromotion;
using Services.Frontend.CustomerManagement;
using Services.Frontend.Locations;
using Services.Frontend.ProductManagement;
using Services.Frontend.Sales;
using Services.Frontend.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.Frontend.Sales;
using Utility.Models.KNET;
using Utility.Models.MasterCard;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public class AppContentModelFactory : IAppContentModelFactory
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        private readonly ISiteContentService _siteContentService;
        private readonly IContactDetailService _contactDetailService;
        private readonly ICustomerFeedbackService _customerFeedbackService;
        private readonly ICategoryService _categoryService;
        private readonly ISocialMediaLinkService _socialMediaLinkService;
        private readonly IBannerService _bannerService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly ICompanySettingService _companySettingService;
        private readonly IGovernorateService _governorateService;
        private readonly IAreaService _areaService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ICartService _cartService;
        private readonly IWalletPackageService _walletPackageService;
        private readonly IQuickPaymentService _quickPaymentService;
        private readonly IAPIHelper _apiHelper;
        private readonly IPushNotification _pushNotification;
        private readonly IOrderService _orderService;
        private readonly IMasterCardHelper _masterCardHelper;
        private readonly IPaymentHelper _paymentHelper;
        private readonly ISubscriptionService _subscriptionService;
        public AppContentModelFactory(
            IMapper mapper,
            ILoggerFactory logger,
            IOptions<AppSettingsModel> appSettings,
            IModelHelper modelHelper,
            ISiteContentService siteContentService,
            IContactDetailService contactDetailService,
            ICustomerFeedbackService customerFeedbackService,
            ICategoryService categoryService,
            ISocialMediaLinkService socialMediaLinkService,
            IBannerService bannerService,
            IProductService productService,
            ICustomerService customerService,
            ICompanySettingService companySettingService,
            IGovernorateService governorateService,
            IAreaService areaService,
            IPaymentMethodService paymentMethodService,
            ICartService cartService,
            IWalletPackageService walletPackageService,
            IQuickPaymentService quickPaymentService,
            IAPIHelper apiHelper,
            IPushNotification pushNotification,
            IOrderService orderService,
            IMasterCardHelper masterCardHelper,
            IPaymentHelper paymentHelper,
            ISubscriptionService subscriptionService)
        {
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(AppContentModelFactory).Name);
            _appSettings = appSettings.Value;
            _modelHelper = modelHelper;
            _siteContentService = siteContentService;
            _contactDetailService = contactDetailService;
            _customerFeedbackService = customerFeedbackService;
            _categoryService = categoryService;
            _socialMediaLinkService = socialMediaLinkService;
            _bannerService = bannerService;
            _productService = productService;
            _customerService = customerService;
            _companySettingService = companySettingService;
            _governorateService = governorateService;
            _areaService = areaService;
            _paymentMethodService = paymentMethodService;
            _cartService = cartService;
            _walletPackageService = walletPackageService;
            _quickPaymentService = quickPaymentService;
            _apiHelper = apiHelper;
            _pushNotification = pushNotification;
            _orderService = orderService;
            _masterCardHelper = masterCardHelper;
            _paymentHelper = paymentHelper;
            _subscriptionService = subscriptionService;
        }
        public async Task<APIResponseModel<CompanySettingModel>> PrepareCompanySettingModel(bool isEnglish)
        {
            var response = new APIResponseModel<CompanySettingModel>();
            try
            {
                CompanySettingModel companySettingModel = new();
                var companySetting = await _companySettingService.GetDefault();
                if (companySetting != null)
                {
                    companySettingModel.IOSVersion = companySetting.IOSVersion;
                    companySettingModel.AppStoreLink = companySetting.AppStoreLink;
                    companySettingModel.AndroidVersion = companySetting.AndroidVersion;
                    companySettingModel.PlayStoreLink = companySetting.PlayStoreLink;
                }

                var socialMediaLink = await _socialMediaLinkService.GetDefault();
                if (socialMediaLink != null)
                {
                    companySettingModel.InstagramLink = socialMediaLink.InstagramLink;
                    companySettingModel.FacebookLink = socialMediaLink.FacebookLink;
                    companySettingModel.TwitterLink = socialMediaLink.TwitterLink;
                    companySettingModel.YoutubeLink = socialMediaLink.YoutubeLink;
                    companySettingModel.WhatsAppLink = socialMediaLink.WhatsAppLink;
                    companySettingModel.TiktokLink = socialMediaLink.TiktokLink;
                    companySettingModel.SnapchatLink = socialMediaLink.SnapchatLink;
                }

                var contactDetail = await _contactDetailService.GetDefault();
                if (contactDetail != null)
                {
                    companySettingModel.Address = isEnglish ? contactDetail.AddressEn : contactDetail.AddressAr;
                    companySettingModel.EmailAddress = contactDetail.EmailAddress;
                    companySettingModel.MobileNumber = contactDetail.MobileNumber;
                    companySettingModel.WhatsAppNumber = contactDetail.WhatsAppNumber;
                }

                response.Data = companySettingModel;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<HomepageModel>> PrepareHomepageContent(bool isEnglish, int customerId = 0, string customerGuidValue = "",
            DeviceType? deviceType = null)
        {
            var response = new APIResponseModel<HomepageModel>();
            try
            {
                Customer customer = null;
                if (customerId > 0)
                {
                    customer = await _customerService.GetCustomerById(customerId);
                    if (customer == null || customer.Deleted || !customer.Active)
                    {
                        customer = null;
                        customerId = 0;
                    }
                }

                var baseUrlCategory = _appSettings.APIBaseUrl + _appSettings.ImageCategory;

                HomepageModel homepage = new();

                var banners = await _bannerService.GetAll();
                if (deviceType != null && (deviceType == DeviceType.Android || deviceType == DeviceType.IOS))
                {
                    banners = banners.Where(a => !a.ExcludeFromApp).ToList();
                }

                foreach (var banner in banners)
                {
                    var bannerModel = _modelHelper.PrepareBannerModel(banner, isEnglish);
                    bannerModel.DeviceDependent = banner.ExcludeFromApp;
                    homepage.Banners.Add(bannerModel);
                }

                var categories = await _categoryService.GetAll();
                foreach (var category in categories)
                {
                    var categoryModel = _modelHelper.PrepareCategoryModel(category, isEnglish);
                    var products = (await _productService.GetAll(categoryId: category.Id)).ToList();
                    foreach (var product in products)
                    {
                        var productModel = await _modelHelper.PrepareProductModel(product: product, isEnglish: isEnglish, customerGuidValue: customerGuidValue,
                            customer: customer, loadPrice: true, calculateStock: true, loadCategory: true, loadCartQuantity: true);
                        categoryModel.Products.Add(productModel);
                    }

                    homepage.Categories.Add(categoryModel);
                }

                var cartItems = await _cartService.GetAllCartItem(customerGuidValue: customerGuidValue, customerId: customerId);
                if (cartItems.Count > 0)
                {
                    response.CartCount = cartItems.Count;

                    var cartModel = await _modelHelper.PrepareCartModel(cartItems, isEnglish);
                    response.FormattedCartTotal = cartModel.FormattedSubTotal;
                }

                var companySettingResponse = await PrepareCompanySettingModel(isEnglish);
                if (companySettingResponse != null && companySettingResponse.Data != null)
                {
                    homepage.CompanySetting = companySettingResponse.Data;
                }

                response.Data = homepage;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<SiteContentModel>> PrepareSiteContent(bool isEnglish, AppContentType appContentTypeId)
        {
            var response = new APIResponseModel<SiteContentModel>();
            try
            {
                var siteContent = await _siteContentService.GetByType(appContentTypeId);
                if (siteContent != null)
                {
                    var siteContentModel = _modelHelper.PrepareSiteContentModel(siteContent, isEnglish: isEnglish);

                    response.Data = siteContentModel;
                    response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<object>> CreateFeedbackRequest(bool isEnglish, CustomerFeedbackModel customerFeedback)
        {
            var response = new APIResponseModel<object>();
            try
            {
                customerFeedback.Message = Regex.Replace(customerFeedback.Message, "<.*?>", String.Empty);
                var feedbackRequest = _mapper.Map(customerFeedback, new CustomerFeedback());
                var result = await _customerFeedbackService.Add(feedbackRequest);
                if (result != null)
                {
                    response.Message = isEnglish ? Messages.AddContactUsSuccess : MessagesAr.AddContactUsSuccess;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<List<GovernorateModel>>> PrepareGovernorates(bool isEnglish, int? id = null)
        {
            var response = new APIResponseModel<List<GovernorateModel>>();
            try
            {
                List<Governorate> governorates = new();
                if (id.HasValue && id.Value > 0)
                {
                    var governorate = await _governorateService.GetById(id.Value);
                    if (governorate != null && governorate.Active && !governorate.Deleted)
                    {
                        governorates.Add(governorate);
                    }
                }
                else
                {
                    governorates = (await _governorateService.GetAll()).ToList();
                }

                var governorateModels = governorates.Select(governorate =>
                {
                    return _modelHelper.PrepareGovernorateModel(governorate, isEnglish);
                }).ToList();

                response.Data = governorateModels;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<List<AreaModel>>> PrepareAreas(bool isEnglish, int? id = null, int? governorateId = null)
        {
            var response = new APIResponseModel<List<AreaModel>>();
            try
            {
                List<Area> areas = new();

                if (id.HasValue && id.Value > 0)
                {
                    var area = await _areaService.GetById(id.Value);
                    if (area != null && area.Active && !area.Deleted)
                    {
                        areas.Add(area);
                    }
                }
                else
                {
                    areas = (await _areaService.GetAll()).ToList();
                }

                if (governorateId.HasValue)
                {
                    areas = areas.Where(a => a.GovernorateId == governorateId.Value).ToList();
                }

                var areaModels = areas.Select(area =>
                {
                    return _modelHelper.PrepareAreaModel(area, isEnglish);
                }).ToList();

                response.Data = areaModels;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<List<PaymentMethodModel>>> PreparePaymentMethods(bool isEnglish, PaymentRequestType paymentRequestType,
            int customerId, DeviceType? deviceType = null)
        {
            var response = new APIResponseModel<List<PaymentMethodModel>>();
            try
            {
                if (customerId > 0)
                {
                    var customer = await _customerService.GetCustomerById(customerId);
                    if (customer == null || customer.Deleted)
                    {
                        response.MessageCode = 401;
                        response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                        return response;
                    }

                    if (!customer.Active)
                    {
                        response.MessageCode = 401;
                        response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                        return response;
                    }
                }

                var paymentMethods = await _paymentMethodService.GetAllPaymentMethod(paymentRequestType: paymentRequestType);

                if (deviceType == null || deviceType != DeviceType.IOS)
                {
                    paymentMethods = paymentMethods.Where(a => a.Id != (int)Utility.Enum.PaymentMethod.ApplePay).ToList();
                }

                var paymentMethodModels = paymentMethods.Select(paymentMethod =>
                {
                    return _modelHelper.PreparePaymentMethodModel(paymentMethod, isEnglish);
                }).ToList();

                if (customerId > 0)
                {
                    if (paymentRequestType == PaymentRequestType.Order)
                    {
                        var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
                        if (cartAttribute != null && cartAttribute.PaymentMethodId.HasValue)
                        {
                            foreach (var paymentMethodModel in paymentMethodModels)
                            {
                                if (paymentMethodModel.Id == cartAttribute.PaymentMethodId.Value)
                                    paymentMethodModel.Selected = true;
                            }
                        }
                    }
                    else if (paymentRequestType == PaymentRequestType.SubscriptionOrder)
                    {
                        var subscriptionAttribute = await _cartService.GetSubscriptionAttributeByCustomerId(customerId: customerId);
                        if (subscriptionAttribute != null && subscriptionAttribute.PaymentMethodId.HasValue)
                        {
                            foreach (var paymentMethodModel in paymentMethodModels)
                            {
                                if (paymentMethodModel.Id == subscriptionAttribute.PaymentMethodId.Value)
                                    paymentMethodModel.Selected = true;
                            }
                        }
                    }
                }

                response.Data = paymentMethodModels;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<List<CategoryModel>>> PrepareCategories(bool isEnglish, int? id = null, string seoName = "")
        {
            var response = new APIResponseModel<List<CategoryModel>>();
            try
            {
                var categories = new List<Category>();
                if (id.HasValue && id.Value > 0)
                {
                    var category = await _categoryService.GetById(id.Value);
                    if (category != null && !category.Deleted && category.Active)
                    {
                        categories.Add(category);
                    }
                }
                else if (!string.IsNullOrEmpty(seoName))
                {
                    var category = await _categoryService.GetBySeoName(seoName);
                    if (category != null && !category.Deleted && category.Active)
                    {
                        categories.Add(category);
                    }
                }
                else
                {
                    categories = await _categoryService.GetAll();
                }

                List<CategoryModel> categoryModels = new();
                foreach (var category in categories)
                {
                    categoryModels.Add(_modelHelper.PrepareCategoryModel(category, isEnglish));
                }

                response.Data = categoryModels;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<List<WalletPackageModel>>> PrepareWalletPackages(bool isEnglish, int id = 0)
        {
            var response = new APIResponseModel<List<WalletPackageModel>>();
            try
            {
                List<WalletPackage> walletPackages = new();
                if (id > 0)
                {
                    var walletPackage = await _walletPackageService.GetWalletPackageById(id);
                    if (walletPackage != null && walletPackage.Active && !walletPackage.Deleted)
                    {
                        walletPackages.Add(walletPackage);
                    }
                }
                else
                {
                    walletPackages = await _walletPackageService.GetAllWalletPackage();
                }

                List<WalletPackageModel> walletPackageModels = new();
                foreach (var walletPackage in walletPackages)
                {
                    walletPackageModels.Add(await _modelHelper.PrepareWalletPackageModel(walletPackage, isEnglish));
                }

                response.Data = walletPackageModels;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<PageHeaderModel>> PreparePageHeader(bool isEnglish)
        {
            var response = new APIResponseModel<PageHeaderModel>();
            try
            {
                PageHeaderModel pageHeader = new();

                var categories = await _categoryService.GetAll();
                foreach (var category in categories)
                {
                    var categoryModel = _modelHelper.PrepareCategoryModel(category, isEnglish);
                    pageHeader.Categories.Add(categoryModel);
                }

                var companySettings = await PrepareCompanySettingModel(isEnglish: isEnglish);
                pageHeader.CompanySettings = companySettings.Data;

                response.Data = pageHeader;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<QuickPaymentModel>> PrepareQuickPaymentModel(bool isEnglish, string paymentNumber)
        {
            var response = new APIResponseModel<QuickPaymentModel>();
            try
            {
                var quickPayment = await _quickPaymentService.GetQuickPaymentByPaymentNumber(paymentNumber);
                if (quickPayment != null && !quickPayment.Deleted)
                {
                    var quickPayModel = await _modelHelper.PrepareQuickPaymentModel(quickPayment, isEnglish);
                    response.Data = quickPayModel;
                }

                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<CreatePaymentModel>> CreateQuickpay(bool isEnglish, QuickPaymentModel quickPaymentModel)
        {
            var response = new APIResponseModel<CreatePaymentModel>();

            try
            {
                if (quickPaymentModel.Amount <= 0 || quickPaymentModel.PaymentMethodId <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var paymentMethod = await _paymentMethodService.GetPaymentMethodById(quickPaymentModel.PaymentMethodId);
                if (paymentMethod == null)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                    return response;
                }

                if (paymentMethod.Deleted)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                    return response;
                }

                if (!paymentMethod.Active)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotActive : MessagesAr.PaymentMethodNotActive;
                    return response;
                }

                if (!paymentMethod.ForQuickPay)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                    return response;
                }

                QuickPayment quickPayment = new();
                quickPayment.PaymentRequestTypeId = PaymentRequestType.QuickPay;
                quickPayment.CustomerIp = quickPaymentModel.CustomerIp;
                quickPayment.MobileNumber = quickPaymentModel.MobileNumber;
                quickPayment.Name = quickPaymentModel.Name;
                quickPayment.PaymentMethodId = quickPaymentModel.PaymentMethodId;
                quickPayment.PaymentStatusId = PaymentStatus.Canceled;
                quickPayment.CustomQuickPay = true;
                quickPayment.CustomerLanguageId = isEnglish ? 1 : 2;
                quickPayment.Amount = quickPaymentModel.Amount;

                if (quickPayment.Amount <= 0)
                {
                    response.Message = isEnglish ? Messages.OrderAmountValidation : MessagesAr.OrderAmountValidation;
                    return response;
                }

                quickPayment = await _quickPaymentService.CreateQuickPayment(quickPayment);
                if (quickPayment != null)
                {
                    var paymentNumber = string.Empty;
                    QuickPayment quickPaymentByPaymentNumber = null;
                    do
                    {
                        paymentNumber = "10" + Common.GenerateRandomNo();
                        quickPaymentByPaymentNumber = await _quickPaymentService.GetQuickPaymentByPaymentNumber(paymentNumber);
                    }
                    while (quickPaymentByPaymentNumber != null);

                    quickPayment.EntityId = quickPayment.Id;
                    quickPayment.PaymentNumber = paymentNumber;
                    await _quickPaymentService.UpdateQuickPayment(quickPayment);
                }

                CreatePaymentModel createPaymentModel = new();
                if (quickPayment.PaymentMethodId == (int)Utility.Enum.PaymentMethod.KNET)
                {
                    var paymentUrlRequestModel = new PaymentUrlRequestModel
                    {
                        LangId = quickPayment.CustomerLanguageId.ToString(),
                        Amount = quickPayment.Amount.ToString("N3"),
                        TrackId = quickPayment.PaymentNumber.ToString(),
                        EntityId = quickPayment.Id.ToString(),
                        CustomerId = "0",
                        RequestType = PaymentRequestType.QuickPay.ToString()
                    };

                    var paymentUrl = await _apiHelper.PostAsync<string>("Home/GetPaymentUrl", paymentUrlRequestModel, baseUrl: _appSettings.PaymentAPIUrl);
                    if (!string.IsNullOrEmpty(paymentUrl))
                    {
                        createPaymentModel.PaymentUrl = paymentUrl;
                        createPaymentModel.RedirectToPaymentPage = true;
                    }
                }
                else if (quickPayment.PaymentMethodId == (int)Utility.Enum.PaymentMethod.VISAMASTER)
                {
                    var value = Cryptography.Encrypt(PaymentRequestType.QuickPay.ToString() + "-" + quickPayment.Id);
                    createPaymentModel.PaymentUrl = _appSettings.APIBaseUrl + _appSettings.MasterCardInteractionRequestUrl + "?value=" + value;
                    createPaymentModel.RedirectToPaymentPage = true;
                }

                createPaymentModel.PaymentReturnUrl = _appSettings.QuickPayUrl + "QPR/" + quickPayment.PaymentNumber;

                createPaymentModel.EntityId = quickPayment.Id;
                createPaymentModel.EntityNumber = quickPayment.PaymentNumber;
                createPaymentModel.PaymentRequestTypeId = PaymentRequestType.QuickPay;

                response.Data = createPaymentModel;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<CreatePaymentModel>> UpdateQuickpay(bool isEnglish, CreatePaymentModel createPaymentModel)
        {
            var response = new APIResponseModel<CreatePaymentModel>();

            try
            {
                if (string.IsNullOrEmpty(createPaymentModel.QuickPayNumber) || createPaymentModel.PaymentMethodId <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var quickPayment = await _quickPaymentService.GetQuickPaymentByPaymentNumber(createPaymentModel.QuickPayNumber);
                if (quickPayment == null || quickPayment.Deleted)
                {
                    response.Message = isEnglish ? Messages.QuickpayNotExists : MessagesAr.QuickpayNotExists;
                    return response;
                }

                if (quickPayment.Used)
                {
                    response.Message = isEnglish ? Messages.QuickpayIsUsed : MessagesAr.QuickpayIsUsed;
                    return response;
                }

                var paymentMethod = await _paymentMethodService.GetPaymentMethodById(createPaymentModel.PaymentMethodId);
                if (paymentMethod == null)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                    return response;
                }

                if (paymentMethod.Deleted)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                    return response;
                }

                if (!paymentMethod.Active)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotActive : MessagesAr.PaymentMethodNotActive;
                    return response;
                }

                if (!paymentMethod.ForQuickPay)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                    return response;
                }

                quickPayment.CustomerIp = createPaymentModel.CustomerIp;
                quickPayment.PaymentMethodId = createPaymentModel.PaymentMethodId;
                quickPayment.PaymentStatusId = PaymentStatus.Canceled;

                if (quickPayment.Amount <= 0)
                {
                    response.Message = isEnglish ? Messages.OrderAmountValidation : MessagesAr.OrderAmountValidation;
                    return response;
                }

                await _quickPaymentService.UpdateQuickPayment(quickPayment);

                if (quickPayment.PaymentMethodId == (int)Utility.Enum.PaymentMethod.KNET)
                {
                    var paymentUrlRequestModel = new PaymentUrlRequestModel
                    {
                        LangId = createPaymentModel.CustomerLanguageId.ToString(),
                        Amount = quickPayment.Amount.ToString("N3"),
                        TrackId = quickPayment.PaymentNumber.ToString(),
                        EntityId = quickPayment.Id.ToString(),
                        CustomerId = "0",
                        RequestType = PaymentRequestType.QuickPay.ToString()
                    };

                    var paymentUrl = await _apiHelper.PostAsync<string>("Home/GetPaymentUrl", paymentUrlRequestModel, baseUrl: _appSettings.PaymentAPIUrl);
                    if (!string.IsNullOrEmpty(paymentUrl))
                    {
                        createPaymentModel.PaymentUrl = paymentUrl;
                        createPaymentModel.RedirectToPaymentPage = true;
                    }
                }
                else if (quickPayment.PaymentMethodId == (int)Utility.Enum.PaymentMethod.VISAMASTER)
                {
                    var value = Cryptography.Encrypt(PaymentRequestType.QuickPay.ToString() + "-" + quickPayment.Id);
                    createPaymentModel.PaymentUrl = _appSettings.APIBaseUrl + _appSettings.MasterCardInteractionRequestUrl + "?value=" + value;
                    createPaymentModel.RedirectToPaymentPage = true;
                }

                createPaymentModel.PaymentReturnUrl = _appSettings.QuickPayUrl + "QPR/" + quickPayment.PaymentNumber;

                createPaymentModel.EntityId = quickPayment.Id;
                createPaymentModel.EntityNumber = quickPayment.PaymentNumber;
                createPaymentModel.PaymentRequestTypeId = PaymentRequestType.QuickPay;

                response.Data = createPaymentModel;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<AppVersionModel>> IsUpdatedApp(bool isEnglish, int deviceTypeId, decimal version)
        {
            var response = new APIResponseModel<AppVersionModel>();
            try
            {
                var companySetting = await _companySettingService.GetDefault();
                if (companySetting != null)
                {
                    AppVersionModel AppVersionModel = new();
                    if (deviceTypeId == 1)
                    {
                        AppVersionModel.Link = companySetting.PlayStoreLink;
                        if (version >= companySetting.AndroidVersion)
                            AppVersionModel.Updated = true;
                        else
                        {
                            AppVersionModel.Updated = false;
                            AppVersionModel.Message = isEnglish ? Messages.AndroidAppNewVersionAvailable : MessagesAr.AndroidAppNewVersionAvailable;
                        }
                    }
                    else if (deviceTypeId == 2)
                    {
                        AppVersionModel.Link = companySetting.AppStoreLink;
                        if (version >= companySetting.IOSVersion)
                            AppVersionModel.Updated = true;
                        else
                        {
                            AppVersionModel.Updated = false;
                            AppVersionModel.Message = isEnglish ? Messages.IOSAppNewVersionAvailable : MessagesAr.IOSAppNewVersionAvailable;
                        }
                    }

                    response.Data = AppVersionModel;
                    response.Success = true;
                    response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public APIResponseModel<bool> TestPush(bool isEnglish, string title, string message, string token)
        {
            var response = new APIResponseModel<bool>();

            try
            {
                var result = _pushNotification.SendNotification(title, message, token);

                response.Data = result;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<CreatePaymentModel>> CreateApplePayRequest(bool isEnglish, int customerId, CreateApplePayRequestModel createApplePayRequestModel)
        {
            var response = new APIResponseModel<CreatePaymentModel>();

            try
            {
                if (customerId <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null || customer.Deleted)
                {
                    response.MessageCode = 401;
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.MessageCode = 401;
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                CreatePaymentModel createPaymentModel = new();
                decimal minimumLimit = Convert.ToDecimal(0.100);
                decimal amount = 0;
                string orderNumber = string.Empty;
                if (createApplePayRequestModel.PaymentRequestTypeId == PaymentRequestType.Order)
                {
                    var order = await _orderService.GetOrderById(createApplePayRequestModel.EntityId);
                    if (order == null)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }

                    amount = order.Total;
                    orderNumber = order.OrderNumber;

                    createPaymentModel.EntityNumber = order.OrderNumber;
                    createPaymentModel.PaymentRequestTypeId = PaymentRequestType.Order;

                }
                else if (createApplePayRequestModel.PaymentRequestTypeId == PaymentRequestType.SubscriptionOrder)
                {
                    var subscriptionOrder = await _subscriptionService.GetSubscriptionOrderById(createApplePayRequestModel.EntityId);
                    if (subscriptionOrder == null)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }

                    amount = subscriptionOrder.Total;
                    orderNumber = subscriptionOrder.OrderNumber;

                    createPaymentModel.EntityNumber = subscriptionOrder.OrderNumber;
                    createPaymentModel.PaymentRequestTypeId = PaymentRequestType.SubscriptionOrder;
                }
                else if (createApplePayRequestModel.PaymentRequestTypeId == PaymentRequestType.WalletPackageOrder)
                {
                    var walletPackageOrder = await _walletPackageService.GetWalletPackageOrderById(createApplePayRequestModel.EntityId);
                    if (walletPackageOrder == null)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }

                    amount = walletPackageOrder.Amount;
                    orderNumber = walletPackageOrder.OrderNumber;

                    createPaymentModel.EntityNumber = walletPackageOrder.OrderNumber;
                    createPaymentModel.PaymentRequestTypeId = PaymentRequestType.WalletPackageOrder;
                }
                else if (createApplePayRequestModel.PaymentRequestTypeId == PaymentRequestType.QuickPay)
                {
                    var quickPayment = await _quickPaymentService.GetQuickPaymentById(createApplePayRequestModel.EntityId);
                    if (quickPayment == null)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }

                    amount = quickPayment.Amount;
                    orderNumber = quickPayment.PaymentNumber;

                    createPaymentModel.EntityNumber = quickPayment.PaymentNumber;
                    createPaymentModel.PaymentRequestTypeId = PaymentRequestType.QuickPay;
                }

                if (amount <= minimumLimit || string.IsNullOrEmpty(orderNumber))
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                createPaymentModel.EntityId = createApplePayRequestModel.EntityId;
                var masterCardTransaction = await _masterCardHelper.CreateApplepayRequest(amount, orderNumber, createApplePayRequestModel.PaymentToken);
                if (masterCardTransaction != null)
                {
                    PaymentResponseModel paymentResponseModel = new();
                    paymentResponseModel.Amount = masterCardTransaction.amount.ToString();

                    if (masterCardTransaction.sourceOfFunds != null && masterCardTransaction.sourceOfFunds.provided != null &&
                        masterCardTransaction.sourceOfFunds.provided.card != null)
                    {
                        paymentResponseModel.PaymentId = masterCardTransaction.sourceOfFunds.provided.card.number;
                        paymentResponseModel.CreditCardType = masterCardTransaction.sourceOfFunds.provided.card.scheme.ToLower() == "visa" ? "001" : "002";
                        paymentResponseModel.CreditCardNumber = masterCardTransaction.sourceOfFunds.provided.card.number;
                    }

                    if (masterCardTransaction.transaction != null)
                    {
                        paymentResponseModel.Auth = masterCardTransaction.transaction.authorizationCode;
                        paymentResponseModel.TransId = masterCardTransaction.transaction.receipt;
                    }

                    paymentResponseModel.TrackId = createPaymentModel.EntityNumber;
                    paymentResponseModel.RefId = createPaymentModel.EntityNumber;
                    paymentResponseModel.EntityId = createPaymentModel.EntityId.ToString();
                    paymentResponseModel.RequestType = createPaymentModel.PaymentRequestTypeId.ToString();
                    paymentResponseModel.BankServiceCharge = _appSettings.MasterCardFee;
                    paymentResponseModel.BankServiceChargeInPercentage = true;
                    paymentResponseModel.Result = "not+captured";
                    if (masterCardTransaction.response.gatewayCode.ToLower() == "approved")
                    {
                        paymentResponseModel.Result = "captured";
                    }

                    await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);

                    response.Data = createPaymentModel;
                    response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<bool>> TestApple(string requestUrl)
        {
            var response = new APIResponseModel<bool>();

            try
            {
                var result = await _masterCardHelper.ValidateApplepayMerchant(requestUrl);

                response.Data = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return response;
        }
    }
}
