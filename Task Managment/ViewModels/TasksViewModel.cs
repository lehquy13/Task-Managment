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
        public static readonly string ImagesPath = Path.GetFullPath("imagesForWpf").Replace("\\bin\\Debug\\", "\\"); 

        //!Properties
        public ObservableCollection<Tasklist> TripleDefaultTaskList { get; set; } 

        public ObservableCollection<Tasklist> TasklistsList { get; set; }

        public ObservableCollection<Task> TasksList { get; set; }

        public ObservableCollection<Subtask> Subtasks { get; set; }

        public Tasklist DefaultMyDayList     { get; set; } = new Tasklist() { Name = "My Day",    IconSource = new Uri(Path.Combine(ImagesPath, "day.png")) };
        public Tasklist DefaultImportantList { get; set; } = new Tasklist() { Name = "Important", IconSource = new Uri(Path.Combine(ImagesPath, "important.png")) };
        public Tasklist DefaultTasksList     { get; set; } = new Tasklist() { Name = "Tasks",     IconSource = new Uri(Path.Combine(ImagesPath, "greenery.png")) };

        private Tasklist _selectedTasklist;
        public Tasklist SelectedTasklist
        {
            get { return _selectedTasklist; }
            set 
            { 
                _selectedTasklist = value;

                this.TasksList.Clear();
                if(SelectedTasklist != null)
                {
                    if(SelectedTasklist.Tasks != null)
                    {
                        if(SelectedTasklist.Tasks.Count > 0)
                        {
                            this.SelectedTask = null;
                            this.SubtasksPaneVisible = !this.SubtasksPaneVisible;

                            foreach (Task task in this.SelectedTasklist.Tasks)
                            {
                                this.TasksList.Add(task);
                            }
                        }
                    }
                }          
                PropertyUpdated("SelectedTasklist");
                this.SubtasksPaneVisible = false;
            }
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                if (_selectedTask == null)
                {
                    _selectedTask = value;

                }
                else if (_selectedTask != value)
                {
                    _selectedTask = value;

                }              
                PropertyUpdated("SelectedTask");
            }
        }

        private Tasklist _selectedSubtask;
        public Tasklist SelectedSubtask
        {
            get { return _selectedSubtask; }
            set
            {
                _selectedSubtask = value;
                PropertyUpdated("SelectedSubtask");
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
        public EndRenameCommand EndRenameCommand {get; set; }

        public DeleteCommand DeleteCommand { get; set; }

        public MarkImportantCommand MarkImportantCommand { get; set; }

        public CloseSubtaskPanelCommand CloseSubtaskPanelCommand { get; set; }
        
        public SelectTaskCommand SelectSubtaskCommand { get; set; }

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

            //get all the tasks of tripledefaultTasklists

            this.TasklistsList = new ObservableCollection<Tasklist>()
            {
                this.DefaultMyDayList,
                this.DefaultImportantList,
                this.DefaultTasksList
            };
           

            //this.TasklistsList =  new ObservableCollection<Tasklist>();
            //this.TasklistsList.Add(this.DefaultMyDayList);
            //this.TasklistsList.Add(this.DefaultImportantList);
            //this.TasklistsList.Add(this.DefaultTasksList);
            
            foreach(Tasklist temp in db.GetAllTasklistOfMember(currentUser)) // lấy những tasklist như myday, importtant, untitledlist
            {
                switch (temp.Name)
                {
                    case "Important":
                        this.DefaultImportantList = temp;
                        this.TasklistsList[1] = temp;
                        break;
                    case "My Day":
                        this.DefaultMyDayList = temp;
                        this.TasklistsList[0] = temp;
                        break;
                    case "Tasks":
                        this.DefaultTasksList = temp;
                        this.TasklistsList[2] = temp;
                        break;

                    default:
                        this.TasklistsList.Add(temp); // sau đó add từng tasklist vào
                        break;

                }

            }
            this.DefaultMyDayList.Tasks = db.GetAllTasksFromTasklist(this.DefaultMyDayList);
            this.DefaultImportantList.Tasks = db.GetAllTasksFromTasklist(this.DefaultImportantList);
            this.DefaultTasksList.Tasks = db.GetAllTasksFromTasklist(this.DefaultTasksList);


            for (int i = 3; i < this.TasklistsList.Count; i++) // duyệt từng tasklist ở trong  this.TasklistsList (tức tổng số tasklist dc lưu ở local bây giờ)
            {
                this.TasklistsList[i].Tasks = db.GetAllTasksFromTasklist(this.TasklistsList[i]); // lấy cái task ở trong từng tasklist đó * tưởng tự chỗ này !!!!
                for(int j = 0; j < this.TasklistsList[i].Tasks.Count; j++)
                {
                    this.TasklistsList[i].Tasks[j].Subtasks = db.GetAllSubTasksFromTask(this.TasklistsList[i].Tasks[j]); // get subtasks
                }
            }
            

           

            this.TasksList = new ObservableCollection<Task>();
            this.SelectedTasklist = this.DefaultImportantList;

            this.Subtasks = new ObservableCollection<Subtask>();
            initCommand();
            PropertyUpdated("TasklistList");

        }

        private void initCommand()
        {
            this.NewTasklistCommand = new NewTasklistCommand(this);
            this.NewTaskCommand = new NewTaskCommand(this);
            this.NewSubtaskCommand = new NewSubtaskCommand(this);

            this.StartRenameCommand = new StartRenameCommand(this);
            this.EndRenameCommand = new EndRenameCommand(this);

            this.DeleteCommand = new DeleteCommand(this);

            this.MarkImportantCommand = new MarkImportantCommand(this);

            this.CloseSubtaskPanelCommand = new CloseSubtaskPanelCommand(this);

            this.SelectSubtaskCommand = new SelectTaskCommand(this);
        }
    }
}