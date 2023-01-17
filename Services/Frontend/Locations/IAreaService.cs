using Data.Locations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Frontend.Locations
{
    public interface IAreaService  
    {
        Task<IList<Area>> GetAll(bool showHidden = false, int? countryId = null);
        Task<Area> GetById(int id);
    }
}
