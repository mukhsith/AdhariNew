using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.API;

namespace API.Helpers
{
    public class PushNotification : IPushNotification
    {
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        public PushNotification(ILoggerFactory logger,
             IOptions<AppSettingsModel> options)
        {
            _logger = logger.CreateLogger(typeof(PushNotification).Name);
            _appSettings = options.Value;
        }
        public bool SendNotification(string title, string body, string deviceToken)
        {
            try
            {
                var requestUri = _appSettings.PushBaseUrl;
                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", _appSettings.PushAuthorizationKey));
                webRequest.Headers.Add(string.Format("Sender: id={0}", _appSettings.PushSenderKey));
                webRequest.ContentType = "application/json";

                if (title == null)
                    title = "   ";
                if (title.Length < 2)
                    title = "   ";

                var data = new
                {
                    to = deviceToken,
                    notification = new
                    {
                        title,
                        body,
                        Date = DateTime.Now,
                        sound = "Enabled",
                        icon = "myicon"
                    }
                };

                string postbody = JsonConvert.SerializeObject(data).ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                webRequest.ContentLength = byteArray.Length;
                using Stream dataStream = webRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                using WebResponse webResponse = webRequest.GetResponse();
                using Stream dataStreamResponse = webResponse.GetResponseStream();
                using StreamReader tReader = new(dataStreamResponse);
                string sResponseFromServer = tReader.ReadToEnd();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }
    }
}
