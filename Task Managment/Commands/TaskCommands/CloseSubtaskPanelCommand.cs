using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.ViewModels;
using Task = Task_Managment.Models.Task;

namespace Task_Managment.Commands
{
    public class CloseSubtaskPanelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private DataAcessForTask db = DataAcessForTask.Instance;

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
            if (TasksViewModels.SubtasksPaneVisible == true)
                TasksViewModels.SubtasksPaneVisible = false;
            if(parameter is Task)
            {
                db.UpdateSelectedTask(parameter as Task);
            }
        }
    }
}
