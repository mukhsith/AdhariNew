using Data.EntityFramework;
using Data.Sales;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.Sales;

namespace Services.Backend.Sales
{
    public class SubscriptionService : ISubscriptionService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public SubscriptionService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Admin  
        //only status will be change
        public async Task<Subscription> UpdateStatus(int id, SubscriptionStatus subscriptionStatus)
        {
            var item = await _dbcontext.Subscriptions.FindAsync(id);
            if (item is not null)
            {
                if (subscriptionStatus == SubscriptionStatus.Cancelled)
                {
                    item.SubscriptionStatusId = SubscriptionStatus.Cancelled;
                }
                else if (subscriptionStatus == SubscriptionStatus.Expired)
                {
                    item.SubscriptionStatusId = SubscriptionStatus.Expired;
                }
                _dbcontext.Subscriptions.Update(item);
                await _dbcontext.SaveChangesAsync();
            }
            return item;
        }
        public async Task<List<Subscription>> GetAllSubscription(int? customerId = null, SubscriptionStatus? subscriptionStatus = null)
        {
            var data = _dbcontext
                           .Subscriptions
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            if (subscriptionStatus != null)
            {
                data = data.Where(a => a.SubscriptionStatusId == subscriptionStatus);
            }

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<List<SubscriptionOrder>> GetOrdersBySubscriptionId(int id)
        {
            var data = await _dbcontext.SubscriptionOrders
                .Include(a => a.Subscription).ThenInclude(a => a.SubscriptionItemDetails)
                .Include(a => a.Subscription).ThenInclude(a => a.Coupon)
                .Include(a => a.Subscription).ThenInclude(a => a.Customer)
                .Include(a => a.Subscription).ThenInclude(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.PaymentMethod)
                .Include(a => a.DeliveryTimeSlot)
                .Where(a => a.SubscriptionId == id)
                .ToListAsync();

            return data;
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            //var data = await _dbcontext.Subscriptions
            //    .Include(a => a.Customer)
            //    .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
            //    .Include(a => a.Coupon)
            //    .Include(a => a.SubscriptionItemDetails)
            //    .Include(a=>a.Product)
            //    .Where(a => a.Id == id)
            //    .FirstOrDefaultAsync();


            var data = await _dbcontext.Subscriptions
                .Include(a => a.Product)
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.SubscriptionItemDetails)
                .Include(a => a.SubscriptionOrders)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            return data;
        }

        public async Task<SubscriptionOrder> GetSubscriptionOrderById(int id)
        {
            var data = await _dbcontext.SubscriptionOrders
                .Include(a => a.Subscription).ThenInclude(a => a.SubscriptionItemDetails)
                .Include(a => a.Subscription).ThenInclude(a => a.Coupon)
                .Include(a => a.Subscription).ThenInclude(a => a.Customer)
                .Include(a => a.Subscription).ThenInclude(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.PaymentMethod)
                .Include(a => a.DeliveryTimeSlot)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<Subscription> GetSubscriptionBySubscriptionNumber(string subscriptionNumber)
        {
            var data = await _dbcontext.Subscriptions
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.SubscriptionItemDetails)
                .Where(a => a.SubscriptionNumber == subscriptionNumber)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<DailySubscriptionSummaryModel> GetSubscriptionTodaySales()
        {
            DailySubscriptionSummaryModel dailySubscriptionOrderSummary = new();

            var items = await _dbcontext.SubscriptionOrders
                                        .Select(x => new { x.CreatedOn, x.Total, x.PaymentStatusId, x.PaymentMethodId })
                                        .Where(x => x.CreatedOn.Date == DateTime.Now.Date).AsNoTracking().ToListAsync();

            dailySubscriptionOrderSummary.SubscriptionOrdersReceivedToday = items.Count;
            dailySubscriptionOrderSummary.SubscriptionSalesAmountToday = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured || x.PaymentStatusId == PaymentStatus.PendingCash).Sum(x => x.Total);

            return dailySubscriptionOrderSummary;
        }

        public async Task<dynamic> GetSubscriptionSalesOrders(AdminSubscriptionOrderParam param)
        {
            DataTableResult<dynamic> result = new() { Draw = param.DatatableParam.Draw };
            try
            {
                var items = _dbcontext.SubscriptionOrders.Include(x => x.Subscription).ThenInclude(x => x.Customer).Where(x => x.Deleted == false)
                    .Select(x => new
                    {
                        x.SubscriptionId,
                        x.OrderNumber,
                        x.CreatedOn,
                        x.Subscription.Customer.Name,
                        x.Subscription.Customer.MobileNumber,
                        x.Total,
                        x.PaymentMethodId,
                        x.PaymentStatusId,
                        x.Delivered,
                        x.DriverId,
                        x.Subscription.Customer.EmailAddress,
                        x.Subscription.SubscriptionStatusId,
                        x.Subscription.CustomerId,
                    });
                if (param.SelectedTab == 0)//paid
                {
                    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured);
                }
                else if (param.SelectedTab == 1) //unpaid
                {
                    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash);
                }
                else if (param.SelectedTab == 2) //failed
                {
                    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Canceled || x.PaymentStatusId == PaymentStatus.NotCaptured);
                }

                if (param.CustomerId.HasValue) //internal id passed from customers List for specific customer
                {
                    items = items.Where(x => x.CustomerId == param.CustomerId.Value);
                }

                if (param.SubscriptionId.HasValue)
                {
                    items = items.Where(x => x.SubscriptionId == param.SubscriptionId.Value);
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
                    items = items.Where(x => x.Name == param.CustomerName);
                }
                if (!string.IsNullOrEmpty(param.CustomerMobile))
                {
                    items = items.Where(x => x.MobileNumber == param.CustomerMobile);
                }
                if (!string.IsNullOrEmpty(param.CustomerEmail))
                {
                    items = items.Where(x => x.EmailAddress == param.CustomerEmail);
                }

                if (param.PaymentMethodId.HasValue)
                {
                    items = items.Where(x => x.PaymentMethodId == param.PaymentMethodId.Value);
                }
                //if (param.OrderTypeId.HasValue)
                //{
                //    items = items.Where(x => x.OrderTypeId == (OrderType)param.OrderTypeId.Value);
                //}
                if (param.SubscriptionStatusId.HasValue)
                {
                    items = items.Where(x => x.SubscriptionStatusId == (SubscriptionStatus)param.SubscriptionStatusId.Value);
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

        public async Task<DataTableResult<List<AdminSubscriptionModel>>> GetSubscriptions(AdminSubscriptionOrderParam param)
        {
            DataTableResult<List<AdminSubscriptionModel>> result = new() { Draw = param.DatatableParam.Draw };
            try
            {
                //from c in _context.Comments
                //join p in _context.Post on c.PostId equals p.Id into grouping
                //from p in grouping.DefaultIfEmpty()
                //select new { Something = p.Content, PostTitle = p != null ? p.Title : "NO POST" }
                //var items = _dbcontext.Subscriptions.Include(x=>x.SubscriptionOrders)
                //    .Include(x => x.Customer)
                //    .Where(x => x.Deleted == false && x.Id==x.SubscriptionOrders)

                var items = from s in _dbcontext.Subscriptions
                            join o in _dbcontext.SubscriptionOrders on s.Id equals o.SubscriptionId
                            join c in _dbcontext.Customers on s.CustomerId equals c.Id
                            where s.Deleted == false
                            select new AdminSubscriptionModel
                            {
                                SubscriptionId = s.Id,
                                SubscriptionNumber = s.SubscriptionNumber,
                                CreatedOn = s.CreatedOn,
                                SubscriptionStatusId = s.SubscriptionStatusId,
                                CustomerId = s.CustomerId,
                                Total = s.Total,
                                FormattedTotal = "",
                                PaymentMethodId = o.PaymentMethodId,
                                PaymentStatusId = o.PaymentStatusId,
                                Delivered = o.Delivered,
                                Name = c.Name,
                                MobileNumber = c.MobileNumber,
                                EmailAddress = c.EmailAddress,

                            };
                if (param.SelectedTab == 0)//paid
                {
                    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Captured);
                }
                else if (param.SelectedTab == 1) //unpaid
                {
                    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Pending || x.PaymentStatusId == PaymentStatus.PendingCash);
                }
                else if (param.SelectedTab == 2) //failed
                {
                    items = items.Where(x => x.PaymentStatusId == PaymentStatus.Canceled || x.PaymentStatusId == PaymentStatus.NotCaptured);
                }

                if (param.CustomerId.HasValue) //internal id passed from customers List for specific customer
                {
                    items = items.Where(x => x.CustomerId == param.CustomerId.Value);
                }

                if (param.SubscriptionId.HasValue)
                {
                    items = items.Where(x => x.SubscriptionId == param.SubscriptionId.Value);
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
                    items = items.Where(x => x.Name == param.CustomerName);
                }
                if (!string.IsNullOrEmpty(param.CustomerMobile))
                {
                    items = items.Where(x => x.MobileNumber == param.CustomerMobile);
                }
                if (!string.IsNullOrEmpty(param.CustomerEmail))
                {
                    items = items.Where(x => x.EmailAddress == param.CustomerEmail);
                }

                if (param.PaymentMethodId.HasValue)
                {
                    items = items.Where(x => x.PaymentMethodId == param.PaymentMethodId.Value);
                }
                //if (param.OrderTypeId.HasValue)
                //{
                //    items = items.Where(x => x.OrderTypeId == (OrderType)param.OrderTypeId.Value);
                //}
                if (param.SubscriptionStatusId.HasValue)
                {
                    items = items.Where(x => x.SubscriptionStatusId == (SubscriptionStatus)param.SubscriptionStatusId.Value);
                }

                //User Search
                if (!string.IsNullOrEmpty(param.DatatableParam.SearchValue))
                {
                    var SearchValue = param.DatatableParam.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.CreatedOn.ToString().ToLower().Contains(SearchValue) ||
                     obj.Name.ToLower().Contains(SearchValue) ||
                     obj.MobileNumber.ToLower().Contains(SearchValue)
                     );
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

        //public async Task<dynamic> GetSubscriptionDetail(int subscriptionId)
        //{ 
        //    try
        //    {
        //        //from c in _context.Comments
        //        //join p in _context.Post on c.PostId equals p.Id into grouping
        //        //from p in grouping.DefaultIfEmpty()
        //        //select new { Something = p.Content, PostTitle = p != null ? p.Title : "NO POST" }
        //        //var items = _dbcontext.Subscriptions.Include(x=>x.SubscriptionOrders)
        //        //    .Include(x => x.Customer)
        //        //    .Where(x => x.Deleted == false && x.Id==x.SubscriptionOrders)
        //        var items1 = _dbcontext.Subscriptions.Include(x => x.SubscriptionItemDetails).Include(x => x.Customer);
        //        var items = from s in _dbcontext.Subscriptions
        //                    join o in _dbcontext.SubscriptionOrders on s.Id equals o.SubscriptionId
        //                    join c in _dbcontext.Customers on s.CustomerId equals c.Id
        //                    where s.Deleted == false
        //                    select new AdminSubscriptionModel
        //                    {
        //                        SubscriptionId = s.Id,
        //                        CreatedOn = s.CreatedOn,
        //                        SubscriptionStatusId = s.SubscriptionStatusId,
        //                        CustomerId = s.CustomerId,
        //                        Total = s.Total,
        //                        FormattedTotal = "",
        //                        PaymentMethodId = o.PaymentMethodId,
        //                        PaymentStatusId = o.PaymentStatusId,
        //                        Delivered = o.Delivered,
        //                        Name = c.Name,
        //                        MobileNumber = c.MobileNumber,
        //                        EmailAddress = c.EmailAddress,

        //                    };

        //        return result;
        //    }
        //    catch (Exception err)
        //    {
        //        result.Error = err;
        //    }
        //    return result;

        //}


        #endregion

    }
}
