using PMA.Mobile.Core.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<AuthenticationResult> TryAutoLogin()
        {
            return Task.FromResult(AuthenticationResult.NoCredentials);
        }


    }
}
