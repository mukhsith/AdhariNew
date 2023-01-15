using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models.Admin.CustomerManagement
{
    public class AdminSmallCustomerModel
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

    }
}
