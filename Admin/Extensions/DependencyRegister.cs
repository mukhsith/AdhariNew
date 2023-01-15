
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Utility.API; 

namespace Admin.Extensions
{
    /// <summary>
    /// Dependency Register
    /// </summary>
    public static class DependencyRegister
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        public static void RegisterService(
            IServiceCollection serviceCollection, 
            IConfiguration configuration)
        {
            serviceCollection.AddScoped<IAPIHelper, APIHelper>();

            //serviceCollection.Configure<AppSettingsModel>(configuration.GetSection("AppSettings"));

            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            serviceCollection.AddScoped<AppSettingsModel>();
            serviceCollection.Configure<AppSettingsModel>(configuration.GetSection("AppSettings"));
//            var appSettingsSection = Configuration.GetSection("AppSettings");
  //          services.Configure<AppSettingsModel>(appSettingsSection);
    //        var appSettings = appSettingsSection.Get<AppSettingsModel>();
        }
    }
}
