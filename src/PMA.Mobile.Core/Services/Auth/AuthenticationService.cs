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
        

        readonly IPmaAppServer _appServerService;
        readonly ICredentialService _credentialService;
        readonly ILocalData _localData;

        public AuthenticationService(IPmaAppServer appServerService
            , ICredentialService credentialService
            , ILocalData localData
            )
        {
            _appServerService = appServerService;
            _credentialService = credentialService;
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
            _credentialService.CleanUpAllCredentials();
        }

        public async Task<UserCreateResult> LogInFromFacebook(string facebookId, string facebookAccessToken, string facebookUserSerialized)
        {

            var result = await _appServerService.LogInWithFacebook(facebookId, facebookAccessToken, facebookUserSerialized);

            PmaAppServerCode finalStatus = result.Status;
            if (result.Status == PmaAppServerCode.Ok)
            {
                _credentialService.SetCurrentAppServerCredentials(new CredentialView(result.Result.UserId, result.Result.Password));

                var loggedInUser = new LoggedInUser
                {
                    UserProfileId = result.Result.UserProfileId
                };

                _localData.Save(new[] { loggedInUser });

                //get local profile

                var userResult = await _appServerService.GetUserById(result.Result.UserProfileId);

                if (userResult.Status == PmaAppServerCode.Ok)
                {
                    //Save to user service
                }
                
                finalStatus = userResult.Status;
            }

            
            if (finalStatus == PmaAppServerCode.Ok)
            {
                return UserCreateResult.Created;
            }
            else if (finalStatus == PmaAppServerCode.ConnectionNotAvailable)
            {
                return UserCreateResult.UnableToCreate_NoInternet;
            }
            else if (finalStatus == PmaAppServerCode.ServerNotAvailable)
            {
                return UserCreateResult.UnableToCreate_NoServer;
            }
            else
            {
                return UserCreateResult.UnableToCreate;
            }
        }

        bool HasNeverLoggedInThisInstallation
        {
            get { return false; }
        }
    }
}
