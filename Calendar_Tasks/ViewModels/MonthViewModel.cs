using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Tasks
{
    public class MonthViewModel : ObservableObject
    {
        private int _currentMonth = -1;
        private string _currentMonthName;
        private int _currentYear = -1;
        private ObservableCollection<DayModel> _lstDays;

        public MonthViewModel()
        {
            DateTime now = DateTime.Now;
            CurrentMonth = now.Month;
            CurrentYear = now.Year;
            Days = CreateDaysList();
        }


        public int CurrentMonth
        {
            get { return _currentMonth; }
            set
            {
                if(value != _currentMonth)
                {
                    _currentMonth = value;
                    switch (_currentMonth)
                    {
                        case 1:
                            CurrentMonthName = "January";
                            break;
                        case 2:
                            CurrentMonthName = "February";
                            break;
                        case 3:
                            CurrentMonthName = "March";
                            break;
                        case 4:
                            CurrentMonthName = "April";
                            break;
                        case 5:
                            CurrentMonthName = "May";
                            break;
                        case 6:
                            CurrentMonthName = "June";
                            break;
                        case 7:
                            CurrentMonthName = "July";
                            break;
                        case 8:
                            CurrentMonthName = "August";
                            break;
                        case 9:
                            CurrentMonthName = "September";
                            break;
                        case 10:
                            CurrentMonthName = "October";
                            break;
                        case 11:
                            CurrentMonthName = "November";
                            break;
                        case 12:
                            CurrentMonthName = "December";
                            break;
                    }
                    OnPropertyChanged("CurrentMonth");
                    if (CurrentYear != -1 && CurrentMonth != -1)
                        Days = CreateDaysList();
                }
            }
        }


        public int CurrentYear
        {
            get { return _currentYear; }
            set
            {
                if(value != _currentYear)
                {
                    _currentYear = value;
                    OnPropertyChanged("CurrentYear");
                    if (CurrentYear != -1 && CurrentMonth != -1)
                        Days = CreateDaysList();
                }
            }
        }

        public string CurrentMonthName
        {
            get { return _currentMonthName; }
            set
            {
                _currentMonthName = value;
                OnPropertyChanged("CurrentMonthName");
            }
        }

        public ObservableCollection<DayModel> Days
        {
            get { return _lstDays; }
            set { _lstDays = value; OnPropertyChanged("Days"); }

        }

        public ObservableCollection<DayModel> CreateDaysList()
        {
            ObservableCollection<DayModel> result = new ObservableCollection<DayModel>();
            int weekday = ((int)new DateTime(CurrentYear, CurrentMonth, 1).DayOfWeek) - 1;
            weekday = weekday < 0 ? 6 : weekday;
            
            // Create days from previous month
            for(int i = 0; i < weekday; i++)
            {
                int previousMonth = (CurrentMonth - 1) < 1 ? 12 : CurrentMonth - 1;
                result.Add(new DayModel(DateTime.DaysInMonth(CurrentYear, previousMonth) - (weekday - i - 1), false));
            }

            Dictionary<int, int> map = new Dictionary<int, int>();
            if (MainWindow.GetUserViewModel().CurrentUser != null)
                map = DataAccess.GetAmountOfTasks(new DateTime(CurrentYear, CurrentMonth, 1), MainWindow.GetUserViewModel().UserID);
            // Create days from current month
            for(int i = 0; i < DateTime.DaysInMonth(CurrentYear, CurrentMonth); i++)
            {
                DayModel day = new DayModel((i + 1), true);
                if (map.ContainsKey(i + 1))
                    day.AmountOfTasks = map[i + 1];
                result.Add(day);
                weekday = weekday + 1 > 6 ? 0 : weekday + 1;
            }

            // Create days from following month
            for(int i = 0; i < 7 - weekday && weekday != 0; i++)
            {
                result.Add(new DayModel(i + 1, false));
            }

            return result;
        }
    }
}
