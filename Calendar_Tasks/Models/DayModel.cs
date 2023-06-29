
namespace Calendar_Tasks
{
    public class DayModel : ObservableObject
    {
        public DayModel()
        {
            Day = 1;
            AmountOfTasks = 0;
        }

        public DayModel(int day, bool isFromCurrentMonth)
        {
            Day = day;
            AmountOfTasks = 0;
            IsFromCurrentMonth = isFromCurrentMonth;
        }

        private int _day;

        public int Day
        {
            get { return _day; }
            set
            {
                if (value != _day)
                {
                    _day = value;
                    OnPropertyChanged("Day");
                }
            }
        }

        private int _amountOfTasks;

        public int AmountOfTasks
        {
            get { return _amountOfTasks; }
            set
            {
                if (value != _amountOfTasks)
                {
                    _amountOfTasks = value;
                    OnPropertyChanged("AmountOfTasks");
                }
            }
        }

        private bool _isFromCurrentMonth;

        public bool IsFromCurrentMonth
        {
            get { return _isFromCurrentMonth; }
            set
            {
                if (value != _isFromCurrentMonth)
                {
                    _isFromCurrentMonth = value;
                    OnPropertyChanged("IsFromCurrentMonth");
                }
            }
        }

    }
}
