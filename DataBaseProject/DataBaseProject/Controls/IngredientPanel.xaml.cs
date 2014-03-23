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
            InitializeComponent();

            int number = 1;
            String[] data = System.IO.File.ReadAllLines("../../DrinkInformation/" + fileName + ".txt", Encoding.UTF8);
            String[] ingredient = data[0].Split(' ');

            foreach (String s in ingredient)
            {
                Grid grid = new Grid();
                grid.Margin = new Thickness(0, 0, 0, 30);

                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(3, GridUnitType.Star);
                ColumnDefinition col2 = new ColumnDefinition();
                col2.Width = new GridLength(7, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col1);
                grid.ColumnDefinitions.Add(col2);

                Image img = new Image();
                img.Source = new BitmapImage(new Uri("../../Image/Item/item" + number.ToString() + ".png", UriKind.Relative));
                img.Height = 30;
                img.Width = 30;
                img.Stretch = Stretch.Fill;
                img.HorizontalAlignment = HorizontalAlignment.Center;
                img.VerticalAlignment = VerticalAlignment.Center;

                TextBlock block = new TextBlock();
                block.Text = s;
                block.Width = 80;
                block.FontSize = 18;
                block.Foreground = Brushes.Gray;
                block.HorizontalAlignment = HorizontalAlignment.Center;
                block.VerticalAlignment = VerticalAlignment.Center;

                grid.Children.Add(img);
                grid.Children.Add(block);
                Grid.SetColumn(img, 0);
                Grid.SetColumn(block, 1);

                _ingredientStackPanel.Children.Add(grid);
                number++;
            }
        }

        public void InitializePosition(double X, double Y)
        {
            _translateTransform.X = X;
            _translateTransform.Y = Y;
        }

        public void MovePanel(double X, double Y,TimeSpan span)
        {
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.Duration = span;
            animation1.To = X;

            DoubleAnimation animation2 = new DoubleAnimation();
            animation2.Duration = span;
            animation2.To = Y;
            Storyboard board = new Storyboard();
            board.Children.Add(animation1);
            board.Children.Add(animation2);
            Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.X"));
            Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.Y"));
            Storyboard.SetTarget(animation1, _ingredientPanel);
            Storyboard.SetTarget(animation2, _ingredientPanel);
            board.Begin();
        }
    }
}
