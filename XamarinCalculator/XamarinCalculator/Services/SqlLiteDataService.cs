using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using XamarinCalculator.Shared;

namespace XamarinCalculator.Services
{
    public class SqlLiteDataService : ILocalDataService
    {
        private SQLiteConnection _database;

        public void CreateCredentialList(IEnumerable<UserCredential> userCredentials)
        {
            _database.InsertAll(userCredentials);
        }

        public void EmptyCredentialTable()
        {
            _database.Execute("Delete From UserCredential");
        }

        public IEnumerable<UserCredential> GetCredentials()
        {
            return _database.Table<UserCredential>().ToList();
        }

        public void Initialize()
        {
            if (_database == null) 
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CredentialAppDb.db3");
                _database = new SQLiteConnection(dbPath);
            }
            _database.CreateTable<UserCredential>();
        }
    }
}
