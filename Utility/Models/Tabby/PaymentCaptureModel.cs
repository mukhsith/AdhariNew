using System.Collections.Generic;

namespace Utility.Models.Tabby
{
    public class PaymentCaptureModel
    {
        public string id { get; set; }
        public string amount { get; set; }
        public string tax_amount { get; set; }
        public string shipping_amount { get; set; }
        public string discount_amount { get; set; }
        public string created_at { get; set; }
        public List<ItemModel> items { get; set; }
    }
}
