using System;

namespace Utility.Models.Tabby
{
    public class BuyerHistoryModel
    {
        public DateTime registered_since { get; set; }
        public int loyalty_level { get; set; }
        public int wishlist_count { get; set; }
        public bool is_social_networks_connected { get; set; }
        public bool is_phone_number_verified { get; set; }
        public bool is_email_verified { get; set; }
    }
}
