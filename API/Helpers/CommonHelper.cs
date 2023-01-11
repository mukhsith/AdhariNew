using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.DeliveryManagement;
using Data.EntityFramework;
using Data.ProductManagement;
using Data.Sales;
using Data.Shop;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Frontend.CouponPromotion.Interface;
using Services.Frontend.CustomerManagement;
using Services.Frontend.DeliveryManagement.Interface;
using Services.Frontend.EmailManagement;
using Services.Frontend.Locations;
using Services.Frontend.Locations.Interface;
using Services.Frontend.ProductManagement.Interface;
using Services.Frontend.Sales;
using Services.Frontend.Shop;
using Services.Frontend.SMS;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Frontend.Sales;

namespace API.Helpers
{
    public class CommonHelper : ICommonHelper
    {
        private readonly AppSettingsModel _appSettings;
        private readonly ICountryService _countryService;
        private readonly ICouponService _couponService;
        private readonly IProductService _productService;
        private readonly IAreaService _areaService;
        private readonly IOrderService _orderService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IGovernorateService _governorateService;
        private readonly IDeliveryBlockedDateService _deliveryBlockedDateService;
        private readonly IDeliveryTimeSlotService _deliveryTimeSlotService;
        private readonly ApplicationDbContext _dbcontext;
        private readonly IPromotionService _promotionService;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IEmailHelper _emailHelper;
        public CommonHelper(IOptions<AppSettingsModel> options,
            ICountryService countryService,
            ICouponService couponService,
            IProductService productService,
            IAreaService cityService,
            IOrderService orderService,
            IQueuedEmailService queuedEmailService,
            IGovernorateService governorateService,
            IDeliveryBlockedDateService deliveryBlockedDateService,
            IDeliveryTimeSlotService deliveryTimeSlotService,
            ApplicationDbContext dbcontext,
            IPromotionService promotionService,
            ICustomerService customerService,
            ICartService cartService,
            INotificationTemplateService notificationTemplateService,
            ISubscriptionService subscriptionService,
            IEmailHelper emailHelper)
        {
            _appSettings = options.Value;
            _countryService = countryService;
            _couponService = couponService;
            _productService = productService;
            _areaService = cityService;
            _orderService = orderService;
            _queuedEmailService = queuedEmailService;
            _governorateService = governorateService;
            _deliveryBlockedDateService = deliveryBlockedDateService;
            _deliveryTimeSlotService = deliveryTimeSlotService;
            _dbcontext = dbcontext;
            _promotionService = promotionService;
            _customerService = customerService;
            _cartService = cartService;
            _notificationTemplateService = notificationTemplateService;
            _subscriptionService = subscriptionService;
            _emailHelper = emailHelper;
        }

        #region Utilities
        public async Task<string> ConvertDecimalToString(decimal value, bool isEnglish, int countryId = 0, bool? includeZero = false)
        {
            string formattedValue = string.Empty;

            if (countryId == 0)
                countryId = 1;

            var country = await _countryService.GetById(countryId);
            if (country == null)
            {
                return string.Empty;
            }

            var currencyEn = country.CurrencyCodeEn;
            var currencyAr = country.CurrencyCodeAr;
            var currencyFormat = "{0:" + country.CurrencyFormat + "}";

            if (includeZero == true)
            {
                if (isEnglish)
                    formattedValue = currencyEn + " " + string.Format(currencyFormat, value);
                else
                    formattedValue = string.Format(currencyFormat, value) + " " + currencyAr;
            }
            else
            {
                if (value > 0)
                {
                    if (isEnglish)
                        formattedValue = currencyEn + " " + string.Format(currencyFormat, value);
                    else
                        formattedValue = string.Format(currencyFormat, value) + " " + currencyAr;
                }
            }

            return formattedValue;
        }
        public Tuple<string, string> GetOrderStatusNameAndColorCode(OrderStatus statusId, bool isEnglish)
        {
            string name = string.Empty;
            string colorCode = string.Empty;
            if (statusId == OrderStatus.Cancelled)
            {
                name = isEnglish ? Messages.Cancelled : MessagesAr.Cancelled;
                colorCode = "#ff4343";
            }
            else if (statusId == OrderStatus.Confirmed)
            {
                name = isEnglish ? Messages.Confirmed : MessagesAr.Confirmed;
                colorCode = "#0d6efd";
            }
            else if (statusId == OrderStatus.Delivered)
            {
                name = isEnglish ? Messages.Delivered : MessagesAr.Delivered;
                colorCode = "#019b06";
            }
            else if (statusId == OrderStatus.Pending)
            {
                name = isEnglish ? Messages.Pending : MessagesAr.Pending;
                colorCode = "#eb9d00";
            }
            else if (statusId == OrderStatus.Discarded)
            {
                name = isEnglish ? Messages.Discarded : MessagesAr.Discarded;
                colorCode = "#a7a5a1";
            }
            else if (statusId == OrderStatus.Failed)
            {
                name = isEnglish ? Messages.Failed : MessagesAr.Failed;
                colorCode = "#d2322d";
            }
            else if (statusId == OrderStatus.Received)
            {
                name = isEnglish ? Messages.Received : MessagesAr.Received;
                colorCode = "#019b06";
            }
            else if (statusId == OrderStatus.OnTheWay)
            {
                name = isEnglish ? Messages.OnTheWay : MessagesAr.OnTheWay;
                colorCode = "#019b06";
            }
            else if (statusId == OrderStatus.Returned)
            {
                name = isEnglish ? Messages.Returned : MessagesAr.Returned;
                colorCode = "#019b06";
            }
            else if (statusId == OrderStatus.CancelledByCustomer)
            {
                name = isEnglish ? Messages.CancelledByCustomer : MessagesAr.CancelledByCustomer;
                colorCode = "#019b06";
            }

            var result = new Tuple<string, string>(name, colorCode);
            return result;
        }
        public Tuple<string, string> GetSubscriptionStatusNameAndColorCode(SubscriptionStatus statusId, bool isEnglish)
        {
            string name = string.Empty;
            string colorCode = string.Empty;

            if (statusId == SubscriptionStatus.Pending)
            {
                name = isEnglish ? Messages.Pending : MessagesAr.Pending;
                colorCode = "#eb9d00";
            }
            else if (statusId == SubscriptionStatus.Confirmed)
            {
                name = isEnglish ? Messages.Confirmed : MessagesAr.Confirmed;
                colorCode = "#0d6efd";
            }
            else if (statusId == SubscriptionStatus.Expired)
            {
                name = isEnglish ? Messages.Expired : MessagesAr.Expired;
                colorCode = "#a7a5a1";
            }

            var result = new Tuple<string, string>(name, colorCode);
            return result;
        }
        public string GetPaymentResultTitle(PaymentStatus PaymentStatusId, bool isEnglish)
        {
            string title;

            if (PaymentStatusId == PaymentStatus.Captured)
            {
                title = isEnglish ? Messages.Captured : MessagesAr.Captured;
            }
            else if (PaymentStatusId == PaymentStatus.NotCaptured)
            {
                title = isEnglish ? Messages.NotCaptured : MessagesAr.NotCaptured;
            }
            else if (PaymentStatusId == PaymentStatus.PendingCash)
            {
                title = isEnglish ? Messages.PendingCash : MessagesAr.PendingCash;
            }
            else
            {
                title = isEnglish ? Messages.Cancelled : MessagesAr.Cancelled;
            }

            return title;
        }
        public Tuple<string, string> GetPaymentResultNameAndColorCode(PaymentStatus PaymentStatusId, bool isEnglish)
        {
            string name;
            string colorCode;
            if (PaymentStatusId == PaymentStatus.Captured)
            {
                name = isEnglish ? Messages.Captured : MessagesAr.Captured;
                colorCode = "#eb9d00";
            }
            else if (PaymentStatusId == PaymentStatus.NotCaptured)
            {
                name = isEnglish ? Messages.NotCaptured : MessagesAr.NotCaptured;
                colorCode = "#eb9d00";
            }
            else if (PaymentStatusId == PaymentStatus.PendingCash)
            {
                name = isEnglish ? Messages.PendingCash : MessagesAr.PendingCash;
                colorCode = "#eb9d00";
            }
            else
            {
                name = isEnglish ? Messages.Cancelled : MessagesAr.Cancelled;
                colorCode = "#eb9d00";
            }

            var result = new Tuple<string, string>(name, colorCode);
            return result;
        }
        public string GetTimeAgo(DateTime dateTime, bool isEnglish)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                if (isEnglish)
                {
                    result = string.Format(Messages.SecondsAgo, timeSpan.Seconds);
                }
                else
                {
                    result = string.Format(MessagesAr.SecondsAgo, timeSpan.Seconds);
                }
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                if (isEnglish)
                {
                    result = timeSpan.Minutes > 1 ? String.Format(Messages.MinutesAgo, timeSpan.Minutes) : Messages.AMinuteAgo;
                }
                else
                {
                    result = timeSpan.Minutes > 1 ? String.Format(MessagesAr.MinutesAgo, timeSpan.Minutes) : MessagesAr.AMinuteAgo;
                }
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                if (isEnglish)
                {
                    result = timeSpan.Hours > 1 ? String.Format(Messages.HoursAgo, timeSpan.Hours) : Messages.AnHourAgo;
                }
                else
                {
                    result = timeSpan.Hours > 1 ? String.Format(MessagesAr.HoursAgo, timeSpan.Hours) : MessagesAr.AnHourAgo;
                }
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                if (isEnglish)
                {
                    result = timeSpan.Days > 1 ? String.Format(Messages.DaysAgo, timeSpan.Days) : Messages.Yesterday;
                }
                else
                {
                    result = timeSpan.Days > 1 ? String.Format(MessagesAr.DaysAgo, timeSpan.Days) : MessagesAr.Yesterday;
                }
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                if (isEnglish)
                {
                    result = timeSpan.Days > 30 ? String.Format(Messages.MonthsAgo, timeSpan.Days / 30) : Messages.AMonthAgo;
                }
                else
                {
                    result = timeSpan.Days > 30 ? String.Format(MessagesAr.MonthsAgo, timeSpan.Days / 30) : MessagesAr.AMonthAgo;
                }
            }
            else
            {
                if (isEnglish)
                {
                    result = timeSpan.Days > 365 ? String.Format(Messages.YearsAgo, timeSpan.Days / 365) : Messages.AYearAgo;
                }
                else
                {
                    result = timeSpan.Days > 365 ? String.Format(MessagesAr.YearsAgo, timeSpan.Days / 365) : MessagesAr.AYearAgo;
                }
            }

            return result;
        }
        #endregion

        #region Token
        public string CreateAccessToken(dynamic customer, out string expiration)
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.APIKey);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string encryptedCustomerId = Cryptography.Encrypt(customer.Id.ToString());

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _appSettings.Audience),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(Constants.ClaimTypeId, encryptedCustomerId)
            };

            DateTime expiry = DateTime.Now.AddMinutes(_appSettings.TokenTimeout);

            var token = new JwtSecurityToken(
                    _appSettings.Issuer,
                    _appSettings.Issuer,
                    claims,
                    null,
                    expires: expiry,
                    signingCredentials: credentials
              );

            expiration = Convert.ToDateTime(expiry, new System.Globalization.CultureInfo("en-US")).ToString("yyyy-MM-ddTHH:mm:ss");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public int GetCustomerIdByToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.APIKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            var encryptedCustomerId = principal.Claims.FirstOrDefault(c => c.Type == Constants.ClaimTypeId)?.Value;
            if (string.IsNullOrWhiteSpace(encryptedCustomerId))
            {
                return 0;
            }

            string customerId = Cryptography.Decrypt(encryptedCustomerId);
            if (string.IsNullOrEmpty(customerId))
            {
                return 0;
            }

            int.TryParse(customerId, out int id);
            return id;
        }
        #endregion

        #region Validation
        public string CouponValidation(Coupon coupon, bool isEnglish, decimal total)
        {
            if (coupon.Deleted == true)
            {
                return isEnglish ? Messages.CouponNotExists : MessagesAr.CouponNotExists;
            }

            if (!coupon.Active)
            {
                return isEnglish ? Messages.CouponNotValid : MessagesAr.CouponNotValid;
            }

            if (coupon.StartDate.HasValue)
            {
                if (DateTime.Now.Date < coupon.StartDate.Value.Date)
                {
                    return isEnglish ? Messages.CouponNotValid : MessagesAr.CouponNotValid;
                }
            }

            if (coupon.EndDate.HasValue)
            {
                if (DateTime.Now.Date > coupon.EndDate.Value.Date)
                {
                    return isEnglish ? Messages.CouponNotValid : MessagesAr.CouponNotValid;
                }
            }

            if (coupon.LimitUsageEnabled)
            {
                if (coupon.QuantityUsed >= coupon.Quantity)
                {
                    return isEnglish ? Messages.CouponNotValid : MessagesAr.CouponNotValid;
                }
            }

            if (coupon.DiscountType == DiscountType.Amount)
            {
                if (coupon.DiscountAmount > total)
                {
                    return isEnglish ? Messages.CouponNotValid : MessagesAr.CouponNotValid;
                }
            }

            return string.Empty;
        }
        #endregion

        #region Delivery
        public async Task<decimal> GetDeliveryFeeByAreaId(int areaId)
        {
            var area = await _areaService.GetById(areaId);
            if (area is not null)
            {
                return area.DeliveryFee;
            }

            return 0;
        }
        public async Task<string> PrepareAddressText(Address address, bool isEnglish)
        {
            string addressText = string.Empty;

            if (!string.IsNullOrEmpty(address.Block))
                addressText = addressText + ", " + (isEnglish ? Messages.Block : MessagesAr.Block) + " " + address.Block;
            if (!string.IsNullOrEmpty(address.Street))
                addressText = addressText + ", " + (isEnglish ? Messages.Street : MessagesAr.Street) + " " + address.Street;
            if (!string.IsNullOrEmpty(address.Avenue))
                addressText = addressText + ", " + (isEnglish ? Messages.Avenue : MessagesAr.Avenue) + " " + address.Avenue;

            if (address.TypeId == (int)AddressType.Home)
            {
                if (!string.IsNullOrEmpty(address.HouseNumber))
                    addressText = addressText + ", " + (isEnglish ? Messages.HouseNo : MessagesAr.HouseNo) + " " + address.HouseNumber;
            }
            else if (address.TypeId == (int)AddressType.Appartment || address.TypeId == (int)AddressType.Office)
            {
                if (!string.IsNullOrEmpty(address.BuildingNumber))
                    addressText = addressText + ", " + (isEnglish ? Messages.BuildingNo : MessagesAr.BuildingNo) + " " + address.BuildingNumber;
                if (!string.IsNullOrEmpty(address.FloorNumber))
                    addressText = addressText + ", " + (isEnglish ? Messages.FloorNo : MessagesAr.FloorNo) + " " + address.FloorNumber;
                if (!string.IsNullOrEmpty(address.FlatNumber))
                    addressText = addressText + ", " + (isEnglish ? Messages.FlatNo : MessagesAr.FlatNo) + " " + address.FlatNumber;
            }
            else if (address.TypeId == (int)AddressType.School)
            {
                if (!string.IsNullOrEmpty(address.SchoolName))
                    addressText = addressText + ", " + (isEnglish ? Messages.SchoolName : MessagesAr.SchoolName) + " " + address.SchoolName;
            }
            else if (address.TypeId == (int)AddressType.Mosque)
            {
                if (!string.IsNullOrEmpty(address.MosqueName))
                    addressText = addressText + ", " + (isEnglish ? Messages.MosqueName : MessagesAr.MosqueName) + " " + address.MosqueName;
            }
            else if (address.TypeId == (int)AddressType.Government)
            {
                if (!string.IsNullOrEmpty(address.GovernmentEntity))
                    addressText = addressText + ", " + (isEnglish ? Messages.GovernmentEntity : MessagesAr.GovernmentEntity) + " " + address.GovernmentEntity;
            }

            if (address.AreaId > 0)
            {
                var area = await _areaService.GetById(address.AreaId);
                if (area != null)
                {
                    addressText = addressText + ", " + (isEnglish ? area.NameEn : area.NameAr);

                    var governorate = await _governorateService.GetById(area.GovernorateId);
                    if (governorate != null)
                    {
                        addressText = addressText + ", " + (isEnglish ? governorate.NameEn : governorate.NameAr);
                    }
                }
            }

            if (addressText.StartsWith(','))
                addressText = addressText.Substring(1, addressText.Length - 1);

            if (addressText.EndsWith(','))
                addressText = addressText.Substring(0, addressText.Length - 2);

            addressText = addressText.TrimStart().TrimEnd();

            return addressText;
        }
        public async Task<Tuple<DateTime, int>> GetAvailableDeliveryDateAndSlot()
        {
            List<DateTime> lstBlockedDates = new();
            var deliveryBlockedDates = await _deliveryBlockedDateService.GetAll();
            foreach (var item in deliveryBlockedDates)
            {
                for (DateTime i = item.FromDate; i <= item.ToDate; i = i.AddDays(1))
                {
                    lstBlockedDates.Add(i.Date);
                }
            }
            lstBlockedDates = lstBlockedDates.Distinct().ToList();

            bool deliveryFull = false;
            var deliveryDate = DateTime.Now;
            DeliveryTimeSlot deliveryTimeSlot;

            do
            {
                deliveryFull = false;
                deliveryDate = deliveryDate.AddDays(1).Date;
                var dayId = Common.GetDayId(deliveryDate);
                deliveryTimeSlot = await _deliveryTimeSlotService.GetByDayId(dayId: dayId);
                if (deliveryTimeSlot != null)
                {
                    if (lstBlockedDates.Contains(deliveryDate))
                    {
                        deliveryFull = true;
                    }
                    else if (!deliveryTimeSlot.Active)
                    {
                        deliveryFull = true;
                    }
                    else
                    {
                        var orderCountByTimeSlot = await _orderService.GetOrderCountByDeliveryTimeSlotId(deliveryTimeSlot.Id, deliveryDate);
                        var subscriptionOrderCountByTimeSlot = await _subscriptionService.GetSubscriptionOrderCountByDeliveryTimeSlotId(deliveryTimeSlot.Id, deliveryDate);
                        if ((orderCountByTimeSlot + subscriptionOrderCountByTimeSlot) >= deliveryTimeSlot.MaximumOrders)
                            deliveryFull = true;
                    }
                }
            }
            while (deliveryFull);

            return new Tuple<DateTime, int>(deliveryDate, deliveryTimeSlot.Id);
        }
        public async Task<Tuple<DateTime, int>> GetAvailableSubscriptionOrderDeliveryDateAndSlot(SubscriptionDeliveryDate subscriptionDeliveryDate)
        {
            List<DateTime> lstBlockedDates = new();
            var deliveryBlockedDates = await _deliveryBlockedDateService.GetAll();
            foreach (var item in deliveryBlockedDates)
            {
                for (DateTime i = item.FromDate; i <= item.ToDate; i = i.AddDays(1))
                {
                    lstBlockedDates.Add(i.Date);
                }
            }
            lstBlockedDates = lstBlockedDates.Distinct().ToList();

            var todayDate = DateTime.Now;
            DeliveryTimeSlot deliveryTimeSlot;
            int deliveryTimeSlotId = 0;

            DateTime fromDate = new(year: todayDate.Year, month: todayDate.Month, day: subscriptionDeliveryDate.FromDay);

            var totalDays = DateTime.DaysInMonth(todayDate.Year, todayDate.Month);
            int toDay = subscriptionDeliveryDate.ToDay;
            if (toDay > 25)
                toDay = totalDays;
            DateTime toDate = new(year: todayDate.Year, month: todayDate.Month, day: toDay);

            if (DateTime.Now >= toDate)
            {
                fromDate = fromDate.AddMonths(1);
                toDate = toDate.AddMonths(1);
            }

            if (DateTime.Now > fromDate)
            {
                fromDate = DateTime.Now;
            }

            bool deliveryFull = false;

            DateTime? deliveryDate = null;
            do
            {
                deliveryFull = false;
                fromDate = fromDate.AddDays(1).Date;
                var dayId = Common.GetDayId(fromDate);
                deliveryTimeSlot = await _deliveryTimeSlotService.GetByDayId(dayId: dayId);
                if (lstBlockedDates.Contains(fromDate))
                {
                    deliveryFull = true;
                }
                else if (!deliveryTimeSlot.Active)
                {
                    deliveryFull = true;
                }
                else
                {
                    var orderCountByTimeSlot = await _orderService.GetOrderCountByDeliveryTimeSlotId(deliveryTimeSlot.Id, fromDate);
                    var subscriptionOrderCountByTimeSlot = await _subscriptionService.GetSubscriptionOrderCountByDeliveryTimeSlotId(deliveryTimeSlot.Id, fromDate);
                    if ((orderCountByTimeSlot + subscriptionOrderCountByTimeSlot) >= deliveryTimeSlot.MaximumOrders)
                        deliveryFull = true;
                }

                if (!deliveryFull)
                {
                    deliveryDate = fromDate;
                    deliveryTimeSlotId = deliveryTimeSlot.Id;
                }
            }
            while (deliveryFull && fromDate < toDate);

            if (deliveryDate == null)
            {
                deliveryDate = toDate;
                var dayId = Common.GetDayId(deliveryDate.Value);
                deliveryTimeSlot = await _deliveryTimeSlotService.GetByDayId(dayId: dayId);
                deliveryTimeSlotId = deliveryTimeSlot.Id;
            }

            return new Tuple<DateTime, int>(deliveryDate.Value, deliveryTimeSlotId);
        }
        public async Task<bool> CheckAvailableDeliveryDate(DateTime dateTime)
        {
            if (dateTime.Date < DateTime.Now.Date)
                return false;

            List<DateTime> lstBlockedDates = new();
            var deliveryBlockedDates = await _deliveryBlockedDateService.GetAll();
            foreach (var item in deliveryBlockedDates)
            {
                for (DateTime i = item.FromDate; i <= item.ToDate; i = i.AddDays(1))
                {
                    lstBlockedDates.Add(i.Date);
                }
            }
            lstBlockedDates = lstBlockedDates.Distinct().ToList();

            if (lstBlockedDates.Contains(dateTime))
            {
                return false;
            }

            var dayId = Common.GetDayId(dateTime);
            var deliveryTimeSlot = await _deliveryTimeSlotService.GetByDayId(dayId: dayId);
            if (deliveryTimeSlot != null)
            {
                if (!deliveryTimeSlot.Active)
                {
                    return false;
                }

                var orderCountByTimeSlot = await _orderService.GetOrderCountByDeliveryTimeSlotId(deliveryTimeSlot.Id, dateTime);
                var subscriptionOrderCountByTimeSlot = await _subscriptionService.GetSubscriptionOrderCountByDeliveryTimeSlotId(deliveryTimeSlot.Id, dateTime);
                if ((orderCountByTimeSlot + subscriptionOrderCountByTimeSlot) >= deliveryTimeSlot.MaximumOrders)
                    return false;
            }

            return true;
        }
        #endregion

        #region Promotion
        public async Task<decimal> GetCashbackAmount(int customerId, decimal amount)
        {
            decimal cashbackAmount = 0;
            var cashbackBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Cashback);
            var promotion = await _promotionService.GetDefault();
            if (promotion != null && promotion.CashbackRedeemEnabled && cashbackBalance >= promotion.CashbackRedeemMinWalletAmount &&
                amount >= promotion.CashbackRedeemMinOrderAmount)
            {
                int noOfTimes = (int)(amount / promotion.CashbackRedeemMinOrderAmount);
                cashbackAmount = noOfTimes * promotion.CashbackValueToDeduct;
            }

            return cashbackAmount;
        }
        public async Task RedeemCashbackAmount(int customerId, decimal amount)
        {
            List<WalletTransaction> walletTransactionsToUpdate = new();
            var walletTransactions = await _customerService.GetAllWalletTransaction(customerId: customerId,
                walletType: WalletType.Cashback, forRedeem: true);
            foreach (var walletTransaction in walletTransactions)
            {
                bool returnFromLoop = false;
                if (amount > 0)
                {
                    if (walletTransaction.RemainingCredit >= amount)
                    {
                        walletTransaction.RemainingCredit -= amount;
                        walletTransactionsToUpdate.Add(walletTransaction);
                        returnFromLoop = true;
                    }
                    else
                    {
                        amount -= walletTransaction.RemainingCredit;
                        walletTransaction.RemainingCredit = 0;
                        walletTransactionsToUpdate.Add(walletTransaction);
                    }
                }
                else
                {
                    break;
                }

                if (returnFromLoop)
                    break;
            }

            await _customerService.UpdateWalletTransactions(walletTransactionsToUpdate);
        }
        #endregion

        #region Order
        public string GetOrderPdfUrl(OrderModel order, string apiBaseUrl, bool isEnglish)
        {
            //  ApplyLicenseKey();
            string[] emailTemplatePath = new string[3] { Directory.GetCurrentDirectory(), "Pdfs", "order_print.html" };
            StreamReader reader = new StreamReader(Path.Combine(emailTemplatePath));
            string orderHtml = reader.ReadToEnd();

            #region Company Info 
            orderHtml = orderHtml.Replace("{Body-Style}", isEnglish ? "direction: ltr;" : "direction: rtl;");
            orderHtml = orderHtml.Replace("{Base-Url}", apiBaseUrl);
            orderHtml = orderHtml.Replace("{all.min.layout}", isEnglish ? "all.min.layout.admin.css" : "all.min.layout.admin.rtl.css");
            orderHtml = orderHtml.Replace("{Logo-Text-Align}", isEnglish ? "text-align: left;" : "text-align: right;");
            orderHtml = orderHtml.Replace("{Company-Details-Text-Align}", isEnglish ? "text-align: right;" : "text-align: left;");
            orderHtml = orderHtml.Replace("{Company-Name}", OrderPDF.CompanyName);
            orderHtml = orderHtml.Replace("{Company-Website}", OrderPDF.CompanyWebsite);
            orderHtml = orderHtml.Replace("{Company-Email}", OrderPDF.CompanyEmail);
            #endregion

            #region Order Details
            orderHtml = orderHtml.Replace("{Order-Details}", isEnglish ? OrderPDF.OrderDetails : OrderPDFAr.OrderDetails);
            orderHtml = orderHtml.Replace("{Order-Type}", isEnglish ? OrderPDF.OrderType : OrderPDFAr.OrderType);
            orderHtml = orderHtml.Replace("{Order-Type-Value}", order.OrderTypeText != null ? order.OrderTypeText : "");
            orderHtml = orderHtml.Replace("{Order-Number}", isEnglish ? OrderPDF.OrderNumber : OrderPDFAr.OrderNumber);
            orderHtml = orderHtml.Replace("{Order-Number-Value}", order.OrderNumber);
            orderHtml = orderHtml.Replace("{Transaction-Date}", isEnglish ? OrderPDF.TransactionDate : OrderPDFAr.TransactionDate);
            orderHtml = orderHtml.Replace("{Transaction-Date-Value}", order.FormattedDate);
            orderHtml = orderHtml.Replace("{Sub-Total}", isEnglish ? OrderPDF.SubTotal : OrderPDFAr.SubTotal);
            orderHtml = orderHtml.Replace("{Sub-Total-Value}", order.FormattedSubTotal);
            orderHtml = orderHtml.Replace("{Delivery-Charges}", isEnglish ? OrderPDF.DeliveryCharges : OrderPDFAr.DeliveryCharges);
            orderHtml = orderHtml.Replace("{Delivery-Charges-Value}", order.FormattedDeliveryFee);
            orderHtml = orderHtml.Replace("{Coupon-Amount-Style}", !string.IsNullOrEmpty(order.FormattedCouponDiscountAmount) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Coupon-Amount}", isEnglish ? OrderPDF.CouponAmount : OrderPDFAr.CouponAmount);
            orderHtml = orderHtml.Replace("{Coupon-Amount-Value}", order.FormattedCouponDiscountAmount + "&nbsp;");
            orderHtml = orderHtml.Replace("{Grand-Total}", isEnglish ? OrderPDF.GrandTotal : OrderPDFAr.GrandTotal);
            orderHtml = orderHtml.Replace("{Grand-Total-Value}", order.FormattedTotal);
            orderHtml = orderHtml.Replace("{Status-Style}", "color: " + order.OrderStatusColor + " !important;");
            orderHtml = orderHtml.Replace("{Status}", isEnglish ? OrderPDF.Status : OrderPDFAr.Status);
            orderHtml = orderHtml.Replace("{Status-Value}", order.OrderStatusName);
            #endregion

            #region  Items Detail  
            //title
            orderHtml = orderHtml.Replace("{Items-Detail}", isEnglish ? OrderPDF.ItemsDetail : OrderPDFAr.ItemsDetail);
            //table columns header
            orderHtml = orderHtml.Replace("{Product-Name}", isEnglish ? OrderPDF.ProductName : OrderPDFAr.ProductName);
            orderHtml = orderHtml.Replace("{Price}", isEnglish ? OrderPDF.Price : OrderPDFAr.Price);
            orderHtml = orderHtml.Replace("{Quantity}", isEnglish ? OrderPDF.Quantity : OrderPDFAr.Quantity);
            orderHtml = orderHtml.Replace("{Total-Amount}", isEnglish ? OrderPDF.TotalAmount : OrderPDFAr.TotalAmount);

            //table items list
            var items = string.Empty;
            foreach (var item in order.OrderItems)
            {

                items = items + @"<tr>
                                       <td>" + item.Product.Title + @"</td> 
                                       <td>" + item.FormattedUnitPrice + @"</td> 
                                       <td>" + item.Quantity + @"</td>
                                       <td>" + item.FormattedTotal + @"</td>
                                     </tr>";

            }
            orderHtml = orderHtml.Replace("{Items-Detail-Value}", items);
            #endregion Items Details

            #region Payment Details
            orderHtml = orderHtml.Replace("{Payment-Details}", isEnglish ? OrderPDF.PaymentDetails : OrderPDFAr.PaymentDetails);
            orderHtml = orderHtml.Replace("{Payment-Method}", isEnglish ? OrderPDF.PaymentMethod : OrderPDFAr.PaymentMethod);
            orderHtml = orderHtml.Replace("{Payment-Method-Value}", order.PaymentMethod != null ? order.PaymentMethod.Name : "");
            orderHtml = orderHtml.Replace("{Payment-Status}", isEnglish ? OrderPDF.PaymentStatus : OrderPDFAr.PaymentStatus);
            orderHtml = orderHtml.Replace("{Payment-Status-Value}", order.PaymentResult != null ? order.PaymentResult : "");
            orderHtml = orderHtml.Replace("{Payment-ID-Style}", !string.IsNullOrEmpty(order.PaymentId) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Payment-ID}", isEnglish ? OrderPDF.PaymentID : OrderPDFAr.PaymentID);
            orderHtml = orderHtml.Replace("{Payment-ID-Value}", order.PaymentId);
            orderHtml = orderHtml.Replace("{Payment-Reference-Id-Style}", !string.IsNullOrEmpty(order.PaymentRefId) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Payment-Reference-Id}", isEnglish ? OrderPDF.PaymentReference : OrderPDFAr.PaymentReference);
            orderHtml = orderHtml.Replace("{Payment-Reference-Id-Value}", order.PaymentRefId != null ? order.PaymentRefId : "");
            #endregion

            #region Customer Details 
            orderHtml = orderHtml.Replace("{Customer-Details}", isEnglish ? OrderPDF.CustomerDetails : OrderPDFAr.CustomerDetails);
            orderHtml = orderHtml.Replace("{Customer-Name}", isEnglish ? OrderPDF.CustomerName : OrderPDFAr.CustomerName);
            orderHtml = orderHtml.Replace("{Customer-Name-Value}", order.Customer.Name);
            orderHtml = orderHtml.Replace("{Customer-Email}", isEnglish ? OrderPDF.CustomerEmail : OrderPDFAr.CustomerEmail);
            orderHtml = orderHtml.Replace("{Customer-Email-Value}", order.Customer.EmailAddress);
            orderHtml = orderHtml.Replace("{Customer-Mobile-Style}", "");
            orderHtml = orderHtml.Replace("{Customer-Mobile}", isEnglish ? OrderPDF.CustomerMobile : OrderPDFAr.CustomerMobile);
            orderHtml = orderHtml.Replace("{Customer-Mobile-Value}", order.Customer.FormattedMobile);
            #endregion

            #region Delivery Detail
            orderHtml = orderHtml.Replace("{Delivery-Detail}", isEnglish ? OrderPDF.DeliveryDetail : OrderPDFAr.DeliveryDetail);
            orderHtml = orderHtml.Replace("{Delivery-Address}", isEnglish ? OrderPDF.DeliveryAddress : OrderPDFAr.DeliveryAddress);
            orderHtml = orderHtml.Replace("{Delivery-Address-Value}", order.Address.AddressText != null ? order.Address.AddressText : "");
            orderHtml = orderHtml.Replace("{Delivery-Notes-Style}", !string.IsNullOrEmpty(order.Address.Notes) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Delivery-Notes}", isEnglish ? OrderPDF.DeliveryNotes : OrderPDFAr.DeliveryNotes);
            orderHtml = orderHtml.Replace("{Delivery-Notes-Value}", order.Address.Notes);
            #endregion

            string path = MediaHelper.HtmlToPdf(orderHtml, "Pdfs/OrderPdfs");

            return apiBaseUrl + path;
        }
        public async Task<bool> UpdateOrderStatus(Order order, OrderStatus orderStatusId, bool refundDeliveryFee = false, string notes = "")
        {
            bool updated = false;
            if (order.OrderStatusId != OrderStatus.Cancelled)
            {
                using var dbContextTransaction = _dbcontext.Database.BeginTransaction();
                try
                {
                    order.Notes = notes;
                    await _orderService.UpdateOrderStatus(order, (int)orderStatusId);

                    if (orderStatusId == OrderStatus.Cancelled)
                    {
                        foreach (var orderItem in order.OrderItems)
                        {
                            if (orderItem.Product.ProductType == ProductType.BaseProduct)
                            {
                                await _productService.AdjustStockQuantity(product: orderItem.Product, stock: orderItem.Quantity,
                                 relatedEntityType: RelatedEntityType.Order, relatedEntityId: orderItem.Id,
                                 productActionType: ProductActionType.AddToStock);
                            }
                            else if (orderItem.Product.ProductType == ProductType.BundledProduct)
                            {
                                foreach (var orderItemDetail in orderItem.OrderItemDetails)
                                {
                                    var childProduct = await _productService.GetById(orderItemDetail.ChildProductId);
                                    if (childProduct != null)
                                    {
                                        await _productService.AdjustStockQuantity(product: childProduct,
                                            stock: orderItem.Quantity * orderItemDetail.Quantity,
                                           relatedEntityType: RelatedEntityType.Order, relatedEntityId: orderItem.Id,
                                           productActionType: ProductActionType.AddToStock);
                                    }
                                }
                            }
                        }

                        var coupon = order.Coupon;
                        if (coupon != null)
                        {
                            coupon.QuantityUsed--;
                            await _couponService.UpdateCoupon(coupon);
                        }

                        if (order.PaymentMethodId == (int)PaymentMethod.Wallet)
                        {
                            decimal refundingWalletUsedAmount = order.WalletUsedAmount;
                            if (!refundDeliveryFee)
                                refundingWalletUsedAmount -= order.DeliveryFee;

                            var walletBalance = await _customerService.GetWalletBalanceByCustomerId(order.CustomerId, WalletType.Wallet);
                            var walletTransaction = new WalletTransaction
                            {
                                TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                CreatedBy = order.CustomerId,
                                CustomerId = order.CustomerId,
                                WalletTypeId = WalletType.Wallet,
                                RelatedEntityTypeId = RelatedEntityType.Order,
                                RelatedEntityId = order.Id,
                                Credit = refundingWalletUsedAmount,
                                Debit = 0,
                                WalletTransactionTypeId = WalletTransactionType.RefundWalletAmount,
                                Balance = walletBalance + refundingWalletUsedAmount
                            };

                            await _customerService.CreateWalletTransaction(walletTransaction);
                        }
                        else if (order.PaymentMethodId == (int)PaymentMethod.KNET || order.PaymentMethodId == (int)PaymentMethod.VISAMASTER ||
                            order.PaymentMethodId == (int)PaymentMethod.Tabby)
                        {
                            WalletTransaction walletTransaction;
                            decimal refundingTotal = order.Total;
                            decimal refundingWalletUsedAmount = order.WalletUsedAmount;
                            if (!refundDeliveryFee)
                            {
                                if (order.DeliveryFee > refundingTotal)
                                {
                                    refundingTotal = 0;
                                    refundingWalletUsedAmount = refundingWalletUsedAmount - order.DeliveryFee + refundingTotal;
                                }
                                else if (order.DeliveryFee == refundingTotal)
                                {
                                    refundingTotal = 0;
                                }
                                else
                                {
                                    refundingTotal -= order.DeliveryFee;
                                }
                            }

                            if (refundingTotal > 0)
                            {
                                var walletBalance = await _customerService.GetWalletBalanceByCustomerId(order.CustomerId, WalletType.Wallet);
                                walletTransaction = new WalletTransaction
                                {
                                    TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                    CreatedBy = order.CustomerId,
                                    CustomerId = order.CustomerId,
                                    WalletTypeId = WalletType.Wallet,
                                    RelatedEntityTypeId = RelatedEntityType.Order,
                                    RelatedEntityId = order.Id,
                                    Credit = refundingTotal,
                                    Debit = 0,
                                    WalletTransactionTypeId = WalletTransactionType.RefundOrderAmount,
                                    Balance = walletBalance + refundingTotal
                                };

                                await _customerService.CreateWalletTransaction(walletTransaction);
                            }

                            if (refundingWalletUsedAmount > 0)
                            {
                                var walletBalance = await _customerService.GetWalletBalanceByCustomerId(order.CustomerId, WalletType.Wallet);
                                walletTransaction = new WalletTransaction
                                {
                                    TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                    CreatedBy = order.CustomerId,
                                    CustomerId = order.CustomerId,
                                    WalletTypeId = WalletType.Wallet,
                                    RelatedEntityTypeId = RelatedEntityType.Order,
                                    RelatedEntityId = order.Id,
                                    Credit = refundingWalletUsedAmount,
                                    Debit = 0,
                                    WalletTransactionTypeId = WalletTransactionType.RefundWalletAmount,
                                    Balance = walletBalance + refundingWalletUsedAmount
                                };

                                await _customerService.CreateWalletTransaction(walletTransaction);
                            }
                        }

                        if (order.ReceivedCashbackAmount > 0)
                        {
                            var walletTransaction = new WalletTransaction
                            {
                                TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                CreatedBy = order.CustomerId,
                                CustomerId = order.CustomerId,
                                WalletTypeId = WalletType.Cashback,
                                RelatedEntityTypeId = RelatedEntityType.Order,
                                RelatedEntityId = order.Id,
                                Credit = 0,
                                Debit = order.ReceivedCashbackAmount,
                                WalletTransactionTypeId = WalletTransactionType.RefundCashbackOnPurchase
                            };

                            await _customerService.CreateWalletTransaction(walletTransaction);
                        }
                    }

                    await dbContextTransaction.CommitAsync();
                    updated = true;
                }
                catch
                {
                    await dbContextTransaction.RollbackAsync();
                }
            }

            return updated;
        }
        public async Task<bool> RescheduleOrderDelivery(Order order, DateTime? newDeliveryDate = null)
        {
            if (newDeliveryDate.HasValue)
            {
                var dayId = Common.GetDayId(newDeliveryDate.Value);
                var deliveryTimeSlot = await _deliveryTimeSlotService.GetByDayId(dayId: dayId);
                if (deliveryTimeSlot == null)
                {
                    return false;
                }

                order.DeliveryDate = newDeliveryDate.Value;
                order.DeliveryTimeSlotId = deliveryTimeSlot.Id;
                await _orderService.UpdateOrder(order);
            }
            else
            {
                var dateAndSlot = await GetAvailableDeliveryDateAndSlot();
                order.DeliveryDate = dateAndSlot.Item1;
                order.DeliveryTimeSlotId = dateAndSlot.Item2;
                await _orderService.UpdateOrder(order);
            }

            return true;
        }
        #endregion

        #region Subscription
        public string GetSubscriptionPdfUrl(SubscriptionModel order, string apiBaseUrl, bool isEnglish)
        {
            //ApplyLicenseKey();
            string[] emailTemplatePath = new string[3] { Directory.GetCurrentDirectory(), "Pdfs", "subscription_print.html" };
            StreamReader reader = new StreamReader(Path.Combine(emailTemplatePath));
            string orderHtml = reader.ReadToEnd();

            #region Company Info 
            orderHtml = orderHtml.Replace("{Body-Style}", isEnglish ? "direction: ltr;" : "direction: rtl;");
            orderHtml = orderHtml.Replace("{Base-Url}", apiBaseUrl);
            orderHtml = orderHtml.Replace("{all.min.layout}", isEnglish ? "all.min.layout.admin.css" : "all.min.layout.admin.rtl.css");
            orderHtml = orderHtml.Replace("{Logo-Text-Align}", isEnglish ? "text-align: left;" : "text-align: right;");
            orderHtml = orderHtml.Replace("{Company-Details-Text-Align}", isEnglish ? "text-align: right;" : "text-align: left;");
            orderHtml = orderHtml.Replace("{Company-Name}", OrderPDF.CompanyName);
            orderHtml = orderHtml.Replace("{Company-Website}", OrderPDF.CompanyWebsite);
            orderHtml = orderHtml.Replace("{Company-Email}", OrderPDF.CompanyEmail);
            #endregion

            #region Order Details
            orderHtml = orderHtml.Replace("{Order-Details}", isEnglish ? OrderPDF.OrderDetails : OrderPDFAr.OrderDetails);
            orderHtml = orderHtml.Replace("{Order-Type}", isEnglish ? OrderPDF.OrderType : OrderPDFAr.OrderType);
            orderHtml = orderHtml.Replace("{Order-Type-Value}", isEnglish ? OrderPDF.Online : OrderPDFAr.Online);
            orderHtml = orderHtml.Replace("{Order-Number}", isEnglish ? OrderPDF.OrderNumber : OrderPDFAr.OrderNumber);
            orderHtml = orderHtml.Replace("{Order-Number-Value}", order.SubscriptionNumber);
            orderHtml = orderHtml.Replace("{Transaction-Date}", isEnglish ? OrderPDF.TransactionDate : OrderPDFAr.TransactionDate);
            orderHtml = orderHtml.Replace("{Transaction-Date-Value}", order.FormattedDate);
            orderHtml = orderHtml.Replace("{Sub-Total}", isEnglish ? OrderPDF.SubTotal : OrderPDFAr.SubTotal);
            orderHtml = orderHtml.Replace("{Sub-Total-Value}", order.FormattedSubTotal);
            orderHtml = orderHtml.Replace("{Delivery-Charges}", isEnglish ? OrderPDF.DeliveryCharges : OrderPDFAr.DeliveryCharges);
            orderHtml = orderHtml.Replace("{Delivery-Charges-Value}", order.FormattedDeliveryFee);
            orderHtml = orderHtml.Replace("{Coupon-Amount-Style}", !string.IsNullOrEmpty(order.FormattedCouponDiscountAmount) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Coupon-Amount}", isEnglish ? OrderPDF.CouponAmount : OrderPDFAr.CouponAmount);
            orderHtml = orderHtml.Replace("{Coupon-Amount-Value}", order.FormattedCouponDiscountAmount + "&nbsp;");
            orderHtml = orderHtml.Replace("{Grand-Total}", isEnglish ? OrderPDF.GrandTotal : OrderPDFAr.GrandTotal);
            orderHtml = orderHtml.Replace("{Grand-Total-Value}", order.FormattedTotal);
            orderHtml = orderHtml.Replace("{Status-Style}", "color: " + order.SubscriptionStatusColor + " !important;");
            orderHtml = orderHtml.Replace("{Status}", isEnglish ? OrderPDF.Status : OrderPDFAr.Status);
            orderHtml = orderHtml.Replace("{Status-Value}", order.SubscriptionStatusName);
            #endregion

            #region  Items Detail  
            orderHtml = orderHtml.Replace("{Items-Detail}", isEnglish ? OrderPDF.ItemsDetail : OrderPDFAr.ItemsDetail);
            orderHtml = orderHtml.Replace("{Product-Name}", isEnglish ? OrderPDF.ProductName : OrderPDFAr.ProductName);
            orderHtml = orderHtml.Replace("{Quantity}", isEnglish ? OrderPDF.Quantity : OrderPDFAr.Quantity);

            //table items list
            var items = "";
            foreach (var item in order.SubscriptionPackTitles)
            {
                items += "<tr>" + "<td>" + item.Title + "</td><td>" + item.Value + "</td></tr>";
            }
            orderHtml = orderHtml.Replace("{Items-Detail-Value}", items);
            #endregion Items Details

            #region Payment Details
            orderHtml = orderHtml.Replace("{Payment-Details}", isEnglish ? OrderPDF.PaymentDetails : OrderPDFAr.PaymentDetails);
            orderHtml = orderHtml.Replace("{Payment-Method}", isEnglish ? OrderPDF.PaymentMethod : OrderPDFAr.PaymentMethod);
            orderHtml = orderHtml.Replace("{Payment-Method-Value}", order.PaymentMethod != null ? order.PaymentMethod.Name : "");
            orderHtml = orderHtml.Replace("{Payment-Status}", isEnglish ? OrderPDF.PaymentStatus : OrderPDFAr.PaymentStatus);
            orderHtml = orderHtml.Replace("{Payment-Status-Value}", order.PaymentResult != null ? order.PaymentResult : "");
            orderHtml = orderHtml.Replace("{Payment-ID-Style}", !string.IsNullOrEmpty(order.PaymentId) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Payment-ID}", isEnglish ? OrderPDF.PaymentID : OrderPDFAr.PaymentID);
            orderHtml = orderHtml.Replace("{Payment-ID-Value}", order.PaymentId);
            orderHtml = orderHtml.Replace("{Payment-Reference-Id-Style}", !string.IsNullOrEmpty(order.PaymentRefId) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Payment-Reference-Id}", isEnglish ? OrderPDF.PaymentReference : OrderPDFAr.PaymentReference);
            orderHtml = orderHtml.Replace("{Payment-Reference-Id-Value}", order.PaymentRefId != null ? order.PaymentRefId : "");
            #endregion

            #region Customer Details 
            orderHtml = orderHtml.Replace("{Customer-Details}", isEnglish ? OrderPDF.CustomerDetails : OrderPDFAr.CustomerDetails);
            orderHtml = orderHtml.Replace("{Customer-Name}", isEnglish ? OrderPDF.CustomerName : OrderPDFAr.CustomerName);
            orderHtml = orderHtml.Replace("{Customer-Name-Value}", order.Customer.Name);
            orderHtml = orderHtml.Replace("{Customer-Email}", isEnglish ? OrderPDF.CustomerEmail : OrderPDFAr.CustomerEmail);
            orderHtml = orderHtml.Replace("{Customer-Email-Value}", order.Customer.EmailAddress);
            orderHtml = orderHtml.Replace("{Customer-Mobile-Style}", "");
            orderHtml = orderHtml.Replace("{Customer-Mobile}", isEnglish ? OrderPDF.CustomerMobile : OrderPDFAr.CustomerMobile);
            orderHtml = orderHtml.Replace("{Customer-Mobile-Value}", order.Customer.FormattedMobile);
            #endregion

            #region Delivery Detail
            orderHtml = orderHtml.Replace("{Delivery-Detail}", isEnglish ? OrderPDF.DeliveryDetail : OrderPDFAr.DeliveryDetail);
            orderHtml = orderHtml.Replace("{Delivery-Address}", isEnglish ? OrderPDF.DeliveryAddress : OrderPDFAr.DeliveryAddress);
            orderHtml = orderHtml.Replace("{Delivery-Address-Value}", order.Address.AddressText != null ? order.Address.AddressText : "");
            orderHtml = orderHtml.Replace("{Delivery-Notes-Style}", !string.IsNullOrEmpty(order.Address.Notes) ? "" : "display: none;");
            orderHtml = orderHtml.Replace("{Delivery-Notes}", isEnglish ? OrderPDF.DeliveryNotes : OrderPDFAr.DeliveryNotes);
            orderHtml = orderHtml.Replace("{Delivery-Notes-Value}", order.Address.Notes);
            #endregion

            string path = MediaHelper.HtmlToPdf(orderHtml, "Pdfs/SubscriptionPdfs");

            return apiBaseUrl + path;
        }
        public async Task<bool> UpdateSubscriptionOrderStatus(Subscription subscription, SubscriptionStatus subscriptionStatus, bool refundDeliveryFee = false, string notes = "")
        {
            bool updated = false;
            if (subscription.SubscriptionStatusId != SubscriptionStatus.Cancelled)
            {
                using var dbContextTransaction = _dbcontext.Database.BeginTransaction();
                try
                {
                    subscription.Notes = notes;
                    subscription.SubscriptionStatusId = subscriptionStatus;
                    await _subscriptionService.UpdateSubscription(subscription);

                    if (subscriptionStatus == SubscriptionStatus.Cancelled && subscription.SubscriptionOrders.Count > 0)
                    {
                        var pendingOrders = subscription.SubscriptionOrders.Where(a => !a.Delivered && a.Confirmed).ToList();
                        foreach (var pendingOrder in pendingOrders)
                        {
                            var subscriptionItemDetails = await _subscriptionService.GetAllSubscriptionItemDetail(pendingOrder.SubscriptionId);
                            foreach (var subscriptionItemDetail in subscriptionItemDetails)
                            {
                                var childProduct = await _productService.GetById(subscriptionItemDetail.ChildProductId);
                                if (childProduct != null)
                                {
                                    await _productService.AdjustStockQuantity(product: childProduct,
                                        stock: subscriptionItemDetail.Subscription.Quantity * subscriptionItemDetail.Quantity,
                                       relatedEntityType: RelatedEntityType.SubscriptionOrder, relatedEntityId: pendingOrder.Id,
                                       productActionType: ProductActionType.AddToStock);
                                }
                            }
                        }

                        #region Refund amount to customer
                        //if (subscription.FullPayment)
                        //{
                        //    var lastSubscriptionOrder = subscription.SubscriptionOrders.OrderByDescending(a => a.Id).FirstOrDefault();
                        //    if (lastSubscriptionOrder.Confirmed)
                        //    {
                        //        int deliveredCount = subscription.SubscriptionOrders.Where(a => a.Delivered).Count();
                        //        if (deliveredCount == 0)
                        //        {
                        //            await RefundFullSubscriptionAmount(subscription, lastSubscriptionOrder, refundDeliveryFee);
                        //        }
                        //        else
                        //        {
                        //            WalletTransaction walletTransaction;
                        //            decimal refundingTotal = lastSubscriptionOrder.Total;
                        //            if (!refundDeliveryFee)
                        //            {
                        //                if (subscription.DeliveryFee > refundingTotal)
                        //                {
                        //                    refundingTotal = 0;
                        //                }
                        //                else if (subscription.DeliveryFee == refundingTotal)
                        //                {
                        //                    refundingTotal = 0;
                        //                }
                        //                else
                        //                {
                        //                    refundingTotal -= subscription.DeliveryFee;
                        //                }
                        //            }

                        //            int remainingOrderCount = subscription.NumberOfMonths - deliveredCount;
                        //            decimal completeRefundingTotal = remainingOrderCount * refundingTotal;

                        //            if (completeRefundingTotal > 0)
                        //            {
                        //                var walletBalance = await _customerService.GetWalletBalanceByCustomerId(subscription.CustomerId, WalletType.Wallet);
                        //                walletTransaction = new WalletTransaction
                        //                {
                        //                    TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                        //                    CreatedBy = subscription.CustomerId,
                        //                    CustomerId = subscription.CustomerId,
                        //                    WalletTypeId = WalletType.Wallet,
                        //                    RelatedEntityTypeId = RelatedEntityType.Subscription,
                        //                    RelatedEntityId = subscription.Id,
                        //                    Credit = completeRefundingTotal,
                        //                    Debit = 0,
                        //                    WalletTransactionTypeId = WalletTransactionType.RefundOrderAmount,
                        //                    Balance = walletBalance + completeRefundingTotal
                        //                };

                        //                await _customerService.CreateWalletTransaction(walletTransaction);
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    var lastSubscriptionOrder = subscription.SubscriptionOrders.OrderByDescending(a => a.Id).FirstOrDefault();
                        //    if (lastSubscriptionOrder.FirstOrder)
                        //    {
                        //        if (lastSubscriptionOrder.Confirmed && !lastSubscriptionOrder.Delivered)
                        //        {
                        //            await RefundFullSubscriptionAmount(subscription, lastSubscriptionOrder, refundDeliveryFee);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        var firstSubscriptionOrder = subscription.SubscriptionOrders.Where(a => a.FirstOrder).FirstOrDefault();
                        //        if (firstSubscriptionOrder.Confirmed)
                        //        {
                        //            if (!firstSubscriptionOrder.Delivered)
                        //            {
                        //                await RefundFullSubscriptionAmount(subscription, firstSubscriptionOrder, refundDeliveryFee);
                        //            }

                        //            var otherOrders = subscription.SubscriptionOrders.Where(a => !a.FirstOrder).ToList();
                        //            foreach (var otherOrder in otherOrders)
                        //            {
                        //                if (otherOrder.Confirmed)
                        //                {
                        //                    WalletTransaction walletTransaction;
                        //                    decimal refundingTotal = otherOrder.Total;
                        //                    if (!refundDeliveryFee)
                        //                    {
                        //                        if (subscription.DeliveryFee > refundingTotal)
                        //                        {
                        //                            refundingTotal = 0;
                        //                        }
                        //                        else if (subscription.DeliveryFee == refundingTotal)
                        //                        {
                        //                            refundingTotal = 0;
                        //                        }
                        //                        else
                        //                        {
                        //                            refundingTotal -= subscription.DeliveryFee;
                        //                        }
                        //                    }

                        //                    if (otherOrder.PaymentMethodId == (int)PaymentMethod.KNET || otherOrder.PaymentMethodId == (int)PaymentMethod.VISAMASTER)
                        //                    {
                        //                        if (refundingTotal > 0)
                        //                        {
                        //                            var walletBalance = await _customerService.GetWalletBalanceByCustomerId(subscription.CustomerId, WalletType.Wallet);
                        //                            walletTransaction = new WalletTransaction
                        //                            {
                        //                                TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                        //                                CreatedBy = subscription.CustomerId,
                        //                                CustomerId = subscription.CustomerId,
                        //                                WalletTypeId = WalletType.Wallet,
                        //                                RelatedEntityTypeId = RelatedEntityType.Subscription,
                        //                                RelatedEntityId = subscription.Id,
                        //                                Credit = refundingTotal,
                        //                                Debit = 0,
                        //                                WalletTransactionTypeId = WalletTransactionType.RefundOrderAmount,
                        //                                Balance = walletBalance + refundingTotal
                        //                            };

                        //                            await _customerService.CreateWalletTransaction(walletTransaction);
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion
                    }

                    await dbContextTransaction.CommitAsync();
                    updated = true;
                }
                catch
                {
                    await dbContextTransaction.RollbackAsync();
                }
            }

            return updated;
        }
        protected async Task RefundFullSubscriptionAmount(Subscription subscription, SubscriptionOrder subscriptionOrder, bool refundDeliveryFee)
        {
            var coupon = subscription.Coupon;
            if (coupon != null)
            {
                coupon.QuantityUsed--;
                await _couponService.UpdateCoupon(coupon);
            }

            if (subscriptionOrder.PaymentMethodId == (int)PaymentMethod.Wallet)
            {
                decimal refundingWalletUsedAmount = subscription.WalletUsedAmount;
                if (!refundDeliveryFee)
                    refundingWalletUsedAmount -= subscription.DeliveryFee;

                var walletBalance = await _customerService.GetWalletBalanceByCustomerId(subscription.CustomerId, WalletType.Wallet);
                var walletTransaction = new WalletTransaction
                {
                    TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                    CreatedBy = subscription.CustomerId,
                    CustomerId = subscription.CustomerId,
                    WalletTypeId = WalletType.Wallet,
                    RelatedEntityTypeId = RelatedEntityType.Subscription,
                    RelatedEntityId = subscription.Id,
                    Credit = refundingWalletUsedAmount,
                    Debit = 0,
                    WalletTransactionTypeId = WalletTransactionType.RefundWalletAmount,
                    Balance = walletBalance + refundingWalletUsedAmount
                };

                await _customerService.CreateWalletTransaction(walletTransaction);
            }
            else if (subscriptionOrder.PaymentMethodId == (int)PaymentMethod.KNET || subscriptionOrder.PaymentMethodId == (int)PaymentMethod.VISAMASTER ||
                 subscriptionOrder.PaymentMethodId == (int)PaymentMethod.Tabby)
            {
                WalletTransaction walletTransaction;
                decimal refundingTotal = subscription.Total;
                decimal refundingWalletUsedAmount = subscription.WalletUsedAmount;
                if (!refundDeliveryFee)
                {
                    if (subscription.DeliveryFee > refundingTotal)
                    {
                        refundingTotal = 0;
                        refundingWalletUsedAmount = refundingWalletUsedAmount - subscription.DeliveryFee + refundingTotal;
                    }
                    else if (subscription.DeliveryFee == refundingTotal)
                    {
                        refundingTotal = 0;
                    }
                    else
                    {
                        refundingTotal -= subscription.DeliveryFee;
                    }
                }

                if (refundingTotal > 0)
                {
                    var walletBalance = await _customerService.GetWalletBalanceByCustomerId(subscription.CustomerId, WalletType.Wallet);
                    walletTransaction = new WalletTransaction
                    {
                        TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                        CreatedBy = subscription.CustomerId,
                        CustomerId = subscription.CustomerId,
                        WalletTypeId = WalletType.Wallet,
                        RelatedEntityTypeId = RelatedEntityType.Subscription,
                        RelatedEntityId = subscription.Id,
                        Credit = refundingTotal,
                        Debit = 0,
                        WalletTransactionTypeId = WalletTransactionType.RefundOrderAmount,
                        Balance = walletBalance + refundingTotal
                    };

                    await _customerService.CreateWalletTransaction(walletTransaction);
                }

                if (refundingWalletUsedAmount > 0)
                {
                    var walletBalance = await _customerService.GetWalletBalanceByCustomerId(subscription.CustomerId, WalletType.Wallet);
                    walletTransaction = new WalletTransaction
                    {
                        TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                        CreatedBy = subscription.CustomerId,
                        CustomerId = subscription.CustomerId,
                        WalletTypeId = WalletType.Wallet,
                        RelatedEntityTypeId = RelatedEntityType.Subscription,
                        RelatedEntityId = subscription.Id,
                        Credit = refundingWalletUsedAmount,
                        Debit = 0,
                        WalletTransactionTypeId = WalletTransactionType.RefundWalletAmount,
                        Balance = walletBalance + refundingWalletUsedAmount
                    };

                    await _customerService.CreateWalletTransaction(walletTransaction);
                }
            }

            if (subscription.ReceivedCashbackAmount > 0)
            {
                var walletTransaction = new WalletTransaction
                {
                    TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                    CreatedBy = subscription.CustomerId,
                    CustomerId = subscription.CustomerId,
                    WalletTypeId = WalletType.Cashback,
                    RelatedEntityTypeId = RelatedEntityType.Subscription,
                    RelatedEntityId = subscription.Id,
                    Credit = 0,
                    Debit = subscription.ReceivedCashbackAmount,
                    WalletTransactionTypeId = WalletTransactionType.RefundCashbackOnPurchase
                };

                await _customerService.CreateWalletTransaction(walletTransaction);
            }
        }
        #endregion

        #region Cart
        public async Task MigrateCarts(string customerGuidValue, int customerId, bool clearexistingCartItems = false)
        {
            var cartItems = await _cartService.GetAllCartItem(customerGuidValue: customerGuidValue);
            var existingCartItems = await _cartService.GetAllCartItem(customerId: customerId);
            if (clearexistingCartItems)
            {
                await _cartService.DeleteCartItems(existingCartItems);
                existingCartItems = new List<CartItem>();
            }

            foreach (var cartItem in cartItems)
            {
                var existingCart = existingCartItems.Where(a => a.ProductId == cartItem.ProductId).FirstOrDefault();
                if (existingCart != null)
                {
                    existingCart.CustomerGuidValue = customerGuidValue;
                    existingCart.Quantity = existingCart.Quantity + cartItem.Quantity;
                    existingCart.ModifiedOn = DateTime.Now;
                    await _cartService.UpdateCartItem(existingCart);

                    await _cartService.DeleteCartItem(cartItem);
                }
                else
                {
                    cartItem.CustomerGuidValue = null;
                    cartItem.CustomerId = customerId;
                    cartItem.ModifiedOn = DateTime.Now;
                    await _cartService.UpdateCartItem(cartItem);
                }
            }
        }
        #endregion

        #region Notifications
        public async Task SendOrderSMSNotification(OrderModel orderModel, bool isEnglish)
        {
            NotificationType notificationType;
            if (orderModel.PaymentStatusId == (int)PaymentStatus.Captured || orderModel.PaymentStatusId == (int)PaymentStatus.PendingCash)
            {
                notificationType = NotificationType.OrderReceipt;
            }
            else
            {
                notificationType = NotificationType.OrderCancelled;
            }

            var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(notificationType);
            if (notificationTemplate != null && notificationTemplate.SMSEnabled)
            {
                string url = _appSettings.WebsiteUrl + "Order/" + orderModel.OrderNumber;
                url = url.Replace(" ", "%20");

                var messageEn = notificationTemplate.SMSMessageEn.Replace("{ordernumber}", orderModel.OrderNumber).Replace("{link}", url);
                var messageAr = notificationTemplate.SMSMessageAr.Replace("{ordernumber}", orderModel.OrderNumber).Replace("{link}", url);

                var message = isEnglish ? messageEn : messageAr;
                await _notificationTemplateService.CreateSMSPush(message, orderModel.Customer.MobileNumber, isEnglish ? 0 : 1);
            }
        }
        public async Task SendSubscriptionSMSNotification(SubscriptionModel subscriptionModel, bool isEnglish)
        {
            NotificationType notificationType;
            if (subscriptionModel.PaymentStatusId == (int)PaymentStatus.Captured || subscriptionModel.PaymentStatusId == (int)PaymentStatus.PendingCash)
            {
                notificationType = NotificationType.OrderReceipt;
            }
            else
            {
                notificationType = NotificationType.OrderCancelled;
            }

            var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(notificationType);
            if (notificationTemplate != null && notificationTemplate.SMSEnabled)
            {
                string url = _appSettings.WebsiteUrl + "Subscription/" + subscriptionModel.SubscriptionNumber;
                url = url.Replace(" ", "%20");

                var messageEn = notificationTemplate.SMSMessageEn.Replace("{subscriptionnumber}", subscriptionModel.SubscriptionNumber).Replace("{link}", url);
                var messageAr = notificationTemplate.SMSMessageAr.Replace("{subscriptionnumber}", subscriptionModel.SubscriptionNumber).Replace("{link}", url);

                var message = isEnglish ? messageEn : messageAr;
                await _notificationTemplateService.CreateSMSPush(message, subscriptionModel.Customer.MobileNumber, isEnglish ? 0 : 1);
            }
        }
        public async Task SendOrderEmailNotification(OrderModel orderModel, bool isEnglish)
        {
            NotificationType notificationType;
            if (orderModel.PaymentStatusId == (int)PaymentStatus.Captured || orderModel.PaymentStatusId == (int)PaymentStatus.PendingCash)
            {
                notificationType = NotificationType.OrderReceipt;
            }
            else
            {
                notificationType = NotificationType.OrderCancelled;
            }

            var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(notificationType);
            if (notificationTemplate != null && notificationTemplate.EmailEnabled)
            {
                string subject = isEnglish ? notificationTemplate.EmailSubjectEn : notificationTemplate.EmailSubjectAr;
                string orderHtml = isEnglish ? notificationTemplate.EmailMessageEn : notificationTemplate.EmailMessageAr;

                orderHtml = orderHtml.Replace("{Base-Url}", _appSettings.APIBaseUrl);
                orderHtml = orderHtml.Replace("{Logo-Image}", _appSettings.APIBaseUrl + "images/logo.png");
                orderHtml = orderHtml.Replace("{Body-Style}", isEnglish ? "direction: ltr;" : "direction: rtl;");
               
                if (notificationType == NotificationType.OrderReceipt)
                {
                    orderHtml = orderHtml.Replace("{Order-Confirmation}", isEnglish ? OrderPDF.OrderConfirmationTitle : OrderPDFAr.OrderConfirmationTitle);
                    orderHtml = orderHtml.Replace("{Description}", isEnglish ? OrderPDF.OrderEmailDescription : OrderPDFAr.OrderEmailDescription);
                }
                else
                {
                    orderHtml = orderHtml.Replace("{Order-Confirmation}", isEnglish ? OrderPDF.OrderCancellationTitle : OrderPDFAr.OrderCancellationTitle);
                    orderHtml = orderHtml.Replace("{Description}", isEnglish ? OrderPDF.OrderCancellationEmailDescription : OrderPDFAr.OrderCancellationEmailDescription);
                }

                orderHtml = orderHtml.Replace("{Customer-Name-Header}", orderModel.Customer.Name);              
                orderHtml = orderHtml.Replace("{Order-Details}", isEnglish ? OrderPDF.OrderDetails : OrderPDFAr.OrderDetails);
                orderHtml = orderHtml.Replace("{Order-Number}", isEnglish ? OrderPDF.OrderNumber : OrderPDFAr.OrderNumber);
                orderHtml = orderHtml.Replace("{Order-Number-Value}", orderModel.OrderNumber);
                orderHtml = orderHtml.Replace("{Transaction-Date}", isEnglish ? OrderPDF.TransactionDate : OrderPDFAr.TransactionDate);
                orderHtml = orderHtml.Replace("{Transaction-Date-Value}", orderModel.FormattedDate + " " + orderModel.FormattedTime);

                orderHtml = orderHtml.Replace("{Sub-Total}", isEnglish ? OrderPDF.SubTotal : OrderPDFAr.SubTotal);
                orderHtml = orderHtml.Replace("{Sub-Total-Value}", orderModel.FormattedSubTotal);

                orderHtml = orderHtml.Replace("{Delivery-Charges-Style}", !string.IsNullOrEmpty(orderModel.FormattedDeliveryFee) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Delivery-Charges}", isEnglish ? OrderPDF.DeliveryCharges : OrderPDFAr.DeliveryCharges);
                orderHtml = orderHtml.Replace("{Delivery-Charges-Value}", orderModel.FormattedDeliveryFee);

                orderHtml = orderHtml.Replace("{Coupon-Amount-Style}", !string.IsNullOrEmpty(orderModel.FormattedCouponDiscountAmount) ? "" : "display:none");
                orderHtml = orderHtml.Replace("{Coupon-Amount}", isEnglish ? OrderPDF.DiscountAmount : OrderPDFAr.DiscountAmount);
                orderHtml = orderHtml.Replace("{Coupon-Amount-Value}", orderModel.FormattedCouponDiscountAmount);

                orderHtml = orderHtml.Replace("{Cashback-Amount-Style}", !string.IsNullOrEmpty(orderModel.FormattedCashbackAmount) ? "" : "display:none");
                orderHtml = orderHtml.Replace("{Cashback-Amount}", isEnglish ? OrderPDF.Cashback : OrderPDFAr.Cashback);
                orderHtml = orderHtml.Replace("{Cashback-Amount-Value}", orderModel.FormattedCashbackAmount);

                orderHtml = orderHtml.Replace("{Wallet-Amount-Style}", !string.IsNullOrEmpty(orderModel.FormattedWalletUsedAmount) ? "" : "display:none");
                orderHtml = orderHtml.Replace("{Wallet-Amount}", isEnglish ? OrderPDF.WalletAmount : OrderPDFAr.WalletAmount);
                orderHtml = orderHtml.Replace("{Wallet-Amount-Value}", orderModel.FormattedWalletUsedAmount);

                orderHtml = orderHtml.Replace("{Grand-Total}", isEnglish ? OrderPDF.GrandTotal : OrderPDFAr.GrandTotal);
                orderHtml = orderHtml.Replace("{Grand-Total-Value}", orderModel.FormattedTotal);

                orderHtml = orderHtml.Replace("{Status}", isEnglish ? OrderPDF.Status : OrderPDFAr.Status);
                orderHtml = orderHtml.Replace("{Status-Value}", orderModel.OrderStatusName);
                orderHtml = orderHtml.Replace("{Items-Details}", isEnglish ? OrderPDF.ItemsDetails : OrderPDFAr.ItemsDetails);
                orderHtml = orderHtml.Replace("{Name}", isEnglish ? OrderPDF.Name : OrderPDFAr.Name);
                orderHtml = orderHtml.Replace("{Price}", isEnglish ? OrderPDF.Price : OrderPDFAr.Price);
                orderHtml = orderHtml.Replace("{Quantity}", isEnglish ? OrderPDF.Quantity : OrderPDFAr.Quantity);
                orderHtml = orderHtml.Replace("{Total-Amount}", isEnglish ? OrderPDF.TotalAmount : OrderPDFAr.TotalAmount);

                var items = string.Empty;
                foreach (var item in orderModel.OrderItems)
                {
                    items = items + @"<tr>      
                                            <td>" + item.Product.Title + @"</td>
                                            <td>" + item.FormattedUnitPrice + @"</td>
                                            <td>" + item.Quantity + @"</td>
                                            <td>" + item.FormattedTotal + @"</td>
                                          </tr>";
                }
                orderHtml = orderHtml.Replace("{Order-Items-Value}", items);

                orderHtml = orderHtml.Replace("{Payment-Details}", isEnglish ? OrderPDF.PaymentDetails : OrderPDFAr.PaymentDetails);
                orderHtml = orderHtml.Replace("{Payment-Method}", isEnglish ? OrderPDF.PaymentMethod : OrderPDFAr.PaymentMethod);
                orderHtml = orderHtml.Replace("{Payment-Method-Value}", orderModel.PaymentMethod.Name);
                orderHtml = orderHtml.Replace("{Payment-Result}", isEnglish ? OrderPDF.PaymentResult : OrderPDFAr.PaymentResult);
                orderHtml = orderHtml.Replace("{Payment-Result-Value}", orderModel.PaymentResult);
                orderHtml = orderHtml.Replace("{Payment-ID-Style}", !string.IsNullOrEmpty(orderModel.PaymentId) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Payment-ID}", isEnglish ? OrderPDF.PaymentID : OrderPDFAr.PaymentID);
                orderHtml = orderHtml.Replace("{Payment-ID-Value}", orderModel.PaymentId);
                orderHtml = orderHtml.Replace("{Payment-Reference-Style}", !string.IsNullOrEmpty(orderModel.PaymentRefId) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Payment-Reference}", isEnglish ? OrderPDF.PaymentReference : OrderPDFAr.PaymentReference);
                orderHtml = orderHtml.Replace("{Payment-Reference-Value}", orderModel.PaymentRefId);
                orderHtml = orderHtml.Replace("{Customer-Details}", isEnglish ? OrderPDF.CustomerDetails : OrderPDFAr.CustomerDetails);
                orderHtml = orderHtml.Replace("{Customer-Name-Style}", !string.IsNullOrEmpty(orderModel.Customer.Name) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Customer-Name}", isEnglish ? OrderPDF.CustomerName : OrderPDFAr.CustomerName);
                orderHtml = orderHtml.Replace("{Customer-Name-Value}", orderModel.Customer.Name);
                orderHtml = orderHtml.Replace("{Customer-Email-Style}", !string.IsNullOrEmpty(orderModel.Customer.EmailAddress) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Customer-Email}", isEnglish ? OrderPDF.CustomerEmail : OrderPDFAr.CustomerEmail);
                orderHtml = orderHtml.Replace("{Customer-Email-Value}", orderModel.Customer.EmailAddress);
                orderHtml = orderHtml.Replace("{Customer-Mobile}", isEnglish ? OrderPDF.CustomerMobile : OrderPDFAr.CustomerMobile);
                orderHtml = orderHtml.Replace("{Customer-Mobile-Value}", orderModel.Customer.MobileNumber);
                orderHtml = orderHtml.Replace("{Delivery-Details}", isEnglish ? OrderPDF.DeliveryDetails : OrderPDFAr.DeliveryDetails);
                orderHtml = orderHtml.Replace("{Estimated-Delivery}", isEnglish ? OrderPDF.EstimatedDelivery : OrderPDFAr.EstimatedDelivery);
                orderHtml = orderHtml.Replace("{Estimated-Delivery-Value}", orderModel.EstimatedDeliveryWithoutHeading);
                orderHtml = orderHtml.Replace("{Delivery-Address}", isEnglish ? OrderPDF.DeliveryAddress : OrderPDFAr.DeliveryAddress);
                orderHtml = orderHtml.Replace("{Delivery-Address-Value}", orderModel.Address.AddressText);
                orderHtml = orderHtml.Replace("{Footer-Value}", isEnglish ? OrderPDF.FooterValue : OrderPDFAr.FooterValue);

                string websiteUrl = _appSettings.WebsiteUrl.EndsWith("/") ? _appSettings.WebsiteUrl.Remove(_appSettings.WebsiteUrl.Length - 1, 1) : _appSettings.WebsiteUrl;
                orderHtml = orderHtml.Replace("{Footer-Website}", websiteUrl);

                await _emailHelper.SendEmail(emailIds: orderModel.Customer.EmailAddress, subject: subject + " - " + orderModel.OrderNumber, emailBody: orderHtml, htmlContent: true);
            }
        }
        public async Task SendSubscriptionEmailNotification(SubscriptionModel subscriptionModel, bool isEnglish)
        {
            NotificationType notificationType;
            if (subscriptionModel.PaymentStatusId == (int)PaymentStatus.Captured || subscriptionModel.PaymentStatusId == (int)PaymentStatus.PendingCash)
            {
                notificationType = NotificationType.OrderReceipt;
            }
            else
            {
                notificationType = NotificationType.OrderCancelled;
            }

            var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(notificationType);
            if (notificationTemplate != null && notificationTemplate.EmailEnabled)
            {
                string subject = isEnglish ? notificationTemplate.EmailSubjectEn : notificationTemplate.EmailSubjectAr;
                string orderHtml = isEnglish ? notificationTemplate.EmailMessageEn : notificationTemplate.EmailMessageAr;

                orderHtml = orderHtml.Replace("{Base-Url}", _appSettings.APIBaseUrl);
                orderHtml = orderHtml.Replace("{Logo-Image}", _appSettings.APIBaseUrl + "images/logo.png");
                orderHtml = orderHtml.Replace("{Body-Style}", isEnglish ? "direction: ltr;" : "direction: rtl;");

                if (notificationType == NotificationType.OrderReceipt)
                {
                    orderHtml = orderHtml.Replace("{Order-Confirmation}", isEnglish ? OrderPDF.OrderConfirmationTitle : OrderPDFAr.OrderConfirmationTitle);
                    orderHtml = orderHtml.Replace("{Description}", isEnglish ? OrderPDF.OrderEmailDescription : OrderPDFAr.OrderEmailDescription);
                }
                else
                {
                    orderHtml = orderHtml.Replace("{Order-Confirmation}", isEnglish ? OrderPDF.OrderCancellationTitle : OrderPDFAr.OrderCancellationTitle);
                    orderHtml = orderHtml.Replace("{Description}", isEnglish ? OrderPDF.OrderCancellationEmailDescription : OrderPDFAr.OrderCancellationEmailDescription);
                }
                
                orderHtml = orderHtml.Replace("{Customer-Name-Header}", subscriptionModel.Customer.Name);             
                orderHtml = orderHtml.Replace("{Order-Details}", isEnglish ? OrderPDF.OrderDetails : OrderPDFAr.OrderDetails);
                orderHtml = orderHtml.Replace("{Order-Number}", isEnglish ? OrderPDF.OrderNumber : OrderPDFAr.OrderNumber);
                orderHtml = orderHtml.Replace("{Order-Number-Value}", subscriptionModel.SubscriptionNumber);
                orderHtml = orderHtml.Replace("{Transaction-Date}", isEnglish ? OrderPDF.TransactionDate : OrderPDFAr.TransactionDate);
                orderHtml = orderHtml.Replace("{Transaction-Date-Value}", subscriptionModel.FormattedDate + " " + subscriptionModel.FormattedTime);
                orderHtml = orderHtml.Replace("{Quantity}", isEnglish ? OrderPDF.Quantity : OrderPDFAr.Quantity);
                orderHtml = orderHtml.Replace("{Quantity-Value}", subscriptionModel.Quantity.ToString());
                orderHtml = orderHtml.Replace("{Duration}", isEnglish ? OrderPDF.Duration : OrderPDFAr.Duration);
                orderHtml = orderHtml.Replace("{Duration-Value}", subscriptionModel.Duration);
                orderHtml = orderHtml.Replace("{Delivery-Day}", isEnglish ? OrderPDF.DeliveryDay : OrderPDFAr.DeliveryDay);
                orderHtml = orderHtml.Replace("{Delivery-Day-Value}", subscriptionModel.DeliveryDate);

                orderHtml = orderHtml.Replace("{Sub-Total}", isEnglish ? OrderPDF.SubTotal : OrderPDFAr.SubTotal);
                orderHtml = orderHtml.Replace("{Sub-Total-Value}", subscriptionModel.FormattedSubTotal);

                orderHtml = orderHtml.Replace("{Delivery-Charges-Style}", !string.IsNullOrEmpty(subscriptionModel.FormattedDeliveryFee) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Delivery-Charges}", isEnglish ? OrderPDF.DeliveryCharges : OrderPDFAr.DeliveryCharges);
                orderHtml = orderHtml.Replace("{Delivery-Charges-Value}", subscriptionModel.FormattedDeliveryFee);

                orderHtml = orderHtml.Replace("{Coupon-Amount-Style}", !string.IsNullOrEmpty(subscriptionModel.FormattedCouponDiscountAmount) ? "" : "display:none");
                orderHtml = orderHtml.Replace("{Coupon-Amount}", isEnglish ? OrderPDF.DiscountAmount : OrderPDFAr.DiscountAmount);
                orderHtml = orderHtml.Replace("{Coupon-Amount-Value}", subscriptionModel.FormattedCouponDiscountAmount);

                orderHtml = orderHtml.Replace("{Cashback-Amount-Style}", !string.IsNullOrEmpty(subscriptionModel.FormattedCashbackAmount) ? "" : "display:none");
                orderHtml = orderHtml.Replace("{Cashback-Amount}", isEnglish ? OrderPDF.Cashback : OrderPDFAr.Cashback);
                orderHtml = orderHtml.Replace("{Cashback-Amount-Value}", subscriptionModel.FormattedCashbackAmount);

                orderHtml = orderHtml.Replace("{Wallet-Amount-Style}", !string.IsNullOrEmpty(subscriptionModel.FormattedWalletUsedAmount) ? "" : "display:none");
                orderHtml = orderHtml.Replace("{Wallet-Amount}", isEnglish ? OrderPDF.WalletAmount : OrderPDFAr.WalletAmount);
                orderHtml = orderHtml.Replace("{Wallet-Amount-Value}", subscriptionModel.FormattedWalletUsedAmount);

                orderHtml = orderHtml.Replace("{Grand-Total}", isEnglish ? OrderPDF.GrandTotal : OrderPDFAr.GrandTotal);
                orderHtml = orderHtml.Replace("{Grand-Total-Value}", subscriptionModel.FormattedTotal);

                orderHtml = orderHtml.Replace("{Status}", isEnglish ? OrderPDF.Status : OrderPDFAr.Status);
                orderHtml = orderHtml.Replace("{Status-Value}", subscriptionModel.SubscriptionStatusName);
                orderHtml = orderHtml.Replace("{Items-Details}", isEnglish ? OrderPDF.ItemsDetails : OrderPDFAr.ItemsDetails);
                orderHtml = orderHtml.Replace("{Name}", isEnglish ? OrderPDF.Name : OrderPDFAr.Name);
                orderHtml = orderHtml.Replace("{Price}", isEnglish ? OrderPDF.Price : OrderPDFAr.Price);
                orderHtml = orderHtml.Replace("{Quantity}", isEnglish ? OrderPDF.Quantity : OrderPDFAr.Quantity);
                orderHtml = orderHtml.Replace("{Total-Amount}", isEnglish ? OrderPDF.TotalAmount : OrderPDFAr.TotalAmount);

                var items = string.Empty;
                foreach (var item in subscriptionModel.SubscriptionPackTitles)
                {
                    items = items + @"<tr>      
                                            <td>" + item.Title + @"</td>                                            
                                          </tr>";
                }
                orderHtml = orderHtml.Replace("{Order-Items-Value}", items);

                orderHtml = orderHtml.Replace("{Payment-Details}", isEnglish ? OrderPDF.PaymentDetails : OrderPDFAr.PaymentDetails);
                orderHtml = orderHtml.Replace("{Payment-Method}", isEnglish ? OrderPDF.PaymentMethod : OrderPDFAr.PaymentMethod);
                orderHtml = orderHtml.Replace("{Payment-Method-Value}", subscriptionModel.PaymentMethod.Name);
                orderHtml = orderHtml.Replace("{Payment-Result}", isEnglish ? OrderPDF.PaymentResult : OrderPDFAr.PaymentResult);
                orderHtml = orderHtml.Replace("{Payment-Result-Value}", subscriptionModel.PaymentResult);
                orderHtml = orderHtml.Replace("{Payment-ID-Style}", !string.IsNullOrEmpty(subscriptionModel.PaymentId) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Payment-ID}", isEnglish ? OrderPDF.PaymentID : OrderPDFAr.PaymentID);
                orderHtml = orderHtml.Replace("{Payment-ID-Value}", subscriptionModel.PaymentId);
                orderHtml = orderHtml.Replace("{Payment-Reference-Style}", !string.IsNullOrEmpty(subscriptionModel.PaymentRefId) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Payment-Reference}", isEnglish ? OrderPDF.PaymentReference : OrderPDFAr.PaymentReference);
                orderHtml = orderHtml.Replace("{Payment-Reference-Value}", subscriptionModel.PaymentRefId);
                orderHtml = orderHtml.Replace("{Customer-Details}", isEnglish ? OrderPDF.CustomerDetails : OrderPDFAr.CustomerDetails);
                orderHtml = orderHtml.Replace("{Customer-Name-Style}", !string.IsNullOrEmpty(subscriptionModel.Customer.Name) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Customer-Name}", isEnglish ? OrderPDF.CustomerName : OrderPDFAr.CustomerName);
                orderHtml = orderHtml.Replace("{Customer-Name-Value}", subscriptionModel.Customer.Name);
                orderHtml = orderHtml.Replace("{Customer-Email-Style}", !string.IsNullOrEmpty(subscriptionModel.Customer.EmailAddress) ? "" : "display:none;");
                orderHtml = orderHtml.Replace("{Customer-Email}", isEnglish ? OrderPDF.CustomerEmail : OrderPDFAr.CustomerEmail);
                orderHtml = orderHtml.Replace("{Customer-Email-Value}", subscriptionModel.Customer.EmailAddress);
                orderHtml = orderHtml.Replace("{Customer-Mobile}", isEnglish ? OrderPDF.CustomerMobile : OrderPDFAr.CustomerMobile);
                orderHtml = orderHtml.Replace("{Customer-Mobile-Value}", subscriptionModel.Customer.MobileNumber);
                orderHtml = orderHtml.Replace("{Delivery-Details}", isEnglish ? OrderPDF.DeliveryDetails : OrderPDFAr.DeliveryDetails);
                orderHtml = orderHtml.Replace("{Delivery-Address}", isEnglish ? OrderPDF.DeliveryAddress : OrderPDFAr.DeliveryAddress);
                orderHtml = orderHtml.Replace("{Delivery-Address-Value}", subscriptionModel.Address.AddressText);
                orderHtml = orderHtml.Replace("{Footer-Value}", isEnglish ? OrderPDF.FooterValue : OrderPDFAr.FooterValue);
                string websiteUrl = _appSettings.WebsiteUrl.EndsWith("/") ? _appSettings.WebsiteUrl.Remove(_appSettings.WebsiteUrl.Length - 1, 1) : _appSettings.WebsiteUrl;
                orderHtml = orderHtml.Replace("{Footer-Website}", websiteUrl);

                await _emailHelper.SendEmail(emailIds: subscriptionModel.Customer.EmailAddress, subject: subject + " - " + subscriptionModel.SubscriptionNumber, emailBody: orderHtml, htmlContent: true);
            }
        }
        #endregion
    }
}
