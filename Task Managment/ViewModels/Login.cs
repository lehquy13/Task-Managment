using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using Task_Managment.Models;
using Task_Managment.Views;

namespace Task_Managment.ViewModels
{
    internal class Login
    {
        private const string ConnectionString = "mongodb+srv://Task_Manager_Team:softintro123456@cluster0.xc1uy.mongodb.net/TestDB?retryWrites=true&w=majority";
        private const string DatabaseName = "TestDB";
        private const string MembersCollection = "Members";

        public string connectionString { get; }
        public string databaseName { get; }
        public string membersCollection { get; }

        public IMongoCollection<T> ConnectToMongo<T>(string collection)
        {
            // Configurate MongoDB Cloud Connection
            var settings = MongoClientSettings.FromConnectionString(ConnectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(DatabaseName);

            return database.GetCollection<T>(collection);
        }

        public void Log_in(string email, string password)
        {
            var collection = ConnectToMongo<Members>(MembersCollection);
            var memberList = collection.Find<Members>(c => c.Email == email).ToList();

            if (memberList.Count > 0 && memberList[0].Password == password)
            {
                wndTaskHomeView newWindow = new wndTaskHomeView();
                newWindow.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Wrong email or password!");
            }
        }
    }
}
