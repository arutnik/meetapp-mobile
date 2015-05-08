using PMA.Mobile.Core.Interfaces.Servers;
using PMA.Mobile.Core.Models.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PMA.Mobile.Core.Utility;
using Cirrious.CrossCore;
using PMA.Mobile.Core.Interfaces.Auth;

namespace PMA.Mobile.Core.Services.Servers
{
    public partial class PmaAppServerService
    {
        ServerResultFormat GetResultFromHttpException(FlurlHttpException fex)
        {
            var content = fex.Call.Response.Content.ReadAsStringAsync().Result;

            return new ServerResultFormat()
            {
                ResponseCode = (int)fex.Call.Response.StatusCode,
                Success = false,
                Message = content,
            };
        }

        ServerResultFormat GetResultFromGeneralException(Exception fex)
        {
            var content = fex.ToString();

            return new ServerResultFormat()
            {
                ResponseCode = (int)500,
                Success = false,
                Message = content,
            };
        }

        bool LogGeneralErrorAndContinue(Exception ex)
        {
            Mvx.Error(ex.Message);
            return true;
        }

        bool LogHttpErrorAndContinue(FlurlHttpException fex)
        {
            Mvx.Error(fex.Message);
            return true;
        }

        bool LogHttpErrorAndQuit(FlurlHttpException fex)
        {
            Mvx.Error(fex.Message);
            return false;
        }

        int DefaultRetries
        {
            get { return 3; }
        }

        TimeSpan DefaultRetrySleep
        {
            get { return TimeSpan.FromMilliseconds(500); }
        }

        private enum AuthNeed
        {
            None,
            User
        }

        private FlurlClient CreateClient(Url url, AuthNeed authNeed)
        {
            var client = url.ConfigureHttpClient((c) => { });

            //Add Hash param

            //Add basic auth
            if (authNeed == AuthNeed.User)
            {
                var credentials = _credentialService.GetCurrentAppServerCredentials();
                if (credentials == null)
                {
                    throw new UnauthorizedAccessException("No credentials stored.");
                }
                client = client.WithBasicAuth(credentials.UserId, credentials.Password);
            }

            return client;
        }

        string GetBaseUrl()
        {
            return @"http://192.168.1.3:3000/api";
        }

        public class ServerResultFormat
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("responseCode")]
            public int ResponseCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            
            public virtual TRes GetResult<TRes>()
            {
                return default(TRes);
            }
        }

        public class ServerResult<T> : ServerResultFormat
        {
            [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
            public T Result { get; set; }

            public override TRes GetResult<TRes>()
            {
                if (Result is TRes)
                {
                    object obj = Result;
                    return (TRes)obj;
                }
                return base.GetResult<TRes>();
            }
        }

        private void LogError(string message)
        {

        }
    }
}
