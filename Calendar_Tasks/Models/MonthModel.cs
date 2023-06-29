using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Tasks
{
    public class MonthModel : ObservableObject
    {
        private int _month;

        public int Month
        {
            get { return _month; }
            set
            {
                if (value != _month)
                {
                    _month = value;
                    OnPropertyChanged("Month");
                }
            }
        }

        private int _amountOfDays;

        public int AmountOfDays
        {
            get { return _amountOfDays; }
            set
            {
                if (value != _amountOfDays)
                {
                    _amountOfDays = value;
                    OnPropertyChanged("AmountOfDays");
                }
            }
        }

    }
}
