using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Frontend.Content.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.Content
{
    public class PaymentMethodService : IPaymentMethodService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public PaymentMethodService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IList<Data.Content.PaymentMethod>> GetAllPaymentMethod(PaymentRequestType paymentRequestType)
        {
            var data = _dbcontext
                           .PaymentMethods
                           .Where(x => x.Deleted == false && x.Id != (int)PaymentMethod.Wallet);

            if (paymentRequestType == PaymentRequestType.Order)
            {
                data = data.Where(a => a.NormalCheckoutRegisteredCustomer);
            }
            else if (paymentRequestType == PaymentRequestType.SubscriptionOrder)
            {
                data = data.Where(a => a.SubscriptionCheckoutRegisteredCustomer);
            }
            else if (paymentRequestType == PaymentRequestType.WalletPackageOrder)
            {
                data = data.Where(a => a.ForWalletPackage);
            }
            else if (paymentRequestType == PaymentRequestType.QuickPay)
            {
                data = data.Where(a => a.ForQuickPay);
            }

            data = data.OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id);

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<Data.Content.PaymentMethod> GetPaymentMethodById(int id)
        {
            var data = await _dbcontext.PaymentMethods.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }
    }
}
