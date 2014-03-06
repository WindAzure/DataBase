using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataBaseProject
{
    public class PageSwitcher
    {
        public static MainWindow _maindow;

        public static void Switch(UserControl control)
        {
            _maindow.Navigate(control);
        }
    }
}
