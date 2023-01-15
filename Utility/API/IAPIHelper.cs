using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.API
{
    public enum PostContentType
    {
        applicationJson = 1,
        /// <summary>
        /// input parameter [FromForm]
        /// </summary>
    }

    public interface IAPIHelper
    {
        /// <summary>
        /// Post async      
        /// </summary>
        /// <param name="url">API url</param>
        /// <param name="requestObj">Request object</param>
        Task<T> PostAsync<T>(string url, object requestObj, PostContentType postType);        

        /// <summary>
        /// Post async      
        /// </summary>
        /// <param name="url">API url</param>
        /// <param name="requestObj">Request object</param>
        /// <param name="includeBaseUrl">Need to include base url</param>
        /// <param name="baseUrl">Base url</param>
        Task<T> PostAsync<T>(string url, object requestObj, string baseUrl = "");
        /// <summary>
        /// Put async      
        /// </summary>
        /// <param name="url">API url</param>
        /// <param name="requestObj">Request object</param>
        Task<T> PutAsync<T>(string url, object requestObj, string baseUrl = "");

        /// <summary>
        /// Delete async      
        /// </summary>
        /// <param name="url">API url</param>
        Task<T> DeleteAsync<T>(string url, string baseUrl = "");

        /// <summary>
        /// Get async      
        /// </summary>
        /// <param name="url">API url</param>
        Task<T> GetAsync<T>(string url, string baseUrl = "");

        /// <summary>
        /// Get user ip address      
        /// </summary>
        /// <param name="url">API url</param>
        string GetUserIP();
    }
}

