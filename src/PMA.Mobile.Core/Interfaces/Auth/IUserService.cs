using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Interfaces.Auth
{
    public interface IUserService
    {
        /// <summary>
        /// Loads fresh data for current user.
        /// </summary>
        /// <returns></returns>
        Task<UserCreateResult> RefreshCurrentUserData();

        /// <summary>
        /// Returns whether the current user's profile is complete enough
        /// to use the app.
        /// </summary>
        /// <returns></returns>
        bool IsCurrentUserProfileComplete();
    }

    
}
