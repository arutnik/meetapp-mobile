using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using PMA.Mobile.Droid.Startup;
using Cirrious.CrossCore;
using PMA.Mobile.Core.Interfaces.Core;
using System.Collections.Generic;
using PMA.Mobile.Core.Framework;

namespace PMA.Mobile.Droid
{
    public class Setup : MvxAndroidSetup
    {
		Core.App _app;
		ICollection<IApplicationService> _services;

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

		protected override void InitializeLastChance ()
		{
			//This is called on NOT the UI Thread.
			base.InitializeLastChance ();

			var reflectionService = Mvx.Resolve<IReflectionService> ();

			//TODO - try catch
			_services = ServiceInitializer.InitializeAppServices(reflectionService).Result;
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}