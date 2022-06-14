﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment.Models;

namespace Task_Managment.DataAccess
{
    public class MemberDataAccess
    {
        #region Singleton
        private static MemberDataAccess _Instance = null;
        public static MemberDataAccess Instance
        {
            get
            {
                if (_Instance == null) _Instance = new MemberDataAccess();
                return _Instance;
            }
        }
        private MemberDataAccess() { }
        private MemberDataAccess(MemberDataAccess dt) { }
        #endregion

        private const string DataAccessKey = "mongodb+srv://Task_Manager_Team:softintro123456@cluster0.xc1uy.mongodb.net/test";
        private const string MongoDatabase = "Task_Management_Application_DB";
        private const string NotebooksCollection = "Notebooks";
        private const string NotesCollection = "Notes";
        private const string MembersCollection = "Members";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(DataAccessKey);
            var db = client.GetDatabase(MongoDatabase);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<T>> GetCollection<T>(string collection)
        {
            var _collection = ConnectToMongo<T>(collection);
            var _results = await _collection.FindAsync(_ => true);
            return _results.ToList();
        }

        public List<Members> GetMemberWithEmailAndPassword(string email, string password)
        {
            var _collection = ConnectToMongo<Members>(MembersCollection);

            var _filter = Builders<Members>.Filter.And(
                Builders<Members>.Filter.Eq("_id", email),
                Builders<Members>.Filter.Eq("Password", password));

            var _results = _collection.Find<Members>(_filter);
            return _results.ToList();
        }
    }
}