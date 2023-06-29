using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar_Tasks.Views
{
    /// <summary>
    /// Interaktionslogik für Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl, ISwitchable
    {
        private MonthViewModel _monthVM;

        public MonthViewModel MonthVM
        {
            get { return _monthVM; }
            set { _monthVM = value; }
        }

        public Calendar()
        {
            InitializeComponent();
            MonthVM = new MonthViewModel();
            calendarTabControl.DataContext = MonthVM;
        }

        public Calendar(DateTime date)
        {
            InitializeComponent();
            MonthVM = new MonthViewModel();
            calendarTabControl.DataContext = MonthVM;
            this.tabDayView.GetDayViewModel().UserVM = MainWindow.GetUserViewModel();
            this.tabDayView.GetDayViewModel().CurrentDate = date;
            this.calendarTabControl.SelectedIndex = 2;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        public static Calendar FindParentCalendar(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            //CHeck if this is the end of the tree
            if (parent == null) return null;

            Calendar parentCalendar = parent as Calendar;
            if (parentCalendar != null)
            {
                return parentCalendar;
            }
            else
            {
                //use recursion until it reaches a Calendar
                return FindParentCalendar(parent);
            }
        }
    }
}
