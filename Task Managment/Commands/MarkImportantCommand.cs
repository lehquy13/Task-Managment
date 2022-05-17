﻿using System;
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
                if (this.TasksViewModel.SelectedSubtask != null)
                {
                    if (parameter as string == this.TasksViewModel.SelectedSubtask.Name)
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

            if (this.TasksViewModel.SelectedSubtask.Important == false)
            {
                this.TasksViewModel.SelectedSubtask.Important = true;             

                //if the important list doesn't contain this task, add it
                if (importantList.Tasks.Contains(this.TasksViewModel.SelectedSubtask) == false)
                {
                    importantList.Tasks.Add(this.TasksViewModel.SelectedSubtask);

                    //update the task counter on the important list
                    importantList.TotalCount = importantList.Tasks.Count.ToString();
                }
            }
            else
            {
                this.TasksViewModel.SelectedSubtask.Important = false;

                //if the important list does contain this task, Remove it
                if (importantList.Tasks.Contains(this.TasksViewModel.SelectedSubtask) == true)
                {
                    importantList.Tasks.Remove(this.TasksViewModel.SelectedSubtask);

                    //if you are in the important list then also remove from the general tasklist collection
                    if(this.TasksViewModel.SelectedTask == importantList)
                    {
                        this.TasksViewModel.TasksList.Remove(this.TasksViewModel.SelectedSubtask);
                    }

                    //update the task counter on the important list
                    importantList.TotalCount = importantList.Tasks.Count.ToString();
                }
            }
        }
    }
}
