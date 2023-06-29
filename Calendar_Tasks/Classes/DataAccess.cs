using System;
using System.Collections.Generic;
using System.Windows;
using MySqlConnector;

namespace Calendar_Tasks
{
    public static class DataAccess
    {
        private static readonly int MAX_TASKS = 10;

        #region connection config

        private static MySqlConnection con;
        private static string _db_host = "localhost";
        private static string _db_name = "calendar_tasks_app";
        private static string _db_user = "root";
        private static string _db_password = "";
        //private static string _db_port = "3306";

        #endregion // connection config


        #region Functions

        private static bool OpenConnection()
        {
            try
            {
                string connectionString = "SERVER=" + _db_host + ";" + "DATABASE=" + _db_name + ";" + "UID=" + _db_user + ";" + "PASSWORD=" + _db_password + ";";
                con = new MySqlConnection(connectionString);
                con.Open();

                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;

                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }

        }

        public static UserModel GetUser(string username, string password, string salt)
        {
            if (OpenConnection())
            {
                string sql = "SELECT * FROM user WHERE username = @username AND password = @password AND salt = @salt";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@salt", salt);
                MySqlDataReader reader = cmd.ExecuteReader();

                UserModel user = null;
                if (reader.Read())
                {
                    user = new UserModel
                    {
                        UserID = (int)reader["ID"],
                        Username = (string)reader["username"],
                        Password = reader.GetString("password"),
                        Salt = reader.GetString("salt")
                    };
                }

                con.Close();

                return user;
            }
            else
            {
                return null;
            }
        }

        public static UserModel GetUser(int id)
        {
            if (OpenConnection())
            {
                string sql = "SELECT * FROM user WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                UserModel user = null;
                if (reader.Read())
                {
                    user = new UserModel
                    {
                        UserID = (int)reader["ID"],
                        Username = (string)reader["username"],
                        Password = reader.GetString("password"),
                        Salt = reader.GetString("salt")
                    };
                }

                con.Close();

                return user;
            }
            else
            {
                return null;
            }
        }

        public static int CreateUser(string username, string password, string salt)
        {
            if (UserNameExists(username)) return 1;
            if(OpenConnection())
            {
                string sql = "INSERT INTO user (username, password, salt) VALUES (@username, @password, @salt);";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.ExecuteNonQuery();

                con.Close();
                return 0;
            }
            return -1;
        }

        public static bool UserNameExists(string username)
        {
            if(OpenConnection())
            {
                string sql = "SELECT COUNT(*) FROM user WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", username);
                int count = Int32.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
            
                if (count > 0)
                    return true;
                return false;
            }
            return false;
        }

        public static bool CreateTask(TaskModel newTask)
        {
            if (OpenConnection())
            {
                string sql = "SELECT COUNT(*) FROM task WHERE Date = @date AND UserID = @userID";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@date", newTask.Date.Year + "-" + newTask.Date.Month + "-" + newTask.Date.Day);
                cmd.Parameters.AddWithValue("@userID", newTask.UserID);
                int count = Int32.Parse(cmd.ExecuteScalar().ToString());
                if (MAX_TASKS != 0 && count >= MAX_TASKS)
                {
                    con.Close();
                    return false;
                }

                sql = "INSERT INTO task(UserID, Title, Content, Date, HourStart, MinuteStart, HourEnd, MinuteEnd, Category) VALUES (@userID, @title, @content, @date, @hourStart, @minuteStart, @hourEnd, @minuteEnd, @category);";
                cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@userID", newTask.UserID);
                cmd.Parameters.AddWithValue("@title", newTask.Title);
                cmd.Parameters.AddWithValue("@content", newTask.Content);
                cmd.Parameters.AddWithValue("@date", newTask.Date.Year + "-" + newTask.Date.Month + "-" + newTask.Date.Day);
                cmd.Parameters.AddWithValue("@hourStart", newTask.HourStart);
                cmd.Parameters.AddWithValue("@minuteStart", newTask.MinuteStart);
                cmd.Parameters.AddWithValue("@hourEnd", newTask.HourEnd);
                cmd.Parameters.AddWithValue("@minuteEnd", newTask.MinuteEnd);
                cmd.Parameters.AddWithValue("@category", newTask.Category);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            return true;
        }

        public static TaskModel GetTask(int taskID)
        {
            if (OpenConnection())
            {
                string sql = "SELECT * FROM task WHERE ID = @taskID";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@taskID", taskID);
                MySqlDataReader reader = cmd.ExecuteReader();
                TaskModel task = null;
                if (reader.Read())
                {
                    task = new TaskModel
                    {
                        TaskID = reader.GetInt32("ID"),
                        UserID = reader.GetInt32("UserID"),
                        Title = reader.GetString("Title"),
                        Content = reader.GetString("Content"),
                        HourStart = reader.GetInt32("HourStart"),
                        MinuteStart = reader.GetInt32("MinuteStart"),
                        HourEnd = reader.GetInt32("HourEnd"),
                        MinuteEnd = reader.GetInt32("MinuteEnd"),
                        Category = reader.GetString("Category"),
                        Date = reader.GetDateTime("Date")
                    };
                }
               
                con.Close();
                return task;
            }
            return null;
        }

        public static List<TaskModel> GetTasks(DateTime date)
        {
            List<TaskModel> tasks = null;
            if (OpenConnection())
            {
                string sql = "SELECT * FROM task WHERE Date = @date ORDER BY HourStart, HourEnd, Title, ID";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@date", (date.Year + "-" + date.Month + "-" + date.Day));
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TaskModel task = new TaskModel
                    {
                        TaskID = reader.GetInt32("ID"),
                        UserID = reader.GetInt32("UserID"),
                        Title = reader.GetString("Title"),
                        Content = reader.GetString("Content"),
                        HourStart = reader.GetInt32("HourStart"),
                        MinuteStart = reader.GetInt32("MinuteStart"),
                        HourEnd = reader.GetInt32("HourEnd"),
                        MinuteEnd = reader.GetInt32("MinuteEnd"),
                        Category = reader.GetString("Category"),
                        Date = reader.GetDateTime("Date")
                    };
                    tasks.Add(task);
                }

                con.Close();
            }
            return tasks;
        }

        public static List<TaskModel> GetTasks(DateTime date, int userID)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            if (OpenConnection())
            {
                string sql = "SELECT * FROM task WHERE Date = @date AND UserID = @userID ORDER BY HourStart, HourEnd, Title, ID";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@date", (date.Year + "-" + date.Month + "-" + date.Day));
                cmd.Parameters.AddWithValue("@userID", userID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TaskModel task = new TaskModel
                    {
                        TaskID = reader.GetInt32("ID"),
                        UserID = reader.GetInt32("UserID"),
                        Title = reader.GetString("Title"),
                        Content = reader.GetString("Content"),
                        HourStart = reader.GetInt32("HourStart"),
                        MinuteStart = reader.GetInt32("MinuteStart"),
                        HourEnd = reader.GetInt32("HourEnd"),
                        MinuteEnd = reader.GetInt32("MinuteEnd"),
                        Category = reader.GetString("Category"),
                        Date = reader.GetDateTime("Date")
                    };
                    tasks.Add(task);
                }

                con.Close();
            }
            return tasks;
        }

        public static void DeleteTask(int taskID)
        {
            if (OpenConnection())
            {
                try
                {
                    string sql = "DELETE FROM task WHERE ID = @taskID";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("taskID", taskID);
                    cmd.ExecuteNonQuery();
                }
                catch 
                {
                    MessageBox.Show("Could not delete task {ID: " + taskID + "}", "Something went wrong...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
        }

        public static void UpdateTask(TaskModel task)
        {
            if (OpenConnection())
            {
                try
                {
                    string sql = "UPDATE task SET title = @title, content = @content, date = @date, hourStart = @hourStart, minuteStart = @minuteStart, hourEnd = @hourEnd, minuteEnd = @minuteEnd, category = @category WHERE id = @taskID";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@taskID", task.TaskID);
                    cmd.Parameters.AddWithValue("@title", task.Title);
                    cmd.Parameters.AddWithValue("@content", task.Content);
                    cmd.Parameters.AddWithValue("@date", task.Date.Year + "-" + task.Date.Month + "-" + task.Date.Day);
                    cmd.Parameters.AddWithValue("@hourStart", task.HourStart);
                    cmd.Parameters.AddWithValue("@minuteStart", task.MinuteStart);
                    cmd.Parameters.AddWithValue("@hourEnd", task.HourEnd);
                    cmd.Parameters.AddWithValue("@minuteEnd", task.MinuteEnd);
                    cmd.Parameters.AddWithValue("@category", task.Category);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Could not update task {ID: " + task.TaskID + "}", "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
        }

        public static Dictionary<int, int> GetAmountOfTasks(DateTime date, int userID)
        {
            if (OpenConnection())
            {
                Dictionary<int, int> map = new Dictionary<int, int>(); 
                try
                {
                    string sql = "SELECT Date, COUNT(*) AS AmountOfTasks FROM task WHERE Date LIKE @date AND UserId = @userID GROUP BY Date;";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@date", date.Year + "-%" + date.Month + "-%");
                    cmd.Parameters.AddWithValue("@userID", userID);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime dateDay = reader.GetDateTime("Date");
                        int amountOfTasks = reader.GetInt32("AmountOfTasks");
                        map.Add(dateDay.Day, amountOfTasks);
                    }
                    
                }
                catch
                {
                    //MessageBox.Show("Something went wrong...", "GetAmountOfTasks", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                con.Close();
                return map;
            }
            return null;
        }

        #endregion // Functions
    }
}
