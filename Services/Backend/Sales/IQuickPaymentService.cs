using Data.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Admin.Sales;


namespace Services.Backend.Sales
{
    public interface IQuickPaymentService
    {
        Task<QuickPayment> Create(QuickPayment model);
        Task<dynamic> GetAllForDataTable(QuickPaymentParam param);

        Task<dynamic> GetAllForCustomDataTable(QuickPaymentParam param);

        Task<bool> Update(int id);
        Task<List<QuickPayment>> GetPaymentById(PaymentRequestType type, int entityId);
        Task<QuickPayment> GetqpayByNumber(string qpayNumber);
        //Task<QuickPayment> GetQuickPaymentByPaymentNumber(string paymentNumber);
        //Task<QuickPayment> CreateQuickPayment(QuickPayment model);
        //Task<bool> UpdateQuickPayment(QuickPayment model);
        //Task<bool> DeleteQuickPayment(QuickPayment model);
    }
}
