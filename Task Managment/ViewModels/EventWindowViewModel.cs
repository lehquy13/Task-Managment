﻿using MongoDB.Bson;
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
using Task = Task_Managment.Models.Task;

namespace Task_Managment.ViewModels
{
    public class EventWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string textboxday;
        private string textboxevent;
        TaskDataAccess db = new TaskDataAccess();
        List<Tasklist> tasks = new List<Tasklist>();
        List<Task> calendar1 = new List<Task>();
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
        }

        public ICommand SaveCM { get; set; }
        public ICommand EditCM { get; set; }
        public ICommand DeleteCM { get; set; }


        private void initCommand()
        {
            SaveCM=new RelayCommand<Task>(p => true, p =>SaveCalendar());
            EditCM = new RelayCommand<Task>(p => true, p => UpdateCalendarAsync());
            DeleteCM = new RelayCommand<Task>(p => true, p => DeleteCalendar());
        }

        public List<MyCalendar> GetAllCalendar()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            List<MyCalendar> calendarList = collectionCalendar.AsQueryable().ToList<MyCalendar>();
            return calendarList;
        }

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
            tasks = db.GetAllTask();
            foreach (Tasklist tasklist in tasks)
            {
                if (tasklist.Name == "Calendarphatlam1811@gmail.com")
                {
                    db.CreateNewTaskToTaskList(new Task() { TasklistID=tasklist.TasklistID, Date = DateTime.Parse(TextBoxDay).AddDays(1), Notes = TextBoxEvent });
                }
            }
 
            //MongoClient client = new MongoClient("mongodb://localhost:27017");
            //IMongoDatabase database = client.GetDatabase("Task_Management");
            //IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            //MyCalendar calendar = new MyCalendar(){ Date= DateTime.Parse(TextBoxDay).AddDays(1), Note = TextBoxEvent };
            //collectionCalendar.InsertOne(calendar);
            textboxevent = "";
            OnPropertyChanged("TextBoxEvent");
            
        }
        private void UpdateCalendarAsync()
        {
           
            calendar1 = db.GetAllTasksCld();
            foreach (Task myCalendar in calendar1)
            {
                if (myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
                {
                    myCalendar.Notes = TextBoxEvent;
                    db.UpdateSelectedTask(myCalendar);
                }
            }

            //MongoClient client = new MongoClient("mongodb://localhost:27017");
            //IMongoDatabase database = client.GetDatabase("Task_Management");
            //IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            //var UpdateDef = Builders<MyCalendar>.Update.Set("Date", DateTime.Parse(TextBoxDay).AddDays(1)).Set("Note", TextBoxEvent);
            //List<MyCalendar> calendar = new List<MyCalendar>();
            //calendar = GetAllCalendar();
            //foreach(MyCalendar myCalendar in calendar)
            //{
            //    if(myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
            //    {
            //        collectionCalendar.UpdateOne(b => b.Id == myCalendar.Id, UpdateDef);
            //    }
            //}
            textboxevent = "";
            OnPropertyChanged("TextBoxEvent");
        } 
        private void DeleteCalendar()
        {
           
            calendar1 = db.GetAllTasksCld();
            foreach (Task myCalendar in calendar1)
            {
                if (myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
                {
                    db.DeleteSelectedTask(myCalendar);
                }
            }

            //MongoClient client = new MongoClient("mongodb://localhost:27017");
            //IMongoDatabase database = client.GetDatabase("Task_Management");
            //List<MyCalendar> calendar = new List<MyCalendar>();
            //calendar = GetAllCalendar();
            //IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            //foreach (MyCalendar myCalendar in calendar)
            //{
            //    if (myCalendar.Date.ToString("M/d/yyyy") == (TextBoxDay))
            //    {
            //        collectionCalendar.DeleteOne(b => b.Id == myCalendar.Id);
            //    }
            //}
            textboxevent = "";
            OnPropertyChanged("TextBoxEvent");
        }
    }
}
