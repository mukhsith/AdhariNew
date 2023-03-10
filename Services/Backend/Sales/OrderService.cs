using Data.EntityFramework;
using Data.Locations;
using Data.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Admin.Delivery;
using Utility.Models.Admin.Sales;
using Utility.ResponseMapper;

namespace Services.Backend.Sales
{
    public class OrderService : IOrderService
    {
        protected readonly ApplicationDbContext _dbcontext;

        public int Confirmed { get; private set; }

        public OrderService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Order 

        /// <summary>
        /// Driver is assigned
        /// </summary>
        /// <param name="param"></param>
        /// <param name="orderNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="orderMode"></param>
        /// <param name="orderType"></param>
        /// <param name="areaId"></param>
        /// <param name="driverId"></param>
        /// <returns></returns>DataTableResult<List<DeliveriesDashboard>>List<DeliveriesDashboard>
        public async Task<DataTableResult<List<DeliveriesDashboard>>> GetDispatchedOrdersDataTable(
            DataTableParam param, string orderNumber = null, DateTime? startDate = null,
            int? orderMode = null, int? orderType = null, int? areaId = null, int? driverId = null)
        {
            DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.Address).ThenInclude(x => x.Area)
                    .Include(x => x.Driver)
                    .Where(x => x.Deleted == false && x.DriverId != null)
                    .Select(x => new DeliveriesDashboard()
                    {

                        Id = x.Id,
                        OrderNumber = x.OrderNumber,
                        DeliveryDate = x.DeliveryDate,
                        PaymentStatusId = x.PaymentStatusId,
                        OrderTypeId = x.OrderTypeId,
                        OrderStatusId = x.OrderStatusId,
                        DriverId = x.DriverId,
                        DeliveryFee = x.DeliveryFee,
                        Total = x.Total,
                        Deleted = x.Deleted,
                        AddressAreadId = x.Address.AreaId,
                        OrderTypeName = "Normal",
                        DriverName = x.Driver != null ? x.Driver.FullName : "",
                        AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                        CustomerId = x.Customer.Id,
                        CreatedOn = x.CreatedOn,

                    });



                if (!string.IsNullOrEmpty(orderNumber))
                {
                    items = items.Where(x => x.OrderNumber == orderNumber);
                }
                if (startDate.HasValue)
                {
                    items = items.Where(x => x.DeliveryDate.Date == startDate.Value.Date);
                }

                if (orderType.HasValue)
                {
                    items = items.Where(x => x.OrderTypeId == (OrderType)orderType.Value);
                }
                if (areaId.HasValue)
                {
                    items = items.Where(x => x.AddressAreadId == areaId.Value);
                }
                if (driverId.HasValue)
                {
                    items = items.Where(x => x.DriverId == driverId.Value);
                }


                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }

                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }
        /// <summary>
        /// Driver is NOT assigned
        /// </summary>
        /// <param name="param"></param>
        /// <param name="orderNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="orderMode"></param>
        /// <param name="orderType"></param>
        /// <param name="areaId"></param>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public async Task<DataTableResult<List<DeliveriesDashboard>>> GetNotDispatchedOrdersDataTable(
            DataTableParam param, string orderNumber = null, DateTime? startDate = null,
            int? orderMode = null, int? orderType = null, int? areaId = null, int? driverId = null)
        {

            DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.Orders.Include(x => x.Customer)
                                             .Include(x => x.Address).ThenInclude(x => x.Area)
                                             .Include(x => x.Driver).Where(x => x.Deleted == false)
                                             .Where(x => x.Deleted == false && x.DriverId == null)
                                                .Select(x => new DeliveriesDashboard()
                                                {

                                                    Id = x.Id,
                                                    OrderNumber = x.OrderNumber,
                                                    DeliveryDate = x.DeliveryDate,
                                                    PaymentStatusId = x.PaymentStatusId,
                                                    OrderTypeId = x.OrderTypeId,
                                                    OrderStatusId = x.OrderStatusId,
                                                    DriverId = x.DriverId,
                                                    DeliveryFee = x.DeliveryFee,
                                                    Total = x.Total,
                                                    Deleted = x.Deleted,
                                                    AddressAreadId = x.Address.AreaId,
                                                    OrderTypeName = "Normal",
                                                    DriverName = x.Driver != null ? x.Driver.FullName : "",
                                                    AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                                                    CustomerId = x.Customer.Id,
                                                    CreatedOn = x.CreatedOn,

                                                });


                if (!string.IsNullOrEmpty(orderNumber))
                {
                    items = items.Where(x => x.OrderNumber == orderNumber);
                }
                if (startDate.HasValue)
                {
                    items = items.Where(x => x.DeliveryDate.Date == startDate.Value.Date);
                }
                if (orderType.HasValue)
                {
                    items = items.Where(x => x.OrderTypeId == (OrderType)orderType.Value);
                }
                if (areaId.HasValue)
                {
                    items = items.Where(x => x.AddressAreadId == areaId.Value);
                }
                if (driverId.HasValue)
                {
                    items = items.Where(x => x.DriverId == driverId.Value);
                }

                //filter for driver not assigned
                // items = items.Where(x => x.DriverId != null);
                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }

                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();


                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        public async Task<DriverDeliverySummaryModel> GetDriverTodayDeliverySummary(int driverId)
        {
            DriverDeliverySummaryModel summary = new();
            var orders = await _dbcontext.Orders
                                         .Select(x => new DeliveriesDashboard() { OrderModeID = OrderMode.Normal, DeliveryDate = x.DeliveryDate, DriverId = x.DriverId, OrderStatusId = x.OrderStatusId })
                                         .Where(x => x.DeliveryDate.Date == DateTime.Now.Date
                                                  && x.DriverId == driverId && x.OrderStatusId != OrderStatus.Cancelled)
                                         .AsNoTracking().ToListAsync();

            var subscriptions = await _dbcontext.SubscriptionOrders
                  .Include(x => x.Subscription)
              .Include(x => x.Driver)
                    .Select(x => new DeliveriesDashboard() { OrderModeID = OrderMode.Subscription, DeliveryDate = x.DeliveryDate, DriverId = x.DriverId, OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending })
                                         .Where(x => x.DeliveryDate.Date == DateTime.Now.Date
                                                  && x.DriverId == driverId).AsNoTracking().ToListAsync();

            var items = orders.Union(subscriptions).AsQueryable();


            summary.Normal = items.Where(x => x.OrderModeID == OrderMode.Normal).Count();
            summary.Subscription = items.Where(x => x.OrderModeID == OrderMode.Subscription).Count();
            summary.Total = summary.Normal + summary.Subscription;

            summary.Pending = items.Where(x => x.OrderStatusId == OrderStatus.Delivered).Count();
            summary.Completed = items.Where(x => x.OrderStatusId != OrderStatus.Delivered).Count();
            return summary;
        }

        public async Task<AdminDeliverySummaryModel> GetTodayDeliverySummary()
        {
            AdminDeliverySummaryModel summary = new();
            var orders = await _dbcontext.Orders
                                         .Select(x => new { x.DeliveryDate, x.DriverId, x.OrderStatusId, x.PaymentStatusId })
                                         .Where(x => x.DeliveryDate.Date == DateTime.Now.Date)
                                         .AsNoTracking().ToListAsync();


            var subscriptionOrders = await _dbcontext.SubscriptionOrders
                                        .Select(x => new { x.CreatedOn, x.Total, x.PaymentStatusId, x.PaymentMethodId, x.DeliveryDate })
                                        .Where(x => x.DeliveryDate.Date == DateTime.Now.Date).AsNoTracking().ToListAsync();

            summary.OrderDeliveries = orders.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash).Count();
            summary.SubscriptionDeliveries = subscriptionOrders.Where(x => x.PaymentStatusId == PaymentStatus.Captured).Count();

            summary.DeliveriesDue = summary.OrderDeliveries + summary.SubscriptionDeliveries;

            return summary;
        }

        //public async Task<DailyOrderSummaryModel> GetTodaySales()
        //{
        //    DailyOrderSummaryModel dailyOrderSummary = new();
        //    // var orders1 = await _dbcontext.Orders.Select(x => new { x.DeliveryDate, x.DriverId })
        //    //                             .Where(x => x.DeliveryDate.Date == DateTime.Now.Date && x.dri).AsNoTracking().ToListAsync();

        //    var orders = await _dbcontext.Orders
        //        .Include(x => x.OrderItems)
        //                                 .Select(x => new { x.OrderItems, x.OrderTypeId, x.CreatedOn, x.Total, x.OrderStatusId, x.PaymentStatusId, x.PaymentMethodId })
        //                                 .Where(x => x.CreatedOn.Date == DateTime.Now.Date).AsNoTracking().ToListAsync();

        //    var sum = orders.Sum(p => p.OrderItems
        //              .Sum(pa => pa.Quantity));
        //    dailyOrderSummary.OrderReceivedToday = orders.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash).Count();
        //    dailyOrderSummary.SalesAmountToday = orders.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash).Sum(x => x.Total);
        //    dailyOrderSummary.ItemsSoldToday = sum;
        //    return dailyOrderSummary;
        //    //return new() { OrderReceived,OrderSalesAmount   }
        //}


        public async Task<DailyOrderSummaryModel> GetTodaySales()
        {

            //  &&       (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash))
            // Quantity = x.OrderItems.Sum(pa => pa.Product.ProductType == ProductType.BaseProduct ? pa.Quantity : pa.OrderItemDetails.Sum(a => a.Quantity) * pa.Quantity),
            DailyOrderSummaryModel dailyOrderSummary = new();
            bool IsEnglish = true;





            var orders = await _dbcontext.Orders
                .Include(x => x.OrderItems).ThenInclude(a => a.Product)
                .Include(x => x.OrderItems).ThenInclude(a => a.OrderItemDetails)
                .Include(x => x.Customer)
                .Include(x => x.Address).ThenInclude(x => x.Area)
                .Include(x => x.Driver)
                .Where(x => x.Deleted == false)
                .Select(x => new DeliveriesDashboard()
                {

                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    DeliveryDate = x.DeliveryDate,
                    PaymentStatusId = x.PaymentStatusId,
                    OrderTypeId = x.OrderTypeId,
                    OrderModeID = OrderMode.Normal,
                    //Quantity = x.OrderItems.Sum(pa => pa.Product.ProductType == ProductType.BaseProduct ? pa.Quantity : pa.OrderItemDetails.Sum(a => a.Quantity) * pa.Quantity),
                    Quantity = x.OrderItems.Sum(pa => pa.Quantity),
                    OrderStatusId = x.OrderStatusId,
                    DriverId = x.DriverId,
                    DeliveryFee = x.DeliveryFee,
                    Total = x.Total,
                    Deleted = x.Deleted,
                    AddressAreadId = x.Address.AreaId,
                    OrderTypeName = IsEnglish ? Messages.Normal : MessagesAr.Normal,
                    OrderModeName = IsEnglish ? Messages.Online : MessagesAr.Online,
                    DriverName = x.Driver != null ? x.Driver.FullName : "",
                    AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                    SubscriptionNumber = "",
                    SubscriptionID = 0,
                    MobileNumber = x.Customer.MobileNumber,
                    CustomerId = x.Customer.Id,
                    CreatedOn = x.CreatedOn,
                    OrderStatus = "",
                    CustomerName = x.Customer.Name,
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? IsEnglish ? Messages.Paid : MessagesAr.Paid : IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                    DeliveryStatus = x.OrderStatusId == OrderStatus.Delivered ? IsEnglish ? Messages.Delivered : MessagesAr.Delivered : IsEnglish ? Messages.Pending : MessagesAr.Pending,
                    Delivered = x.OrderStatusId == OrderStatus.Delivered ? true : false,

                }).ToListAsync();

            // var subscriptions = await _dbcontext.SubscriptionOrders

            //    //var items = _dbcontext.SubscriptionOrders
            //    .Include(x => x.Subscription)
            ////.ThenInclude(y => y.Customer)
            //.Include(x => x.Driver)
            // .Where(x => x.Deleted == false)
            //      .Select(x => new DeliveriesDashboard()
            //      {
            //          Id = x.Id,
            //          OrderNumber = x.OrderNumber,
            //          DeliveryDate = x.DeliveryDate,
            //          PaymentStatusId = x.PaymentStatusId.HasValue ? (PaymentStatus)x.PaymentStatusId : PaymentStatus.Pending,
            //          OrderTypeId = OrderType.Online,
            //          OrderModeID = OrderMode.Subscription,
            //          Quantity = x.Subscription.SubscriptionItemDetails.Sum(pa => pa.Quantity),
            //          OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending,
            //          DriverId = x.DriverId,
            //          DeliveryFee = x.DeliveryFee,
            //          Total = x.Total,
            //          Deleted = x.Deleted,
            //          AddressAreadId = x.Subscription.Address.AreaId,
            //          OrderTypeName = IsEnglish ? Messages.Subscription : MessagesAr.Subscription,
            //          OrderModeName = IsEnglish ? Messages.Online : MessagesAr.Online,
            //          DriverName = x.Driver != null ? x.Driver.FullName : "",
            //          AreaName = x.Subscription.Address.Area != null ? x.Subscription.Address.Area.NameEn : "",
            //          SubscriptionNumber = x.Subscription.SubscriptionNumber,
            //          SubscriptionID = x.Subscription.Id,
            //          MobileNumber = x.Subscription.Customer.MobileNumber,
            //          CustomerId = x.Subscription.CustomerId,
            //          CreatedOn = x.CreatedOn,
            //          OrderStatus = "",
            //          CustomerName = x.Subscription.Customer.Name,

            //          PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? IsEnglish ? Messages.Paid : MessagesAr.Paid : IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
            //          DeliveryStatus = x.Delivered ? IsEnglish ? Messages.Delivered : MessagesAr.Delivered : IsEnglish ? Messages.Pending : MessagesAr.Pending,
            //          Delivered = x.Delivered,
            //      }).ToListAsync();

            // var items = orders.Union(subscriptions).AsQueryable();
            var items = orders.AsQueryable();
            items = items.Where(x => x.CreatedOn.Date == DateTime.Now.Date);

            dailyOrderSummary.KNET = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.KNET && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.VISA_Master = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.VISAMASTER && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.Tabby = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.Tabby && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.COD = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.Cash && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.Wallet = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.Wallet && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.QPay = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.QPay && x.OrderStatusId == OrderStatus.Confirmed).Count();


            var sum = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash).Sum(p => p.Quantity);
            dailyOrderSummary.FailedOrderReceived = items.Where(x => x.PaymentStatusId != PaymentStatus.Captured && x.PaymentStatusId != PaymentStatus.Pending && x.PaymentStatusId != PaymentStatus.PendingCash).Count();
            dailyOrderSummary.OrderReceivedToday = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.OrderCompleted = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.OrderStatusId == OrderStatus.Delivered).Count();
            dailyOrderSummary.SalesAmountToday = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash).Sum(x => x.Total);
            dailyOrderSummary.ItemsSoldToday = (int)sum;
            return dailyOrderSummary;

        }



        public async Task<DailyOrderSummaryModel> GetFilterSalesSummary(DateTime? startDate = null, DateTime? endDate = null)
        {

            //  &&       (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash))
            // Quantity = x.OrderItems.Sum(pa => pa.Product.ProductType == ProductType.BaseProduct ? pa.Quantity : pa.OrderItemDetails.Sum(a => a.Quantity) * pa.Quantity),
            DailyOrderSummaryModel dailyOrderSummary = new();
            bool IsEnglish = true;





            var orders = await _dbcontext.Orders
                .Include(x => x.OrderItems).ThenInclude(a => a.Product)
                .Include(x => x.OrderItems).ThenInclude(a => a.OrderItemDetails)
                .Include(x => x.Customer)
                .Include(x => x.Address).ThenInclude(x => x.Area)
                .Include(x => x.Driver)
                .Where(x => x.Deleted == false)
                .Select(x => new DeliveriesDashboard()
                {

                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    DeliveryDate = x.DeliveryDate,
                    PaymentStatusId = x.PaymentStatusId,
                    OrderTypeId = x.OrderTypeId,
                    OrderModeID = OrderMode.Normal,
                    //Quantity = x.OrderItems.Sum(pa => pa.Product.ProductType == ProductType.BaseProduct ? pa.Quantity : pa.OrderItemDetails.Sum(a => a.Quantity) * pa.Quantity),
                    Quantity = x.OrderItems.Sum(pa => pa.Quantity),
                    OrderStatusId = x.OrderStatusId,
                    DriverId = x.DriverId,
                    DeliveryFee = x.DeliveryFee,
                    Total = x.Total,
                    Deleted = x.Deleted,
                    AddressAreadId = x.Address.AreaId,
                    OrderTypeName = IsEnglish ? Messages.Normal : MessagesAr.Normal,
                    OrderModeName = IsEnglish ? Messages.Online : MessagesAr.Online,
                    DriverName = x.Driver != null ? x.Driver.FullName : "",
                    AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                    SubscriptionNumber = "",
                    SubscriptionID = 0,
                    MobileNumber = x.Customer.MobileNumber,
                    CustomerId = x.Customer.Id,
                    CreatedOn = x.CreatedOn,
                    OrderStatus = "",
                    CustomerName = x.Customer.Name,
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? IsEnglish ? Messages.Paid : MessagesAr.Paid : IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                    DeliveryStatus = x.OrderStatusId == OrderStatus.Delivered ? IsEnglish ? Messages.Delivered : MessagesAr.Delivered : IsEnglish ? Messages.Pending : MessagesAr.Pending,
                    Delivered = x.OrderStatusId == OrderStatus.Delivered ? true : false,

                }).ToListAsync();

            // var subscriptions = await _dbcontext.SubscriptionOrders

            //    //var items = _dbcontext.SubscriptionOrders
            //    .Include(x => x.Subscription)
            ////.ThenInclude(y => y.Customer)
            //.Include(x => x.Driver)
            // .Where(x => x.Deleted == false)
            //      .Select(x => new DeliveriesDashboard()
            //      {
            //          Id = x.Id,
            //          OrderNumber = x.OrderNumber,
            //          DeliveryDate = x.DeliveryDate,
            //          PaymentStatusId = x.PaymentStatusId.HasValue ? (PaymentStatus)x.PaymentStatusId : PaymentStatus.Pending,
            //          OrderTypeId = OrderType.Online,
            //          OrderModeID = OrderMode.Subscription,
            //          Quantity = x.Subscription.SubscriptionItemDetails.Sum(pa => pa.Quantity),
            //          OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending,
            //          DriverId = x.DriverId,
            //          DeliveryFee = x.DeliveryFee,
            //          Total = x.Total,
            //          Deleted = x.Deleted,
            //          AddressAreadId = x.Subscription.Address.AreaId,
            //          OrderTypeName = IsEnglish ? Messages.Subscription : MessagesAr.Subscription,
            //          OrderModeName = IsEnglish ? Messages.Online : MessagesAr.Online,
            //          DriverName = x.Driver != null ? x.Driver.FullName : "",
            //          AreaName = x.Subscription.Address.Area != null ? x.Subscription.Address.Area.NameEn : "",
            //          SubscriptionNumber = x.Subscription.SubscriptionNumber,
            //          SubscriptionID = x.Subscription.Id,
            //          MobileNumber = x.Subscription.Customer.MobileNumber,
            //          CustomerId = x.Subscription.CustomerId,
            //          CreatedOn = x.CreatedOn,
            //          OrderStatus = "",
            //          CustomerName = x.Subscription.Customer.Name,

            //          PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? IsEnglish ? Messages.Paid : MessagesAr.Paid : IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
            //          DeliveryStatus = x.Delivered ? IsEnglish ? Messages.Delivered : MessagesAr.Delivered : IsEnglish ? Messages.Pending : MessagesAr.Pending,
            //          Delivered = x.Delivered,
            //      }).ToListAsync();

            // var items = orders.Union(subscriptions).AsQueryable();
            var items = orders.AsQueryable();
            items = items.Where(x => x.CreatedOn.Date >= startDate.Value && x.CreatedOn.Date <= endDate.Value);


            dailyOrderSummary.KNET = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.KNET && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.VISA_Master = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.VISAMASTER && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.Tabby = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.Tabby && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.COD = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.Cash && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.Wallet = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.Wallet && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.QPay = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.PaymentMethodId == (int)PaymentMethod.QPay && x.OrderStatusId == OrderStatus.Confirmed).Count();

            var sum = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash).Sum(p => p.Quantity);
            dailyOrderSummary.FailedOrderReceived = items.Where(x => x.PaymentStatusId != PaymentStatus.Captured && x.PaymentStatusId != PaymentStatus.Pending && x.PaymentStatusId != PaymentStatus.PendingCash).Count();
            dailyOrderSummary.OrderReceivedToday = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.OrderStatusId == OrderStatus.Confirmed).Count();
            dailyOrderSummary.OrderCompleted = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && x.OrderStatusId == OrderStatus.Delivered).Count();
            dailyOrderSummary.SalesAmountToday = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash).Sum(x => x.Total);
            dailyOrderSummary.ItemsSoldToday = (int)sum;
            return dailyOrderSummary;

        }




        public async Task<DataTableResult<List<AdminOrderedItemModel>>> GetAllForOrderedItemDataTable(AdminOrderedItemParam param)
        {

            //  &&       (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash))
            // Quantity = x.OrderItems.Sum(pa => pa.Product.ProductType == ProductType.BaseProduct ? pa.Quantity : pa.OrderItemDetails.Sum(a => a.Quantity) * pa.Quantity),
            AdminOrderedItemModel dailyOrderSummary = new();
            bool IsEnglish = true;

            DataTableResult<List<AdminOrderedItemModel>> result = new() { Draw = param.DatatableParam.Draw };



            //var orders = await _dbcontext.Orders
            //    .Include(x => x.OrderItems).ThenInclude(a => a.Product)
            //    .Include(x => x.OrderItems).ThenInclude(a => a.OrderItemDetails)
            //    .Include(x => x.Customer)
            //    .Include(x => x.Address).ThenInclude(x => x.Area)
            //    .Include(x => x.Driver)
            //    .Where(x => x.Deleted == false)
            //    .Select(x => new AdminOrderedItemModel()
            //    {
            //        Title = IsEnglish ? "":"",
            //       ProductName="",
            //        ProductNameAr = "",
            //        Quantity = x.OrderItems.Sum(pa => pa.Quantity),
            //        CreatedOn = x.CreatedOn,

            //    }).ToListAsync();


            var orderItems = await _dbcontext.OrderItems.Include(a => a.Order).Include(a => a.Product)
                  .Where(x => x.Order.Deleted == false && x.Order.CreatedOn.Date >= param.startDate.Value.Date && x.Order.CreatedOn.Date <= param.endDate.Value.Date &&
                 (x.Order.PaymentStatusId == PaymentStatus.Captured || x.Order.PaymentStatusId == PaymentStatus.Pending || x.Order.PaymentStatusId == PaymentStatus.PendingCash)
                             ).AsNoTracking().ToListAsync();


            //var orderItems = _dbcontext.OrderItems.Include(a => a.Order).Include(a => a.Product).AsQueryable();

            //orderItems = orderItems.Where(x => x.Order.Deleted);

            //orderItems = orderItems.Where(x => x.Order.CreatedOn.Date >= param.startDate.Value.Date && x.Order.CreatedOn.Date <= param.endDate.Value.Date);

            ////orderItems = orderItems.Where(x => x.Order.PaymentStatusId == PaymentStatus.Captured || x.Order.PaymentStatusId == PaymentStatus.Pending || x.Order.PaymentStatusId == PaymentStatus.PendingCash);
            ////      .Where(x => x.Deleted == false && x.Order.CreatedOn.Date >= param.startDate.Value.Date && x.Order.CreatedOn.Date <= param.endDate.Value.Date &&
            ////     (x.Order.PaymentStatusId == PaymentStatus.Captured || x.Order.PaymentStatusId == PaymentStatus.Pending || x.Order.PaymentStatusId == PaymentStatus.PendingCash)
            ////                 ).AsNoTracking().ToListAsync();



            var adminOrderedItemModels = orderItems
                .GroupBy(a => a.ProductId)
                .Select(x => new AdminOrderedItemModel()
                {
                    ProductId = x.FirstOrDefault().ProductId,
                    Title = IsEnglish ? x.FirstOrDefault().Product.NameEn : x.FirstOrDefault().Product.NameAr,
                    ProductName = x.FirstOrDefault().Product.NameEn,
                    ProductNameAr = x.FirstOrDefault().Product.NameAr,
                    Quantity = x.Sum(a => a.Quantity),
                    CreatedOn = x.FirstOrDefault().Order.CreatedOn
                }).ToList();



            // var subscriptions = await _dbcontext.SubscriptionOrders

            //    //var items = _dbcontext.SubscriptionOrders
            //    .Include(x => x.Subscription)
            ////.ThenInclude(y => y.Customer)
            //.Include(x => x.Driver)
            // .Where(x => x.Deleted == false)
            //      .Select(x => new DeliveriesDashboard()
            //      {
            //          Id = x.Id,
            //          OrderNumber = x.OrderNumber,
            //          DeliveryDate = x.DeliveryDate,
            //          PaymentStatusId = x.PaymentStatusId.HasValue ? (PaymentStatus)x.PaymentStatusId : PaymentStatus.Pending,
            //          OrderTypeId = OrderType.Online,
            //          OrderModeID = OrderMode.Subscription,
            //          Quantity = x.Subscription.SubscriptionItemDetails.Sum(pa => pa.Quantity),
            //          OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending,
            //          DriverId = x.DriverId,
            //          DeliveryFee = x.DeliveryFee,
            //          Total = x.Total,
            //          Deleted = x.Deleted,
            //          AddressAreadId = x.Subscription.Address.AreaId,
            //          OrderTypeName = IsEnglish ? Messages.Subscription : MessagesAr.Subscription,
            //          OrderModeName = IsEnglish ? Messages.Online : MessagesAr.Online,
            //          DriverName = x.Driver != null ? x.Driver.FullName : "",
            //          AreaName = x.Subscription.Address.Area != null ? x.Subscription.Address.Area.NameEn : "",
            //          SubscriptionNumber = x.Subscription.SubscriptionNumber,
            //          SubscriptionID = x.Subscription.Id,
            //          MobileNumber = x.Subscription.Customer.MobileNumber,
            //          CustomerId = x.Subscription.CustomerId,
            //          CreatedOn = x.CreatedOn,
            //          OrderStatus = "",
            //          CustomerName = x.Subscription.Customer.Name,

            //          PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? IsEnglish ? Messages.Paid : MessagesAr.Paid : IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
            //          DeliveryStatus = x.Delivered ? IsEnglish ? Messages.Delivered : MessagesAr.Delivered : IsEnglish ? Messages.Pending : MessagesAr.Pending,
            //          Delivered = x.Delivered,
            //      }).ToListAsync();

            // var items = orders.Union(subscriptions).AsQueryable();
            //var items = orderItems.AsQueryable();
            //items = items.Where(x => x.CreatedOn.Date >= param.startDate.Value && x.CreatedOn.Date <= param.endDate.Value);
            result.RecordsTotal = adminOrderedItemModels.Count();
            result.RecordsFiltered = adminOrderedItemModels.Count();
            result.Data = adminOrderedItemModels.Skip(param.DatatableParam.Skip).Take(param.DatatableParam.PageSize).ToList();

            return result;


        }


        /// <summary>
        /// Gell all deliveries for admin dashboard
        /// </summary>
        /// <param name="param"></param>
        /// <param name="orderNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="orderMode"></param>
        /// <param name="orderType"></param>
        /// <param name="areaId"></param>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public async Task<DataTableResult<List<DeliveriesDashboard>>>
            GetAllForDeliveriesDataTable(DataTableParam param, string orderNumber = null,
                                      DateTime? startDate = null, int? orderMode = null, int? orderType = null,
                                      int? areaId = null, int? driverId = null)
        {
            DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.Address).ThenInclude(x => x.Area)
                    .Include(x => x.Driver)
                    .Where(x => x.Deleted == false)
                    .Select(x => new DeliveriesDashboard()
                    {

                        Id = x.Id,
                        OrderNumber = x.OrderNumber,
                        DeliveryDate = x.DeliveryDate,
                        PaymentStatusId = x.PaymentStatusId,
                        OrderTypeId = x.OrderTypeId,
                        OrderStatusId = x.OrderStatusId,
                        DriverId = x.DriverId,
                        DeliveryFee = x.DeliveryFee,
                        Total = x.Total,
                        Deleted = x.Deleted,
                        AddressAreadId = x.Address.AreaId,
                        OrderTypeName = "Normal",
                        DriverName = x.Driver != null ? x.Driver.FullName : "",
                        AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                        CustomerId = x.Customer.Id,
                        CreatedOn = x.CreatedOn,

                        //FormattedTotal = x.Total.ToString(),
                        //FormattedDeliveryFee = x.DeliveryFee.ToString(),

                    });



                if (!string.IsNullOrEmpty(orderNumber))
                {
                    items = items.Where(x => x.OrderNumber == orderNumber);
                }
                if (startDate.HasValue)
                {
                    items = items.Where(x => x.DeliveryDate.Date == startDate.Value.Date);
                }

                if (orderType.HasValue)
                {
                    items = items.Where(x => x.OrderTypeId == (OrderType)orderType.Value);
                }
                if (areaId.HasValue)
                {
                    items = items.Where(x => x.AddressAreadId == areaId.Value);
                }
                if (driverId.HasValue)
                {
                    items = items.Where(x => x.DriverId == driverId.Value);
                }


                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }

                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        public async Task<DataTableResult<List<DeliveriesDashboard>>> GetAllOrdersForDeliveries(AdminOrderDeliveriesParam param)
        {
            DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.DatatableParam.Draw };
            try
            {
                var orders = await _dbcontext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.Address).ThenInclude(x => x.Area)
                    .Include(x => x.Driver)
                    .Where(x => x.Deleted == false &&
                    (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash))
                    .Select(x => new DeliveriesDashboard()
                    {

                        Id = x.Id,
                        OrderNumber = x.OrderNumber,
                        DeliveryDate = x.DeliveryDate,
                        PaymentStatusId = x.PaymentStatusId,
                        OrderTypeId = x.OrderTypeId,
                        OrderModeID = OrderMode.Normal,
                        OrderStatusId = x.OrderStatusId,
                        DriverId = x.DriverId,
                        DeliveryFee = x.DeliveryFee,
                        Total = x.Total,
                        Deleted = x.Deleted,
                        AddressAreadId = x.Address.AreaId,
                        OrderTypeName = param.IsEnglish ? Messages.Normal : MessagesAr.Normal,
                        OrderModeName = param.IsEnglish ? Messages.Online : MessagesAr.Online,
                        DriverName = x.Driver != null ? x.Driver.FullName : "",
                        AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                        SubscriptionNumber = "",
                        SubscriptionID = 0,
                        MobileNumber = x.Customer.MobileNumber,
                        CustomerId = x.Customer.Id,
                        CreatedOn = x.CreatedOn,
                        OrderStatus = "",
                        CustomerName = x.Customer.Name,
                        Notes = x.Notes,
                        PaymentMethodId = x.PaymentMethodId,
                        PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? param.IsEnglish ? Messages.Paid : MessagesAr.Paid : param.IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                        DeliveryStatus = x.OrderStatusId == OrderStatus.Delivered ? param.IsEnglish ? Messages.Delivered : MessagesAr.Delivered : param.IsEnglish ? Messages.Pending : MessagesAr.Pending,
                        Delivered = x.OrderStatusId == OrderStatus.Delivered ? true : false,
                    }).ToListAsync();

                var subscriptions = await _dbcontext.SubscriptionOrders
                   //var items = _dbcontext.SubscriptionOrders
                   .Include(x => x.Subscription)
               //.ThenInclude(y => y.Customer)
               .Include(x => x.Driver)
                .Where(x => x.Deleted == false)
                     .Select(x => new DeliveriesDashboard()
                     {
                         Id = x.Id,
                         OrderNumber = x.OrderNumber,
                         DeliveryDate = x.DeliveryDate,
                         PaymentStatusId = x.PaymentStatusId.HasValue ? (PaymentStatus)x.PaymentStatusId : PaymentStatus.Pending,
                         OrderTypeId = OrderType.Online,
                         OrderModeID = OrderMode.Subscription,
                         OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending,
                         DriverId = x.DriverId,
                         DeliveryFee = x.DeliveryFee,
                         Total = x.Total,
                         Deleted = x.Deleted,
                         AddressAreadId = x.Subscription.Address.AreaId,
                         OrderTypeName = param.IsEnglish ? Messages.Subscription : MessagesAr.Subscription,
                         OrderModeName = param.IsEnglish ? Messages.Online : MessagesAr.Online,
                         DriverName = x.Driver != null ? x.Driver.FullName : "",
                         AreaName = x.Subscription.Address.Area != null ? x.Subscription.Address.Area.NameEn : "",
                         SubscriptionNumber = x.Subscription.SubscriptionNumber,
                         SubscriptionID = x.Subscription.Id,
                         MobileNumber = x.Subscription.Customer.MobileNumber,
                         CustomerId = x.Subscription.CustomerId,
                         CreatedOn = x.CreatedOn,
                         OrderStatus = "",
                         CustomerName = x.Subscription.Customer.Name,
                         Notes = x.Notes,
                         PaymentMethodId = x.PaymentMethodId,
                         PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? param.IsEnglish ? Messages.Paid : MessagesAr.Paid : param.IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                         DeliveryStatus = x.Delivered ? param.IsEnglish ? Messages.Delivered : MessagesAr.Delivered : param.IsEnglish ? Messages.Pending : MessagesAr.Pending,
                         Delivered = x.Delivered,
                     }).ToListAsync();

                var items = orders.Union(subscriptions).AsQueryable();

                //var items = _dbcontext.SubscriptionOrders
                //    .Include(x => x.Subscription)
                //    //.ThenInclude(y => y.Customer)
                //    .Include(x => x.Driver)
                //     .Where(x => x.Deleted == false &&
                //     (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash))
                //     .Select(x => new DeliveriesDashboard()
                //     {
                //         Id = x.Id,
                //         OrderNumber = x.OrderNumber,
                //         DeliveryDate = x.DeliveryDate,
                //         PaymentStatusId = (PaymentStatus)x.PaymentStatusId,
                //         OrderTypeId = 0,
                //         OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending,
                //         DriverId = x.DriverId,
                //         DeliveryFee = x.DeliveryFee,
                //         Total = x.Total,
                //         Deleted = x.Deleted,
                //         AddressAreadId = 112,
                //         OrderTypeName = "Sub",
                //         DriverName = x.Driver != null ? x.Driver.FullName : "",
                //         AreaName = "",
                //         CustomerId = x.Subscription.CustomerId,
                //         CreatedOn = x.CreatedOn,
                //     });


                if (param.SelectedTab == 0)//Pending
                {
                    items = items.Where(x => x.DriverId == null && x.OrderStatusId != OrderStatus.Delivered);
                }
                else if (param.SelectedTab == 1) //Assigned
                {
                    items = items.Where(x => x.DriverId != null && x.OrderStatusId != OrderStatus.Delivered);
                }
                else if (param.SelectedTab == 3) //Delivered
                {
                    items = items.Where(x => x.DriverId != null && x.OrderStatusId == OrderStatus.Delivered);
                }
                else if (param.SelectedTab == 2) //Returned
                {
                    items = items.Where(x => x.DriverId == null && x.OrderStatusId == OrderStatus.ReturnedByDriver);
                }


                if (!string.IsNullOrEmpty(param.OrderNumber))
                {
                    items = items.Where(x => x.OrderNumber == param.OrderNumber);
                }



                if (param.OrderModeId.HasValue)
                {
                    items = items.Where(x => x.OrderModeID == (OrderMode)param.OrderModeId.Value);
                }
                if (param.OrderTypeId.HasValue)
                {
                    items = items.Where(x => x.OrderTypeId == (OrderType)param.OrderTypeId.Value);
                }
                if (param.AreaId.HasValue)
                {
                    items = items.Where(x => x.AddressAreadId == param.AreaId.Value);
                }
                if (param.DriverId.HasValue)
                {
                    items = items.Where(x => x.DriverId == param.DriverId.Value);

                }
                //filter based on delivery date
                if (param.DeliveryDate.HasValue)
                {
                    items = items.Where(x => x.DeliveryDate.Date == param.DeliveryDate.Value.Date);
                }
                //else
                //{   //default, today deliveries only
                //    items = items.Where(x => x.DeliveryDate.Date >= DateTime.Now.Date);
                //}

                if (param.OrderStatusID == OrderStatus.ReturnedByDriver)
                {
                    items = items.Where(x => x.OrderStatusId == OrderStatus.ReturnedByDriver);
                }
                else
                {   //default, today deliveries only
                    items = items.Where(x => x.OrderStatusId != OrderStatus.ReturnedByDriver);
                }

                //Sorting
                if (!string.IsNullOrEmpty(param.DatatableParam.SortColumn) && !string.IsNullOrEmpty(param.DatatableParam.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.DatatableParam.SortColumn + " " + param.DatatableParam.SortColumnDirection);
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }



                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = items.Skip(param.DatatableParam.Skip).Take(param.DatatableParam.PageSize).AsNoTracking().ToList();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }


        public async Task<DailySubscriptionSummaryModel> GetCustomerOrdersSummary(AdminOrderDeliveriesParam param)
        {
            //DataTableResult<DailySubscriptionSummaryModel> result = new() { Draw = param.DatatableParam.Draw };
            DailySubscriptionSummaryModel dailyOrderSummary = new();

            var orders = await _dbcontext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Address).ThenInclude(x => x.Area)
                .Include(x => x.Driver)
                .Where(x => x.Deleted == false &&
                (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash))
                .Select(x => new DeliveriesDashboard()
                {

                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    DeliveryDate = x.DeliveryDate,
                    PaymentStatusId = x.PaymentStatusId,
                    OrderTypeId = x.OrderTypeId,
                    OrderModeID = OrderMode.Normal,
                    OrderStatusId = x.OrderStatusId,
                    DriverId = x.DriverId,
                    DeliveryFee = x.DeliveryFee,
                    Total = x.Total,
                    Deleted = x.Deleted,
                    AddressAreadId = x.Address.AreaId,
                    OrderTypeName = param.IsEnglish ? Messages.Normal : MessagesAr.Normal,
                    OrderModeName = param.IsEnglish ? Messages.Online : MessagesAr.Online,
                    DriverName = x.Driver != null ? x.Driver.FullName : "",
                    AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                    SubscriptionNumber = "",
                    SubscriptionID = 0,
                    MobileNumber = x.Customer.MobileNumber,
                    CustomerId = x.Customer.Id,
                    CreatedOn = x.CreatedOn,
                    OrderStatus = "",
                    CustomerName = x.Customer.Name,
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? param.IsEnglish ? Messages.Paid : MessagesAr.Paid : param.IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                    DeliveryStatus = x.OrderStatusId == OrderStatus.Delivered ? param.IsEnglish ? Messages.Delivered : MessagesAr.Delivered : param.IsEnglish ? Messages.Pending : MessagesAr.Pending,
                    Delivered = x.OrderStatusId == OrderStatus.Delivered ? true : false,
                }).ToListAsync();

            var subscriptions = await _dbcontext.SubscriptionOrders
               //var items = _dbcontext.SubscriptionOrders
               .Include(x => x.Subscription)
           //.ThenInclude(y => y.Customer)
           .Include(x => x.Driver)
            .Where(x => x.Deleted == false)
                 .Select(x => new DeliveriesDashboard()
                 {
                     Id = x.Id,
                     OrderNumber = x.OrderNumber,
                     DeliveryDate = x.DeliveryDate,
                     PaymentStatusId = x.PaymentStatusId.HasValue ? (PaymentStatus)x.PaymentStatusId : PaymentStatus.Pending,
                     OrderTypeId = OrderType.Online,
                     OrderModeID = OrderMode.Subscription,
                     OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending,
                     DriverId = x.DriverId,
                     DeliveryFee = x.DeliveryFee,
                     Total = x.Total,
                     Deleted = x.Deleted,
                     AddressAreadId = x.Subscription.Address.AreaId,
                     OrderTypeName = param.IsEnglish ? Messages.Subscription : MessagesAr.Subscription,
                     OrderModeName = param.IsEnglish ? Messages.Online : MessagesAr.Online,
                     DriverName = x.Driver != null ? x.Driver.FullName : "",
                     AreaName = x.Subscription.Address.Area != null ? x.Subscription.Address.Area.NameEn : "",
                     SubscriptionNumber = x.Subscription.SubscriptionNumber,
                     SubscriptionID = x.Subscription.Id,
                     MobileNumber = x.Subscription.Customer.MobileNumber,
                     CustomerId = x.Subscription.CustomerId,
                     CreatedOn = x.CreatedOn,
                     OrderStatus = "",
                     CustomerName = x.Subscription.Customer.Name,
                     PaymentMethodId = x.PaymentMethodId,
                     PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? param.IsEnglish ? Messages.Paid : MessagesAr.Paid : param.IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                     DeliveryStatus = x.Delivered ? param.IsEnglish ? Messages.Delivered : MessagesAr.Delivered : param.IsEnglish ? Messages.Pending : MessagesAr.Pending,
                     Delivered = x.Delivered,
                 }).ToListAsync();

            var items = orders.Union(subscriptions).AsQueryable();




            if (param.SelectedTab == 0)//Not Dispatched (Driver Not assigned)
            {
                items = items.Where(x => x.DriverId == null);
            }
            else if (param.SelectedTab == 1) //Dispatched
            {
                items = items.Where(x => x.DriverId != null);
            }

            if (!string.IsNullOrEmpty(param.OrderNumber))
            {
                items = items.Where(x => x.OrderNumber == param.OrderNumber);
            }



            if (param.OrderModeId.HasValue)
            {
                items = items.Where(x => x.OrderModeID == (OrderMode)param.OrderModeId.Value);
            }
            if (param.OrderTypeId.HasValue)
            {
                items = items.Where(x => x.OrderTypeId == (OrderType)param.OrderTypeId.Value);
            }
            if (param.AreaId.HasValue)
            {
                items = items.Where(x => x.AddressAreadId == param.AreaId.Value);
            }
            if (param.DriverId.HasValue)
            {
                items = items.Where(x => x.DriverId == param.DriverId.Value);

            }
            //filter based on delivery date
            if (param.DeliveryDate.HasValue)
            {
                items = items.Where(x => x.DeliveryDate.Date == param.DeliveryDate.Value.Date);
            }
            //else
            //{   //default, today deliveries only
            //    items = items.Where(x => x.DeliveryDate.Date >= DateTime.Now.Date);
            //}

            if (param.OrderStatusID == OrderStatus.ReturnedByDriver)
            {
                items = items.Where(x => x.OrderStatusId == OrderStatus.ReturnedByDriver);
            }
            else
            {   //default, today deliveries only
                items = items.Where(x => x.OrderStatusId != OrderStatus.ReturnedByDriver);
            }

            ////Sorting
            //if (!string.IsNullOrEmpty(param.DatatableParam.SortColumn) && !string.IsNullOrEmpty(param.DatatableParam.SortColumnDirection))
            //{
            //    //using System.Linq.Dynamic.Core;
            //    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
            //    items = items.OrderBy(param.DatatableParam.SortColumn + " " + param.DatatableParam.SortColumnDirection);
            //}
            //else
            //{
            //    items = items.OrderByDescending(x => x.CreatedOn);
            //}

            items = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash);

            dailyOrderSummary.SubscriptionOrdersReceivedToday = items.Count();
            dailyOrderSummary.SubscriptionSalesAmountToday = items.Sum(x => x.Total);

            return dailyOrderSummary;

        }


        public async Task<dynamic> GetAllSalesOrders(AdminSalesOrderParam param)
        {
            DataTableResult<dynamic> result = new() { Draw = param.DatatableParam.Draw };
            try
            {

                var items = _dbcontext.Orders.Include(x => x.Customer).Where(x => x.Deleted == false);
                //if (param.SelectedTab == 0)//paid
                //{
                //    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured);
                //}
                //else if (param.SelectedTab == 1) //unpaid
                //{
                //    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash);
                //}
                if (param.SelectedTab == 0)//paid
                {
                    items = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && (x.OrderStatusId == OrderStatus.Confirmed || x.OrderStatusId == OrderStatus.Delivered));
                }
                if (param.SelectedTab == 1)//paid
                {
                    items = items.Where(x => (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash) && (x.OrderStatusId == OrderStatus.Cancelled || x.OrderStatusId == OrderStatus.Returned || x.OrderStatusId == OrderStatus.CancelledByCustomer));
                }

                else if (param.SelectedTab == 2) //failed
                {
                    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Canceled || x.PaymentStatusId == PaymentStatus.NotCaptured);
                }

                if (param.CustomerId.HasValue) //internal id passed from customers List for specific customer
                {
                    items = items.Where(x => x.CustomerId == param.CustomerId.Value);
                }

                if (!string.IsNullOrEmpty(param.OrderNumber))
                {
                    items = items.Where(x => x.OrderNumber == param.OrderNumber);
                }
                if (param.StartDate.HasValue)
                {
                    items = items.Where(x => x.CreatedOn.Date >= param.StartDate.Value.Date);
                }
                if (param.EndDate.HasValue)
                {
                    items = items.Where(x => x.CreatedOn.Date <= param.EndDate.Value.Date);
                }

                if (!string.IsNullOrEmpty(param.CustomerName))
                {
                    items = items.Where(x => x.Customer.Name == param.CustomerName);
                }
                if (!string.IsNullOrEmpty(param.MobileNumber))
                {
                    items = items.Where(x => x.Customer.MobileNumber == param.MobileNumber);
                }
                if (!string.IsNullOrEmpty(param.CustomerEmail))
                {
                    items = items.Where(x => x.Customer.EmailAddress == param.CustomerEmail);
                }

                if (param.PaymentMethodId.HasValue)
                {
                    items = items.Where(x => x.PaymentMethodId == param.PaymentMethodId.Value);
                }
                if (param.OrderTypeId.HasValue)
                {
                    items = items.Where(x => x.OrderTypeId == (OrderType)param.OrderTypeId.Value);
                }
                if (param.OrderStatusId.HasValue)
                {
                    items = items.Where(x => x.OrderStatusId == (OrderStatus)param.OrderStatusId.Value);
                }

                //Sorting
                if (!string.IsNullOrEmpty(param.DatatableParam.SortColumn) && !string.IsNullOrEmpty(param.DatatableParam.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.DatatableParam.SortColumn + " " + param.DatatableParam.SortColumnDirection);
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }

                result.RecordsTotal = await items.CountAsync();
                result.RecordsFiltered = await items.CountAsync();
                result.Data = await items.Skip(param.DatatableParam.Skip).Take(param.DatatableParam.PageSize).AsNoTracking().ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;

        }

        public async Task<dynamic> GetSalesOrderForDataTable(DataTableParam param,
                                string orderNumber = null, DateTime? startDate = null, DateTime? endDate = null,
                                string customerName = null, string mobileNumber = null, string customerEmail = null, int? paymentMethod = null, int? orderMode = null)
        {
            DataTableResult<dynamic> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.Orders.Include(x => x.Customer).Where(x => x.Deleted == false);

                if (!string.IsNullOrEmpty(orderNumber))
                {
                    items = items.Where(x => x.OrderNumber == orderNumber);
                }
                if (startDate.HasValue)
                {
                    items = items.Where(x => x.CreatedOn.Date >= startDate.Value.Date);
                }
                if (endDate.HasValue)
                {
                    items = items.Where(x => x.CreatedOn.Date <= endDate.Value.Date);
                }

                if (!string.IsNullOrEmpty(customerName))
                {
                    items = items.Where(x => x.Customer.Name == customerName);
                }
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    items = items.Where(x => x.Customer.MobileNumber == mobileNumber);
                }
                if (!string.IsNullOrEmpty(customerEmail))
                {
                    items = items.Where(x => x.Customer.EmailAddress == customerEmail);
                }
                //if (!string.IsNullOrEmpty(paymentMethod))
                //{
                //    items = items.Where(x => x.PaymentMethodId == customerEmail);
                //}

                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }

                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        /// <summary>
        /// without any restriction list of orders
        /// && x.DriverId==driverId && x.DeliveryDate.Date == DateTime.Today.Date
        /// </summary>
        /// <param name="param"></param>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public async Task<DataTableResult<List<DeliveriesDashboard>>> GetTodayDeliveriesDataTable(DataTableParam param, int driverId)
        {
            DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.Draw };
            try
            {

                var orders = await _dbcontext.Orders
                     .Include(x => x.Customer)
                     .Include(x => x.Address).ThenInclude(x => x.Area)
                     .Include(x => x.Driver)
                     .Where(x => x.Deleted == false &&
                     (x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash))
                     .Select(x => new DeliveriesDashboard()
                     {

                         Id = x.Id,
                         OrderNumber = x.OrderNumber,
                         DeliveryDate = x.DeliveryDate,
                         PaymentStatusId = x.PaymentStatusId,
                         OrderTypeId = x.OrderTypeId,
                         OrderModeID = OrderMode.Normal,
                         OrderStatusId = x.OrderStatusId,
                         DriverId = x.DriverId,
                         DeliveryFee = x.DeliveryFee,
                         Total = x.Total,
                         Deleted = x.Deleted,
                         AddressAreadId = x.Address.AreaId,
                         OrderTypeName = param.IsEnglish ? Messages.Normal : MessagesAr.Normal,
                         OrderModeName = param.IsEnglish ? Messages.Online : MessagesAr.Online,
                         DriverName = x.Driver != null ? x.Driver.FullName : "",
                         AreaName = x.Address.Area != null ? x.Address.Area.NameEn : "",
                         SubscriptionNumber = "",
                         SubscriptionID = 0,
                         MobileNumber = x.Customer.MobileNumber,
                         CustomerId = x.Customer.Id,
                         CreatedOn = x.CreatedOn,
                         OrderStatus = "",
                         CustomerName = x.Customer.Name,
                         PaymentMethodId = x.PaymentMethodId,
                         PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? param.IsEnglish ? Messages.Paid : MessagesAr.Paid : param.IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                         DeliveryStatus = x.OrderStatusId == OrderStatus.Delivered ? param.IsEnglish ? Messages.Delivered : MessagesAr.Delivered : param.IsEnglish ? Messages.Pending : MessagesAr.Pending,
                         Delivered = x.OrderStatusId == OrderStatus.Delivered ? true : false,
                     }).ToListAsync();

                var subscriptions = await _dbcontext.SubscriptionOrders
                   //var items = _dbcontext.SubscriptionOrders
                   .Include(x => x.Subscription)
               //.ThenInclude(y => y.Customer)
               .Include(x => x.Driver)
                .Where(x => x.Deleted == false)
                     .Select(x => new DeliveriesDashboard()
                     {
                         Id = x.Id,
                         OrderNumber = x.OrderNumber,
                         DeliveryDate = x.DeliveryDate,
                         PaymentStatusId = x.PaymentStatusId.HasValue ? (PaymentStatus)x.PaymentStatusId : PaymentStatus.Pending,
                         OrderTypeId = OrderType.Online,
                         OrderModeID = OrderMode.Subscription,
                         OrderStatusId = x.Confirmed ? OrderStatus.Confirmed : OrderStatus.Pending,
                         DriverId = x.DriverId,
                         DeliveryFee = x.DeliveryFee,
                         Total = x.Total,
                         Deleted = x.Deleted,
                         AddressAreadId = x.Subscription.Address.AreaId,
                         OrderTypeName = param.IsEnglish ? Messages.Subscription : MessagesAr.Subscription,
                         OrderModeName = param.IsEnglish ? Messages.Online : MessagesAr.Online,
                         DriverName = x.Driver != null ? x.Driver.FullName : "",
                         AreaName = x.Subscription.Address.Area != null ? x.Subscription.Address.Area.NameEn : "",
                         SubscriptionNumber = x.Subscription.SubscriptionNumber,
                         SubscriptionID = x.Subscription.Id,
                         MobileNumber = x.Subscription.Customer.MobileNumber,
                         CustomerId = x.Subscription.CustomerId,
                         CreatedOn = x.CreatedOn,
                         OrderStatus = "",
                         CustomerName = x.Subscription.Customer.Name,
                         PaymentMethodId = x.PaymentMethodId,
                         PaymentStatus = x.PaymentStatusId == PaymentStatus.Captured ? param.IsEnglish ? Messages.Paid : MessagesAr.Paid : param.IsEnglish ? Messages.NotPaid : MessagesAr.NotPaid,
                         DeliveryStatus = x.Delivered ? param.IsEnglish ? Messages.Delivered : MessagesAr.Delivered : param.IsEnglish ? Messages.Pending : MessagesAr.Pending,
                         Delivered = x.Delivered
                     }).ToListAsync();

                var items = orders.Union(subscriptions).AsQueryable();

                if (driverId > 0)
                {
                    items = items.Where(x => x.DriverId == driverId);

                }

                items = items.Where(x => x.DeliveryDate.Date == DateTime.Now.Date);


                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }

                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                // result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();
                result.Data = items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToList();
                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }
        //public async Task<bool> UpdateOrderStatus(int orderId, OrderStatus orderStatusId)
        //{
        //    var item = await _dbcontext.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
        //    if (item is not null)
        //    {
        //        item.OrderStatusId = orderStatusId;
        //        _dbcontext.Orders.Update(item);
        //        return await _dbcontext.SaveChangesAsync() > 1;
        //    }
        //    return false;
        //}



        #endregion




        #region Order
        public async Task<List<Order>> GetAllOrder(int? customerId = null, PaymentStatus? paymentStatus = null, OrderStatus? orderStatus = null,
            string customerGuidValue = "", int? countryId = null)
        {
            var data = _dbcontext
                           .Orders
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            if (paymentStatus != null)
            {
                data = data.Where(a => a.PaymentStatusId == paymentStatus);
            }

            if (orderStatus != null)
            {
                data = data.Where(a => a.OrderStatusId == orderStatus);
            }

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            var data = await _dbcontext.Orders
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.OrderItems).ThenInclude(a => a.OrderItemDetails)
                .Include(a => a.OrderItems).ThenInclude(a => a.Product)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }

        public async Task<bool> AddDriver(int id, int driverId, int OrderTypeID)
        {
            if (OrderTypeID == (int)OrderMode.Normal)
            {
                var data = await _dbcontext.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (data is not null)
                {
                    data.DriverId = driverId;
                    _dbcontext.Update(data);
                    return await _dbcontext.SaveChangesAsync() > 0;
                }
            }
            else
            {
                var data = await _dbcontext.SubscriptionOrders.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (data is not null)
                {
                    data.DriverId = driverId;
                    _dbcontext.Update(data);
                    return await _dbcontext.SaveChangesAsync() > 0;
                }
            }
            return false;
        }
        public async Task<bool> RemoveDriver(int id)
        {
            var data = await _dbcontext.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is not null)
            {
                data.DriverId = null;
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> RescheduleDelivery(int id, DateTime? dateTime = null)
        {
            var data = await _dbcontext.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is not null)
            {
                //data.DeliveryDate = driverId;
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<Order> GetOrderByOrderNumber(string orderNumber)
        {
            var data = await _dbcontext.Orders
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.OrderItems).ThenInclude(a => a.OrderItemDetails)
                .Include(a => a.OrderItems).ThenInclude(a => a.Product)
                .Where(a => a.OrderNumber == orderNumber)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<Order> CreateOrder(Order model)
        {
            await _dbcontext.Orders.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateOrder(Order model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteOrder(Order model)
        {
            model.Deleted = true;
            return await UpdateOrder(model);
        }
        public async Task UpdateOrderStatus(Order order, int orderStatusId)
        {
            order.OrderStatusId = (OrderStatus)orderStatusId;
            _dbcontext.Orders.Update(order);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> UpdateOrderPaymentStatus(Order order, int orderStatusId, int paymentStatusId)
        {
            order.OrderStatusId = (OrderStatus)orderStatusId;
            order.PaymentStatusId = (PaymentStatus)paymentStatusId;

            _dbcontext.Orders.Update(order);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateDriverdetails(Order order, int orderStatusId, int paymentStatusId)
        {
            order.OrderStatusId = (OrderStatus)orderStatusId;
            order.PaymentStatusId = (PaymentStatus)paymentStatusId;

            order.DriverId = null;
            _dbcontext.Orders.Update(order);
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<int> GetOrderCountByCouponAndCustomer(int couponId, int? customerId = null, string customerGuidValue = "")
        {
            var data = _dbcontext.Orders.Where(a => a.CouponId == couponId);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            return await data.CountAsync();
        }
        #endregion

        #region Order items
        public async Task<List<OrderItem>> GetAllOrderItem(int orderId)
        {
            var data = _dbcontext
                           .OrderItems
                           .Include(a => a.Order)
                           .Where(x => x.Deleted == false && x.OrderId == orderId);

            return await data.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
