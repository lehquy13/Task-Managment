using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Task_Managment.Models;

namespace Task_Managment.ViewModels
{
    public class HomeViewModel
    {
        public Members _currentUser { get; set; }
        private TaskDataAccess db = TaskDataAccess.Instance;
        public static readonly string ImagesPath = Path.GetFullPath("imagesForWpf\\TaskResource\\iconForTasks\\").Replace("\\bin\\Debug\\", "\\");

        #region ImageList & UserSetting
        public ObservableCollection<TaskIcon> IconTaskList { get; set; }
        public ObservableCollection<TaskIcon> BackgroundList { get; set; }

        public ImageSource background { get; set; }

        #endregion

        public HomeViewModel()
        {
            //init commands
            Members currentUser = new Members("phatlam1811@gmail.com", "phatlam1811", "123");
            init(currentUser);

        }


        public HomeViewModel(Members currentUser)
        {
            //init commands
            init(currentUser);

        }

        public void init(Members currentUser)
        {
            _currentUser = currentUser;

            //get all the tasks of tripledefaultTasklists
          
            InitCommand();
            InitIconAndBackground();


        }
        private void InitIconAndBackground()
        {
            IconTaskList = new ObservableCollection<TaskIcon>();
            IconTaskList.Clear();

            BackgroundList = new ObservableCollection<TaskIcon>();
            BackgroundList.Clear();
           
            string[] backgroundOptions =
            {
                "\\imagesForWpf\\TaskResource\\iconForTasks\\img_background.png",
                "\\imagesForWpf\\TaskResource\\iconForTasks\\img2_background.png",
                "\\imagesForWpf\\TaskResource\\iconForTasks\\img3_background.png",
                "\\imagesForWpf\\TaskResource\\iconForTasks\\img4_background.png"
            };

            foreach (string temp in backgroundOptions)
            {
                BackgroundList.Add(new TaskIcon(temp));
            }

            background = new BitmapImage(new Uri((ImagesPath + _currentUser.Setting.taskBackground)));
        }

        private void InitCommand()
        {
            
        }
    }
}
