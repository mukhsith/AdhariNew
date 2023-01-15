using Data.Content;
using Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CustomizedModel;

namespace Services.Frontend.Content.Interface
{
    public interface ISiteContentService  
    {
        //Task<PageFooterContentModel> GetSiteContent(bool isEnglish);
        Task<IList<SiteContent>> GetAll();
        Task<SiteContent> GetByType(AppContentType appContentType);
        
    }
}
