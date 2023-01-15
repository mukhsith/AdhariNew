using Data.Common;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.SystemUserManagement
{
    public partial class SystemUserRole : BaseEntityCommon
    {
        [StringLength(Constants.SmallDataSize)]
        public string Name { get; set; }
    }
}
