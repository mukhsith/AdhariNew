using Data.Content;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public interface ISocialMediaLinkService
    {
        Task<SocialMediaLink> GetDefault(); 
    }     
}
