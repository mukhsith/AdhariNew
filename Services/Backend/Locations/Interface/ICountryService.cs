using Data.Locations; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  Services.Backend.Locations
{
    public interface ICountryService   
    {
        Task<Country> GetById(int Id);
        Task<IList<Country>> GetAll(bool showHidden = false);
        Task<Country> GetDefaultCountry();
    }
}
