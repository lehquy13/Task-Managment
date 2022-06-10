using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.Views;
using TrayIcon.Services;
using Task_Managment.Stores;

namespace Task_Managment.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        Members currentUser { get; set; }
        public static readonly string ImagesPath = Path.GetFullPath("imagesForWpf\\").Replace("\\bin\\Debug\\", "\\");
        public ICommand openNoteViewCommand { get; set; }

        public ICommand openCalendarCommand { get; set; }
        public ICommand openTaskViewCommand { get; set; }
        public ICommand openHomeViewCommand { get; set; }


       
        private static NotifyIcon _notifyIconInstance = null;
        public static NotifyIcon NotifyIconInstance
        {
            get
            {
                if (_notifyIconInstance == null) _notifyIconInstance = new NotifyIcon();
                return _notifyIconInstance;
            }
        }

        private static TimerStore _timerStoreInstance;
        public static TimerStore TimerStoreInstance
        {
            get
            {
                if (_timerStoreInstance == null) _timerStoreInstance = new TimerStore((new NotifyIconNotificationService(_notifyIconInstance)));
                return _timerStoreInstance;
            }
        }


        private Uri _frameSource;
        public Uri FrameSource
        {
            get { return _frameSource; }

            set
            {
                _frameSource = value;
                
                PropertyUpdated("FrameSource");
            }
        }

        public MainWindowViewModel()
        {
            currentUser = new Members("phatlam1811@gmail.com", "phatlam1811", "123");
            _notifyIconInstance = new System.Windows.Forms.NotifyIcon();
            _notifyIconInstance.Icon = new Icon(ImagesPath + "\\app.ico");
            _notifyIconInstance.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIconInstance.ContextMenuStrip.Items.Add(currentUser.Email);
            _notifyIconInstance.ContextMenuStrip.Items.Add(currentUser.UserName);
            _notifyIconInstance.Visible = true;
            init(currentUser);
        }

        private void init(Members currentUser)
        {
            FrameSource = new Uri("/Views/TaskHomeView.xaml", UriKind.Relative);
            openNoteViewCommand = new RelayCommand<Frame>(p => true, p => OpenNoteView());
            openTaskViewCommand = new RelayCommand<Frame>(p => true, p => OpenTaskView());
            openHomeViewCommand = new RelayCommand<Frame>(p => true, p => OpenHomeView());
            openCalendarCommand = new RelayCommand<Frame>(p => true, p => OpenCalendarView());
        }

        private void OpenCalendarView()
        {
            FrameSource = new Uri("/Views/Calendar.xaml", UriKind.Relative);
        }

        private void OpenNoteView()
        {
            FrameSource = new Uri("/Views/pNoteHomeView.xaml", UriKind.Relative);
        }
        private void OpenTaskView()
        {
            FrameSource = new Uri("/Views/TaskHomeView.xaml", UriKind.Relative);
        }

        private void OpenHomeView()
        {
            FrameSource = new Uri("/Views/MainHomeView.xaml", UriKind.Relative);
        }

        public MainWindowViewModel(Members members)
        {
            init(members);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void PropertyUpdated(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
