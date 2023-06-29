using System.Windows;

namespace Calendar_Tasks
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static UserViewModel _userViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _userViewModel = new UserViewModel();
            navbar.DataContext = _userViewModel;

            // Log in as admin on program startup
            _userViewModel.CurrentUser = null;

            // Initialize pageswitcher and switch to starting page
            Switcher.pageSwitcher = PageSwitcher;
            Switcher.Switch(new Views.Calendar(), "calendar");
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Views.Login(), "login");
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Views.Register(), "register");
        }

        public static void SetCurrentUser(UserModel user)
        {
            _userViewModel.CurrentUser = user;  
        }

        public static UserViewModel GetUserViewModel()
        {
            return _userViewModel;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentUser(null);
            Switcher.Switch(new Views.Login(), "login");
        }

        private void BtnNavbarCalendar_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Views.Calendar(), "calendar");
        }

        private void BtnNavbarNewTask_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Views.NewTask(), "newTask");
        }
    }
}
