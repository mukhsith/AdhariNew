using Data.EntityFramework;
using Data.Sales;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.Sales
{
    public class QuickPaymentService : IQuickPaymentService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public QuickPaymentService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<QuickPayment> GetQuickPaymentById(int id)
        {
            var data = await _dbcontext.QuickPayments
                .Include(a => a.PaymentMethod)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<QuickPayment> GetQuickPaymentByPaymentNumber(string paymentNumber)
        {
            var data = await _dbcontext.QuickPayments
                .Include(a => a.PaymentMethod)
                .Where(a => a.PaymentNumber == paymentNumber)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<bool> UpdateQuickPayment(QuickPayment model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
    }
}
