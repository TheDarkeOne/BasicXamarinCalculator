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

        private DelegateCommand addCredential;

        public DelegateCommand AddCredential =>
            addCredential ?? (addCredential = new DelegateCommand(() =>
            {
                var credential = new UserCredential(0, "AmmonsTestUser", "AmmonsTestPassword", "http://localhost:5000");
                credentialService.AddCredential(credential);
            }));

        private DelegateCommand updateCredential;

        public DelegateCommand UpdateCredential =>
            updateCredential ?? (updateCredential = new DelegateCommand(() =>
            {
                var credential1 = new UserCredential(1, "TestUser1Editted", "GenericPasswordEditted", "https://github.com/TheDarkeOne/CalenderSchedule.git");
                credentialService.UpdateCredential(credential1);
            }));

        private DelegateCommand deleteCredential;

        public DelegateCommand DeleteCredential =>
            deleteCredential ?? (deleteCredential = new DelegateCommand(() =>
            {
                var credential1 = new UserCredential(1, "TestUser1", "GenericPassword", "https://github.com/TheDarkeOne/CalenderSchedule.git");
                credentialService.DeleteCredential(credential1);
            }));
        
    }
}
