using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinCalculator.Shared;

namespace XamarinCalculator.API.Data
{
    public class EFCoreService : IRepository
    {
        private readonly ApplicationDBContext applicationDBContext;

        public EFCoreService(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext ?? throw new ArgumentNullException(nameof(applicationDBContext));
        }

        public async Task<IEnumerable<UserCredential>> GetCredentialList()
        {
            return await applicationDBContext.UserCredentials.ToListAsync();
        }

        public async Task<UserCredential> GetSingleCredential(int Id)
        {
            return await applicationDBContext.UserCredentials.FindAsync(Id);
        }

        public async Task AddCredentialAsync(UserCredential userCredential)
        {
            applicationDBContext.UserCredentials.Add(userCredential);
            await applicationDBContext.SaveChangesAsync();
        }

        public async Task DeleteCredentialAsync(UserCredential userCredential)
        {
            applicationDBContext.UserCredentials.Remove(userCredential);
            await applicationDBContext.SaveChangesAsync();
        }

        public async Task EditCredentialAsync(UserCredential userCredential)
        {
            applicationDBContext.UserCredentials.Update(userCredential);
            await applicationDBContext.SaveChangesAsync();
        }

        public async Task UpdateCredentialListAsync(IEnumerable<UserCredential> userCredentials)
        {
            var templist = await applicationDBContext.UserCredentials.ToListAsync();
            applicationDBContext.UserCredentials.RemoveRange(templist);
            applicationDBContext.UserCredentials.AddRange(userCredentials);
            await applicationDBContext.SaveChangesAsync();
        }
    }
}
