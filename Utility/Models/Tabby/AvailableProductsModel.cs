using System.Collections.Generic;

namespace Utility.Models.Tabby
{
    public class AvailableProductsModel
    {
        public List<InstallmentModel> installments { get; set; }
        public List<MonthlyBillingModel> monthly_billing { get; set; }
        public List<CreditCardInstallmentModel> credit_card_installments { get; set; }
    }
}
