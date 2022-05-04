using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task_Managment.ViewModels;

namespace Task_Managment.Commands
{
    public class CloseSubtaskPanelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        TasksViewModel TasksViewModels { get; set; }


        //!Ctor
        public CloseSubtaskPanelCommand(TasksViewModel tasksViewModel)
        {
            this.TasksViewModels = tasksViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TasksViewModels.SubtasksPaneVisible = false;
        }
    }
}
