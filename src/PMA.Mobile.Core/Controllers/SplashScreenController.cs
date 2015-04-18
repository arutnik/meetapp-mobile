using System;
using PMA.Mobile.Core.ViewModels;
using Cirrious.MvvmCross.ViewModels;
using PMA.Mobile.Core.Interfaces.Auth;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Controllers
{
	public class SplashScreenController : ControllerBase<SplashScreenViewModel>
	{
        IAuthenticationService _authService;

		public SplashScreenController(
            IAuthenticationService authService
            )
		{
            _authService = authService;
		}

		protected override async Task OnControllerInitialize ()
		{
			await base.OnControllerInitialize ();

            var result = await Task.Factory.StartNew(() => _authService.TryAutoLogin().Result);

            OnAutoLoginResult(result);
		}

        private void OnAutoLoginResult(AutoLoginResult result)
        {
            switch (result)
            {
                case AutoLoginResult.InvalidCredentials:
                case AutoLoginResult.NoCredentials:
                    ShowViewModel<LoginViewModel>();
                    break;
                case AutoLoginResult.OnlineLoggedIn:
                case AutoLoginResult.OfflineLoggedIn:
                    ShowViewModel<MainFrameViewModel>();
                    break;
                default:
                    throw new NotSupportedException(result.ToString());
            }
        }
	}
}

