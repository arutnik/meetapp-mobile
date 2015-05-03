using PMA.Mobile.Core.Interfaces.Auth;
using PMA.Mobile.Core.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHS.MvvmCross.Plugins.Keychain;
using PMA.Mobile.Core.Interfaces.Data;
using PMA.Mobile.Core.Models;
using System.Linq.Expressions;

namespace PMA.Mobile.Core.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        public const string AppKeyChainAccount = "PMAAppCredentials";

        readonly IPmaAppServer _appServerService;
        readonly IKeychain _keyChain;
        readonly ILocalData _localData;

        public AuthenticationService(IPmaAppServer appServerService
            , IKeychain keychain
            , ILocalData localData
            )
        {
            _appServerService = appServerService;
            _keyChain = keychain;
            _localData = localData;
        }

        public Task<AutoLoginResult> TryAutoLogin()
        {
            if (HasNeverLoggedInThisInstallation)
            {
                CleanUpCredentialStore();
                return Task.FromResult(AutoLoginResult.NoCredentials);
            }

            return Task.FromResult(AutoLoginResult.NoCredentials);
        }

        private void CleanUpCredentialStore()
        {
            LoginDetails login = null;

            do
            {
                try
                {
                    login = _keyChain.GetLoginDetails(AppKeyChainAccount);

                    if (login != null)
                    {
                        _keyChain.DeleteAccount(AppKeyChainAccount, login.Username);
                    }
                }
                catch { }
            } while (login != null);
        }

        public async Task<UserCreateResult> LogInFromFacebook(string facebookId, string facebookAccessToken, string facebookUserSerialized)
        {

            var result = await _appServerService.LogInWithFacebook(facebookId, facebookAccessToken, facebookUserSerialized);

            if (result.Status == PmaAppServerCode.Ok)
            {
                try
                {
                    _keyChain.DeleteAccount(AppKeyChainAccount, result.Result.UserId);
                }
                catch { }

                try
                {
                    _keyChain.SetPassword(result.Result.Password, AppKeyChainAccount, result.Result.UserId);
                }
                catch { }
            }

            throw new NotImplementedException();
        }

        bool HasNeverLoggedInThisInstallation
        {
            get { return false; }
        }
    }
}
