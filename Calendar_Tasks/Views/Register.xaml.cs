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
    /// Interaktionslogik für Register.xaml
    /// </summary>
    public partial class Register : UserControl, ISwitchable
    {
        public Register()
        {
            this.InitializeComponent();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string username = txtRegisterUsername.Text;
            string password = txtRegisterPassword.Password;
            string confirmPassword = txtRegisterConfirmPassword.Password;

            if (username.Equals("") || password.Equals("") || confirmPassword.Equals("")) return;
            if (!confirmPassword.Equals(password))
            {
                lblRegisterInformation.Content = "Passwords must match";
                

                return;
            }
            switch(DataAccess.CreateUser(username, password, ""))
            {
                case -1:
                    lblRegisterInformation.Content = "Oops...Something went wrong";
                    break;
                case 0:
                    Switcher.Switch(new Login(), "login");
                    break;
                case 1:
                    lblRegisterInformation.Content = "User already exists";
                    break;
            }
            
        }
    }
}
