using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;
using Java.Security;
using Android.Util;

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

		protected override void OnCreate (Android.OS.Bundle bundle)
		{
			base.OnCreate (bundle);

			try {
				PackageInfo info =     PackageManager.GetPackageInfo("pma.mobile.droid", PackageInfoFlags.Signatures);
				foreach (Android.Content.PM.Signature signature in info.Signatures) {
					MessageDigest md = MessageDigest.GetInstance("SHA");
					md.Update(signature.ToByteArray());
					string sign= Base64.EncodeToString(md.Digest(), Base64Flags.Default);
					Log.Debug("MY KEY HASH:", sign);
				}
			} catch (Android.Content.PM.PackageManager.NameNotFoundException) {
			} catch (NoSuchAlgorithmException) {
			}
		}
    }
}