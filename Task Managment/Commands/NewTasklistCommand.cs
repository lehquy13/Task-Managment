using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.ViewModels;


namespace Task_Managment.ViewModel.Commands
{
    public class NewTasklistCommand : ICommand
    {
        //!Fields
        private const int MaxNumberofTaskLists = 15;

        //!Properties
        public TasksViewModel TasksViewModel { get; set; }


        //!Events
        public event EventHandler CanExecuteChanged;

        //!Ctor
        public NewTasklistCommand(TasksViewModel tasksViewModel)
        {
            this.TasksViewModel = tasksViewModel;
        }

        //!Methods

        public bool CanExecute(object parameter)
        {
            if(TasksViewModel.TripleDefaultTaskList.Count < MaxNumberofTaskLists)
            {
                return true;
            }
            else return false;
        }

        public void Execute(object parameter)
        {
            Tasklist newTasklist = new Tasklist();
            
            TasksViewModel.TripleDefaultTaskList.Add(newTasklist);
            TasksViewModel.IsTasklistRenaming = true;
            TasksViewModel.SelectedTasklist = newTasklist;
        }
    }
}
