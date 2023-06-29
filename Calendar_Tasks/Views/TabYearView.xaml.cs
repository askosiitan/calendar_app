using Calendar_Tasks.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Calendar_Tasks.Views
{
    /// <summary>
    /// Interaction logic for TabYearView.xaml
    /// </summary>
    public partial class TabYearView : UserControl
    {
        MonthViewModel monthVM = null;

        public TabYearView()
        {
            InitializeComponent();
            txtYear.txtBox.Text = DateTime.Now.Year.ToString();
        }

        private void BtnMonth_Click(object sender, RoutedEventArgs e)
        {
            monthVM = (Calendar.FindParentCalendar(this).tabMonth.Content as TabMonthView).DataContext as MonthViewModel;
            if (string.IsNullOrEmpty(txtYear.txtBox.Text)) return;

            try
            {
                monthVM.CurrentYear = Int32.Parse(txtYear.txtBox.Text);
            }
            catch
            {
                MessageBox.Show("Year must be a number", "Oops", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            monthVM.CurrentMonth = (sender as ButtonMonth).Month;

            Calendar.FindParentCalendar(this).calendarTabControl.SelectedIndex = 1;
        }

    }
}
