using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Task_Managment.Models
{
    public class NotebookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string _ownerId { get; set; }
        public string _name { get; set; }
        public ObservableCollection<Note> _collection { get; set; }
        public DateTime _createdDate { get; set; }
    }
}
