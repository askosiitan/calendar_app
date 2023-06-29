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
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : UserControl, ISwitchable
    {

        public Login()
        {
            this.InitializeComponent();

            txtLoginUsername.Text = "admin";
            txtLoginPassword.Password = "admin";
        }

        #region ISwitchable Members 

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }


        #endregion // ISwitchable Members

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtLoginUsername.Text;
            string password = txtLoginPassword.Password;
            if (username.Equals("") || password.Equals("")) return;

            UserModel user = DataAccess.GetUser(username, password, "");
            if(user == null)
            {
                lblLoginInformation.Content = "User not found! Check your credentials!";
                txtLoginUsername.Text = "";
                txtLoginPassword.Password = "";
            }
            else
            {
                //MessageBox.Show(user.ToString());
                //MainWindow.GetMainWindow()._userViewModel.CurrentUser = user;
                MainWindow.SetCurrentUser(user);
                Switcher.Switch(new Calendar(), "calendar");
            }
            
        }
    }
}
