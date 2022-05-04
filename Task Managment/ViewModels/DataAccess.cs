using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using wndNoteView.Models;

namespace wndNoteView.ViewModels
{
    public class DataAccess
    {
        #region Singleton
        private static DataAccess _Instance = null;
        public static DataAccess Instance
        {
            get
            {
                if (_Instance == null) _Instance = new DataAccess();
                return _Instance;
            }
        }
        private DataAccess() { }
        private DataAccess(DataAccess dt) { }
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

        #region Notes_Data_Access
        public List<Note> GetAllNotesOfMember(Users currentMember)
        {
            var _collection = ConnectToMongo<Note>(NotesCollection);
            var _results = _collection.Find<Note>(c => c.MemberId == currentMember.Email);
            return _results.ToList();
        }

        public List<Note> GetAllNotesFromNotebook(NotebookModel selectedNotebook)
        {
            var _collection = ConnectToMongo<Note>(NotesCollection);
            var _results = _collection.Find<Note>(c => c.NotebookId == selectedNotebook._id);
            return _results.ToList();
        }

        public Task CreateNewNote(Note newNote)
        {
            var _collection = ConnectToMongo<Note>(NotesCollection);
            return _collection.InsertOneAsync(newNote);
        }

        public Task UpdateSelectedNote(Note selectedNote)
        {
            var _collection = ConnectToMongo<Note>(NotesCollection);
            var _filter = Builders<Note>.Filter.Eq("_id", selectedNote.Id);
            return _collection.ReplaceOneAsync(_filter, selectedNote, new ReplaceOptions { IsUpsert = true });
        }

        public Task DeleteSelectedNote(Note selectedNote)
        {
            var _collection = ConnectToMongo<Note>(NotesCollection);
            return _collection.DeleteOneAsync(c => c.Id == selectedNote.Id);
        }
        #endregion
    }
}
