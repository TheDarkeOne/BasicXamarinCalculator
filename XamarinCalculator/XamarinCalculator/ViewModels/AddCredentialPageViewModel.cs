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
        public AddCredentialPageViewModel(INavigationService navigationService, ICredentialService credentialService) 
            : base(navigationService)
        {
            Title = "Add Credential";
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.credentialService = credentialService ?? throw new ArgumentNullException(nameof(credentialService));
        }

        private readonly INavigationService navigationService;
        private readonly ICredentialService credentialService;

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
            addCredential ?? (addCredential = new DelegateCommand(() =>
            {
                var credential = new UserCredential(0, Username, Password, SiteUrl);
                credentialService.AddCredential(credential);
                navigationService.GoBackAsync();
            }));

        private DelegateCommand updateCredential;

        public DelegateCommand UpdateCredential =>
            updateCredential ?? (updateCredential = new DelegateCommand(() =>
            {
                var credential1 = new UserCredential(3, "AmmonsTestUserEditted", "AmmonsTestPasswordEditted", "http://localhost:5000");
                credentialService.UpdateCredential(credential1);
            }));

        private DelegateCommand deleteCredential;

        public DelegateCommand DeleteCredential =>
            deleteCredential ?? (deleteCredential = new DelegateCommand(() =>
            {
                var credential1 = new UserCredential(3, "AmmonsTestUserEditted", "AmmonsTestPasswordEditted", "http://localhost:5000");
                credentialService.DeleteCredential(credential1);
            }));
        
    }
}
