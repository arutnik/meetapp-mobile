using System;
using PMA.Mobile.Core.ViewModels;

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
		}
	}
}

