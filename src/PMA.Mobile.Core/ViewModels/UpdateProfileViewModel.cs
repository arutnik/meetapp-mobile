using Cirrious.MvvmCross.Plugins.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMA.Mobile.Core.ViewModels
{
    public class UpdateProfileViewModel : ControllerViewModelBase
    {

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string UserFullName { get; set; }

		public string ProfilePictureUrl
		{
			get;
			set;
		}

		public ICommand DoneCommand { get; set; }
    }
}
