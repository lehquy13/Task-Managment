using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment.ViewModel.Commands;
using Task_Managment.Commands;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Globalization;
using Task_Managment.UserControls;
using Task_Managment.DataAccess;
using System.Collections.ObjectModel;
using MongoDB.Driver;
using Task_Managment.Models;

namespace Task_Managment.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private string monthyear;
        int month, year;
        public static int static_month, static_year;
        ObservableCollection<UserControlDays> userControlDays = new ObservableCollection<UserControlDays>();
        public static string static_day;
        public event PropertyChangedEventHandler PropertyChanged;
        private const string ConnectionString = "mongodb+srv://Task_Manager_Team:softintro123456@cluster0.xc1uy.mongodb.net/TestDB?retryWrites=true&w=majority";
        private const string DatabaseName = "Task_Management_Application_DB";
        private const string CalendarCollection = "Calendar";

     
        public CalendarViewModel()
        {
            InitCommands();
            displayevent();
            GetUserControlDays();
        }
        public string MonthYear
        {
            get {
                return monthyear; 
            }
            set
            {

                monthyear = value;
                OnPropertyChanged("MonthYear");
            }
        }
        public ObservableCollection<UserControlDays> UserControlDays
        {
            get {
                return userControlDays; }
            set
            {
                userControlDays = value;
                OnPropertyChanged("UserControlDays");
            }
        }
        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }
        public void displayevent()
        {
            //CalendarDataaccess db = new CalendarDataaccess();
            //DateTime a = new DateTime();
            //a = DateTime.Parse(static_month + "/"+ UserControlDays.static_day + "/" + static_year);
            //db.GetCalendar(a);
        }
        public ObservableCollection<UserControlDays> GetUserControlDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            MonthYear = monthname + " " + year.ToString();
            static_month = month;
            static_year = year;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlDays a = new UserControlDays();
                a.dayss("");
                UserControlDays.Add(a);

            }
            for (int i = 1; i <= days; i++)
            {
                UserControlDays a=new UserControlDays();
                a.days(i);
                UserControlDays.Add(a);
            }
            return UserControlDays;
        }
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public ICommand PreviousCM { get; set; }
        public ICommand NextCM { get; set; }

        private void InitCommands()
        {
            PreviousCM = new RelayCommand<RichTextBox>(p => true, p => PreviousMonth());
            NextCM = new RelayCommand<RichTextBox>(p => true, p => NextMonth());
        }
        private void PreviousMonth()
        {
            UserControlDays.Clear();
            month--;
            static_month = month;
            static_year = year;
            DateTime now = DateTime.Now;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            MonthYear = monthname + " " + year.ToString();
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlDays a = new UserControlDays();
                a.dayss("");
                UserControlDays.Add(a);
            }
            for (int i = 1; i <= days; i++)
            {
                UserControlDays a = new UserControlDays();
                a.days(i);
                UserControlDays.Add(a);
            }
        }
        private void NextMonth()
        {
            UserControlDays.Clear();
            month++;
            static_month = month;
            static_year = year;
            DateTime now = DateTime.Now;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            MonthYear = monthname + " " + year.ToString();
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlDays a = new UserControlDays();
                a.dayss("");
                UserControlDays.Add(a);
            }
            for (int i = 1; i <= days; i++)
            {
                UserControlDays a = new UserControlDays();
                a.days(i);
                UserControlDays.Add(a);
            }
        }
      
   
    }
}
