using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Models.Frontend.Content;

namespace Services.Frontend.Content.Interface
{
    public interface IBannerService
    {
        #region Banner Service 
        Task<IEnumerable<BannerModel>> GetAll(bool isEnglish);
        #endregion
    }
}
