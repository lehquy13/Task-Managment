using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.ViewModels;
using Task_Managment.Views;
using Task = Task_Managment.Models.Task;

namespace Task_Managment.Commands.TaskCommands
{
    public class PickTaskIconCommand : ICommand
    {


        //!Fields
        private DataAcessForTask db = DataAcessForTask.Instance; // 1.

        //!Properties
        TasksViewModel TasksViewModels { get; set; }

        //!Events
        public event EventHandler CanExecuteChanged;
       

        //!Ctor
        public PickTaskIconCommand(TasksViewModel tasksViewModel)
        {
            this.TasksViewModels = tasksViewModel;
        }

        //!Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PickingTaskIcon pickTaskIconWindow = new PickingTaskIcon();
            pickTaskIconWindow.Show();
        }
    }

}
