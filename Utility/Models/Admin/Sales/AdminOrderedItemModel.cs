using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Utility.Models.Admin.Sales
{

    public class AdminOrderedItemParam
    {
        public bool IsEnglish { get; set; }
        public int SelectedTab { get; set; }
        public DataTableParam DatatableParam { get; set; }
        public string OrderNumber { get; set; } = null;
        public DateTime? startDate { get; set; } = null;
        public DateTime? endDate { get; set; } = null;
        public int? OrderModeId { get; set; } = null;
        public int? OrderTypeId { get; set; } = null;
        public int? AreaId { get; set; } = null;
        public int? DriverId { get; set; } = null;

        public OrderStatus OrderStatusID { get; set; }
    }

    public class AdminOrderedItemModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
