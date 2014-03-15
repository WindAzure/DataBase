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

namespace DataBaseProject.Forms
{
    /// <summary>
    /// Interaction logic for DrinkInfomationForm.xaml
    /// </summary>
    public partial class DrinkInformationForm : UserControl
    {
        public DrinkInformationForm()
        {
            InitializeComponent();
        }

        private void OnRecommendImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new RecommendDrinkListForm());
        }

        private void OnSpecialOfferImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new SpecialOfferDrinkListForm());
        }

        private void OnTeaImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new DrinkOfTeaListForm());
        }

        private void OnJuiceImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new DrinkOfJuiceListForm());
        }

        private void OnSmoothieImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new DrinkOfSmoothieListForm());
        }
    }
}
