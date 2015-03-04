using Cirrious.MvvmCross.Plugins.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMA.Mobile.Core.ViewModels
{
    public class SplashScreenViewModel : ControllerViewModelBase
    {

		public ICommand OnIsLoggedIn { get; set; }
    }
}
