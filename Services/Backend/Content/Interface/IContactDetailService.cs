
using Data.Content;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Backend.Content.Interface
{
    public interface IContactDetailService
    {
        Task<ContactDetail> GetDefault();
        Task<bool> Edit(ContactDetail item);
    }
     
}
