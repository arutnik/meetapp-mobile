using System;
using PMA.Mobile.Core.ViewModels;
using Cirrious.MvvmCross.ViewModels;

namespace PMA.Mobile.Core.Controllers
{
	public class SplashScreenController : ControllerBase<SplashScreenViewModel>
	{
		public SplashScreenController ()
		{
		}

		protected override async System.Threading.Tasks.Task OnControllerInitialize ()
		{
			await base.OnControllerInitialize ();

			ViewModel.OnIsLoggedIn = new MvxCommand<bool>(OnIsLoggedIn);
		}

		void OnIsLoggedIn(bool isloggedin)
		{
			if (isloggedin)
			{
				ShowViewModel<MainFrameViewModel>();
			}
			else
			{
				ShowViewModel<LoginViewModel>();
			}
		}
	}
}

