using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Task_Managment.Models
{
    public class Task : INotifyPropertyChanged
    {
        //!Fields

        //!Properties
        public string TasklistID { get; set; }

        public string TaskID { get; set; }

        public string Name { get; set; }

        public bool Completed { get; set; }

        private bool _important;

        public bool Important
        {
            get { return _important; }
            set
            {
                _important = value;
                PropertyUpdated("Important");
            }
        }

        public string Notes { get; set; }

        public List<Subtask> Subtasks { get; set; }

        //!Events
        public event PropertyChangedEventHandler PropertyChanged;

        //!Ctor
        public Task(string tasklistId)
        {
            this.TaskID = Guid.NewGuid().ToString();
            this.TasklistID = tasklistId;
            this.Subtasks = new List<Subtask>();
        }

        //!Methods
        public void PropertyUpdated(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
