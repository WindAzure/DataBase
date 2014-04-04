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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseProject.Controls
{
    /// <summary>
    /// Interaction logic for PricePanel.xaml
    /// </summary>
    public partial class PricePanel : UserControl
    {
        public PricePanel()
        {
            InitializeComponent();
        }

        public void StartMove()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0;
            animation.Duration = ConstValue.INGREDIENT_SHOP_CAR_SPEED;
            Storyboard board = new Storyboard();
            board.Children.Add(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
            Storyboard.SetTarget(animation,_pricePanel);
            board.Begin();
        }
    }
}
