using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models.Admin.CustomerManagement
{
    public class AdminSMSPushModel
    {
        public int CustomerId { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public int languageId { get; set; }
    }
}
