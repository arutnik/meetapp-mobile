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
using Xamarin.Facebook;
using Cirrious.CrossCore;
using Android.Graphics;
using Xamarin.Facebook.Widget;
using PMA.Mobile.Core.ViewModels;
using Newtonsoft.Json;

namespace PMA.Mobile.Droid.Views
{
	[Activity (Label = "LoginView"
		, Theme = "@style/Theme.Login"
	)]			
	public class LoginView : MvxActivity, Session.IStatusCallback, Request.IGraphUserCallback, LoginButton.IUserInfoChangedCallback
	{
		LoginButton _loginButton;
		TextView _loginStatusText;

		public LoginViewModel LoginViewModel
		{
			get { return ViewModel as LoginViewModel; }
		}

		protected override void OnViewModelSet ()
		{
			base.OnViewModelSet ();

			SetContentView(Resource.Layout.activity_login);

			_loginButton = (LoginButton)FindViewById(Resource.Id.btn_native_facebooklogin);
			_loginButton.SetReadPermissions(new[] { "user_interests", "user_birthday", "user_activities", "email", "user_friends", "public_profile" });
			_loginButton.UserInfoChangedCallback = this;
			_loginButton.SessionStatusCallback = this;

			_loginStatusText = (TextView)FindViewById (Resource.Id.txt_login_loginstatus);
		}

		public void OnUserInfoFetched (Xamarin.Facebook.Model.IGraphUser user)
		{
			if (user != null)
			{
				_loginButton.Visibility = ViewStates.Invisible;
				_loginStatusText.Visibility = ViewStates.Visible;

				LoginViewModel.LogInWithFacebook.ExecuteAsync (CreateLogInInfo(user));
				Mvx.Trace("Got the user {0} {1}", user.FirstName, Session.ActiveSession.AccessToken);
			}
			else
			{
				Mvx.Error("Failed to get user.");
			}
		}

		LoginViewModel.FacebookLoginInfo CreateLogInInfo(Xamarin.Facebook.Model.IGraphUser user)
		{
			var model = new LoginViewModel.FacebookLoginInfo () {
				FacebookId = user.Id,
				FacebookAccessToken = Session.ActiveSession.AccessToken
			};

			var fbUser = new {
				firstName = user.FirstName,
				lastName = user.LastName,
				gender = user.GetProperty("gender").ToString()
			};
			model.FacebookUserSerialized = JsonConvert.SerializeObject (fbUser);
			return model;
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			// Relay the result to our FB Session
			Session.ActiveSession.OnActivityResult (this, requestCode, (int)resultCode, data);
		}

		public void Call (Session session, SessionState state, Java.Lang.Exception ex)
		{
			if (session.IsOpened)
			{
				//Request.ExecuteMeRequestAsync (session, this);
				Mvx.Trace("Finished call");
			}
			else
			{
				Mvx.Error("Failed to open session. {0}", ex != null ? ex.Message : "");
			}
		}

		public void OnCompleted (Xamarin.Facebook.Model.IGraphUser user, Response p1)
		{
			if (user != null)
			{
				LoginViewModel.LogInWithFacebook.ExecuteAsync (CreateLogInInfo(user));
				Mvx.Trace("Got the user {0}", user.FirstName);
			}
			else
			{
				Mvx.Error("Failed to get user.");
			}
		}
	}
}

