using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models.Admin.Delivery
{
    public class AdminDeliveryOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string OrderType { get; set; } = "Order";
        public string AreaName { get; set; }
        public decimal DeliveryFee { get; set; }
        public string FormattedDeliveryFee { get; set; }
        public decimal Total { get; set; }
        public string FormattedTotal { get; set; }
        public string DriverName { get; set; }
    }
}
