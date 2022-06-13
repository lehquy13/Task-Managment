using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment.Models;

namespace Task_Managment.ViewModels
{
    internal class vmNotebookHomeView : INotifyPropertyChanged
    {
        private Members mCurrentUser;

        public ObservableCollection<NotebookModel> mNotebooks;

        public event PropertyChangedEventHandler PropertyChanged;

        public vmNotebookHomeView(Members currentUser)
        {
            Initialize(currentUser);
            mCurrentUser = currentUser;
        }

        #region Commands
        #endregion

        #region Functions
        private void Initialize(Members currentUser)
        {
            mCurrentUser = currentUser;
        }
        #endregion
    }
}
