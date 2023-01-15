using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models.Admin.Delivery
{
    public class AdminDriverModel
    {
        public int OrderId { get; set; }
        public int OrderTypeId { get; set; }
        public int  DriverId { get; set; }
        public string Notes { get; set; }
    }
}
