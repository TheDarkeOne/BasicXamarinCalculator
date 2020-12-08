using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinCalculator.Shared;

namespace XamarinCalculator.API.Data
{
    public interface IRepository
    {
        Task<IEnumerable<UserCredential>> GetCredentialList();
        Task<UserCredential> GetSingleCredential(int Id);
        Task AddCredentialAsync(UserCredential userCredential);
        Task EditCredentialAsync(UserCredential userCredential);
        Task DeleteCredentialAsync(UserCredential userCredential);
    }
}
