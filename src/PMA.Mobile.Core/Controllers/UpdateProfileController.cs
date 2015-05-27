using System;
using PMA.Mobile.Core.ViewModels;
using System.Linq;
using PMA.Mobile.Core.Interfaces.Auth;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;

namespace PMA.Mobile.Core.Controllers
{
	public class UpdateProfileController : ControllerBase<UpdateProfileViewModel>
	{
		readonly IUserService _userService;

		public UpdateProfileController (IUserService userService)
		{
			_userService = userService;
		}

		protected override async System.Threading.Tasks.Task OnControllerInitialize ()
		{
			await base.OnControllerInitialize();

			ViewModel.DoneCommand = new MvxCommand(OnDone);

			var user = _userService.CurrentUser;

			ViewModel.FirstName = user.RealName.FirstName;
			ViewModel.LastName = user.RealName.LastName;
			ViewModel.UserFullName = user.RealName.FirstName + " " + user.RealName.LastName;

			if (user.UserPictureUris.Any())
			{
				ViewModel.ProfilePictureUrl = user.UserPictureUris.First ();
			}
		}

		private void OnDone()
		{
			ShowViewModel<MainFrameViewModel>();
		}
	}
}

