using System;
using Cirrious.MvvmCross.Droid.Views;
using PMA.Mobile.Core.ViewModels;

namespace PMA.Mobile.Droid.Presentation
{
	public class DroidMasterViewPresenter : MvxAndroidViewPresenter, IMvxAndroidViewPresenter
	{
		#region IMvxViewPresenter implementation

		public override void Show (Cirrious.MvvmCross.ViewModels.MvxViewModelRequest request)
		{
			if (request.ViewModelType == typeof(SplashScreenViewModel))
			{
				//TODO: splashscreen presenter
			}
		}

		public override void ChangePresentation (Cirrious.MvvmCross.ViewModels.MvxPresentationHint hint)
		{
			

			base.ChangePresentation(hint);
		}

		#endregion

		public DroidMasterViewPresenter ()
		{
		}
	}
}

