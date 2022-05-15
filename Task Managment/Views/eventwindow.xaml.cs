using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task_Managment.DataAccess;
using Task_Managment.Models;
using Task_Managment.UserControls;
using Task_Managment.ViewModels;

namespace Task_Managment.Views
{
    /// <summary>
    /// Interaction logic for eventwindow.xaml
    /// </summary>
    public partial class eventwindow : Window
    {
        public eventwindow()
        {
            InitializeComponent();
            tbxdate.Text = CalendarViewModel.static_month + "/" + UserControlDays.static_day + "/" + CalendarViewModel.static_year;

        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    tbxdate.Text = CalendarViewModel.static_month + "/" + UserControlDays.static_day+"/"+CalendarViewModel.static_year;
        //}

        //private void btnsave_Click(object sender, RoutedEventArgs e)
        //{
        //    CalendarDataaccess db = new CalendarDataaccess();
        //    db.CreateCalendar(new MyCalendar() { Date = DateTime.Parse(tbxdate.Text), Note = tbxevent.Text });
        //}
    }
}
