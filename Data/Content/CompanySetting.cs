using Data.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Helpers;

namespace Data.Content
{
    public partial class CompanySetting : BaseEntityDate
    {
        [Column(TypeName = Constants.AmountDataType)]
        public decimal IOSVersion { get; set; }
        public string AppStoreLink { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal AndroidVersion { get; set; }
        public string PlayStoreLink { get; set; }
    }
}
