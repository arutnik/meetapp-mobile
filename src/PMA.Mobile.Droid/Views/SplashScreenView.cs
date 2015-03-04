
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using PMA.Mobile.Core.ViewModels;
using Xamarin.Facebook;
using Cirrious.CrossCore;

namespace PMA.Mobile.Droid.Views
{
	[Activity (Label = "SplashScreenView")]			
	public class SplashScreenView : MvxActivity, Session.IStatusCallback
	{
		public new SplashScreenViewModel ViewModel
		{
			get { return base.ViewModel as SplashScreenViewModel; }
			set {base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
		}

		protected override async void OnViewModelSet ()
		{
			base.OnViewModelSet ();

			SetContentView(Resource.Layout.activity_splash_screen);

			await ViewModel.Controller.WaitForInitialize();

			var session = Session.OpenActiveSession(this, false, this);

			if (session == null)
			{
				ViewModel.OnIsLoggedIn.Execute(false);
			}
		}

		public void Call (Session session, SessionState p1, Java.Lang.Exception p2)
		{
			if (session != null && session.IsOpened)
			{
				ViewModel.OnIsLoggedIn.Execute(true);
			}
			else
			{
				ViewModel.OnIsLoggedIn.Execute(false);
			}
		}
	}
}

