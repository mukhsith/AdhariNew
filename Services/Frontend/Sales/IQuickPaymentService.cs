using Data.Sales;
using System.Threading.Tasks;

namespace Services.Frontend.Sales
{
    public interface IQuickPaymentService
    {
        Task<QuickPayment> GetQuickPaymentById(int id);
        Task<QuickPayment> GetQuickPaymentByPaymentNumber(string paymentNumber);
        Task<bool> UpdateQuickPayment(QuickPayment model);
    }
}
