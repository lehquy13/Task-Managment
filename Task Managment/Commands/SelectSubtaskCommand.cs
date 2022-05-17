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
            if (parameter == null && TasksViewModel.SelectedSubtask != null)
            {
                this.TasksViewModel.SubtasksPaneVisible = !this.TasksViewModel.SubtasksPaneVisible;
                TasksViewModel.PropertyUpdated("SelectedTask");
                return;
            }
            if (parameter != TasksViewModel.SelectedSubtask && this.TasksViewModel.SubtasksPaneVisible == true)
            {
                TasksViewModel.PropertyUpdated("SelectedTask");
                return;
            }
            else
            {
                // check and revert the panel status
                Task task = (Task)parameter;
                //if (task == TasksViewModel.SelectedTask)
                //    return;

                //TasksViewModel.SelectedTask = task;
                if (TasksViewModel.SelectedTask != null)
                {
                    if (TasksViewModel.SelectedTask.Tasks != null)
                    {
                        if (TasksViewModel.SelectedTask.Tasks.Count > 0)
                        {
                            if (TasksViewModel.SelectedSubtask != null)
                            {

                                this.TasksViewModel.SubtasksPaneVisible = !this.TasksViewModel.SubtasksPaneVisible;
                                this.TasksViewModel.Subtasks.Clear();
                                if (TasksViewModel.SelectedSubtask.Subtasks != null)
                                {
                                    if (TasksViewModel.SelectedSubtask.Subtasks.Count > 0)
                                    {
                                        foreach (Subtask subTask in this.TasksViewModel.SelectedSubtask.Subtasks)
                                        {
                                            this.TasksViewModel.Subtasks.Add(subTask);
                                        }
                                    }
                                }
                            }
                            else this.TasksViewModel.SubtasksPaneVisible = false;
                        }
                    }
                }
                TasksViewModel.PropertyUpdated("SelectedTask");


            }

            //TasksViewModel.PropertyUpdated("SelectedTask");
            //return;



        }

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