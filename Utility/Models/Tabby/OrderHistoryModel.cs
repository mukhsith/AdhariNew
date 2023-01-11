using System;
using System.Collections.Generic;

namespace Utility.Models.Tabby
{
    public class OrderHistoryModel
    {
        public DateTime purchased_at { get; set; }
        public string amount { get; set; }
        public string payment_method { get; set; }
        public string status { get; set; }
        public BuyerModel buyer { get; set; }
        public ShippingAddressModel shipping_address { get; set; }
        public List<ItemModel> items { get; set; }
    }
}
