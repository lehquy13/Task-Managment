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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task_Managment.Views;
using Calendar = Task_Managment.Views.Calendar;

namespace Task_Managment.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlDays.xaml
    /// </summary>
    public partial class UserControlDays : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void test()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            displayevent();
        }
       
        public static string static_day;
        string conn = @"Data Source=DESKTOP-KLH8VFB\SQLEXPRESS;Initial Catalog = TaskManagement; Integrated Security = True";
        public UserControlDays()
        {
            InitializeComponent();
        }
        public void days(int numday)
        {
            lbdays.Content = numday + "";
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            static_day = lbdays.Content.ToString();
            test();
            eventwindow e_form=new eventwindow();
            e_form.ShowDialog();
        }
        private void displayevent()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            string sql = "select * from tbl_calendar where date = '"+ Calendar.static_month.ToString() +"/" + lbdays.Content +"/"+Calendar.static_year.ToString()+"'";
            SqlCommand cmd=con.CreateCommand();
            cmd.CommandText = sql;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lbevent.Text = reader["event"].ToString();
            }
            reader.Dispose();
            cmd.Dispose();
            con.Close();
        }
    }
}
