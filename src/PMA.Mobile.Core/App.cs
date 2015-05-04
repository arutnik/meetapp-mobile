using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Controllers;
using PMA.Mobile.Core.Utility.Reflection;

namespace PMA.Mobile.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<ViewModels.SplashScreenViewModel>();
        }

        public void InitializeIOC()
        {
            foreach (var pair in CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces())
            {
                IocHelper.LazyConstructAndRegisterSingletonForAll(pair.ServiceTypes, pair.ImplementationType);
            }
        }

        protected override Cirrious.MvvmCross.ViewModels.IMvxViewModelLocator CreateDefaultViewModelLocator()
        {
            var defaultLocator = base.CreateDefaultViewModelLocator();

            var binder = new ViewModelBinder(CreatableTypes);

            // register default view model binder
            Mvx.RegisterSingleton<IViewModelBinder>(binder);

            return new BindingViewModelLocator(defaultLocator, binder);
        }
    }
}