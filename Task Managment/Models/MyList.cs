using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class MyList
    {
        public int ListId { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }

        //navigation property
        public ICollection<MyTask> Tasks { get; set; }
    }
}
