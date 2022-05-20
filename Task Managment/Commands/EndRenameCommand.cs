using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.ViewModels;

namespace Task_Managment.ViewModel.Commands
{
    public class EndRenameCommand : ICommand
    {
        private DataAcessForTask db = DataAcessForTask.Instance;
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
            if(parameter is Tasklist)
            {
                Tasklist currrentTasklist = parameter as Tasklist;
                db.UpdateSelectedTasklist(currrentTasklist);
            }

            //All I need to do is set this to false
            this.TasksViewModel.IsTasklistRenaming = false;
        }
    }
}
