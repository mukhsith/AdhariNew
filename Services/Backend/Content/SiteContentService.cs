
using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Backend.Content.Interface;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Backend.Content
{
    public class SiteContentService : Repository<SiteContent>, ISiteContentService
    {
        public SiteContentService(ApplicationDbContext dbcontext) : base(dbcontext) { }
        public async Task<IList<SiteContent>> GetAll()
        {
            var data = await _dbcontext
                        .SiteContents
                        .Where(x => x.Deleted == false)
                        .AsNoTracking()
                        .ToListAsync();
            return data;
        }

        public async Task<SiteContent> GetByType(AppContentType appContentType)
        {
            var data = await _dbcontext
                       .SiteContents
                       .Where(x => x.Deleted == false && x.AppContentType == appContentType)
                       .AsNoTracking()
                       .FirstOrDefaultAsync();
            if(data is null)
            {  //if no data exists, create one and return the same
                var entityEntry = new SiteContent() { AppContentType = appContentType };
                await _dbcontext.AddAsync(entityEntry);
                await _dbcontext.SaveChangesAsync();
                return entityEntry;
            }
            if (data is not null)
                {
                    data.Active = await GetDisplayWebControl(data);
                }
            
            return data;
        }

        public async override Task<bool> Update(SiteContent model)
        {
            var updateData = await _dbcontext
                       .SiteContents
                       .Where(x => x.Deleted == false && x.AppContentType == model.AppContentType) 
                       .FirstOrDefaultAsync();
             
            if (updateData is not null)
            {

                updateData.ContentEn = model.ContentEn;
                updateData.ContentAr = model.ContentAr;
                if (!string.IsNullOrEmpty(model.ImageName))
                {
                    updateData.ImageName = model.ImageName;
                }
                updateData.ModifiedBy = model.ModifiedBy;
                updateData.ModifiedOn = DateTime.Now;

                //set status for DisplayWebControl table
                updateData.Active = true;// model.Active;
                _dbcontext.Update(updateData);

                //update web control active status
                //await UpdateDisplayWebControl(updateData); 
            }
            else
            {
                await _dbcontext.AddAsync(model);
            }

            return await _dbcontext.SaveChangesAsync() > 0;

        }
        private async Task<bool> GetDisplayWebControl(SiteContent item)
        {
            var webcontrol = await _dbcontext.DisplayWebControls.Where(x => x.ControlId == (int)item.AppContentType).AsNoTracking().FirstOrDefaultAsync();
            if (webcontrol is not null)
            {
                return webcontrol.Active;
            }
            return false;
        }

        //Terms & Condition + Privacy Policy + Refund Policy --> these should be active/inactive because app store need these content(always active)

        //private async Task<bool> UpdateDisplayWebControl(SiteContent item)
        //{
        //    var webcontrol = await _dbcontext.DisplayWebControls.Where(x => x.ControlId == (int)item.AppContentType).FirstOrDefaultAsync();
        //    if (webcontrol is not null)
        //    {
        //        webcontrol.ModifiedBy = item.ModifiedBy;
        //        webcontrol.ModifiedOn = DateTime.Now;
        //        webcontrol.Active = item.Active;
        //    } else
        //    {
        //        var entity = new DisplayWebControl() { ControlId = (int)item.AppContentType, Active = item.Active, CreatedBy=item.CreatedBy, CreatedOn=item.CreatedOn, Description= item.AppContentType.ToString()  };
        //        await _dbcontext.DisplayWebControls.AddAsync(entity);
        //    }
        //    //_dbcontext.Update(webcontrol);
        //    return await _dbcontext.SaveChangesAsync() > 0;
        //}
    }
}
