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
    public class CredentialDetailsPageViewModel : ViewModelBase
    {
        public CredentialDetailsPageViewModel(INavigationService navigationService, ICredentialService credentialService) : base(navigationService)
        {
            Title = "Credential Detail Page";
            this.credentialService = credentialService ?? throw new ArgumentNullException(nameof(credentialService));
        }

        public UserCredential Credential { get; private set; }

        private int id;
        public int Id
        {
            get => id;
            set { SetProperty(ref id, value); }
        }

        private string username;
        public string Username
        {
            get => username;
            set { SetProperty(ref username, value); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { SetProperty(ref password, value); }
        }

        private string siteUrl;
        private readonly ICredentialService credentialService;

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
                NavigationParameters Parameters = new NavigationParameters
            {
                { "credential", Credential }
            };
                NavigationService.NavigateAsync(nameof(Views.CredentialEditPage), Parameters, false, true);
            }));

        private DelegateCommand deleteCredential;
        public DelegateCommand DeleteCredential =>
            deleteCredential ?? (deleteCredential = new DelegateCommand(() =>
            {
                credentialService.DeleteCredential(Credential);
            }));
    }
}
