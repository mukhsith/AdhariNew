using Admin.Models;
using Data.Common;
using Data.CustomerManagement;
using Data.SystemUserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Admin.Delivery;
using Utility.ResponseMapper;


namespace Admin.Controllers
{
    
    public class DeliveryController : BaseController
    {

        private readonly IAPIHelper _apiHelper;
        public DeliveryController(IAPIHelper apiHelper, IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options,   logger.CreateLogger(typeof(DeliveryController).Name))
        {
            _apiHelper = apiHelper;
        }
    
        public async Task<IActionResult> DeliveriesDashboard()
        {
            AdminDeliverySummaryModel responseModel = await _apiHelper.GetAsync<AdminDeliverySummaryModel>("delivery/TodayDeliveries", "");

            if (responseModel != null)
            {
                return View(responseModel);
            }
            else
            {
                return View(new AdminDeliverySummaryModel());
            }
         
        }


   




        public IActionResult DriversDashboard()
        {
            return View();
        }
         
        public IActionResult GovernorateList()
        {
            return View();
        }
        public IActionResult GovernorateAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }
        public IActionResult AreaList()
        {
            return View();
        }
        public IActionResult AreaAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }
        
        public class Day
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; } = true;
        }
        public IActionResult TimeSlotList()
        {

            //List<Day> days = new();
            //days.Add(new Day { Id = 1, Name = "Sunday" });
            //days.Add(new Day { Id = 2, Name = "Monday" });
            //days.Add(new Day { Id = 3, Name = "Tuesday" });
            //days.Add(new Day { Id = 4, Name = "Wednesday" });
            //days.Add(new Day { Id = 5, Name = "Thrusday" });
            //days.Add(new Day { Id = 6, Name = "Friday" }); 
            //days.Add(new Day { Id = 7, Name = "Saturday" });
              
            return View();
        }
        public IActionResult BlockedDateList()
        {
            return View();
        }
        public IActionResult BlockedDateAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
