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
        public CredentialEditPageViewModel(INavigationService navigationService, ICredentialService credentialService)
            : base(navigationService)
        {
            Title = "Add Credential";
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.credentialService = credentialService ?? throw new ArgumentNullException(nameof(credentialService));
        }

        private readonly INavigationService navigationService;
        private readonly ICredentialService credentialService;

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
            editCredential ?? (editCredential = new DelegateCommand(() =>
            {
                Credential = new UserCredential(Id, Username, Password, SiteUrl);
                credentialService.UpdateCredential(Credential);
                navigationService.GoBackAsync();
            }));
    }
}
