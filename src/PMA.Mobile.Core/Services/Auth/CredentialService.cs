using IHS.MvvmCross.Plugins.Keychain;
using PMA.Mobile.Core.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Services.Auth
{
    public class CredentialService : ICredentialService
    {
        public const string AppKeyChainAccount = "PMAAppCredentials";

        readonly IKeychain _keyChain;

        public CredentialService(IKeychain keychain
            )
        {
            _keyChain = keychain;
        }


        public CredentialView GetCurrentAppServerCredentials()
        {
            LoginDetails login = null;

            try
            {
                login = _keyChain.GetLoginDetails(AppKeyChainAccount);
            }
            catch
            {
                return null;
            }

            if (login == null)
                return null;

            return new CredentialView(login.Username, login.Password);
        }

        public void SetCurrentAppServerCredentials(CredentialView credentials)
        {
            try
            {
                _keyChain.DeleteAccount(AppKeyChainAccount, credentials.UserId);
            }
            catch { }

            try
            {
                _keyChain.SetPassword(credentials.Password, AppKeyChainAccount, credentials.UserId);
            }
            catch {  }
        }

        public void CleanUpAllCredentials()
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
    }
}
