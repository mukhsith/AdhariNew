using System.Collections.Generic;

namespace Utility.Models.Tabby
{
    public class PaymentModel
    {
        public string id { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public BuyerModel buyer { get; set; }
        public ShippingAddressModel shipping_address { get; set; }
        public OrderModel order { get; set; }
        public BuyerHistoryModel buyer_history { get; set; }
        public List<OrderHistoryModel> order_history { get; set; }
        public MetaModel meta { get; set; }
        public AttachmentModel attachment { get; set; }
    }
}
