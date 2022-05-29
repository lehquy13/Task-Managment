using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task_Managment.DataAccess;
using Task_Managment.Models;
using Task_Managment.UserControls;
using Task_Managment.Views;

namespace Task_Managment.ViewModels
{
    public class EventWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string textboxday;
        private string textboxevent;
        public string TextBoxDay
        {
            get { return textboxday; }
            set
            {
                textboxday = value;
                OnPropertyChanged("TextBoxDay");
            }
        }
        public string TextBoxEvent
        {
            get { return textboxevent; }
            set
            {
                textboxevent = value;
                OnPropertyChanged("TextBoxEvent");
            }
        }
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public EventWindowViewModel() 
        {
            initCommand();
            if(TextBoxDay != null)
            {
                GetCalendar();
            }
        }

        public ICommand SaveCM { get; set; }
        public ICommand EditCM { get; set; }
        public ICommand DeleteCM { get; set; }


        private void initCommand()
        {
            SaveCM=new RelayCommand<MyCalendar>(p => true, p =>SaveCalendar());
            EditCM = new RelayCommand<MyCalendar>(p => true, p => UpdateCalendarAsync());
            DeleteCM = new RelayCommand<MyCalendar>(p => true, p => DeleteCalendar());
        }

        public List<MyCalendar> GetAllCalendar()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            List<MyCalendar> calendarList = collectionCalendar.AsQueryable().ToList<MyCalendar>();
            return calendarList;
        }
        // không thể show được do màn hình chưa show lên nên textboxday đã bị null =)))
        public void GetCalendar()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            List<MyCalendar> calendar = new List<MyCalendar>();
            calendar = GetAllCalendar();
            foreach (MyCalendar myCalendar in calendar)
            {
                if (myCalendar.Date.ToString("M/d/yyyy") == TextBoxDay)
                {
                    TextBoxEvent = myCalendar.Note;
                }
            }
        }
        private void SaveCalendar()
        {
            CalendarDataaccess db = new CalendarDataaccess();
            db.CreateCalendar(new MyCalendar() { Date = DateTime.Parse(TextBoxDay).AddDays(1), Note =TextBoxEvent });
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            MyCalendar calendar = new MyCalendar(){ Date= DateTime.Parse(TextBoxDay).AddDays(1), Note = TextBoxEvent };
            collectionCalendar.InsertOne(calendar);
            eventwindow a=new eventwindow();
            a.Close();
            
        }
        private void UpdateCalendarAsync()
        {
            CalendarDataaccess db = new CalendarDataaccess();
            List<MyCalendar> calendar1 = new List<MyCalendar>();
            calendar1 = db.GetAllCalendar();
            foreach (MyCalendar myCalendar in calendar1)
            {
                if (myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
                {
                    myCalendar.Note = TextBoxEvent;
                    db.UpdateCalendar(myCalendar);
                }
            }

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            var UpdateDef = Builders<MyCalendar>.Update.Set("Date", DateTime.Parse(TextBoxDay).AddDays(1)).Set("Note", TextBoxEvent);
            List<MyCalendar> calendar = new List<MyCalendar>();
            calendar = GetAllCalendar();
            foreach(MyCalendar myCalendar in calendar)
            {
                if(myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
                {
                    collectionCalendar.UpdateOne(b => b.Id == myCalendar.Id, UpdateDef);
                }
            }
        } 
        private void DeleteCalendar()
        {
            CalendarDataaccess db = new CalendarDataaccess();
            List<MyCalendar> calendar1 = new List<MyCalendar>();
            calendar1 = db.GetAllCalendar();
            foreach (MyCalendar myCalendar in calendar1)
            {
                if (myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
                {
                    db.DeleteCalendar(myCalendar);
                }
            }

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            List<MyCalendar> calendar = new List<MyCalendar>();
            calendar = GetAllCalendar();
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            foreach (MyCalendar myCalendar in calendar)
            {
                if (myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
                {
                    collectionCalendar.DeleteOne(b => b.Id == myCalendar.Id);
                }
            }
        }
    }
}
