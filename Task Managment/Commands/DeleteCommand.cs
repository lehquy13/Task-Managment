﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.ViewModels;
using Task = Task_Managment.Models.Task;

namespace Task_Managment.ViewModel.Commands
{
    public class DeleteCommand : ICommand
    {
        //!Fields

        //!Properties
        TasksViewModel TasksViewModels { get; set; }

        //!Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //!Ctor
        public DeleteCommand(TasksViewModel tasksViewModel)
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
            if(parameter is Task)
            {
                Task selectedTask = this.TasksViewModels.SelectedTask;

                //remove it from the observable collection if its not null
                this.TasksViewModels.TasksList?.Remove(selectedTask);
                //and remove selected task from task list if its not null
                this.TasksViewModels.SelectedTasklist.Tasks?.Remove(selectedTask);

                //update the Totalcount
                this.TasksViewModels.SelectedTasklist.TotalCount =
                        this.TasksViewModels.SelectedTasklist.Tasks.Count.ToString();
            }

            else if(parameter is Tasklist)
            {
                Tasklist selectedTasklist = this.TasksViewModels.SelectedTasklist;

                this.TasksViewModels.TripleDefaultTaskList?.Remove(selectedTasklist);
            } 
        }
    }
}
