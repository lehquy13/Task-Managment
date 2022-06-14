using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Task_Managment.Models;

namespace Task_Managment.ViewModels
{
    public class vmNotebookHomeView : INotifyPropertyChanged
    {
        private Members mCurrentUser;

        private DataAccess db = DataAccess.Instance;

        public ObservableCollection<NotebookModel> mNotebooks { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NoteSwichCommand { get; set; }  
      

        public vmNotebookHomeView()
        {
            MainWindow newWindow = new MainWindow();

            StartWindowViewModel startWindowViewModel = new StartWindowViewModel();
            Members currentUser = startWindowViewModel.getCurrentUser();
            Initialize(currentUser);
            

            List<NotebookModel> tempList = db.GetAllNotebooksOfMember(currentUser);

            if (tempList.Count > 0)
            {
                this.mNotebooks = new ObservableCollection<NotebookModel>();
                foreach (NotebookModel temp in tempList) // lấy những tasklist như myday, importtant, untitledlist
                {
                    {
                        this.mNotebooks.Add(temp); // sau đó add từng tasklist vào
                    }
                }
                for (int i = 0; i < this.mNotebooks.Count; i++) // duyệt từng tasklist ở trong  this.TasklistsList (tức tổng số tasklist dc lưu ở local bây giờ)
                {

                    this.mNotebooks[i]._collection = db.GetAllNotesFromNotebook(this.mNotebooks[i]); // lấy cái task ở trong từng tasklist đó * tưởng tự chỗ này !!!!

                    //for (int j = 0; j < this.mNotebooks[i].Tasks.Count; j++)
                    //{
                    //    this.mNotebooks[i].Tasks[j].Subtasks = db.GetAllSubTasksFromTask(this.TasklistsList[i].Tasks[j]); // get subtasks
                    //}
                }

            }
            else
            {
                NotebookModel notebookModel = new NotebookModel(currentUser.Email,"First notebook");
                db.CreateNewNotebook(notebookModel);
                this.mNotebooks = new ObservableCollection<NotebookModel> { notebookModel };
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("mNotebooks"));
            }
        }

        #region Commands

        private void InitCommand() {
            NoteSwichCommand = new RelayCommand<ListViewItem>(p => true, p => noteSwitch());
        }

        private void noteSwitch()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Functions
        private void Initialize(Members currentUser)
        {
            mCurrentUser = currentUser;
        }
        #endregion
    }
}
