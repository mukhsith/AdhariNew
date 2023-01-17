using Data.Content;
using Data.EntityFramework;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public CustomerFeedbackService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<CustomerFeedback> Add(CustomerFeedback model)
        {
            var data = await _dbcontext.CustomerFeedbacks.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return data.Entity;
        }
    }
}
