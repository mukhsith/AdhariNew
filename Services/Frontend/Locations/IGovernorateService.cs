using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Locations;

namespace Services.Frontend.Locations
{
    public interface IGovernorateService
    {
        Task<IEnumerable<Governorate>> GetAll();
        Task<Governorate> GetById(int id);     
    }
}
