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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseProject.Forms
{
    /// <summary>
    /// Interaction logic for DrinkOfTeaListForm.xaml
    /// </summary>
    public partial class DrinkOfTeaListForm : UserControl
    {
        private Grid _tempGrid = null;

        private void DrawTitle()
        {
            _title.Children.Clear();
            double height = _title.ActualHeight;
            double width = _title.ActualWidth;
            for (double i = 10.0; i < width; i += 10.0)
            {
                Line line = new Line();
                line.X1 = i;
                line.Y1 = 0.0;
                line.X2 = i - 10.0;
                line.Y2 = height - 10;
                line.StrokeThickness = 0.5;
                line.Stroke = Brushes.Black;
                _title.Children.Add(line);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            DrawTitle();
        }

        public DrinkOfTeaListForm()
        {
            InitializeComponent();
        }

        private void OnMouseWheelStackPanel(object sender, MouseWheelEventArgs e)
        {
            _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - e.Delta);
        }

        private void ChangePosition(int activePos, double location)
        {
            UIElement item = _teaStackPanel.Children[activePos];
            var loc = item.PointToScreen(new Point(0, 0));
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(200);
            animation.To = location;

            Storyboard story = new Storyboard();
            story.Children.Add(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
            Storyboard.SetTarget(animation, item);
            story.Begin();
        }

        private void ArrangeItems(Grid sender)
        {
            bool flag = false;
            int length = _teaStackPanel.Children.Count;
            for (int i = 0; i < length; i++)
            {
                Grid item = _teaStackPanel.Children[i] as Grid;
                var loc = item.PointToScreen(new Point(0, 0));
                if (item == sender)
                {
                    ChangePosition(i, ConstValue.BASE_POINT-loc.X);
                    flag = true;
                }
                else
                {
                    if (!flag)
                        ChangePosition(i, -loc.X + ConstValue.HEAD_POINT);
                    else
                        ChangePosition(i, -loc.X + ConstValue.TAIL_POINT);
                }
            }
        }

        private void OnMouseDownGrid(object sender, MouseButtonEventArgs e)
        {
            if (_tempGrid == null)
            {
                ArrangeItems(sender as Grid);
                _tempGrid = sender as Grid;
            }
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            if (_tempGrid == null)
            {
                PageSwitcher.Switch(new DrinkInformationForm());
            }
            else
            {
                ArrangeItems(_tempGrid);
                _tempGrid = null;
            }
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
