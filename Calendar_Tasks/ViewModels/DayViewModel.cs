using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Tasks.ViewModels
{
    public class DayViewModel : ObservableObject
    {
        private DateTime _currentDate;
        private ObservableCollection<TaskModel> _tasksLow;
        private int _amountOfTasksLow;
        private ObservableCollection<TaskModel> _tasksNormal;
        private int _amountOfTasksNormal;
        private ObservableCollection<TaskModel> _tasksHigh;
        private int _amountOfTasksHigh;
        private ObservableCollection<TaskModel> _tasksVeryHigh;
        private int _amountOfTasksVeryHigh;
        private bool _isUserLoggedIn;
        UserViewModel _userVM;

        public DayViewModel(UserViewModel userVM)
        {
            CurrentDate = DateTime.Now;
            _userVM = userVM;
            IsUserLoggedIn = _userVM.IsUserLoggedIn;
        }

        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged("CurrentDate");
                RetrieveTasks();
            }
        }

        public ObservableCollection<TaskModel> TasksLow
        {
            get { return _tasksLow; }
            set
            {
                _tasksLow = value;
                OnPropertyChanged("TasksLow");
                AmountOfTasksLow = _tasksLow.Count();
            }
        }

        public ObservableCollection<TaskModel> TasksNormal
        {
            get { return _tasksNormal; }
            set
            {
                _tasksNormal = value;
                OnPropertyChanged("TasksNormal");
                AmountOfTasksNormal = _tasksNormal.Count();
            }
        }

        public ObservableCollection<TaskModel> TasksHigh
        {
            get { return _tasksHigh; }
            set
            {
                _tasksHigh = value;
                OnPropertyChanged("TasksHigh");
                AmountOfTasksHigh = _tasksHigh.Count();
            }
        }

        public ObservableCollection<TaskModel> TasksVeryHigh
        {
            get { return _tasksVeryHigh; }
            set
            {
                _tasksVeryHigh = value;
                OnPropertyChanged("TasksVeryHigh");
                AmountOfTasksVeryHigh = _tasksVeryHigh.Count();
            }
        }

        public UserViewModel UserVM
        {
            get { return _userVM; }
            set { _userVM = value; OnPropertyChanged("UserVM"); }
        }

        public int AmountOfTasksLow
        {
            get { return _amountOfTasksLow ; }
            set
            {
                _amountOfTasksLow = value;
                OnPropertyChanged("AmountOfTasksLow");
            }
        }

        public int AmountOfTasksNormal
        {
            get { return _amountOfTasksNormal; }
            set
            {
                _amountOfTasksNormal = value;
                OnPropertyChanged("AmountOfTasksNormal");
            }
        }

        public int AmountOfTasksHigh
        {
            get { return _amountOfTasksHigh; }
            set
            {
                _amountOfTasksHigh = value;
                OnPropertyChanged("AmountOfTasksHigh");
            }
        }

        public int AmountOfTasksVeryHigh
        {
            get { return _amountOfTasksVeryHigh; }
            set
            {
                _amountOfTasksVeryHigh = value;
                OnPropertyChanged("AmountOfTasksVeryHigh");
            }
        }

        public bool IsUserLoggedIn
        {
            get { return _isUserLoggedIn; }
            set { _isUserLoggedIn = value; OnPropertyChanged("IsUserLoggedIn"); }
        }

        private void RetrieveTasks()
        {
            if (_userVM == null || _currentDate == null) return;
            List<TaskModel> tasks = DataAccess.GetTasks(_currentDate, _userVM.UserID);
            if (tasks == null) return;
            TasksLow = new ObservableCollection<TaskModel>();
            TasksNormal = new ObservableCollection<TaskModel>();
            TasksHigh = new ObservableCollection<TaskModel>();
            TasksVeryHigh = new ObservableCollection<TaskModel>();
            foreach(TaskModel task in tasks){
                switch (task.Category)
                {
                    case "Low":
                        TasksLow.Add(task);
                        AmountOfTasksLow++;
                        break;
                    case "Normal":
                        TasksNormal.Add(task);
                        AmountOfTasksNormal++;
                        break;
                    case "High":
                        TasksHigh.Add(task);
                        AmountOfTasksHigh++;
                        break;
                    case "Very High":
                        TasksVeryHigh.Add(task);
                        AmountOfTasksVeryHigh++;
                        break;
                }
            }
        }

        public void RefreshTasks()
        {
            RetrieveTasks();
        }
    }
}
