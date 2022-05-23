using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment.DataAccess;
using Task_Managment.Views;
using Task_Managment.UserControls;
using MongoDB.Driver;
using Task_Managment.Models;

namespace Task_Managment.ViewModels
{
    public class UCdayViewModel : INotifyPropertyChanged
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public event PropertyChangedEventHandler PropertyChanged;
        private string Labeldays;
        private string Labelevents;
    
        public void test()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
       
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            displayevent();
        }

        public string LabelDay
        {
            get { return Labeldays; }
            set
            {
                Labeldays = value;
                OnPropertyChanged("LabelDay");
            }
        }
        public string LabelEvent
        {
            get { return Labelevents; }
            set
            {
                Labelevents = value;
                OnPropertyChanged("LabelEvent");
            }
        }
        public List<MyCalendar> GetAllCalendar()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            List<MyCalendar> calendarList = collectionCalendar.AsQueryable().ToList<MyCalendar>();
            return calendarList;
        }
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public void displayevent()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Task_Management");
            IMongoCollection<MyCalendar> collectionCalendar = database.GetCollection<MyCalendar>("Calendar");
            List<MyCalendar> calendar = new List<MyCalendar>();
            calendar = GetAllCalendar();
            foreach (MyCalendar myCalendar in calendar)
            {
                if (CalendarViewModel.static_month.ToString() + "/" + UserControlDays.static_day + "/" + CalendarViewModel.static_year.ToString() == myCalendar.Date.ToString("M/d/yyyy")){
                   LabelEvent = myCalendar.Note;
                   OnPropertyChanged("LabelEvent");
                }
            }
        }
        public void mouseleft()
        {
            test();
            eventwindow e_form = new eventwindow();
            e_form.ShowDialog();
        }

    }
}
