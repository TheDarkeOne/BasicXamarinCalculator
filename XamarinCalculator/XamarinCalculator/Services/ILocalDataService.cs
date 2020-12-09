using System.Collections.Generic;
using System.Text;
using XamarinCalculator.Shared;

namespace XamarinCalculator.Services
{
    public interface ILocalDataService
    {
        void Initialize();

        IEnumerable<UserCredential> GetCredentials();

        void CreateCredentialList(IEnumerable<UserCredential> userCredential);

        void EmptyCredentialTable();
    }
}
