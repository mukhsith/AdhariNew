using Data.Content;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.Content
{
    public interface ISiteContentService  
    {
        Task<SiteContent> GetByType(AppContentType appContentType);        
    }
}
