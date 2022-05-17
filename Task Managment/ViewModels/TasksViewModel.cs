using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using Task_Managment.Models;
using Task_Managment.ViewModel.Commands;
using Task_Managment.Commands;

namespace Task_Managment.ViewModels
{
    public class TasksViewModel : INotifyPropertyChanged
    {
        public Members _currentUser { get; set; }
        private DataAcessForTask db = DataAcessForTask.Instance;
        //!Fields
        public static readonly string ImagesPath = @"C:\Users\Admin\source\repos\Task-Managment\Task Managment\imagesForWpf";

        //!Properties
        public ObservableCollection<Tasklist> TasklistList { get; set; } 

        public ObservableCollection<Tasklist> DefaultTasklistsList { get; set; }

        public ObservableCollection<Task> TasksList { get; set; }

        public ObservableCollection<Subtask> Subtasks { get; set; }

        public Tasklist DefaultMyDayList     { get; set; } = new Tasklist() { Name = "My Day",    IconSource = new Uri(Path.Combine(ImagesPath, "day.png")) };
        public Tasklist DefaultImportantList { get; set; } = new Tasklist() { Name = "Important", IconSource = new Uri(Path.Combine(ImagesPath, "important.png")) };
        public Tasklist DefaultTasksList     { get; set; } = new Tasklist() { Name = "Tasks",     IconSource = new Uri(Path.Combine(ImagesPath, "greenery.png")) };

        private Tasklist _selectedTask;
        public Tasklist SelectedTask
        {
            get { return _selectedTask; }
            set 
            { 
                _selectedTask = value;

                this.TasksList.Clear();
                if(SelectedTask != null)
                {
                    if(SelectedTask.Tasks != null)
                    {
                        if(SelectedTask.Tasks.Count > 0)
                        {
                            this.SelectedSubtask = null;
                            this.SubtasksPaneVisible = !this.SubtasksPaneVisible;

                            foreach (Task task in this.SelectedTask.Tasks)
                            {
                                this.TasksList.Add(task);
                            }
                        }
                    }
                }          
                PropertyUpdated("SelectedTask");
                this.SubtasksPaneVisible = false;
            }
        }

        private Task _selectedSubtask;
        public Task SelectedSubtask
        {
            get { return _selectedSubtask; }
            set
            {
                if (_selectedSubtask == null)
                {
                    _selectedSubtask = value;
                    //SelectSubtaskCommand.Execute(value);
                }
                else if (_selectedSubtask != value)
                {
                    _selectedSubtask = value;
                    //SelectSubtaskCommand.Execute(value);
                }
                //else
                //{
                //    SelectSubtaskCommand.Execute(value);
                //}
            }
        }

        private string _addaTaskText;
        public string AddaTaskText
        {
            get { return _addaTaskText; }
            set 
            {
                _addaTaskText = value;
                PropertyUpdated("AddaTaskText");
            }
        }     

        private bool _isTasklistRenaming;
        public bool IsTasklistRenaming
        {
            get { return _isTasklistRenaming; }
            set 
            { 
                _isTasklistRenaming = value;
                PropertyUpdated("IsTasklistRenaming");
            }
        }

        private bool _isTaskRenaming;
        public bool IsTaskRenaming
        {
            get { return _isTaskRenaming; }
            set
            {
                _isTaskRenaming = value;
                PropertyUpdated("IsTaskRenaming");
            }
        }

        private bool _subtasksPaneVisible;
        public bool SubtasksPaneVisible
        {
            get { return _subtasksPaneVisible; }
            set 
            { 
                _subtasksPaneVisible = value;
                PropertyUpdated("SubtasksPaneVisible");
            }
        }

        public NewTasklistCommand NewTasklistCommand { get; set; }
        public NewTaskCommand NewTaskCommand { get; set; }
        public NewSubtaskCommand NewSubtaskCommand { get; set; }

        public StartRenameCommand StartRenameCommand { get; set; }
        public EndRenameCommand EndRenameCommand { get; set; }

        public DeleteCommand DeleteCommand { get; set; }

        public MarkImportantCommand MarkImportantCommand { get; set; }

        public CloseSubtaskPanelCommand CloseSubtaskPanelCommand { get; set; }
        
        public SelectSubtaskCommand SelectSubtaskCommand { get; set; }

        //!Events
        public event PropertyChangedEventHandler PropertyChanged;

        //!Ctor

        public TasksViewModel()
        {
            //init commands
            Members currentUser = new Members("phatlam1811@gmail.com", "phatlam1811", "123");
            init(currentUser);

        }


        public TasksViewModel(Members currentUser)
        {
            //init commands
            init(currentUser);

        }

        
        //!Methods
        public void PropertyUpdated(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void init(Members currentUser)
        {
            _currentUser = currentUser;

            this.TasklistList = new ObservableCollection<Tasklist>
            {
                this.DefaultMyDayList,
                this.DefaultImportantList,
                this.DefaultTasksList
            };

            
            this.TasksList = new ObservableCollection<Task>(db.GetAllTaskOfMember(currentUser));
            this.SelectedTask = this.DefaultImportantList;

            this.Subtasks = new ObservableCollection<Subtask>();


            this.NewTasklistCommand = new NewTasklistCommand(this);
            this.NewTaskCommand = new NewTaskCommand(this);
            this.NewSubtaskCommand = new NewSubtaskCommand(this);

            this.StartRenameCommand = new StartRenameCommand(this);
            this.EndRenameCommand = new EndRenameCommand(this);

            this.DeleteCommand = new DeleteCommand(this);

            this.MarkImportantCommand = new MarkImportantCommand(this);

            this.CloseSubtaskPanelCommand = new CloseSubtaskPanelCommand(this);

            this.SelectSubtaskCommand = new SelectSubtaskCommand(this);
        }
    }
}