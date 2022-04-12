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
    /// Interaction logic for wndRegister.xaml
    /// </summary>
    public partial class wndRegister : Window
    {
        private Register registerHandler;
        private bool isValidAccount;

        public wndRegister()
        {
            InitializeComponent();

            btnClose.Click += BtnClose_onClick;
            btnRegister.Click += BtnRegister_onClick;
            tbEmail.LostFocus += TbEmail_onLostFocus;
            pbPassVerify.LostFocus += PbPassVerify_onLostFocus;

            registerHandler = new Register();
            isValidAccount = true;
        }

        private void PbPassVerify_onLostFocus(object sender, RoutedEventArgs e)
        {
            string Password = pbPassWord.Password;
            string verifyPass = pbPassVerify.Password;

            registerHandler.isPasswordValid(Password, verifyPass, ref isValidAccount);
        }

        private void TbEmail_onLostFocus(object sender, RoutedEventArgs e)
        {
            string Email = tbEmail.Text;
            string Username = tbUserName.Text;

            registerHandler.isValidatePatternCheck(Email, ref isValidAccount);
            
            if (!isValidAccount) return;

            registerHandler.isExistedCheck(Email, ref isValidAccount);
        }

        private void BtnRegister_onClick(object sender, RoutedEventArgs e)
        {
            if (!isValidAccount) return;

            string Email = tbEmail.Text;
            string Username = tbUserName.Text;
            string Password = pbPassWord.Password;
            string verifyPass = pbPassVerify.Password;

            try 
            {
                string verificationCode = registerHandler.GenerateVerficationCode();

                registerHandler.SendVerificationCode(verificationCode ,Email);

                wndVerifyCode newWindow = new wndVerifyCode(verificationCode);
                newWindow.ShowDialog();
                if (newWindow.DialogResult == false) return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                registerHandler.AddNewMember(Email, Username, Password);

                wndLogin newWindow = new wndLogin();
                newWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnClose_onClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
