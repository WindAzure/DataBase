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
    /// Interaction logic for RegisterPanel.xaml
    /// </summary>
    public partial class RegisterPanel : UserControl
    {
        void MakeScaleAnimation()
        {
            Storyboard board=new Storyboard();
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.Duration = TimeSpan.FromMilliseconds(300);
            animation1.To = 1;
            board.Children.Add(animation1);
            Storyboard.SetTargetProperty(animation1,new PropertyPath("RenderTransform.Children[0].ScaleX"));
            Storyboard.SetTarget(animation1, _panel);

            DoubleAnimation animation2 = new DoubleAnimation();
            animation2.Duration = TimeSpan.FromMilliseconds(300);
            animation2.To = 1;
            board.Children.Add(animation2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.Children[0].ScaleY"));
            Storyboard.SetTarget(animation2, _panel);
            board.Begin();
        }

        void MakeTranslateAnimation()
        {
            Storyboard board = new Storyboard();
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.To = 0;
            animation1.Duration = TimeSpan.FromMilliseconds(300);
            board.Children.Add(animation1);
            Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.Children[1].X"));
            Storyboard.SetTarget(animation1, _panel);

            DoubleAnimation animation2 = new DoubleAnimation();
            animation2.To = 0;
            animation2.Duration = TimeSpan.FromMilliseconds(300);
            board.Children.Add(animation2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.Children[1].Y"));
            Storyboard.SetTarget(animation2, _panel);
            board.Begin();
        }

        public RegisterPanel()
        {
            InitializeComponent();
            MakeScaleAnimation();
            MakeTranslateAnimation();
        }
    }
}
