using Prism;
using Prism.Ioc;
using XamarinCalculator.ViewModels;
using XamarinCalculator.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Refit;
using XamarinCalculator.Services;

namespace XamarinCalculator
{
    public partial class App
    {
        string scheduleAddress = "http://localhost:5000/";
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();



            var EPService = RestService.For<ICredentialService>(scheduleAddress);
            containerRegistry.RegisterInstance(EPService);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CredentialListPage, CredentialListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddCredentialPage, AddCredentialPageViewModel>();
            containerRegistry.RegisterForNavigation<CredentialDetailsPage, CredentialDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<CredentialEditPage, CredentialEditPageViewModel>();
        }
    }
}
