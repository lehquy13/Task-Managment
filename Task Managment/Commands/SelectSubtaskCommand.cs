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
    public class SelectSubtaskCommand : ICommand
    {

        //!Fields

        //!Properties
        public TasksViewModel TasksViewModel { get; set; }

        //!Events
        public event EventHandler CanExecuteChanged;

        //!Ctor
        public SelectSubtaskCommand(TasksViewModel tasksViewModel)
        {
            this.TasksViewModel = tasksViewModel;
        }

        //!Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //parameter is the TasksViewModel object
            if (parameter is Task)
            {
                Task task = (Task)parameter;
                TasksViewModel.SelectedTask = task;
            }

            return;

            ////create a new string subtask
            //Subtask newSubtask = new Subtask(this.TasksViewModel.SelectedTask.TaskID)
            //{
            //    Name = ""
            //};

            ////add the string to the observable collection
            //this.TasksViewModel.Subtasks.Add(newSubtask);

            ////add the string to the subtasks list
            //this.TasksViewModel.SelectedTask.Subtasks.Add(newSubtask);
        }
    }
}