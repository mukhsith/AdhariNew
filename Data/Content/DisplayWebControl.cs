using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Data.Content
{
    public class DisplayWebControl 
    {
        [Key]
        public  int Id { get; set; }
        public int ControlId { get; set; }
        public bool Active { get; set; } = false;
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        [StringLength(Constants.ExtraLargeDataSize)]
        public string Description { get; set; }
       
        
    }
}
