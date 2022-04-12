using System;
using System.ComponentModel.DataAnnotations;
using MaterialDesignThemes.Wpf;



namespace TodoList.Models
{
    public class MyTask
    {
        public int TaskId { get; set; }
        public int ListId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        public bool IsChecked { get; set; }

        public PackIconKind IconKind
        {
            get { return _IconKind; }
            set { SetField(ref _IconKind, value); }
        }
        private PackIconKind _IconKind = PackIconKind.LanDisconnect;
        public MyTask()
        {
            TaskId = 0;
            ListId = 0;
            Content = "Write down notes that u want";
            DateCreated = DateTime.Now;
            IsChecked = false;
        }

        //navigation property

    }
}