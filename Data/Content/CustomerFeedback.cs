using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Data.Content
{
    public class CustomerFeedback : BaseEntityDate
    {
        [StringLength(Constants.NameDataSize)]
        public string Name { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string MobileNumber { get; set; }

        [StringLength(Constants.EmailDataSize)]
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }

        [NotMapped]
        public virtual int PendingNotificationBadgeCount {get;set;}
         
    }
}
