using System;
using Cirrious.MvvmCross.Plugins.Controllers;
using PropertyChanged;
using PMA.Mobile.Core.Framework.Commands;

namespace PMA.Mobile.Core.ViewModels
{
    [ImplementPropertyChanged]
	public class LoginViewModel : ControllerViewModelBase
	{
        public IAsyncCommand<FacebookLoginInfo> LogInWithFacebook { get; set; }

		public LoginViewModel ()
		{
		}

        public class FacebookLoginInfo
        {
            public string FacebookId { get; set; }

            public string FacebookAccessToken { get; set; }

            public string FacebookUserSerialized { get; set; }
        }
	}

    
}

