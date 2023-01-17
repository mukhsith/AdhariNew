using Data.Content;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public interface ICustomerFeedbackService
    {
        Task<CustomerFeedback> Add(CustomerFeedback model);
    }
}
