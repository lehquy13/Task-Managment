using System;
using System.Collections.Generic;
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

using Task_Managment.ViewModels;

namespace Task_Managment.Views
{
    /// <summary>
    /// Interaction logic for wndLogin.xaml
    /// </summary>
    public partial class wndLogin : Window
    {
        private Login loginHandler;

        public wndLogin()
        {
            InitializeComponent();

            btnLogin.Click += BtnLogin_onClick;

            loginHandler = new Login();
        }

        private void BtnLogin_onClick(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;
            string password = pbPassWord.Password;

            loginHandler.Log_in(this, email, password);
        }
    }
}
