using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Interfaces.Auth
{
    /// <summary>
    /// Controls user authentication in the app.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Attempts to automatically log in using stored credentials
        /// on device.
        /// </summary>
        /// <returns>Returns a result indicating result of log in attempt.</returns>
        Task<AutoLoginResult> TryAutoLogin();

        /// <summary>
        /// Logs in with facebook - this either creates a new user on the server
        /// or returns the existing one. The user profile from the 
        /// server overwrites the current one upon success.
        /// </summary>
        /// <param name="facebookId"></param>
        /// <param name="facebookAccessToken"></param>
        /// <param name="facebookUserSerialized"></param>
        /// <returns>A value indicating the result of the operation.</returns>
        Task<UserCreateResult> LogInFromFacebook(string facebookId, string facebookAccessToken, string facebookUserSerialized);
    }

    public enum AutoLoginResult
    {
        NoCredentials,
        InvalidCredentials,
        OfflineLoggedIn,
        OnlineLoggedIn
    }

    public enum UserCreateResult
    {
        UnableToCreate,
        UnableToCreate_NoInternet,
        UnableToCreate_NoServer,
        Created,
    }
}
