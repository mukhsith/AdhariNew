using Utility.Models.Base;

namespace Utility.Models.Frontend.Locations
{
    public partial class CountryModel : BaseModel
    { 
        public string Title { get; set; }               
        public bool Default { get; set; } 
        public string MobileCode { get; set; } 
        public string CurrencyCode  { get; set; } 
        public string MyFatoorahCurrencyCode { get; set; } 
        public string CurrencyFormat { get; set; } 
        public string CountryCode { get; set; }
        public string DeliveryText  { get; set; } 
        public decimal DeliveryFee { get; set; } 
        public string FlagIcon { get; set; }
    }
}
