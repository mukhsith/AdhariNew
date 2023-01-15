using Data.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Services.Backend.Content.Interface
{
    public interface IPaymentMethodService
    {
        #region Banner Service 
        Task<IList<Data.Content.PaymentMethod>> GetAllPaymentMethod(PaymentRequestType paymentRequestType);
        Task<IEnumerable<Data.Content.PaymentMethod>> GetAll();
        Task<DataTableResult<List<Data.Content.PaymentMethod>>> GetAllForDataTable(DataTableParam param, string baseImageUrl);
        Task<Data.Content.PaymentMethod> GetById(int id);
        Task<Data.Content.PaymentMethod> Create(Data.Content.PaymentMethod model);
        Task<bool> Update(Data.Content.PaymentMethod model);
        //Task<bool> Delete(Data.Content.PaymentMethod model);
        Task<bool> ToggleNormalRegistered(int id);
        Task<bool> ToggleSubscriptionRegistered(int id);
        #endregion

        Task<IList<Data.Content.PaymentMethod>> GetAllPaymentMethod(RelatedEntityType relatedEntityType, bool guest);
        Task<Data.Content.PaymentMethod> GetPaymentMethodById(int id);
    }
}
