using SQLite;
using System;
using System.Text.Json.Serialization;

namespace XamarinCalculator.Shared
{
    public class UserCredential
    {
        public UserCredential()
        {
            Id = 0;
            Username = "Default";
            Password = "Default";
            SiteUrl = "http://localhost:5000";
        }

        public UserCredential(int Id, string Username, string Password, string SiteUrl)
        {
            this.Id = Id;
            this.Username = Username;
            this.Password = Password;
            this.SiteUrl = SiteUrl;
        }
        [PrimaryKey]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string SiteUrl { get; set; }
    }
}
