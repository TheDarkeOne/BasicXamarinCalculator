using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinCalculator.Shared;

namespace XamarinCalculator.API.Data
{
    public class IMRepository : IRepository
    {
        public List<UserCredential> credentialList;

        public IMRepository()
        {
            credentialList = new List<UserCredential>();
            var credential1 = new UserCredential(credentialList.Count + 1, "TestUser1", "GenericPassword", "https://github.com/TheDarkeOne/CalenderSchedule.git");
            credentialList.Add(credential1);
            var credential2 = new UserCredential(credentialList.Count + 1, "TestUser2", "GenericPassword", "https://github.com/TheDarkeOne/CalenderSchedule.git");
            credentialList.Add(credential2);
            var credential3 = new UserCredential(credentialList.Count + 1, "TestUser3", "GenericPassword", "https://github.com/TheDarkeOne/CalenderSchedule.git");
            credentialList.Add(credential3);
            var credential4 = new UserCredential(credentialList.Count + 1, "TestUser4", "GenericPassword", "https://github.com/TheDarkeOne/CalenderSchedule.git");
            credentialList.Add(credential4);
            var credential5 = new UserCredential(credentialList.Count + 1, "TestUser5", "GenericPassword", "https://github.com/TheDarkeOne/CalenderSchedule.git");
            credentialList.Add(credential5);
        }

        public async Task AddCredentialAsync(UserCredential userCredential)
        {
            userCredential.Id = (credentialList.Count + 1);
            credentialList.Add(userCredential);
        }

        public async Task DeleteCredentialAsync(UserCredential userCredential)
        {
            credentialList.Remove(userCredential);
        }

        public async Task EditCredentialAsync(UserCredential userCredential)
        {
            credentialList.Remove(credentialList[0]);
            credentialList.Add(userCredential);
        }

        public async Task<IEnumerable<UserCredential>> GetCredentialList()
        {
            return credentialList;
        }

        public async Task<UserCredential> GetSingleCredential(int Id)
        {
            return credentialList.Where(u => u.Id == Id).FirstOrDefault();
        }

        public Task UpdateCredentialListAsync(IEnumerable<UserCredential> userCredentials)
        {
            throw new NotImplementedException();
        }
    }
}
