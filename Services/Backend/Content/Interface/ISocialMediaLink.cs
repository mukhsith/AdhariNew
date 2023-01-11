
using Data.Content;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Backend.Content.Interface
{
    public interface ISocialMediaLinkService
    {
        Task<SocialMediaLink> GetDefault();
        Task<SocialMediaLink> Create(int UserId);
        Task<bool> Edit(SocialMediaLink item);
    }
     
}
