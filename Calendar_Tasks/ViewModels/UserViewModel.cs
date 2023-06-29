using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calendar_Tasks
{
    public class UserViewModel : ObservableObject
    {
        #region Fields

        private UserModel _currentUser;
        private string _username;
        private int _userID;
        private bool _isUserLoggedIn;

        #endregion

        #region Public Properties/Commands

        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (value != _currentUser)
                {
                    _currentUser = value;
                    if (value != null)
                    {
                        UserID = value.UserID;
                        Username = value.Username;
                        IsUserLoggedIn = true;
                    }
                    else
                    {
                        UserID = -1;
                        Username = "";
                        IsUserLoggedIn = false;
                    }
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        public int UserID
        {
            get { return _userID; }
            set
            {
                if (value != _userID)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }
        
        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public bool IsUserLoggedIn
        {
            get { return _isUserLoggedIn; }
            set
            {
                if(value != _isUserLoggedIn)
                {
                    _isUserLoggedIn = value;
                    OnPropertyChanged("IsUserLoggedIn");
                }
            }
        }

        #endregion

        #region Private Helpers

        #endregion
    }
}
