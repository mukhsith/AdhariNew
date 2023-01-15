namespace Utility.Models.MyFatoorah
{
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDirectPayment { get; set; }
        public string ImageUrl { get; set; }
    }
}
