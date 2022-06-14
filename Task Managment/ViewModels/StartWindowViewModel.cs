using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Task_Managment.DataAccess;
using Task_Managment.Models;

namespace Task_Managment.ViewModels
{
    public class StartWindowViewModel
    {
        private MemberDataAccess db = MemberDataAccess.Instance;

        public string mEmail { get; set; }
        public string mPassword { get; set; }
        public bool mIsUserRemember { get; set; }

        private static Members mCurrentUser;
        private static bool mIsUser;

        public Action CloseAction { get; set; }
        public Action HideAction { get; set; }
        public Action ShowAction { get; set; }

        public StartWindowViewModel()
        {
            Initialize();
        }

        #region Commands
        public ICommand LoginAsUserCmd { get; set; }
        public ICommand LoginAsGuestCmd { get; set; }
        public ICommand RegisterNewAccountCmd { get; set; }
        #endregion

        #region Functions
        private void Initialize()
        {
            InitCommands();
        }

        public bool IsUserLoggedIn() { return mIsUser; }

        public Members getCurrentUser() { return mCurrentUser; }

        private void InitCommands()
        {
            LoginAsUserCmd = new RelayCommand<PasswordBox>(p => true, p => LoginAsUser(p));
            LoginAsGuestCmd = new RelayCommand<Button>(p => true, p => LoginAsGuest());
            RegisterNewAccountCmd = new RelayCommand<Button>(p => true, p => StartRegisterWindow());
        }

        private void StartRegisterWindow()
        {

        }

        private void LoginAsGuest()
        {
            mCurrentUser = null;
            mIsUser = false;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            CloseAction.Invoke();
        }

        private void LoginAsUser(PasswordBox p)
        {
            mPassword = p.Password.ToString();

            List<Members> members = db.GetMemberWithEmailAndPassword(mEmail, mPassword);

            if (members != null && members.Count == 1)
            {
                mCurrentUser = members[0];
                mIsUser = true;
                MainWindow mainWindow = new MainWindow();
                CloseAction.Invoke();
                mainWindow.Show();
            }
            else
            {
                System.Windows.MessageBox.Show("Wrong email or password!");
            }
        }
        #endregion
    }
}
