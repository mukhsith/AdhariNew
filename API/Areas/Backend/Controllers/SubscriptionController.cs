using API.Areas.Backend.Factories;
using API.Helpers;
using Data.ProductManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.ProductManagement.Interface;
using Services.Backend.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.ProductManagement;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    //[Route("api/{Controller}/")]
    public class SubscriptionController : BaseController 
    {
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly ICommonHelper _commonHelper;
        private readonly IProductService _get;
        private readonly IDisplayWebControlService _getDisplayWebControl;
        private readonly ILogger _logger;
        private readonly ISubscriptionModelFactory _subscriptionModelFactory;
        public SubscriptionController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IProductService get, 
            IDisplayWebControlService getDisplayWebControl,
            ISubscriptionModelFactory  subscriptionModelFactory,
            IOrderModelFactory  orderModelFactory,
            ICommonHelper commonHelper,
        ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.Subscription)
        {
            _get = get;
            _getDisplayWebControl = getDisplayWebControl;
            _logger = logger.CreateLogger(typeof(SubscriptionController).Name);
            _subscriptionModelFactory = subscriptionModelFactory;
            _orderModelFactory  =orderModelFactory;
            _commonHelper = commonHelper;
    }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/Subscription/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (id > 0)
                { //, GetBaseImageUrl(AppSettings.ImageProduct
                    var item = await _get.GetById(id, GetBaseImageUrl(AppSettings.ImageProduct), ProductType.SubscriptionProduct);
                    response.GetById(item);
                }


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        
       [HttpGet, Route("api/Subscription/GetAllProductAndCategory")]
        public async Task<IActionResult> GetAllProductAndCategory()
        {
            ResponseMapper<ProductAndCategoryModel> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                var productImagePath = GetBaseImageUrl(AppSettings.ImageProduct);
                var item = await _get.GetAllProductAndCategory(productImagePath);
                response.GetById(item);


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/Subscription/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] Product item)
        {
            ResponseMapper<Product> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (await _get.Exists(item.Id, item.NameEn, item.NameAr, ProductType.SubscriptionProduct))
                {
                    accessResponse.Message = "Subscription Name Already Exists";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 300;
                    return Ok(accessResponse);
                }
                //for english seo fix space with dash
                if (item.NameEn is not null)
                {
                    item.SeoName = item.NameEn.Replace(" ", "-").Replace(" & ","");
                }
                item.ProductType = ProductType.SubscriptionProduct;
                SaveImage(ref item);
                if (item.Id > 0)
                {
                    item.ModifiedBy = UserId;
                    await _get.UpdateBundle(item);
                    response.Update(item);
                }
                else
                {
                    item.CreatedBy = UserId;
                    await _get.Create(item);
                    response.Create(item);
                }


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/Subscription/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<Product> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _get.ToggleActive(Id);
                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/Subscription/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<Product> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _get.UpdateDisplayOrder(Id, num);
                response.DisplayOrder(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/Subscription/UpdateDisplayWebControl")]
        private async Task<bool> UpdateDisplayWebControl(bool active)
        {
            var webcontrol = await _getDisplayWebControl.GetDefaultOrUpdate(AppContentType.Subscription,active);
            if (webcontrol is not null)
            {
                return webcontrol.Active;
            }
            return false;
        }


        [HttpGet, Route("api/Subscription/GetSubscriptionDurations")]
        public async Task<IActionResult> GetSubscriptionDurations()
        {
            ResponseMapper<List<SubscriptionDuration>> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAllSubscriptionDurations();
                response.GetAll(items.ToList());

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpPost, Route("api/Subscription/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        { 
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAllForDataTableByProductType(base.GetDataTableParameters, GetBaseImageUrl(AppSettings.ImageProduct), ProductType.SubscriptionProduct);
               // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        #region Utility
        private void SaveImage(ref Product model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.Image, "/" + AppSettings.ImageProduct, AppSettings.ImageProductResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageName = fileName;
            }

           

        }

        private List<Product> BuildUrls(List<Product> items)
        {
            for (var index = 0; index < items.Count; index++)
            {
                items[index] = BuildUrl(items[index]);
            }
            return items;
        }
        private Product BuildUrl(Product item)
        {

            if (!string.IsNullOrEmpty(item.ImageName))
            {
                item.ImageUrl = GetImageUrl(AppSettings.ImageProduct, item.ImageName);

            }
            
            return item;
        }

        #endregion Utitlity


        /// <summary>
        /// subscriptions list
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/Subscription/GetSubscriptionsForDataTable")]
        public async Task<IActionResult> GetSubscriptionSalesOrderForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                AdminSubscriptionOrderParam param = new();

                param.SelectedTab = Utility.Helpers.Common.ConvertTextToInt(HttpContext.Request.Form["selectedTab"].FirstOrDefault());
                param.DatatableParam = base.GetDataTableParameters;
                param.CustomerId = Utility.Helpers.Common.ConvertTextToIntOptional(HttpContext.Request.Form["customerId"].FirstOrDefault());

                var subscriptionId = HttpContext.Request.Form["subscriptionId"].FirstOrDefault();
                var startDate = HttpContext.Request.Form["startDate"].FirstOrDefault();
                var endDate = HttpContext.Request.Form["endDate"].FirstOrDefault();
                var paymentMethodId = HttpContext.Request.Form["paymentMethodId"].FirstOrDefault();
                //var orderTypeId = HttpContext.Request.Form["orderTypeId"].FirstOrDefault();
                var orderStatusId = HttpContext.Request.Form["orderStatusId"].FirstOrDefault();

                param.SubscriptionId = Utility.Helpers.Common.ConvertTextToIntOptional(subscriptionId);
                param.CustomerName = HttpContext.Request.Form["customerName"].FirstOrDefault();
                param.CustomerMobile = HttpContext.Request.Form["customerMobile"].FirstOrDefault();
                param.CustomerEmail = HttpContext.Request.Form["customerEmail"].FirstOrDefault();
                param.StartDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(startDate);
                param.EndDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(endDate);
                param.PaymentMethodId = Utility.Helpers.Common.ConvertTextToIntOptional(paymentMethodId);
                //param.OrderTypeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderTypeId);
                param.SubscriptionStatusId = Utility.Helpers.Common.ConvertTextToIntOptional(orderStatusId);

                var items = await _subscriptionModelFactory.GetSubscriptionsForDataTable(param);

                // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        /// <summary>
        /// To get subscriptions
        /// </summary>
        /// <returns>Subscriptions</returns>
        //[HttpGet, Route("api/subscription/subscriptionDetails")]
        //public async Task<APIResponseModel<List<SubscriptionModel>>> GetSubscriptions(int id)
        //{
        //    return await _subscriptionModelFactory.GetSubscriptions(IsEnglish,null, id);
        //}
        [HttpGet, Route("api/subscription/UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int id, int status)
        {
            var item = await _subscriptionModelFactory.UpdateStatus(id, status);
            return Ok(item);
        }

        /// <summary>
        /// To get subscription in pdf
        /// </summary>
        /// <returns>Subscription pdf</returns>
        [HttpGet, Route("api/subscription/GetPDF")]
        public async Task<IActionResult> GetPDF(int? customerId, int? subscriptionId )
        {

            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                if (customerId.HasValue && subscriptionId.HasValue)
                {
                     var orderItem = await _subscriptionModelFactory.GetSubscriptions(IsEnglish, customerId:customerId.Value, subscriptionId.Value);
                    if (orderItem != null)
                    {
                        var url = _commonHelper.GetSubscriptionPdfUrl(orderItem.Data[0], base.AppSettings.APIBaseUrl, true);
                        response.GetById(url);
                    }
                }
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogInformation(ex.Message);
            }
            return Ok(response);

        }




        


    }
}
