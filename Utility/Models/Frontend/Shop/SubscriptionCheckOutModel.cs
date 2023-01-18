using System.Collections.Generic;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.Shop
{
    public class SubscriptionCheckOutModel
    {
        public string SubscriptionTitle { get; set; }
        public string AddressName { get; set; }
        public string AddressText { get; set; }
        public string Duration { get; set; }
        public string DeliveryDate { get; set; }
        public string Quantity { get; set; }
        public string FormattedTotal { get; set; }
        public CustomerModel Customer { get; set; }
        public ProductModel Product { get; set; }
        public SubscriptionSummaryModel SubscriptionSummary { get; set; } = new();
        public Content.PaymentMethodModel PaymentMethod { get; set; } = new();
        public List<KeyValuPairModel> SubscriptionPackTitles { get; set; } = new();
        public List<Content.PaymentMethodModel> PaymentMethods { get; set; } = new();
    }
}
