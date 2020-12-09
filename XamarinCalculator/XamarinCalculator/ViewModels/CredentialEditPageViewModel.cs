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
    public class CredentialEditPageViewModel : ViewModelBase
    {
        public CredentialEditPageViewModel(INavigationService navigationService, ICredentialService credentialService, IDialogService dialogService)
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

        public UserCredential Credential { get; private set; }

        int id;
        public int Id
        {
            get => id;
            set { SetProperty(ref id, value); }
        }

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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Credential = (UserCredential)parameters["credential"];

            if (Credential == null)
            {
                NavigationService.GoBackAsync();
            }
            else
            {
                Id = Credential.Id;
                Username = Credential.Username;
                Password = Credential.Password;
                SiteUrl = Credential.SiteUrl;
            }
        }

        private DelegateCommand editCredential;

        public DelegateCommand EditCredential =>
            editCredential ?? (editCredential = new DelegateCommand(async () =>
            {
                Credential = new UserCredential(Id, Username, Password, SiteUrl);
                
                try
                {
                    await credentialService.UpdateCredential(Credential);
                }
                catch
                {
                    await dialogService.ShowDialog("You cannot Edit a credential When you are not connected", "Cant Edit", "OK");
                }
                
                await navigationService.GoBackAsync();
            }));
    }
}
