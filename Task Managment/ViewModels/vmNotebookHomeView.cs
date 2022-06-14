using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private string _notebooksCount;
        public string NotebooksCount
        {
            get { return _notebooksCount; }
            set
            {
                _notebooksCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NotebooksCount"));
            }
        }

        public ICommand NoteSwichCommand { get; set; }
        public ICommand CreateNewNoteBookCommand { get; set; }
        public ICommand EnableRenameSelectedNoteBookCommand { get; set; }
        public ICommand DeleteSelectedNoteBookCommand { get; set; }

        private static NotebookModel _selectedNotebook;

        public NotebookModel SelectedNotebook
        {
            get => _selectedNotebook;
            set
            {
                if (value != null)
                {
                    _selectedNotebook = value;
                    //DateTime temp = _selectedTime;
                    //temp = temp.AddHours(_selectedClockTime.Hour - temp.Hour);
                    //temp = temp.AddMinutes(_selectedClockTime.Minute - temp.Minute);
                    //temp = temp.AddSeconds(0 - temp.Second);
                    //if (SelectedTask != null)
                    //    this.SelectedTime = temp;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNotebook"));

                }

            }
        }

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
                foreach (NotebookModel temp in tempList)
                {
                    this.mNotebooks.Add(temp);
                }
                for (int i = 0; i < this.mNotebooks.Count; i++)
                {
                    this.mNotebooks[i]._collection = db.GetAllNotesFromNotebook(this.mNotebooks[i]);
                }
            }
            else
            {
                NotebookModel notebookModel = new NotebookModel(currentUser.Email, "First notebook");
                db.CreateNewNotebook(notebookModel);
                this.mNotebooks = new ObservableCollection<NotebookModel> { notebookModel };
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("mNotebooks"));
            }
            InitCommand();
        }

        #region Commands

        private void InitCommand()
        {
            NoteSwichCommand = new RelayCommand<ListViewItem>(p => true, p => noteSwitch());
            CreateNewNoteBookCommand = new RelayCommand<Button>(p => true, p => CreateNewNoteBook());
            EnableRenameSelectedNoteBookCommand = new RelayCommand<ListViewItem>(p => true, p => EnableRenameSelectedNoteBook(p));
            DeleteSelectedNoteBookCommand = new RelayCommand<Button>(p => true, p => DeleteSelectedNoteBook());
        }

        private void DeleteSelectedNoteBook()
        {
            try
            {
                if (_selectedNotebook._name.Equals("First notebook"))
                {
                    MessageBox.Show("First notebook can't be delete!!");
                    return;
                }
                db.DeleteSelectedNotebook(_selectedNotebook);
                MessageBox.Show("Successfully deleted!!");
                mNotebooks.Remove(_selectedNotebook);
                NotebooksCount = mNotebooks.Count.ToString();
                SelectedNotebook = mNotebooks.Count > 0 ? mNotebooks[0] : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EnableRenameSelectedNoteBook(ListViewItem p)
        {
            p.IsEnabled = true;
        }

        private void CreateNewNoteBook()
        {
            NotebookModel notebookModel = new NotebookModel(mCurrentUser.Email, "Untitled");
            db.CreateNewNotebook(notebookModel);
            if (this.mNotebooks == null)
                this.mNotebooks = new ObservableCollection<NotebookModel> { notebookModel };
            else
                this.mNotebooks.Add(notebookModel);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("mNotebooks"));
        }

        private void noteSwitch()
        {
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
