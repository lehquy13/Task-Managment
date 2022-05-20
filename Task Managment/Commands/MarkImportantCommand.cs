using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.ViewModels;

namespace Task_Managment.ViewModel.Commands
{
    public class MarkImportantCommand : ICommand
    {
        private DataAcessForTask db = DataAcessForTask.Instance;


        //!Properties
        public TasksViewModel TasksViewModel { get; set; }

        //!Ctor
        public MarkImportantCommand(TasksViewModel tasksViewModel)
        {
            this.TasksViewModel = tasksViewModel;
        }

        //!Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //!Methods
        public bool CanExecute(object parameter)
        {
            //you can only mark important if the task you are trying to mark important == selected task
            if (parameter is string)
            {
                if (this.TasksViewModel.SelectedTask != null)
                {
                    if (parameter as string == this.TasksViewModel.SelectedTask.Name)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public void Execute(object parameter)
        {
            Tasklist importantList = this.TasksViewModel.DefaultImportantList;

            if (this.TasksViewModel.SelectedTask.Important == false)
            {
                this.TasksViewModel.SelectedTask.Important = true;             

                //if the important list doesn't contain this task, add it
                if (importantList.Tasks.Contains(this.TasksViewModel.SelectedTask) == false)
                {
                    importantList.Tasks.Add(this.TasksViewModel.SelectedTask);

                    //update the task counter on the important list
                    importantList.TotalCount = importantList.Tasks.Count.ToString();
                }
            }
            else
            {
                this.TasksViewModel.SelectedTask.Important = false;

                //if the important list does contain this task, Remove it
                if (importantList.Tasks.Contains(this.TasksViewModel.SelectedTask) == true)
                {
                    importantList.Tasks.Remove(this.TasksViewModel.SelectedTask);

                    //if you are in the important list then also remove from the general tasklist collection
                    if(this.TasksViewModel.SelectedTasklist == importantList)
                    {
                        this.TasksViewModel.TasksList.Remove(this.TasksViewModel.SelectedTask);
                    }

                    //update the task counter on the important list
                    importantList.TotalCount = importantList.Tasks.Count.ToString();
                }
            }
            db.UpdateSelectedTasklist(importantList);
            db.UpdateSelectedTask(this.TasksViewModel.SelectedTask);
            //db.UpdateSelectedTasklist(importantList); because we dont do about 3 default list, so just ignore them
        }
    }
}
