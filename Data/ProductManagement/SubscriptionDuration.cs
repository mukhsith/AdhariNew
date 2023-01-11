using Data.Common;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.ProductManagement
{
    public partial class SubscriptionDuration : BaseEntityCommon
    {
        [StringLength(Constants.MediumDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string NameAr { get; set; }
        public int NumberOfMonths { get; set; }
    }
}
