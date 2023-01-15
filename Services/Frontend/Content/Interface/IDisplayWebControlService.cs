using System.Threading.Tasks;
using Utility.Models.Frontend.CustomizedModel;

namespace Services.Frontend.Content.Interface
{
    public interface IDisplayWebControlService
    {
        #region Display Web Control Service 
        Task<ContentStatusModel> GetContentStatus();
        #endregion


    }
}
