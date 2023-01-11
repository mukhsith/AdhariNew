namespace Utility.Models.Frontend.CouponPromotion
{
    public class WalletPackageModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string FormattedAmount { get; set; }
    }
}
