using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Utility.Models.Frontend.Content;
using Microsoft.Extensions.Options;

namespace Services.Frontend.Content.Interface
{
    public  class BannerService :  IBannerService
    {
        protected readonly AppSettingsModel _appSettings;
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public BannerService(ApplicationDbContext dbcontext, IOptions<AppSettingsModel> options)
        {
            _dbcontext = dbcontext;
            _appSettings = options.Value;
        }

        #region Banner 

        public async Task<IEnumerable<BannerModel>> GetAll(bool isEnglish)
        {
            var baseUrl = _appSettings.APIBaseUrl + _appSettings.ImageBanner;
            IEnumerable<BannerModel> items = await _dbcontext
                                           .Banners
                                           .Where(x => x.Deleted == false && x.Active == true)
                                           .Select(x => new BannerModel()
                                           {
                                               ImageUrl = baseUrl + (isEnglish ? x.ImageNameEn : x.ImageNameAr),
                                               LinkEnabled=x.LinkEnabled,
                                               LinkType=x.LinkType,
                                               LinkUrl=x.LinkUrl,
                                               ProductId=x.ProductId
                                               
                                           }) 
                                           .AsNoTracking()
                                           .ToListAsync();
            return items;
        }

         
        #endregion
       
    }
}
