using System;
using System.Collections.Generic;

namespace Utility.Models.Tabby
{
    public class OrderModel
    {
        public string tax_amount { get; set; }
        public string shipping_amount { get; set; }
        public string discount_amount { get; set; }
        public DateTime updated_at { get; set; }
        public string reference_id { get; set; }
        public List<ItemModel> items { get; set; }
    }
}
