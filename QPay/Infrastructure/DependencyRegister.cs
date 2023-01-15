using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Utility.API;
using Utility.Models;

namespace QPay.Infrastructure
{
    /// <summary>
    /// Dependency Register
    /// </summary>
    public static class DependencyRegister
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        public static void RegisterService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddScoped<IAPIHelper, APIHelper>();
            serviceCollection.Configure<AppSettingsModel>(configuration.GetSection("AppSettings"));
        }
    }
}
