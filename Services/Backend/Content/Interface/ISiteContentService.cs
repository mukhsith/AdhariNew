
using Data.Content;
using Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Backend.Content.Interface
{
    public interface ISiteContentService : IRepository<SiteContent>
    {
        Task<IList<SiteContent>> GetAll();
        Task<SiteContent> GetByType(AppContentType appContentType);
        
    }
}
