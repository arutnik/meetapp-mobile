
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
using PMA.Mobile.Core.ViewModels;
using Newtonsoft.Json;

namespace PMA.Mobile.Droid.Views
{
	[Activity (Label = "UpdatEProfileView"
		, Theme = "@style/Theme.UpdateProfile"
	)]			
	public class UpdateProfileView : MvxActivity
	{
		public UpdateProfileViewModel UpdateProfileViewModel
		{
			get { return ViewModel as UpdateProfileViewModel; }
		}

		protected override void OnViewModelSet ()
		{
			base.OnViewModelSet ();

			SetContentView(Resource.Layout.activity_update_profile);
		}

	}
}

