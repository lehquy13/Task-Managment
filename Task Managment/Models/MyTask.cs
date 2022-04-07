#nullable disable
using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class MyTask
    {
        [Key] public int TaskId { get; set; }
        public int ListId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        //navigation property
        public MyList List { get; set; }
    }
}