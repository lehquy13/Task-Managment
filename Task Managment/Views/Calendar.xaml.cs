using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
using Task_Managment.UserControls;

namespace Task_Managment.Views
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Window
    {
        string conn= @"Data Source=DESKTOP-KLH8VFB\SQLEXPRESS;Initial Catalog = TaskManagement; Integrated Security = True";
        int month, year;
        public static int static_month, static_year;
        public Calendar()
        {
            InitializeComponent();
            displaydays();
            displayevent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void displayevent()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            string sql = "select * from tbl_calendar where date = '" + static_month.ToString() + "/" + UserControlDays.static_day + "/" + static_year.ToString() + "'";
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
        }
        private void displaydays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            tblmonthyear.Text = monthname + " " + year.ToString();
            static_month = month;
            static_year=year;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year,month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"))+1;
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Children.Add(ucblank);
            }
            for(int i = 1; i <= days; i++)
            {
                UserControlDays ucdays=new UserControlDays();
                ucdays.days(i);
                daycontainer.Children.Add(ucdays);
            }

        }

        private void btnprevious_Click(object sender, RoutedEventArgs e)
        {
            daycontainer.Children.Clear();
            month--;
            static_month = month;
            static_year = year;
            DateTime now = DateTime.Now;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            tblmonthyear.Text = monthname + " " + year.ToString();
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Children.Add(ucblank);
            }
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Children.Add(ucdays);
            }
        }

        private void btnnext_Click(object sender, RoutedEventArgs e)
        {
            daycontainer.Children.Clear();
            month++;
            static_month = month;
            static_year = year;
            DateTime now = DateTime.Now;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            tblmonthyear.Text = monthname + " " + year.ToString();
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year,month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Children.Add(ucblank);
            }
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Children.Add(ucdays);
            }
        }
    }
}
