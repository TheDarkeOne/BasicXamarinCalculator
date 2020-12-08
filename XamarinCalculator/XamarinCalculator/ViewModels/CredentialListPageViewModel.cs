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
        public CredentialListPageViewModel(INavigationService navigationService, ICredentialService credentialService)
            : base(navigationService)
        {
            Title = "Saved Credentials";
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.credentialService = credentialService ?? throw new ArgumentNullException(nameof(credentialService));
        }


        private DelegateCommand accessCredentials;

        public DelegateCommand AccessCredentials =>
            accessCredentials ?? (accessCredentials = new DelegateCommand(async () =>
            {
                Credentials = await credentialService.GetCredentialsAsync();
        }));

        private DelegateCommand addPageNavigation;

        public DelegateCommand AddPageNavigation =>
            addPageNavigation ?? (addPageNavigation = new DelegateCommand( () =>
            {
                NavigationService.NavigateAsync(nameof(Views.AddCredentialPage));
            }));

        private readonly INavigationService navigationService;
        private readonly ICredentialService credentialService;
        private IEnumerable<UserCredential> credentials;

        public IEnumerable<UserCredential> Credentials
        {
            get { return credentials; }
            set { SetProperty(ref credentials, value); }
        }
    }
}
