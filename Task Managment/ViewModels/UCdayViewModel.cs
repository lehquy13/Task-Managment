using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Managment.ViewModels
{
    public class UCdayViewModel : INotifyPropertyChanged
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public event PropertyChangedEventHandler PropertyChanged;
        private string Labeldays;
        private string Labelevents;
    
        private void test()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
       
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //displayevent();
        }
        public void days(int numday)
        {
            LabelDay = numday + "";
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
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
