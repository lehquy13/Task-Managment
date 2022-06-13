using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Task_Managment.Models;
using Task_Managment.ViewModels;


namespace Task_Managment.ViewModel.Commands
{
    public class Command1 : ICommand
    {
        private TaskDataAccess db = TaskDataAccess.Instance;
        //!Properties
        public TasksViewModel TasksViewModel { get; set; }
        public MainWindowViewModel MainWindowViewModel { get; set; }

        //!Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //!Ctor
        public Command1(MainWindowViewModel tasksViewModel)
        {
            this.MainWindowViewModel = tasksViewModel;
        }

        //!Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //Create textbox overlayed on top of the tasklist textblock
            //But only set the tasklistrenaming = true for the Selected List

            if(parameter is Tasklist)
            {
                TasksViewModel.IsTasklistRenaming = true;
            }
            
        }
    }
}
