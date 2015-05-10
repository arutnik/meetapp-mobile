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
    public partial class PmaAppServerService : IPmaAppServer
    {
        ICredentialService _credentialService;

        public PmaAppServerService(ICredentialService credentialService)
        {
            _credentialService = credentialService;
        }

        public async Task<PmaAppServerResult<ServerLoginResult>> LogInWithFacebook(string facebookId, string facebookAccessToken, string facebookUserSerialized)
        {
            var url = GetBaseUrl()
                    .AppendPathSegments("user", "register", "fb");

            try
            {
                ServerResultFormat serverResult = await new HttpRetryHelper<ServerResultFormat>(DefaultRetries, DefaultRetrySleep)
                .Method(async () =>
                {
                    var client = CreateClient(url, AuthNeed.None);

                    return await client
								.PostJsonAsync(new { fbId = facebookId, fbAccessToken = facebookAccessToken, fbUser = JsonConvert.DeserializeObject<JToken>(facebookUserSerialized) })
                        .ReceiveJson<ServerResult<JToken>>();
                })
                .OnCode(400, LogHttpErrorAndQuit, GetResultFromHttpException)
                .OnCode(404, LogHttpErrorAndContinue, GetResultFromHttpException)
                .OnCode(500, LogHttpErrorAndContinue, GetResultFromHttpException)
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

                var data = serverResult.GetResult<JToken>();

                if (data["userId"] == null)
                    throw new Exception("User id not in result");

                if (data["password"] == null)
                    throw new Exception("password not in result");

                if (data["userProfileId"] == null)
                    throw new Exception("userProfileId not in result");

                clientResult.UserId = data["userId"].Value<String>();
                clientResult.Password = data["password"].Value<String>();
                clientResult.UserProfileId = data["userProfileId"].Value<string>();
                 
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

        public async Task<PmaAppServerResult<ServerUser>> GetUserById(string userProfileId)
        {
            var url = GetBaseUrl()
                    .AppendPathSegments("user", userProfileId);
                    

            try
            {
                ServerResultFormat serverResult = await new HttpRetryHelper<ServerResultFormat>(DefaultRetries, DefaultRetrySleep)
                .Method(async () =>
                {
                    var client = CreateClient(url, AuthNeed.User);

                    return await client
                                .GetJsonAsync<ServerResult<ServerUser>>()
                        ;
                })
                .OnCode(400, LogHttpErrorAndQuit, GetResultFromHttpException)
                .OnCode(404, LogHttpErrorAndContinue, GetResultFromHttpException)
                .OnCode(500, LogHttpErrorAndContinue, GetResultFromHttpException)
                .OnAnyOtherError(LogGeneralErrorAndContinue, GetResultFromGeneralException)
                .Execute()
                ;

                if (!serverResult.Success)
                {
                    return new PmaAppServerResult<ServerUser>()
                    {
                        Message = "Failed",
                        Result = new ServerUser(),
                        Status = PmaAppServerCode.ConnectionNotAvailable
                    };
                }

                 
                return new PmaAppServerResult<ServerUser>()
                {
                    Result = serverResult.GetResult<ServerUser>()
                };
            }
            catch (Exception ex)
            {
                Mvx.Error(ex.ToString());
                //Log unexpected / parsing error
            }

            return null;
        }

        
    }
}
