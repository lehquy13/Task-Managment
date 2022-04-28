using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using Task_Managment.ViewModels;

namespace Task_Managment.Model
{
    public class Tasklist : INotifyPropertyChanged
    {
        //!Fields
        public static readonly string DefaultIcon = Path.Combine(TasksViewModel.ImagesPath, "menu.png");

        //!Properties
        public string TasklistID { get; set; }

        public List<Task> Tasks { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                PropertyUpdated("Name");
            }
        }

        public Uri IconSource { get; set; }

        private string _totalCount;
        public string TotalCount
        {
            get 
            {
                if (Tasks.Count > 0)
                {
                    return Tasks.Count.ToString();
                }
                else return ""; 
            }
            set
            {
                _totalCount = Tasks.Count.ToString();
                PropertyUpdated("TotalCount");
            }
        }

        //!Ctor
        public Tasklist()
        {
            this.TasklistID = Guid.NewGuid().ToString();
            this.Name = "Untitled list";
            this.Tasks = new List<Task>();
            this.IconSource = new Uri(Tasklist.DefaultIcon);
        }

        //!Events
        public event PropertyChangedEventHandler PropertyChanged;

        //!Methods
        public void PropertyUpdated(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
