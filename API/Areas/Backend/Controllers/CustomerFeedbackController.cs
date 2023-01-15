using Data.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    public class CustomerFeedbackController : BaseController 
    {
        private readonly ICustomerFeedbackService _get;
        private readonly ILogger _logger;
        public CustomerFeedbackController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            ICustomerFeedbackService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.CustomerFeedback)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(CustomerFeedbackController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpGet, Route("api/CustomerFeedback/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<CustomerFeedback> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (id > 0)
                {
                    var item = await _get.GetById(id); 
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

         
        

        [HttpPost, Route("api/CustomerFeedback/UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromForm] CustomerFeedback item)
        {
            ResponseMapper<CustomerFeedback> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var result = await _get.UpdateStatus(item);
                response.Update(result);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
       

        [HttpPost, Route("api/CustomerFeedback/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAllForDataTable(base.GetDataTableParameters);
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


        //[HttpPost, Route("api/CustomerFeedback/UpdateStatus")]
        //public async Task<IActionResult> UpdateStatus(int Id, int status = 0)
        //{
        //    ResponseMapper<CustomerFeedback> response = new();
        //    try
        //    {
        //        if (!await Allowed()) { return Ok(accessResponse); }

        //        var item = await _get.UpdateStatus(Id, status);
        //        response.Update(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.CacheException(ex);
        //        _logger.LogError(ex.Message);
        //    }

        //    return Ok(response);
        //}
    }
}
