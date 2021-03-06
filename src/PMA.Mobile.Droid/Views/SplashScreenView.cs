﻿
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
	[Activity (Label = "SplashScreenView"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
	)]			
	public class SplashScreenView : MvxActivity
	{
		public new SplashScreenViewModel ViewModel
		{
			get { return base.ViewModel as SplashScreenViewModel; }
			set {base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

		}

		protected override void OnStart ()
		{
			base.OnStart ();

		}

		protected override void OnViewModelSet ()
		{
			base.OnViewModelSet ();

			SetContentView(Resource.Layout.activity_splash_screen);
		}
	}
}

