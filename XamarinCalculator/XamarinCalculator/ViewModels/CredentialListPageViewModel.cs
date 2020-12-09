using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinCalculator.Services;
using XamarinCalculator.Shared;

namespace XamarinCalculator.ViewModels
{
    public class CredentialListPageViewModel : ViewModelBase
    {
        public CredentialListPageViewModel(INavigationService navigationService, ICredentialService credentialService, ILocalDataService localDataService)
            : base(navigationService)
        {
            Title = "Saved Credentials";
            this.localDataService = localDataService ?? throw new ArgumentNullException(nameof(localDataService));
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.credentialService = credentialService ?? throw new ArgumentNullException(nameof(credentialService));

            localDataService.Initialize();
        }


        private DelegateCommand accessCredentials;

        public DelegateCommand AccessCredentials =>
            accessCredentials ?? (accessCredentials = new DelegateCommand(async () =>
            {
                try 
                {
                    Credentials = await credentialService.GetCredentialsAsync();
                    var tempCredentials = localDataService.GetCredentials();
                    if (tempCredentials == null)
                    {
                        localDataService.CreateCredentialList(Credentials);
                    }
                    else 
                    {
                        localDataService.EmptyCredentialTable();
                        localDataService.CreateCredentialList(Credentials);
                    }
                } 
                catch 
                {
                    Credentials = localDataService.GetCredentials();
                }
                
        }));

        private DelegateCommand accessLocalCredentials;

        public DelegateCommand AccessLocalCredentials =>
            accessLocalCredentials ?? (accessLocalCredentials = new DelegateCommand(async () =>
            {

                Credentials = localDataService.GetCredentials();

            }));

        private DelegateCommand addPageNavigation;

        public DelegateCommand AddPageNavigation =>
            addPageNavigation ?? (addPageNavigation = new DelegateCommand( () =>
            {
                NavigationService.NavigateAsync(nameof(Views.AddCredentialPage));
            }));

        private DelegateCommand<UserCredential> itemTappedCommand;

        public DelegateCommand<UserCredential> ItemTappedCommand => itemTappedCommand ?? (itemTappedCommand = new DelegateCommand<UserCredential>(ExecuteItemTappedCommand));

        public void ExecuteItemTappedCommand(UserCredential selectedCredential)
        {
            NavigationParameters Parameters = new NavigationParameters
            {
                { "credential", selectedCredential }
            };
            NavigationService.NavigateAsync(nameof(Views.CredentialDetailsPage), Parameters, false, true);

        }

        private readonly INavigationService navigationService;
        private readonly ICredentialService credentialService;
        private readonly ILocalDataService localDataService;
        private IEnumerable<UserCredential> credentials;

        public IEnumerable<UserCredential> Credentials
        {
            get { return credentials; }
            set { SetProperty(ref credentials, value); }
        }
    }
}
