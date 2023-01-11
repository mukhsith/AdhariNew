using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.Delivery
{
    public class DeliveriesDashboard
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }

        public string SubscriptionNumber { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime DeliveryDate { get; set; }
        public PaymentStatus PaymentStatusId { get; set; }
        public OrderType OrderTypeId { get; set; }

        public OrderMode OrderModeID { get; set; }
        
        public OrderStatus OrderStatusId { get; set; }
        public int? DriverId { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
        public bool Deleted { get; set; }
        public int AddressAreadId { get; set; }
        public string OrderTypeName { get; set; }

        public string OrderModeName { get; set; }
        public string DriverName{ get; set; }
        public string AreaName{ get; set; }
        public string FormattedTotal{ get; set; }
        public string FormattedDeliveryFee{ get; set; }
        public int CustomerId { get; set; }
        public int SubscriptionID { get; set; }

        public int SubscriptionID2 { get; set; }
    }



}
