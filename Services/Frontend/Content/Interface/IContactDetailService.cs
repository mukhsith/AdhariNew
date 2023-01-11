
using Data.Content;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.Content.Interface
{
    public interface IContactDetailService
    {
        Task<ContactDetail> GetDefault(); 
    }
     
}
