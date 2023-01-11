using System.Collections.Generic;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;

namespace Utility.Models.Admin.Sales
{
    public class AdminOrderModel : Frontend.Sales.OrderModel
    { 
        public AdminOrderSummaryModel OrderSummary { get; set; }
    }
}
