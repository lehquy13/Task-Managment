using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoList.Models;


namespace Task_Managment.Views
{
    /// <summary>
    /// Interaction logic for ToDoTasks.xaml
    /// </summary>
    public partial class ToDoTasks : Page
    {
        public ToDoTasks()
        {
            InitializeComponent();
            var tasks = new List<MyTask>();
            tasks.Add(new MyTask() { Content = "alo" });
            tasks.Add(new MyTask() { Content = "alo" });
            tasks.Add(new MyTask() { Content = "alo",IsChecked = true });

            toDoList.ItemsSource = tasks;

        }

     
        private void ToggleButton_Initialized(object sender, EventArgs e)
        {
            ToggleButton a = (ToggleButton)sender;
            if(a.IsChecked == true)
            {
                a.Content = new PackIconKind() ;
            }
           
        }
    }
}
