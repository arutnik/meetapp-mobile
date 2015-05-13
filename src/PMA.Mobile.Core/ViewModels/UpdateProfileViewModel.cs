using Cirrious.MvvmCross.Plugins.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.ViewModels
{
    public class UpdateProfileViewModel : ControllerViewModelBase
    {

		public string FirstName { get; set; }

		public string LastName { get; set; }

		string _p;
		//public string ProfilePictureUrl { get; set; }

		public string ProfilePictureUrl
		{
			get { return _p; }
			set { SetProperty (ref _p, value); }
		}

    }
}
