using Data.Common;
using Data.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Helpers;

namespace Data.DeliveryManagement
{
    public class DeliveryBlockedDate// : BaseEntityCommon
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Note { get; set; }
        ///// <summary>
        ///// User name who blocked the date
        ///// </summary>
        // public int? BlockedBy { get; set; }
        //[ForeignKey("BlockedBy")]
        //public virtual SystemUser BlockedByUser { get; set; }
        //[NotMapped]
        //public string BlockedByUserName { get; set; }
        //public DateTime? BlockedOn { get; set; }

        public  int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")] 
        public virtual SystemUser CreatedByUser { get; set; }
        [NotMapped]
        public string CreatedByUserName { get; set; }

        public  int? ModifiedBy { get; set; }
        [ForeignKey("ModifiedBy")]
        public virtual SystemUser ModifiedByUser { get; set; }
        [NotMapped]
        public string ModifiedByUserName { get; set; }

        //public DateTime CreatedOn { get; set; } = DateTime.Now;
        //public int? ModifiedBy { get; set; }

        public int DisplayOrder { get; set; } = 0;
        public bool Active { get; set; } = false;

        //public int  CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
      //  public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Deleted { get; set; } = false;

    }
}
