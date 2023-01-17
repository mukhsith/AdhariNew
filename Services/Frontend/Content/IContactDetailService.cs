using Data.Content;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public interface IContactDetailService
    {
        Task<ContactDetail> GetDefault();
    }     
}
