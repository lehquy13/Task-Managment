using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.Views;

namespace Task_Managment.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        Members currentUser { get; set; }
        public ICommand openNoteViewCommand { get; set; }
        public ICommand openTaskViewCommand { get; set; }
        public ICommand openHomeViewCommand { get; set; }

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

            init(currentUser);
        }

        private void init(Members currentUser)
        {
            FrameSource = new Uri("/Views/TaskHomeView.xaml", UriKind.Relative);
            openNoteViewCommand = new RelayCommand<Frame>(p => true, p => OpenNoteView());
            openTaskViewCommand = new RelayCommand<Frame>(p => true, p => OpenTaskView());
            openHomeViewCommand = new RelayCommand<Frame>(p => true, p => OpenHomeView());
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
            FrameSource = null;
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
