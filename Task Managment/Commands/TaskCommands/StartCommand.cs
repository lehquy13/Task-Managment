using System;
using System.Collections.Generic;
using System.Text;
using Task_Managment.Stores;
using Task_Managment.ViewModels;
using System.Windows.Input;
using System.IO;
using System.Windows.Forms;

namespace Task_Managment.Commands
{
    public class StartCommand : ICommand
    {
        TasksViewModel TasksViewModels { get; set; }

        private readonly NotifyIcon _notifyIcon;

        public static readonly string ImagesPath = Path.GetFullPath("imagesForWpf").Replace("\\bin\\Debug\\", "\\");
        private readonly TimerStore _timerStore; 
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public StartCommand(TasksViewModel tasksViewModel)
        {
            _notifyIcon = MainWindowViewModel.NotifyIconInstance;
            _timerStore = MainWindowViewModel.TimerStoreInstance;
            this.TasksViewModels = tasksViewModel;
        }

        public  void Execute(object parameter)
        {
            _timerStore.Start(TasksViewModels.Duration);
        }
    }
}
