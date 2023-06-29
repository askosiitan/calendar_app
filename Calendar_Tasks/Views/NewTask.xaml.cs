using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Calendar_Tasks.Views
{
    /// <summary>
    /// Interaction logic for NewTask.xaml
    /// </summary>
    public partial class NewTask : UserControl
    {
        // Values: 0 = create, 1 = update
        private readonly int mode;
        private readonly int taskID;

        public NewTask()
        {
            InitializeComponent();
            mode = 0;
        }

        public NewTask(TaskModel task)
        {
            InitializeComponent();
            mode = 1;

            // Save the id of the task to be edited globally in the class
            taskID = task.TaskID;

            // Set values from the task to be edited into input fields
            txtTitle.Text = task.Title;
            txtContent.Text = task.Content;
            switch (task.Category)
            {
                case "Low":
                    cBoxPriority.SelectedIndex = 0;
                    break;
                case "Normal":
                    cBoxPriority.SelectedIndex = 1;
                    break;
                case "High":
                    cBoxPriority.SelectedIndex = 2;
                    break;
                case "Very High":
                    cBoxPriority.SelectedIndex = 3;
                    break;
            }
            datePickerTask.SelectedDate = task.Date;
            timeControlStartTime.Value = new TimeSpan(task.HourStart, task.MinuteStart, 0);
            timeControlEndTime.Value = new TimeSpan(task.HourEnd, task.MinuteEnd, 0);
            btnCreateNewTask.Content = "Save changes";
        }

        private void BtnCreateNewTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtContent.Text) || cBoxPriority.SelectedIndex == -1 || datePickerTask.SelectedDate.Value.Date == null) return;
            TaskModel newTask = new TaskModel()
            {
                TaskID = taskID,
                Title = txtTitle.Text,
                Content = txtContent.Text,
                Category = cBoxPriority.SelectedValue.ToString(),
                Date = datePickerTask.SelectedDate.Value.Date,
                HourStart = timeControlStartTime.Value.Hours,
                MinuteStart = timeControlStartTime.Value.Minutes,
                HourEnd = timeControlEndTime.Value.Hours,
                MinuteEnd = timeControlEndTime.Value.Minutes,
                UserID = MainWindow.GetUserViewModel().UserID
            };
            if (newTask.HourEnd * 60 + newTask.MinuteEnd <= newTask.HourStart * 60 + newTask.MinuteStart)
            {
                MessageBox.Show("End time must be later than start time");
                return;
            }
            if (mode == 0)
            {
                if (!DataAccess.CreateTask(newTask))
                {
                    MessageBox.Show("Could not create task. Each day can have a maximum of 10 tasks.", "Oops...", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                txtTitle.Text = "";
                txtContent.Text = "";
                cBoxPriority.SelectedIndex = -1;
                datePickerTask.SelectedDate = null;
                timeControlStartTime.Value = new TimeSpan(0, 0, 0);
                timeControlEndTime.Value = new TimeSpan(0, 0, 0);
            }
            else if(mode == 1)
            {
                DataAccess.UpdateTask(newTask);

                Switcher.Switch(new Calendar(newTask.Date), "calendar");
            }
        }

        // Change default watermark text
        void datePickerTask_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerTask.SelectedDate != null) return;

            FieldInfo fiTextBox = typeof(DatePicker)
                .GetField("_textBox", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fiTextBox != null)
            {
                DatePickerTextBox dateTextBox =
                  (DatePickerTextBox)fiTextBox.GetValue(datePickerTask);

                if (dateTextBox != null)
                {
                    PropertyInfo piWatermark = dateTextBox.GetType()
                      .GetProperty("Watermark", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (piWatermark != null)
                    {
                        piWatermark.SetValue(dateTextBox, "Select a date", null);
                    }
                }
            }
        }
    }
}
