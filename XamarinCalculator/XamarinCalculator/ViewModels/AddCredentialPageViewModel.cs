using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using XamarinCalculator.Services;
using XamarinCalculator.Shared;

namespace XamarinCalculator.ViewModels
{
    public class AddCredentialPageViewModel : ViewModelBase
    {
        public AddCredentialPageViewModel(INavigationService navigationService, ICredentialService credentialService, IDialogService dialogService) 
            : base(navigationService)
        {
            Title = "Add Credential";
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.credentialService = credentialService ?? throw new ArgumentNullException(nameof(credentialService));
            this.dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        private readonly INavigationService navigationService;
        private readonly ICredentialService credentialService;
        private readonly IDialogService dialogService;
        string username;
        public string Username
        {
            get => username;
            set { SetProperty(ref username, value); }
        }

        string password;
        public string Password
        {
            get => password;
            set { SetProperty(ref password, value); }
        }

        string siteUrl;
        public string SiteUrl
        {
            get => siteUrl;
            set { SetProperty(ref siteUrl, value); }
        }

        private DelegateCommand addCredential;

        public DelegateCommand AddCredential =>
            addCredential ?? (addCredential = new DelegateCommand(async () =>
            {
                var credential = new UserCredential(0, Username, Password, SiteUrl);

                try
                {
                    await credentialService.AddCredential(credential);
                }
                catch
                {
                    await dialogService.ShowDialog("You cannot Add a credential When you are not connected", "Cant Add", "OK");
                }
                
                await navigationService.GoBackAsync();
            }));
    }
}
