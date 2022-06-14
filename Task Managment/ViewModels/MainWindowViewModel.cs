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
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace Task_Managment.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        Members currentUser { get; set; }

        public static readonly string ImagesPath = Path.GetFullPath("imagesForWpf").Replace("\\bin\\Debug\\", "\\");
        public ICommand openNoteViewCommand { get; set; }
        public ICommand openCalendarCommand { get; set; }
        public ICommand openTaskViewCommand { get; set; }
        public ICommand openHomeViewCommand { get; set; }
        public ICommand openNotebookViewCommand { get; set; }
        public ICommand onCloseCommand { get; set; }
        public ICommand onMinimizeCommand { get; set; }

        private static NotifyIcon _notifyIconInstance = null;
        public static NotifyIcon NotifyIconInstance
        {
            get
            {
                if (_notifyIconInstance == null) _notifyIconInstance = new NotifyIcon();
                return _notifyIconInstance;
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
            StartWindowViewModel wdvm = new StartWindowViewModel();

            if (wdvm.IsUserLoggedIn())
            {
                // currentUser = new Members("phatlam1811@gmail.com", "phatlam1811", "123");
                currentUser = new Members(wdvm.getCurrentUser());
            }
            else currentUser = new Members("guest@gmail.com", "Guest", "123");


            _notifyIconInstance = new System.Windows.Forms.NotifyIcon();
            Uri iconUri = new Uri("pack://application:,,,/app.ico", UriKind.RelativeOrAbsolute);
            //string temp = ImagesPath + "/app.ico";
            _notifyIconInstance.Icon = new Icon(Path.Combine(System.Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\imagesForWpf\\"), "app.ico"));
            _notifyIconInstance.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIconInstance.ContextMenuStrip.Items.Add("Open");
            _notifyIconInstance.ContextMenuStrip.Items.Add(currentUser.UserName);
            _notifyIconInstance.Visible = true;
            _notifyIconInstance.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            init(currentUser);
        }

        private void init(Members currentUser)
        {
            FrameSource = new Uri("/Views/MainHomeView.xaml", UriKind.Relative);
            openNoteViewCommand = new RelayCommand<Frame>(p => true, p => OpenNoteView());
            openTaskViewCommand = new RelayCommand<Frame>(p => true, p => OpenTaskView());
            openHomeViewCommand = new RelayCommand<Frame>(p => true, p => OpenHomeView());
            openCalendarCommand = new RelayCommand<Frame>(p => true, p => OpenCalendarView());
            openNotebookViewCommand = new RelayCommand<Frame>(p => true, p => OpenNotebookView());
            onCloseCommand = new RelayCommand<Window>(p => true, p => Dispose());
            //onMinimizeCommand = new RelayCommand<Window>(p => true, p => onClose());
            //onClose = new RelayCommand<Frame>(p => true, p => Dispose());
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Application is running.", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #region button methods
        private void OpenCalendarView()
        {
            FrameSource = new Uri("/Views/Calendar.xaml", UriKind.Relative);
        }

        private void OpenNoteView()
        {
            FrameSource = new Uri("/Views/pNoteHomeView.xaml", UriKind.Relative);
        }

        private void OpenNotebookView()
        {
            FrameSource = new Uri("/Views/pNotebookHomeView.xaml", UriKind.Relative);
        }

        private void OpenTaskView()
        {
            FrameSource = new Uri("/Views/TaskHomeView.xaml", UriKind.Relative);
        }

        private void OpenHomeView()
        {
            FrameSource = new Uri("/Views/MainHomeView.xaml", UriKind.Relative);
        }
        #endregion
        public MainWindowViewModel(Members members)
        {
            init(members);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void PropertyUpdated(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnClose(Window p)
        {
            if(App.Current.MainWindow.WindowState == WindowState.Minimized)
                App.Current.MainWindow.Hide();
          

        }
            }

        }

        public void Dispose()
        {
            _notifyIconInstance.Dispose();
        }
    }
}
