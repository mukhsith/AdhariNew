using Data.Common;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.SystemUserManagement
{
    public partial class SystemUserHistory : BaseEntityDate
    {
        [StringLength(Constants.SmallDataSize)] 
        public string Name { get; set; }

        [StringLength(Constants.ExtraLargeDataSize)]
        public string Description { get; set; }
        public int UserId { get; set; } = 0;
    }
}
