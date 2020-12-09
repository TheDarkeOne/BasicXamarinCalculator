using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinCalculator.Shared;

namespace XamarinCalculator.Services
{
   

    public interface ICredentialService
    {
        [Get("/credentials/GetAllCredentials")]
        Task<IEnumerable<UserCredential>> GetCredentialsAsync();
        [Get("/credentials/GetSingleCredential/{Id}")]
        Task<UserCredential> GetCredentialsByIdAsync(int Id);
        [Post("/credentials/CreateCredential")]
        Task AddCredential(UserCredential userCredential);
        
        [Post("/credentials/UpdateCredential")]
        Task UpdateCredential(UserCredential userCredential);
        
        [Post("/credentials/DeleteCredential")]
        Task DeleteCredential(UserCredential userCredential);
        
        [Post("/credentials/UpdateCredentialList")]
        Task UpdateCredentialList(IEnumerable<UserCredential> userCredential);
    }

}
