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

namespace PMA.Mobile.Core.Services.Servers
{
    public class PmaAppServerService : IPmaAppServerService
    {
        public async Task<PmaAppServerResult<ServerLoginResult>> LogInWithFacebook(string facebookId, string facebookAccessToken, string facebookUserSerialized)
        {
            var url = GetBaseUrl()
                    .AppendPathSegments("user", "register", "fb");

            try
            {
                ServerGenericResult serverResult = await new HttpRetryHelper<ServerGenericResult>(DefaultRetries, DefaultRetrySleep)
                .Method(async () =>
                {
                    var client = CreateClient(url);

                    return await client
                        .PostJsonAsync(new { fbId = facebookId, fbAccessToken = facebookAccessToken })
                        .ReceiveJson<ServerGenericResult>();
                })
                .OnCode(400, LogHttpErrorAndQuit, GetResultFromHttpException)
                .OnCode(404, LogHttpErrorAndContinue, GetResultFromHttpException)
                .OnAnyOtherError(LogGeneralErrorAndContinue, GetResultFromGeneralException)
                .Execute()
                ;

                if (!serverResult.Success)
                {
                    return new PmaAppServerResult<ServerLoginResult>()
                    {
                        Message = "Failed",
                        Result = new ServerLoginResult(),
                        Status = PmaAppServerCode.ConnectionNotAvailable
                    };
                }

                var clientResult = new ServerLoginResult();

                if (serverResult.Result["userId"] == null)
                    throw new Exception();

                if (serverResult.Result["password"] == null)
                    throw new Exception();

                clientResult.UserId = serverResult.Result["userId"].Value<String>();
                clientResult.Password = serverResult.Result["userId"].Value<String>();

                return new PmaAppServerResult<ServerLoginResult>()
                {
                    Result = clientResult
                };
            }
            catch (Exception ex)
            {
                Mvx.Error(ex.ToString());
                //Log unexpected / parsing error
            }

            return null;
        }

        ServerGenericResult GetResultFromHttpException(FlurlHttpException fex)
        {
            var content = fex.Call.Response.Content.ReadAsStringAsync().Result;

            return new ServerGenericResult()
            {
                ResponseCode = (int)fex.Call.Response.StatusCode,
                Success = false,
                Message = content,
            };
        }

        ServerGenericResult GetResultFromGeneralException(Exception fex)
        {
            var content = fex.ToString();

            return new ServerGenericResult()
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

        private FlurlClient CreateClient(Url url)
        {
            var client = url.ConfigureHttpClient((c) => { });

            //Add Hash param

            //Add basic auth

            return client;
        }

        string GetBaseUrl()
        {
            return @"http://192.168.1.3:3000";
        }

        public class ServerGenericResult
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("result")]
            public JToken Result { get; set; }

            [JsonProperty("responseCode")]
            public int ResponseCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }
        }

        private void LogError(string message)
        {

        }
    }
}
