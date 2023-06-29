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

namespace Calendar_Tasks.UserControls
{
    /// <summary>
    /// Interaction logic for ButtonMonth.xaml
    /// </summary>
    public partial class ButtonMonth : UserControl
    {
        public event RoutedEventHandler Click;

        public ButtonMonth()
        {
            InitializeComponent();
            DataContext = this;
        }

        public int Month
        {
            get { return (int)GetValue(MonthProperty); }
            set
            {
                SetValue(MonthProperty, value);
                
            }
        }

        // Using a DependencyProperty as the backing store for Month.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(int), typeof(ButtonMonth), new PropertyMetadata(0, ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ButtonMonth;
            
            switch (control.Month)
            {
                case 1:
                    control.MonthName = "January";
                    break;
                case 2:
                    control.MonthName = "February";
                    break;
                case 3:
                    control.MonthName = "March";
                    break;
                case 4:
                    control.MonthName = "April";
                    break;
                case 5:
                    control.MonthName = "May";
                    break;
                case 6:
                    control.MonthName = "June";
                    break;
                case 7:
                    control.MonthName = "July";
                    break;
                case 8:
                    control.MonthName = "August";
                    break;
                case 9:
                    control.MonthName = "September";
                    break;
                case 10:
                    control.MonthName = "October";
                    break;
                case 11:
                    control.MonthName = "November";
                    break;
                case 12:
                    control.MonthName = "December";
                    break;
                default:
                    control.MonthName = "Error1";
                    break;
            }
        }

        public string MonthName
        {
            get { return (string)GetValue(MonthNameProperty); }
            set { SetValue(MonthNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MonthName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthNameProperty =
            DependencyProperty.Register("MonthName", typeof(string), typeof(ButtonMonth), new PropertyMetadata("Error"));


        void onButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                Click(this, e);
            }
        }

    }
}
