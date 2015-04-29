using PMA.Mobile.Core.Interfaces.Auth;
using PMA.Mobile.Core.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        IPmaAppServerService _appServerService;

        public AuthenticationService(IPmaAppServerService appServerService)
        {
            _appServerService = appServerService;
        }

        public Task<AutoLoginResult> TryAutoLogin()
        {
            return Task.FromResult(AutoLoginResult.NoCredentials);
        }

        public async Task<UserCreateResult> LogInFromFacebook(string facebookId, string facebookAccessToken, string facebookUserSerialized)
        {

            var result = await _appServerService.LogInWithFacebook(facebookId, facebookAccessToken, facebookUserSerialized);



            throw new NotImplementedException();
        }
    }
}
