using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Managment.Models
{
    public class Subtask
    {
        //!Fields
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MemberID { get; set; }

        public string TaskID { get; set; }

        public string SubtaskID { get; set; }

        //!Properties
        public string Name { get; set; }


        public bool Completed { get; set; }

        //!Events

        //!Ctor
        public Subtask(string parentTaskID)
        {
            this.SubtaskID = Guid.NewGuid().ToString();
            this.TaskID = parentTaskID;
        }

        //!Methods
    }
}
