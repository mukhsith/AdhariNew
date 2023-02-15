namespace Utility.Models.Admin.Sales
{
    public class AdminCreateOrderModel : Frontend.Sales.CreatePaymentModel
    {
        public int CustomerId { get; set; }

    }
}
