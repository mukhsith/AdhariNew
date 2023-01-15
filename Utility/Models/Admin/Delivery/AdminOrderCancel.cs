using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.Delivery
{
    public class AdminOrderCancel
    {
        public int OrderId { get; set; }
        public int OrderTypeId { get; set; }
        public OrderStatus OrderStatusId { get; set; }
        public bool RefundDeliveryFee { get; set; }
        public string Notes { get; set; }
    }
}
