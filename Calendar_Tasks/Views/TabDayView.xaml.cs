using Calendar_Tasks.ViewModels;
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
    /// Interaction logic for TabDayView.xaml
    /// </summary>
    public partial class TabDayView : UserControl
    {
        private DayViewModel _dayViewModel;

        public TabDayView()
        {
            InitializeComponent();
            _dayViewModel = new DayViewModel(MainWindow.GetUserViewModel());
            DataContext = _dayViewModel;
        }

        private void BtnBackDayView_Click(object sender, RoutedEventArgs e)
        {
            Calendar.FindParentCalendar(this).calendarTabControl.SelectedIndex = 1;
        }

        public DayViewModel GetDayViewModel()
        {
            return _dayViewModel;
        }

        private void BtnPreviousDay_Click(object sender, RoutedEventArgs e)
        {
            _dayViewModel.CurrentDate = _dayViewModel.CurrentDate.AddDays(-1);
        }

        private void BtnNextDay_Click(object sender, RoutedEventArgs e)
        {
            _dayViewModel.CurrentDate = _dayViewModel.CurrentDate.AddDays(1);
        }

        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                Button btn = sender as Button;
                int taskID = Int32.Parse(btn.Tag.ToString());
                //MessageBox.Show("Deleting task {ID: " + taskID + "}");
                DataAccess.DeleteTask(taskID);
                _dayViewModel.RefreshTasks();
            }
        }

        private void BtnUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int taskID = Int32.Parse(btn.Tag.ToString());
            TaskModel task = DataAccess.GetTask(taskID);
            Switcher.Switch(new NewTask(task), "newTask");
        }
    }
}
