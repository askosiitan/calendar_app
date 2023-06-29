﻿using System;
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

namespace Calendar_Tasks.UserControls
{
    /// <summary>
    /// Interaction logic for TimeControl.xaml
    /// </summary>
    public partial class TimeControl : UserControl
    {
        public TimeControl()
        {
            InitializeComponent();

            TimeControl control = this as TimeControl;
            control.Value = new TimeSpan(0, 0, 0);
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TimeControl control = obj as TimeControl;
            var newTime = (TimeSpan)e.NewValue;

            control.Hours = newTime.Hours;
            control.Minutes = newTime.Minutes;
        }


        private static void OnTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TimeControl control = obj as TimeControl;
            control.Value = new TimeSpan(control.Hours, control.Minutes, 0);
        }

        public TimeSpan Value
        {
            get { return (TimeSpan)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(TimeSpan), typeof(TimeControl),
        new FrameworkPropertyMetadata(DateTime.Now.TimeOfDay, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnValueChanged)));



        public int Hours
        {
            get { return (int)GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }
        public static readonly DependencyProperty HoursProperty =
        DependencyProperty.Register("Hours", typeof(int), typeof(TimeControl),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTimeChanged)));

        public int Minutes
        {
            get { return (int)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
        public static readonly DependencyProperty MinutesProperty =
        DependencyProperty.Register("Minutes", typeof(int), typeof(TimeControl),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnTimeChanged)));

        private Tuple<int, int> GetMaxAndCurentValues(String name)
        {
            int maxValue = 0;
            int currValue = 0;

            switch (name)
            {

                case "hh":
                    maxValue = 24;
                    currValue = Hours;
                    break;
                case "mm":
                    maxValue = 60;
                    currValue = Minutes;
                    break;
            }

            return new Tuple<int, int>(maxValue, currValue);
        }

        private void UpdateTimeValue(String name, int delta)
        {
            var values = GetMaxAndCurentValues(name);
            int maxValue = values.Item1;
            int currValue = values.Item2;

            // Set new value
            int newValue = currValue + delta;

            if (newValue == maxValue)
            {
                newValue = 0;
            }
            else if (newValue < 0)
            {
                newValue += maxValue;
            }


            switch (name)
            {

                case "hh":
                    Hours = newValue;
                    break;
                case "mm":
                    Minutes = newValue;
                    break;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs args)
        {
            try
            {
                int delta = 0;
                String name = ((TextBox)sender).Name;

                if (args.Key == Key.Up) { delta = 1; }
                else if (args.Key == Key.Down) { delta = -1; }

                UpdateTimeValue(name, delta);
            }
            catch { }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                var g = (Grid)(sender);
                var value = g.Children.OfType<TextBox>().FirstOrDefault();

                UpdateTimeValue(value.Name, e.Delta / Math.Abs(e.Delta));
            }
            catch { }

        }

        private Boolean IsTextAllowed(String name, String text)
        {
            try
            {
                foreach (Char c in text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c)) continue;
                    else return false;
                }

                var values = GetMaxAndCurentValues(name);
                int maxValue = values.Item1;

                int newValue = Convert.ToInt32(text);

                if (newValue < 0 || newValue >= (Int32)maxValue)
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }


            return true;
        }

        // Use the OnPreviewTextInput to respond to key presses 
        private void OnPreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            try
            {
                var tb = (TextBox)sender;


                e.Handled = !IsTextAllowed(tb.Name, tb.Text + e.Text);
            }
            catch { }
        }

        // Use the DataObject.Pasting Handler  
        private void OnTextPasting(object sender, DataObjectPastingEventArgs e)
        {
            try
            {
                String name = ((TextBox)sender).Name;

                if (e.DataObject.GetDataPresent(typeof(String)))
                {
                    String text = (String)e.DataObject.GetData(typeof(String));
                    if (!IsTextAllowed(name, text)) e.CancelCommand();
                }
                else e.CancelCommand();
            }
            catch { }
        }

    }
}
