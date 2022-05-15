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
        }

        public ICommand SaveCM { get; set; }

        private void initCommand()
        {
            SaveCM=new RelayCommand<MyCalendar>(p => true, p =>SaveCalendar());
        }

        private void SaveCalendar()
        {
            CalendarDataaccess db = new CalendarDataaccess();
            db.CreateCalendar(new MyCalendar() { Date = DateTime.Parse(TextBoxDay), Note =TextBoxEvent });
        }
    }
}
