using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.Delivery
{
    public class OrderRequestParam
    {
        /// <summary>
        /// generic model for all popup api request (UpdateOrder)
        /// </summary>
        public int OrderId { get; set; }
        public int OrderTypeId { get; set; }
        public OrderStatus OrderStatusId { get; set; }
        public bool RefundDeliveryFee = false;
        public string RescheduleDeliveryDate { get; set; }
        public string Notes { get; set; }
    }
}
