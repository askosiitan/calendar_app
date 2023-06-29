using System;
using System.Windows.Controls;

namespace Calendar_Tasks
{
    public static class Switcher
    {
        public static ContentControl pageSwitcher;
        private static string tabName;

        public static void Switch(UserControl newPage, string tabName)
        {
            if (tabName.Equals(Switcher.tabName)) return;
            pageSwitcher.Content = (newPage);
            Switcher.tabName = tabName;
        }

        public static void Switch(UserControl newPage, string tabName, object state)
        {
            if (tabName.Equals(Switcher.tabName)) return;
            pageSwitcher.Content = newPage;
            Switcher.tabName = tabName;

            ISwitchable s = newPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("newPage is not ISwitchable! "
                  + newPage.Name.ToString());
        }
    }
}
