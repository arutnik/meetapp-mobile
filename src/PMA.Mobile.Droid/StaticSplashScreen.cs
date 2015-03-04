using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;

namespace PMA.Mobile.Droid
{
    [Activity(
		Label = "PMA.Mobile.Droid"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class StaticSplashScreen : MvxSplashScreenActivity
    {
		public StaticSplashScreen()
			: base(Resource.Layout.static_splash_screen)
        {

        }


    }
}