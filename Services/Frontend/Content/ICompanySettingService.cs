using Data.Content;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public interface ICompanySettingService
    {
        Task<CompanySetting> GetDefault();
    }
}
