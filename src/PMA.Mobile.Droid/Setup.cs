using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using PMA.Mobile.Droid.Startup;

namespace PMA.Mobile.Droid
{
    public class Setup : MvxAndroidSetup
    {
		Core.App _app;

        public Setup(Context applicationContext) : base(applicationContext)
        {
			_app = new Core.App ();
        }

        protected override IMvxApplication CreateApp()
        {
			return _app;
        }

		protected override void InitializeIoC ()
		{
			base.InitializeIoC ();
			_app.InitializeIOC ();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}