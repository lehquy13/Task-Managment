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
using Task_Managment.UserControls;

namespace Task_Managment.Views
{
    /// <summary>
    /// Interaction logic for eventwindow.xaml
    /// </summary>
    public partial class eventwindow : Window
    {
        string conn = @"Data Source=DESKTOP-KLH8VFB\SQLEXPRESS;Initial Catalog = TaskManagement; Integrated Security = True";
        public eventwindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbxdate.Text = Calendar.static_month + "/" + UserControlDays.static_day+"/"+Calendar.static_year;
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            string sql = "insert into tbl_calendar (date,event)" +
             "values (N'" + tbxdate.Text + "',N'" + tbxevent.Text + "')";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("saved");
            con.Close();
        }
    }
}
