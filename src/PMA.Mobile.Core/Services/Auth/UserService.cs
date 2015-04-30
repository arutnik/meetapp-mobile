using System;
using PMA.Mobile.Core.Interfaces.Auth;

namespace PMA.Mobile.Core
{
	public class UserService : IUserService
	{


		public UserService ()
		{
		}

		#region IUserService implementation

		public bool IsCurrentUserProfileComplete ()
		{
			return true;
		}

		#endregion
	}
}

