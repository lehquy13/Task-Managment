using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Task_Managment.ViewModels;

namespace Task_Managment.ViewModel.Commands
{
    public class NotifyCommand : ICommand
    {
        private readonly NotifyIcon _notifyIcon;
        TasksViewModel TasksViewModels { get; set; }
        public static readonly string ImagesPath = Path.GetFullPath("imagesForWpf").Replace("\\bin\\Debug\\", "\\");
        public NotifyCommand(TasksViewModel tasksViewModel)
        {
            _notifyIcon =  new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = new Icon(ImagesPath+"\\info.ico");

            //_notifyIcon.Visible = true;

          
            this.TasksViewModels = tasksViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _notifyIcon.ShowBalloonTip(3000, this.TasksViewModels.SelectedTask.Name, this.TasksViewModels.SelectedTask.Expiretime.ToString(), ToolTipIcon.Info);
        }

        private void OnStatusClicked(object sender, EventArgs e)
        {
            string status = "Timer is running Timer is stopped.";
            System.Windows.MessageBox.Show(status, "Status", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
