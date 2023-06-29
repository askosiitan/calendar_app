using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Tasks
{
    public class TaskModel : ObservableObject
    {
        #region attributes

        private int _taskID;
        private int _userID;
        private string _title;
        private string _content;
        private DateTime _date;
        private int _hourStart;
        private int _minuteStart;
        private int _hourEnd;
        private int _minuteEnd;
        private string _category;

        #endregion

        #region properties

        public int TaskID
        {
            get { return _taskID; }
            set
            {
                if(value != _taskID)
                {
                    _taskID = value;
                    OnPropertyChanged("TaskID");
                }
            }
        }

        public int UserID
        {
            get { return _userID; }
            set
            {
                if(value != _userID)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        public string Title 
        {
            get { return _title; }
            set
            {
                if(value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if(value != _content)
                {
                    _content = value;
                    OnPropertyChanged("Content");
                }
            }
        }

        public DateTime Date
        {
            get { return _date ; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public int HourStart
        {
            get { return _hourStart; }
            set
            {
                if(value != _hourStart)
                {
                    _hourStart = value;
                    OnPropertyChanged("HourStart");
                }
            }
        }

        public int MinuteStart
        {
            get { return _minuteStart; }
            set
            {
                if(value != _minuteStart)
                {
                    _minuteStart = value;
                    OnPropertyChanged("MinuteStart");
                }
            }
        }

        public int HourEnd
        {
            get { return _hourEnd; }
            set
            {
                if(value != _hourEnd)
                {
                    _hourEnd = value;
                    OnPropertyChanged("HourEnd");
                }
            }
        }

        public int MinuteEnd
        {
            get { return _minuteEnd; }
            set
            {
               if(value != _minuteEnd)
                {
                    _minuteEnd = value;
                    OnPropertyChanged("MinuteEnd");
                } 
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                if(value != _category)
                {
                    _category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

        override
        public string ToString()
        {
            return "ID: " + TaskID + ", UserID: " + UserID + ", Title: " + Title + ", Content: " + Content + ", Category: " + Category + ", Date: " + Date +
                ", Start: " + HourStart + ":" + MinuteStart + ", End: " + HourEnd + ":" + MinuteEnd;
        }

        #endregion
    }
}
