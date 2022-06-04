﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.IO;
using Task_Managment.Models;
using Task_Managment.ViewModel.Commands;
using Task_Managment.Commands;
using Task_Managment.Commands.TaskCommands;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Task_Managment.ViewModels
{
    public class TasksViewModel : INotifyPropertyChanged
    {
        public Members _currentUser { get; set; }
        private TaskDataAccess db = TaskDataAccess.Instance;
        //!Fields
        public static readonly string ImagesPath = Path.GetFullPath("imagesForWpf\\TaskResource\\iconForTasks\\").Replace("\\bin\\Debug\\", "\\");

        //!Properties
        #region TasklistList 
        public ObservableCollection<Tasklist> TasklistsList { get; set; }

        public ObservableCollection<Task> TasksLists { get; set; }

        public ObservableCollection<Subtask> Subtasks { get; set; }

        public Tasklist DefaultMyDayList { get; set; }

        public Tasklist DefaultImportantList { get; set; }
        public Tasklist DefaultTasksList { get; set; }
        #endregion

        #region ImageList & UserSetting
        public ObservableCollection<TaskIcon> IconTaskList { get; set; }
        public ObservableCollection<TaskIcon> BackgroundList { get; set; }

        public ImageSource background { get; set; }

        #endregion
        private Tasklist _selectedTasklist;
        public Tasklist SelectedTasklist
        {
            get { return _selectedTasklist; }
            set
            {
                _selectedTasklist = value;

                this.TasksLists.Clear();
                if (SelectedTasklist != null)
                {
                    if (SelectedTasklist.Tasks != null)
                    {
                        if (SelectedTasklist.Tasks.Count > 0)
                        {
                            this.SelectedTask = null;
                            this.SubtasksPaneVisible = !this.SubtasksPaneVisible;

                            foreach (Task task in this.SelectedTasklist.Tasks)
                            {
                                this.TasksLists.Add(task);
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
                //PropertyUpdated("SelectedTask");
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

        private bool _iconPaneVisible;
        public bool IconPaneVisible
        {
            get { return _iconPaneVisible; }
            set
            {
                _iconPaneVisible = value;
                PropertyUpdated("IconPaneVisible");
            }
        }

        private bool _morePaneVisible;
        public bool MorePaneVisible
        {
            get { return _morePaneVisible; }
            set
            {
                _morePaneVisible = value;
                PropertyUpdated("MorePaneVisible");
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

        public SelectTaskCommand SelectTaskCommand { get; set; }

        public PickTaskIconCommand PickTaskIconCommand { get; set; }

        public PickTaskThemeCommand PickTaskThemeCommand { get; set; }
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

            InitUserTasklist();
            this.TasksLists = new ObservableCollection<Task>();
            this.SelectedTasklist = this.DefaultImportantList;

            this.Subtasks = new ObservableCollection<Subtask>();
            InitCommand();
            InitIconAndBackground();


        }

        public void InitUserTasklist()
        {
            if (this.TasklistsList != null)
                this.TasklistsList.Clear();
            this.TasklistsList = new ObservableCollection<Tasklist>();

            List<Tasklist> tempList = db.GetAllTasklistOfMember(_currentUser);
            if (tempList.Count > 0)
            {
                this.TasklistsList = new ObservableCollection<Tasklist>();
                foreach (Tasklist temp in tempList) // lấy những tasklist như myday, importtant, untitledlist
                {
                    switch (temp.Name)
                    {

                        default:
                            this.TasklistsList.Add(temp); // sau đó add từng tasklist vào
                            break;

                    }
                }
                for (int i = 0; i < this.TasklistsList.Count; i++) // duyệt từng tasklist ở trong  this.TasklistsList (tức tổng số tasklist dc lưu ở local bây giờ)
                {
                    this.TasklistsList[i].Tasks = db.GetAllTasksFromTasklist(this.TasklistsList[i]); // lấy cái task ở trong từng tasklist đó * tưởng tự chỗ này !!!!
                    for (int j = 0; j < this.TasklistsList[i].Tasks.Count; j++)
                    {
                        this.TasklistsList[i].Tasks[j].Subtasks = db.GetAllSubTasksFromTask(this.TasklistsList[i].Tasks[j]); // get subtasks
                    }
                }

                this.DefaultMyDayList = this.TasklistsList[0];
                this.DefaultImportantList = this.TasklistsList[1];
                this.DefaultTasksList = this.TasklistsList[2];


            }
            else
            {
                DefaultMyDayList = new Tasklist() { Name = "My Day", IconSource = new Uri(Path.Combine(ImagesPath, "day.png")), MemberId = _currentUser.Email };
                DefaultImportantList = new Tasklist() { Name = "Important", IconSource = new Uri(Path.Combine(ImagesPath, "important.png")), MemberId = _currentUser.Email };
                DefaultTasksList = new Tasklist() { Name = "Tasks", IconSource = new Uri(Path.Combine(ImagesPath, "greenery.png")), MemberId = _currentUser.Email };
                db.CreateNewTasklist(DefaultMyDayList);
                db.CreateNewTasklist(DefaultImportantList);
                db.CreateNewTasklist(DefaultTasksList);
                this.TasklistsList = new ObservableCollection<Tasklist>()
            {
                this.DefaultMyDayList,
                this.DefaultImportantList,
                this.DefaultTasksList
            };

            }
        }

        private void InitIconAndBackground()
        {
            IconTaskList = new ObservableCollection<TaskIcon>();
            IconTaskList.Clear();

            BackgroundList = new ObservableCollection<TaskIcon>();
            BackgroundList.Clear();

            string[] icon ={

               "baseball.png",

                "basketball.png",

                "call.png",

                "car.png",

                "christmas.png",

                "church.png",

                "clothes.png",

                "computer.png",

                "confetti.png",

                "day.png",

                "defaultTask.png",

                "earth.png",

                "food.png",

                "fuel.png",

                "game.png",

                "gift.png",

                "greenery.png",

                "hospital.png",

                "important.png",

                "like.png",

                "love.png",

                "mom.png",

                "muscle.png",

                "music.png",

                "party.png",

                "plan.png",

                "school.png",

                "shopping.png",

                "soccer.png",

                "sunday.png",

                "sunflower.png",

                "support.png"

        };

            foreach (string temp in icon)
            {
                IconTaskList.Add(new TaskIcon(temp));
            }

            string[] backgroundOptions =
            {
                "img_background.png",
                "img2_background.png",
                "img3_background.png",
                "img4_background.png"
            };

            foreach (string temp in backgroundOptions)
            {
                BackgroundList.Add(new TaskIcon(temp));
            }

            background = new BitmapImage(new Uri((ImagesPath + _currentUser.Setting.taskBackground)));


        }

        private void InitCommand()
        {
            this.NewTasklistCommand = new NewTasklistCommand(this);
            this.NewTaskCommand = new NewTaskCommand(this);
            this.NewSubtaskCommand = new NewSubtaskCommand(this);

            this.StartRenameCommand = new StartRenameCommand(this);
            this.EndRenameCommand = new EndRenameCommand(this);

            this.DeleteCommand = new DeleteCommand(this);

            this.MarkImportantCommand = new MarkImportantCommand(this);

            this.CloseSubtaskPanelCommand = new CloseSubtaskPanelCommand(this);

            this.SelectTaskCommand = new SelectTaskCommand(this);

            this.PickTaskIconCommand = new PickTaskIconCommand(this);

            PickTaskThemeCommand = new PickTaskThemeCommand(this);  
        }
    }
}