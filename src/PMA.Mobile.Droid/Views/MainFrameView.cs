
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
using Xamarin.Facebook;
using Cirrious.CrossCore;
using Android.Graphics;
using Xamarin.Facebook.Widget;

namespace PMA.Mobile.Droid.Views
{
	[Activity (Label = "LoginView")]			
	public class MainFrameView : MvxActivity
	{

		protected override void OnViewModelSet ()
		{
			base.OnViewModelSet ();

			SetContentView(Resource.Layout.activity_main_frame);
		}


	}
}

