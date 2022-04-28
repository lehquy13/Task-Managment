using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Task_Managment.ViewModels;

namespace Task_Managment.ViewModel.Commands
{
    public class EndRenameCommand : ICommand
    {
        //!Properties
        public TasksViewModel TasksViewModel { get; set; }

        //!Events
        public event EventHandler CanExecuteChanged;

        //!Ctor
        public EndRenameCommand(TasksViewModel tasksViewModel)
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
            //parameter is the selectedtasklist (not required because we've also bound the textbox to the tasklist name
            ////Tasklist selectedTasklist = parameter as Tasklist;

            //All I need to do is set this to false
            this.TasksViewModel.IsTasklistRenaming = false;
        }
    }
}
