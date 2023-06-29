

namespace Calendar_Tasks
{
    public class UserModel : ObservableObject
    {
        #region Fields

        private int _userID;
        private string _username;
        private string _password;
        private string _salt;

        #endregion // Fields

        #region Properties

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

        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Salt
        {
            get { return _salt; }
            set
            {
                if(value != _salt)
                {
                    _salt = value;
                    OnPropertyChanged("Salt");
                }
            }
        }

        override
        public string ToString()
        {
            return "ID: " + UserID + ", Username: " + Username + ", Password: " + Password + ", Salt: " + Salt;
        }        

        #endregion // Properties
    }
}
