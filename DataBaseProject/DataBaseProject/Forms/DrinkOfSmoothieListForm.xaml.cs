﻿using DataBaseProject.Controls;
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
    /// Interaction logic for DrinkOfSmoothieListForm.xaml
    /// </summary>
    public partial class DrinkOfSmoothieListForm : UserControl
    {
        private bool _canScroll = true;
        private IngredientPanel _panel = null;
        private IngredientImagePanel _imagePanel = null;
        private PricePanel _pricePanel = null;
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

        public DrinkOfSmoothieListForm()
        {
            InitializeComponent();
        }

        private void OnMouseWheelStackPanel(object sender, MouseWheelEventArgs e)
        {
            if (_canScroll)
            {
                _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - e.Delta);
            }
        }

        private void ChangePosition(int activePos, double location)
        {
            UIElement item = _smoothieStackPanel.Children[activePos];
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = ConstValue.DRINK_LIST_SPREAD_SPEED;
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
            int length = _smoothieStackPanel.Children.Count;
            for (int i = 0; i < length; i++)
            {
                Grid item = _smoothieStackPanel.Children[i] as Grid;
                var loc = item.PointToScreen(new Point(0, 0));
                if (item == sender)
                {
                    ChangePosition(i, ConstValue.BASE_POINT - loc.X);
                    flag = true;
                }
                else
                {
                    if (!flag)
                        ChangePosition(i, ConstValue.HEAD_POINT - loc.X);
                    else
                        ChangePosition(i, ConstValue.TAIL_POINT - loc.X);
                }
            }
        }

        private void ArrangeIngredient(Grid sender)
        {
            String name = ((DependencyObject)sender).GetValue(FrameworkElement.NameProperty) as String;

            _panel = new IngredientPanel(name);
            _panel.Margin = new Thickness(ConstValue.INGREDIENT_END_POINTX, ConstValue.INGREDIENT_END_POINTY, 0, 0);
            _secondGrid.Children.Add(_panel);
            _panel.StartMove();

            _imagePanel = new IngredientImagePanel(name);
            _imagePanel.Margin = new Thickness(ConstValue.INGREDIENT_IMAGE_END_POINTX, ConstValue.INGREDIENT_IMAGE_END_POINTY, 0, 0);
            _thirdGrid.Children.Add(_imagePanel);
            _imagePanel.StartMove();

            _pricePanel = new PricePanel(name);
            _pricePanel.Margin = new Thickness(ConstValue.INGREDIENT_SHOP_END_POINTX,ConstValue.INGREDINET_SHOP_END_POINTY, 0, 0);
            _fourGrid.Children.Add(_pricePanel);
            _pricePanel.StartMove();
        }

        private void OnMouseDownGrid(object sender, MouseButtonEventArgs e)
        {
            if (_tempGrid == null)
            {
                ArrangeItems(sender as Grid);
                ArrangeIngredient(sender as Grid);
                _tempGrid = sender as Grid;
                _canScroll = false;
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
                _secondGrid.Children.Remove(_panel);
                _thirdGrid.Children.Remove(_imagePanel);
                _fourGrid.Children.Remove(_pricePanel);
                _imagePanel = null;
                _panel = null;
                ArrangeItems(_tempGrid);
                _tempGrid = null;
                _canScroll = true;
            }
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
