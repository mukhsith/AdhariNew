using Data.Locations;
using Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Frontend.Locations
{
    public interface ICountryService   
    {
        Task<Country> GetById(int Id);
        Task<IList<Country>> GetAll(bool showHidden = false);
        Task<Country> GetDefaultCountry();
    }
}
