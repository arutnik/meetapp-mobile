using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Interfaces.Auth
{
    public interface ICredentialService
    {
        CredentialView GetCurrentAppServerCredentials();
        void SetCurrentAppServerCredentials(CredentialView credentials);

        void CleanUpAllCredentials(); 
    }

    public class CredentialView
    {
        public string UserId { get; private set; }
        public string Password { get; private set; }

        public CredentialView(string userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}
