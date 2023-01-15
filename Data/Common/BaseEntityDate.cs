using System;

namespace Data.Common
{
    public partial class BaseEntityDate : BaseEntityId
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
