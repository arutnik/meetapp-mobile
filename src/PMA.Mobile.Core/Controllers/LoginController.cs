using System;
using PMA.Mobile.Core.ViewModels;
using System.Threading.Tasks;
using PMA.Mobile.Core.Framework.Commands;
using PMA.Mobile.Core.Interfaces.Auth;

namespace PMA.Mobile.Core.Controllers
{
	public class LoginController : ControllerBase<LoginViewModel>
	{
        readonly IAuthenticationService _authService;
        readonly IUserService _userService;

		public LoginController (IAuthenticationService authService
            , IUserService userService
            )
		{
            _authService = authService;
            _userService = userService;
		}

        protected override async Task OnControllerInitialize()
        {
            await base.OnControllerInitialize();

            ViewModel.LogInWithFacebook = new AsyncCommand<LoginViewModel.FacebookLoginInfo>(OnFacebookLogin);
        }

        private async Task OnFacebookLogin(LoginViewModel.FacebookLoginInfo arg)
        {
            var result = await _authService.LogInFromFacebook(arg.FacebookId, arg.FacebookAccessToken, arg.FacebookUserSerialized);

            if (result == UserCreateResult.Created)
            {
                OnLogInSuccess();
            }
        }

        void OnLogInSuccess()
        {
            if (_userService.IsCurrentUserProfileComplete())
            {
                ShowViewModel<MainFrameViewModel>();
            }
            else
            {
                ShowViewModel<UpdateProfileViewModel>();
            }
        }
	}
}

