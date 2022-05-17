using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment.ViewModels;

namespace Task_Managment.Models
{
    public class DataAcessForTask 
    {
        #region framework
        #region Singleton
        private static DataAcessForTask _Instance = null;
        public static DataAcessForTask Instance
        {
            get
            {
                if (_Instance == null) _Instance = new DataAcessForTask();
                return _Instance;
            }
        }
        public DataAcessForTask() { }
        public DataAcessForTask(DataAcessForTask dt) { }
        #endregion

        private const string DataAccessKey = "mongodb+srv://Task_Manager_Team:softintro123456@cluster0.xc1uy.mongodb.net/test";
        private const string MongoDatabase = "Task_Management_Application_DB";
        private const string TasksCollection = "Tasks";
        private const string SubtasksCollection = "SubTasks";
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
        #endregion

        //get list of tasks (means many task of user)
        public List<Task> GetAllTaskOfMember(Members currentMember)
        {
            var _collection = ConnectToMongo<Task>(TasksCollection);
            var _results = _collection.Find<Task>(c => c.MemberId == currentMember.Email);
            return _results.ToList();
        }

        //get list of subtasks (means many subtasks of that task of that user)
        public List<Subtask> GetAllSubTasksFromTask(Task selectedTask)
        {
            var _collection = ConnectToMongo<Subtask>(SubtasksCollection);
            var _results = _collection.Find<Subtask>(
                c => c.TaskID == selectedTask.TaskID
            );
            return _results.ToList();
        }

        //create new task to the created TaskList 
        public System.Threading.Tasks.Task CreateNewTaskToTaskList(Task newTask)
        {
            var _collection = ConnectToMongo<Task>(TasksCollection);
            return _collection.InsertOneAsync(newTask);
        }

        public System.Threading.Tasks.Task UpdateSelectedNote(Task selectedTask)
        {
            var _collection = ConnectToMongo<Task>(TasksCollection);
            var _filter = Builders<Task>.Filter.Eq("_id", selectedTask.TaskID);
            return _collection.ReplaceOneAsync(_filter, selectedTask, new ReplaceOptions { IsUpsert = true });
        }

        public System.Threading.Tasks.Task DeleteSelectedNote(Task selectedTask)
        {
            var _collection = ConnectToMongo<Task>(TasksCollection);
            return _collection.DeleteOneAsync(c => c.TaskID == selectedTask.TaskID);
        }


        //create new subtask to the created TaskList 
        public System.Threading.Tasks.Task CreateNewSubTaskToTaskList(Subtask newSubTask)
        {
            var _collection = ConnectToMongo<Subtask>(SubtasksCollection);
            return _collection.InsertOneAsync(newSubTask);
        }

        public System.Threading.Tasks.Task UpdateSelectedSubTask(Subtask selectedSubTask)
        {
            var _collection = ConnectToMongo<Subtask>(SubtasksCollection);
            var _filter = Builders<Subtask>.Filter.Eq("_id", selectedSubTask.SubtaskID);
            return _collection.ReplaceOneAsync(_filter, selectedSubTask, new ReplaceOptions { IsUpsert = true });
        }

        public System.Threading.Tasks.Task DeleteSelectedSubTask(Subtask selectedSubTask)
        {
            var _collection = ConnectToMongo<Subtask>(SubtasksCollection);
            return _collection.DeleteOneAsync(c => c.SubtaskID == selectedSubTask.SubtaskID);
        }


    }
}
