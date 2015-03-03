using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Plugins.Controllers;

namespace PMA.Mobile.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<ViewModels.SplashViewModel>();
        }

        public void InitializeIOC()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
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