using System;
using PMA.Mobile.Core.Interfaces.Auth;
using System.Threading.Tasks;
using PMA.Mobile.Core.Interfaces.Servers;
using PMA.Mobile.Core.Models;
using PMA.Mobile.Core.Interfaces.Data;
using System.Linq;

namespace PMA.Mobile.Core
{
	public class UserService : IUserService
	{
        readonly IPmaAppServer _appServerService;
        readonly ILocalData _dataStore;

        public LocalUserProfile CurrentUser { get; set; }

		public UserService (IPmaAppServer appServerService
            , ILocalData localData)
		{
            _appServerService = appServerService;
            _dataStore = localData;
		}

		public bool IsCurrentUserProfileComplete ()
		{
			if (CurrentUser == null)
			{
				return false;
			}

			if (CurrentUser.Dob.Year < 1900)
			{
				return false;
			}

			if (CurrentUser.UserPictureUris.Count () == 0)
			{
				return false;
			}

			return true;
		}


        public async Task<UserCreateResult> RefreshCurrentUserData()
        {
            string userProfileId = null;

            if (CurrentUser != null)
            {
                userProfileId = CurrentUser.UserProfileId;
            }
            else
            {
                var loggedInUser = _dataStore.All<LoggedInUser>().FirstOrDefault();

                if (loggedInUser == null)
                {
                    //TODO log
                    return UserCreateResult.UnableToCreate;
                }

                userProfileId = loggedInUser.UserProfileId;
            }

            var serverResult = await _appServerService.GetUserById(userProfileId);

            switch (serverResult.Status)
            {
                case PmaAppServerCode.Ok:
                    break;
                case PmaAppServerCode.ConnectionNotAvailable:
                    return UserCreateResult.UnableToCreate_NoInternet;
                case PmaAppServerCode.ServerNotAvailable:
                    return UserCreateResult.UnableToCreate_NoServer;
                default:
                    return UserCreateResult.UnableToCreate;
            }

            var localUser = new LocalUserProfile()
            {
                UserProfileId = userProfileId,
                Dob = serverResult.Result.Dob,
                Gender = new Gender() { Id = serverResult.Result.Gender },
                RealName = new PersonName(serverResult.Result.RealName.FirstName, serverResult.Result.RealName.LastName),

            };

			localUser.UserPictureUris = serverResult.Result.Pictures.Select (p => p.Uri).ToArray ();

            //TODO interests, pictures, meets

            _dataStore.Save(new[] { localUser });
            CurrentUser = localUser;

            return UserCreateResult.Created;
        }
    }
}

