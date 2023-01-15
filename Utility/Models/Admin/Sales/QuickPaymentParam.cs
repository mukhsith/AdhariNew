using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.API;

namespace Utility.Models.Admin.Sales
{
    public class QuickPaymentParam
    {
        public DataTableParam DataTableParam { get; set; }
        public int SelectedTab { get; set; } = 0; //paid
        public int? PaymentLinkId { get; set; }
        public string CustomerMobile { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public int? PaymentMethodId { get; set; } = null;
    }
}
