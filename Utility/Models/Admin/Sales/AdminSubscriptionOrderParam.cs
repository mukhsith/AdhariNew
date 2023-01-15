using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Utility.Models.Admin.Sales
{
    public class AdminSubscriptionOrderParam
    {
        public bool IsEnglish { get; set; }
        public int SelectedTab { get; set; } 
        public DataTableParam DatatableParam { get; set; }
        public int? CustomerId { get; set; } = null;
        public int? SubscriptionId { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public string CustomerName { get; set; } = null;
        public string CustomerMobile { get; set; } = null;
        public string CustomerEmail { get; set; } = null;
        public int? PaymentMethodId { get; set; } = null; 
        public int? SubscriptionStatusId { get; set; }
    }
}
