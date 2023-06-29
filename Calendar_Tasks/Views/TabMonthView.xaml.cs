using System;
using System.Windows;
using System.Windows.Controls;

namespace Calendar_Tasks.Views
{
    /// <summary>
    /// Interaction logic for TabMonthView.xaml
    /// </summary>
    public partial class TabMonthView : UserControl
    {
        private MonthViewModel monthVM = null;

        public TabMonthView()
        {
            InitializeComponent();
            this.DataContext = new MonthViewModel();
            monthVM = this.DataContext as MonthViewModel;
        }

        private void BtnBackMonthView_Click(object sender, RoutedEventArgs e)
        {
            Calendar parentCalendar = Calendar.FindParentCalendar(this);

            parentCalendar.calendarTabControl.SelectedIndex = 0;
        }

        private void BtnCalendarDay_Click(object sender, RoutedEventArgs e)
        {
            Calendar parentCalendar = Calendar.FindParentCalendar(this);
            parentCalendar.tabDayView.GetDayViewModel().UserVM = MainWindow.GetUserViewModel();
            parentCalendar.tabDayView.GetDayViewModel().CurrentDate = new DateTime(monthVM.CurrentYear, monthVM.CurrentMonth, ((sender as Button).DataContext as DayModel).Day);
            parentCalendar.calendarTabControl.SelectedIndex = 2;
        }

        private void BtnPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            int previousMonth = monthVM.CurrentMonth - 1 == 0 ? 12 : monthVM.CurrentMonth - 1;
            monthVM.CurrentMonth = previousMonth;
            if(previousMonth == 12)
                monthVM.CurrentYear--;
            
        }

        private void BtnNextMonth_Click(object sender, RoutedEventArgs e)
        {
            int nextMonth = monthVM.CurrentMonth + 1 == 13 ? 1 : monthVM.CurrentMonth + 1;
            monthVM.CurrentMonth = nextMonth;
            if (nextMonth == 1)
                monthVM.CurrentYear++;
        }
    }
}
