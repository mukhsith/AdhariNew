using API.Areas.Frontend.Helpers;
using API.Helpers;
using AutoMapper;
using Data.Content;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.Locations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Frontend.Content.Interface;
using Services.Frontend.CouponPromotion.Interface;
using Services.Frontend.CustomerManagement;
using Services.Frontend.Locations;
using Services.Frontend.Locations.Interface;
using Services.Frontend.ProductManagement.Interface;
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
            IPushNotification pushNotification)
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
        public async Task<APIResponseModel<HomepageModel>> PrepareHomepageContent(bool isEnglish, int customerId = 0, string customerGuidValue = "")
        {
            var response = new APIResponseModel<HomepageModel>();
            try
            {
                Customer customer = null;
                if (customerId > 0)
                {
                    customer = await _customerService.GetCustomerById(customerId);
                    if (customer == null || customer.Deleted)
                    {
                        response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                        return response;
                    }

                    if (!customer.Active)
                    {
                        response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                        return response;
                    }
                }

                var baseUrlCategory = _appSettings.APIBaseUrl + _appSettings.ImageCategory;

                HomepageModel homepage = new();
                homepage.Banners = await _bannerService.GetAll(isEnglish);

                var categories = (await _categoryService.GetAllHero(isEnglish, baseUrlCategory)).ToList();
                foreach (var category in categories)
                {
                    var products = (await _productService.GetAll(categoryId: category.Id)).ToList();
                    foreach (var product in products)
                    {
                        var productModel = await _modelHelper.PrepareProductModel(product: product, isEnglish: isEnglish, customerGuidValue: customerGuidValue,
                            customer: customer, loadPrice: true, calculateStock: true, loadCategory: true, loadCartQuantity: true);
                        category.Products.Add(productModel);
                    }
                }
                homepage.Categories = categories;

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
        public async Task<APIResponseModel<List<PaymentMethodModel>>> PreparePaymentMethods(bool isEnglish, PaymentRequestType paymentRequestType, int customerId)
        {
            var response = new APIResponseModel<List<PaymentMethodModel>>();
            try
            {
                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null || customer.Deleted)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                var paymentMethods = await _paymentMethodService.GetAllPaymentMethod(paymentRequestType: paymentRequestType);
                var paymentMethodModels = paymentMethods.Select(paymentMethod =>
                {
                    return _modelHelper.PreparePaymentMethodModel(paymentMethod, isEnglish);
                }).ToList();

                if (paymentRequestType == PaymentRequestType.Order)
                {
                    var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customer.Id)).FirstOrDefault();
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
                    var subscriptionAttribute = (await _cartService.GetSubscriptionAttributeByCustomerId(customerId: customer.Id));
                    if (subscriptionAttribute != null && subscriptionAttribute.PaymentMethodId.HasValue)
                    {
                        foreach (var paymentMethodModel in paymentMethodModels)
                        {
                            if (paymentMethodModel.Id == subscriptionAttribute.PaymentMethodId.Value)
                                paymentMethodModel.Selected = true;
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
                var baseUrlCategory = _appSettings.APIBaseUrl + _appSettings.ImageCategory;
                var categories = (await _categoryService.GetAllHero(isEnglish, baseUrlCategory)).ToList();

                if (id.HasValue)
                {
                    categories = categories.Where(a => a.Id == id.Value).ToList();
                }
                else if (!string.IsNullOrEmpty(seoName))
                {
                    categories = categories.Where(a => a.SeoName.ToLower() == seoName.ToLower()).ToList();
                }

                response.Data = categories;
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
                var baseUrlCategory = _appSettings.APIBaseUrl + _appSettings.ImageCategory;
                var categories = (await _categoryService.GetAllHero(isEnglish, baseUrlCategory)).ToList();

                PageHeaderModel pageHeader = new();
                pageHeader.Categories = categories;

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
        public async Task<APIResponseModel<CreatePaymentModel>> CreateQuickpay(bool isEnglish, CreatePaymentModel createPaymentModel)
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
        public async Task<APIResponseModel<bool>> TestPush(bool isEnglish, string title, string message, string token)
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
    }
}
