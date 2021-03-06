﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using ShitterTwitter.Common.Objects;
using ShitterTwitter.DAL;

namespace ShitterTwitter.Common.DAL
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

        public async Task AddMessage(IShitterTwitterMessage toAdd)
        {
            toAdd.id = Guid.NewGuid().ToString();
            toAdd.DateAdded = DateTime.Now.ToString("O");
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


        public IShitterTwitterMessage GetMessageToTweet()
        {
            Random rand = new Random();
            string query = "SELECT * " +
                           "From Tweets t " +
                           "Where t.DateLastUsed = null";
            List<IShitterTwitterMessage> results = new List<IShitterTwitterMessage>();

            var vars =
                _client.CreateDocumentQuery<ShitterTwitterMessage>("dbs/" + _database.Id + "/colls/" + _collection.Id,
                    query).ToList();
            results.AddRange(vars);

            int max = results.Count ;
            if (max <1)
            {
                results = GetAllShitterMessages();
                max = results.Count;
            }


            int randnum = rand.Next(0, max);
            var returnVal = results[randnum]; 
            //Mar as red 
            returnVal.DateLastUsed = DateTime.Now.ToString("o");
            UpdateMessage(returnVal);
            return returnVal;
        }


        public void UpdateMessage(IShitterTwitterMessage toUpdate)
        {
            var doc = GetDocument(toUpdate.id);

           _client.ReplaceDocumentAsync(doc.SelfLink,
                toUpdate).Wait();
        }

        public IShitterTwitterMessage GetByID(string id)
        {
            return _client.CreateDocumentQuery<IShitterTwitterMessage>("dbs/" + _database.Id + "/colls/" + _collection.Id)
                    .Where(e => e.id == id)
                    .AsEnumerable()
                    .First();
        }


        private  Document GetDocument( string id)
        {
            return _client.CreateDocumentQuery("dbs/" + _database.Id + "/colls/" + _collection.Id)
                   .Where(e => e.Id == id)
                   .AsEnumerable()
                   .First();
        }

        public  List<IShitterTwitterMessage> GetAllShitterMessages()
        {
            string query = "SELECT * " +
                           "FROM Tweets T ";
            List<IShitterTwitterMessage> returnVal = new List<IShitterTwitterMessage>();

            var resuslts =
                _client.CreateDocumentQuery<ShitterTwitterMessage>("dbs/" + _database.Id + "/colls/" + _collection.Id,
                    query).ToList();
            returnVal.AddRange(resuslts);
            return returnVal;
        }
    }
}