
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; 
using Utility.API;
using Utility.Enum;
using Utility.ResponseMapper;
using API.Areas.Backend.Factories; 
using Services.Backend.CustomerManagement;
using Services.Backend.SystemUserManagement;
using Utility.Models.Admin; 
//using Utility.Models.Admin.CustomerManagement;
using System.Collections.Generic;
using Utility.Models.Frontend.CustomerManagement;

namespace API.Areas.Backend.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _get;
        private readonly ICustomerModelFactory _customerModelFactory; 
        private readonly ILogger _logger;
        public CustomerController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            ICustomerService get,
            ICustomerModelFactory customerModelFactory,
            ILoggerFactory logger) :
            base(options, systemUserService, PermissionTypes.CreateOfflineOrder)
        {
            _get = get;
            _customerModelFactory = customerModelFactory;
            _logger = logger.CreateLogger(typeof(CustomerController).Name);

        }


        /// <summary>
        /// To get customer data
        /// </summary>
        [HttpGet, Route("api/customer/GetByMobileNumber")]
        //[Authorize]
        public async Task<IActionResult> GetByMobileNumber(string mobilenumber)
        {
            ResponseMapper<AdminCustomerModel> response = new();
              try
                {
                    if (!await Allowed()) { return Ok(accessResponse); }

                    if (!string.IsNullOrEmpty(mobilenumber))
                    {
                        var item = await _get.GetByMobileNumber(mobilenumber);
                        if(item is not null)
                        {
                            item.Wallet= await _customerModelFactory.GetWalletByCustomerId(IsEnglish, item.Id);
                            //item.Addresses =  await _customerModelFactory.GetAddress(isEnglish: base.IsEnglish, customerId: item.Id);
                            //item.Governorates = await _customerModelFactory.get
                        }
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


        /// <summary>
        /// To get customer data
        /// </summary>
        [HttpGet, Route("api/customer/GetById")]
        //[Authorize]
        public async Task<IActionResult> GetById(int customerId)
        {
            ResponseMapper<AdminCustomerModel> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

              
                 
                var customer = await _customerModelFactory.GetByCustomerId(IsEnglish, customerId);
                response.GetById(customer);

               
                return Ok(response);


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);


        }

        [HttpPost, Route("api/customer/Create")]
        //[Authorize]
        public async Task<IActionResult> Create([FromForm] AdminCustomerModel customer)
        {
            ResponseMapper<AdminCustomerModel> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                if (string.IsNullOrEmpty(customer.Name))
                {
                    response.NoRecord(customer);
                    response.Message = "Customer name is missing";
                    return Ok(response);
                }
                if (string.IsNullOrEmpty(customer.MobileNumber))
                {
                    response.NoRecord(customer);
                    response.Message = "Customer Mobile Number is missing";
                    return Ok(response);
                }
                //if (string.IsNullOrEmpty(customer.EmailAddress))
                //{
                //    response.NoRecord(customer);
                //    response.Message = "Customer Email Address is missing";
                //    return Ok(response);
                //}
                var created = await _get.CreateCustomer(customer.Name, customer.MobileNumber, customer.EmailAddress, customer.B2B, this.UserId, customer.LanguageId);
                response.Create(created);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);

        }
        [HttpGet, Route("api/Customer/GetAllForSMSNotification")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<Utility.Models.Admin.CustomerManagement.AdminSmallCustomerModel>> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAllForSMSNotification();
                response.GetAll(items);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/Customer/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        { 
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var customerName = HttpContext.Request.Form["customerName"].FirstOrDefault();
                var customerMobile = HttpContext.Request.Form["customerMobile"].FirstOrDefault();
                var customerEmail = HttpContext.Request.Form["customerEmail"].FirstOrDefault();
                var customerType = HttpContext.Request.Form["customerType"].FirstOrDefault();
                var _customerType = Utility.Helpers.Common.ConvertTextToBoolean(customerType);
                var items = await _get.GetAllForDataTable(base.GetDataTableParameters, customerName,customerMobile,customerEmail, customerType);
               
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
         
        [HttpGet, Route("api/customer/GetWalletBalanceByCustomerId")]
        //[Authorize]
        public async Task<IActionResult> GetWalletBalanceByCustomerId(int id)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); } 

                var wallet = await _customerModelFactory.GetWalletByCustomerId(IsEnglish, id); 
                response.GetById(wallet);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet, Route("api/customer/getAllAddress")]
        //[Authorize]
        public async Task<IActionResult> GetAllAddress(int id)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }


                var addressList = await _customerModelFactory.GetAddress(isEnglish: base.IsEnglish, customerId: id);
                //return Ok(addressList);
                response.GetAll(addressList);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }


        [HttpGet, Route("api/customer/getAllAddressByID")]
        //[Authorize]
        public async Task<IActionResult> GetAllAddress(int id,int customerID)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }


                var addressList = await _customerModelFactory.GetAddressById(isEnglish: base.IsEnglish,id: id, customerId: customerID);
                //return Ok(addressList);
                response.GetAll(addressList);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        /// <summary>
        /// To add address
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        [HttpPost, Route("api/customer/addaddress")]
      //  [Authorize]
        public async   Task<IActionResult> AddAddress([FromBody] AddressModel addressModel)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }


                var address= await _customerModelFactory.AddAddress(isEnglish: IsEnglish, addressModel: addressModel);
                response.GetAll(address);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
            
        }



        /// <summary>
        /// To add address
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        [HttpPost, Route("api/customer/updateaddress")]
        //  [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressModel addressModel)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }


                var address = await _customerModelFactory.UpdateAddress(isEnglish: IsEnglish, addressModel: addressModel);
                response.GetAll(address);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);

        }


        [HttpPost, Route("api/customer/UpdateCustomerType")]
        public async Task<IActionResult> UpdateCustomerType([FromForm] AdminCustomerModel customerModel)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (ModelState.IsValid)
                {
                    if (customerModel.Id >0)
                    {
                        var item = await _get.UpdateCustomerType(customerModel.Id, customerModel.B2B);

                        response.GetById(item);
                    }
                }

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);

        }




        [HttpPost, Route("api/customer/UpdateCustomerInfo")]
        public async Task<IActionResult> UpdateCustomerInfo([FromForm] AdminCustomerModel customerModel)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (ModelState.IsValid)
                {
                    if (customerModel.Id > 0)
                    {
                        var Cust = await _get.GetCustomerById(customerModel.Id);
                        Cust.Name = customerModel.Name;
                        Cust.MobileNumber = customerModel.MobileNumber;
                        Cust.EmailAddress = customerModel.EmailAddress;

                        var item = await _get.UpdateCustomer(Cust);

                        response.GetById(item);
                    }
                }

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);

        }






        [HttpGet, Route("api/customer/wallet-history")]
        public async Task<IActionResult> GetWalletHistory(int customerId)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (customerId > 0)
                    {
                        var item = await _get.GetAllWalletTransaction(customerId, (int)WalletType.Wallet);
                        response.GetAll(item);
                    }
                 

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);

        }
        [HttpPost, Route("api/Customer/GetCashbackHistoryForDataTable")]
        public async Task<IActionResult> GetCashbackHistoryForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var customerId = Utility.Helpers.Common.ConvertTextToInt(HttpContext.Request.Form["customerId"].FirstOrDefault());
                var items = await _get.GetCashbackHistoryForDataTable(base.GetDataTableParameters, customerId);

                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/Customer/GetWalletHistoryForDataTable")]
        public async Task<IActionResult> GetWalletHistoryAllForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var customerId = Utility.Helpers.Common.ConvertTextToInt(HttpContext.Request.Form["customerId"].FirstOrDefault()); 
                var items = await _get.GetWalletHistoryForDataTable(base.GetDataTableParameters,   customerId);

                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }



        [HttpPost, Route("api/Customer/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<dynamic> response = new();
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


    }
}
