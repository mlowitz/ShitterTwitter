using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using ShitterTwitter.Common.Objects;

namespace ShitterTwitter.DAL
{
    public class DatabaseManeger :IDatabaseManeger
    {
        private const string _collectionString = "ShitterTwitterMessages";
        private const string _dataBaseString = "ShitterTwitter";
        private readonly string _endPoint = ConfigurationManager.AppSettings["EndPoint"];
        private readonly string _primaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
        private DocumentClient _client;
        private Database _database;
        private DocumentCollection _collection;

        public DatabaseManeger()
        {
            _client = new DocumentClient(new Uri(_endPoint), _primaryKey);
            _database =
                _client.CreateDatabaseQuery().Where(db => db.Id == _dataBaseString).AsEnumerable().FirstOrDefault();
            _collection =
                _client.CreateDocumentCollectionQuery("dbs/" + _database.Id)
                    .Where(c => c.Id == _collectionString)
                    .AsEnumerable()
                    .FirstOrDefault();
            if (_collection == null)
            {
                AddCollection();
            }
        }


        public async void AddCollection()
        {
            _collection = await _client.CreateDocumentCollectionAsync("dbs/" + _database.Id,
                    new DocumentCollection
                    {
                        Id = _collectionString
                    });

        }

        public async void AddMessage(IShitterTwitterMessage toAdd)
        {
            toAdd.id = Guid.NewGuid().ToString();

            var document =
                _client.CreateDocumentQuery("dbs/" + _database.Id + "/colls/" + _collection.Id)
                    .Where(d => d.Id == toAdd.id)
                    .AsEnumerable()
                    .FirstOrDefault();

            if (document == null)
            {
                try
                {
                    await _client.CreateDocumentAsync("dbs/" + _database.Id + "/colls/" + _collection.Id, toAdd);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }


     
        public  List<IShitterTwitterMessage> GetAllShitterMessages()
        {
            string query = "SELECT * " +
                           "FROM Tweets T ";
            var resuslts =
                _client.CreateDocumentQuery<IShitterTwitterMessage>("dbs/" + _database.Id + "/colls/" + _collection.Id,
                    query).ToList();

            return resuslts;
        }
    }
}