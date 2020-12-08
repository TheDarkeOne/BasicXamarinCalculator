using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XamarinCalculator.API.Data;
using XamarinCalculator.Shared;

namespace XamarinCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialsController : Controller
    {
        private readonly IRepository repository;

        public CredentialsController(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet("[action]")]
        public UserCredential GetDefaultCredential()
        {

            var user = new UserCredential();
            user.Id = 1;
            user.Username = "AmmonZerkle";
            user.Password = "FakePassword";
            user.SiteUrl = "http://Localhost:5000";

            return user;
        }


        [HttpGet("[action]")]
        public async Task<IEnumerable<UserCredential>> GetAllCredentials()
        {
            return await repository.GetCredentialList();
        }


        [HttpGet("[action]/{Id}")]
        public async Task<UserCredential> GetSingleCredential(int Id)
        {
            return await repository.GetSingleCredential(Id);
        }

        [HttpPost("[action]")]
        public async Task CreateCredential([FromBody] UserCredential userCredential)
        {
            await repository.AddCredentialAsync(userCredential);
        }

        [HttpPost("[action]")]
        public async Task UpdateCredential([FromBody] UserCredential userCredential)
        {
            await repository.EditCredentialAsync(userCredential);
        }

        [HttpPost("[action]")]
        public async Task DeleteCredential([FromBody] UserCredential userCredential)
        {
            await repository.DeleteCredentialAsync(userCredential);
        }
    }
}
