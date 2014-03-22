using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DataBaseProject.Controls
{
    /// <summary>
    /// Interaction logic for IngredientPanel.xaml
    /// </summary>
    public partial class IngredientPanel : UserControl
    {
        public IngredientPanel()
        {
            InitializeComponent();
        }

        public IngredientPanel(String fileName)
        {
            String[] data = System.IO.File.ReadAllLines("../../DrinkInformation/_appleJuice.txt", Encoding.UTF8);
            foreach (String d in data)
            {
                Debug.WriteLine(d);
            }
        }
    }
}
