using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Task_Managment.Models
{
    public class Task : INotifyPropertyChanged
    {
        //!Fields
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string _taskID;
        public string TaskID {
            get { return _taskID; }
            set 
            {
                _taskID = value;
                PropertyUpdated("TaskID");
            }
        }

        public string TasklistID { get; set; }

        public string MemberId { get; set; }

        //!Properties
        public DateTime Date { get; set; }

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
            //this.TaskID = Guid.NewGuid().ToString();
            this.TasklistID = tasklistId;
            this.Subtasks = new List<Subtask>();
        }

        public Task(Task taskTemp)
        {
            //this.TaskID = Guid.NewGuid().ToString();
            this.TasklistID = taskTemp.TasklistID;
            //this.TaskID = taskTemp.TaskID;
            this.Subtasks = taskTemp.Subtasks;
            this.Completed = taskTemp.Completed;
            this.Name = taskTemp.Name;
            this.Notes = taskTemp.Notes;
            this.Date = taskTemp.Date;
            this.Important = taskTemp.Important;
        }
        //!Methods
        public void PropertyUpdated(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
