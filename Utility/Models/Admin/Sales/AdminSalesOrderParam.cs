using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Utility.Models.Admin.Sales
{
    public class AdminSalesOrderParam
    {
        public int SelectedTab { get; set; } 
        public DataTableParam DatatableParam { get; set; }
        public int? CustomerId { get; set; } = null;
        public string OrderNumber { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public string CustomerName { get; set; } = null;
        public string MobileNumber { get; set; } = null;
        public string CustomerEmail { get; set; } = null;
        public int? PaymentMethodId { get; set; } = null;
        public int? OrderTypeId { get; set; } = null; 
        public int? OrderStatusId { get; set; }
    }
}
