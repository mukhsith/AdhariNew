using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Utility.Models.Admin.Sales
{
    public class AdminOrderDeliveriesParam
    {
        public bool IsEnglish { get; set; }
        public int SelectedTab { get; set; }
        public DataTableParam DatatableParam { get; set; }
        public string OrderNumber { get; set; } = null;
        public DateTime? DeliveryDate { get; set; } = null;
        public int? OrderModeId { get; set; } = null;
        public int? OrderTypeId { get; set; } = null;
        public int? AreaId { get; set; } = null;
        public int? DriverId { get; set; } = null;

        public OrderStatus OrderStatusID { get; set; }
    }
}
