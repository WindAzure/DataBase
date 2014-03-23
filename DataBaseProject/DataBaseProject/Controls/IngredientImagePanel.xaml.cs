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
    /// Interaction logic for IngredientImagePanel.xaml
    /// </summary>
    public partial class IngredientImagePanel : UserControl
    {
        public IngredientImagePanel()
        {
            InitializeComponent();
        }

        private String GetMapping(String fileName)
        {
            if (fileName == "_appleJuice")
                return "/Juice/Apple/";

            return "";
        }

        private void InitializePosition()
        {
            foreach (Image img in _ingredientImagePanel.Children)
            {
                TranslateTransform trans = img.RenderTransform as TranslateTransform;
                trans.X = 0;
                trans.Y = 400;
            }
        }

        private void AddImage3(Image img, int pos)
        {
            if (pos == 0)
            {
                img.Margin = new Thickness(81, -56, 319, 56);
            }
            else if (pos == 1)
            {
                img.Margin = new Thickness(36, 0, 364, 0);
            }
            else
            {
                img.Margin = new Thickness(111, 10, 289, -10);
                img.RenderTransformOrigin = new Point(0.03, 0.57);
            }
        }

        private void AddImage4(Image img, int pos)
        {
            if (pos == 0)
            {
                img.Margin = new Thickness(81, -56, 319, 56);
            }
            else if (pos == 1)
            {
                img.Margin = new Thickness(36, 0, 364, 0);
            }
            else if (pos == 2)
            {
                img.Margin = new Thickness(136, -46, 249, 24);
            }
            else
            {
                img.Margin = new Thickness(111, 10, 289, -10);
                img.RenderTransformOrigin = new Point(0.03, 0.57);
            }
        }

        private void AddImage5(Image img, int pos)
        {
            if (pos == 0)
            {
                img.Margin = new Thickness(81, -56, 319, 56);
            }
            else if (pos == 1)
            {
                img.Margin = new Thickness(36, 0, 364, 0);
            }
            else if (pos == 2)
            {
                img.Margin = new Thickness(136, -46, 249, 24);
            }
            else if (pos == 3)
            {
                img.Margin = new Thickness(181, 20, 228, 0);
            }
            else
            {
                img.Margin = new Thickness(111, 10, 289, -10);
                img.RenderTransformOrigin = new Point(0.03, 0.57);
            }
        }

        public IngredientImagePanel(String fileName)
        {
            InitializeComponent();
            
            String[] data = System.IO.File.ReadAllLines("../../DrinkInformation/" + fileName + ".txt", Encoding.UTF8);
            String[] ingredient = data[0].Split(' ');
            String folderName = GetMapping(fileName);

            for (int i = 0; i < ingredient.Length; i++)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("../../Image" + folderName + "pic" + (i + 2).ToString() + ".png", UriKind.Relative));
                img.Stretch = Stretch.Fill;
                img.Height = 100;
                img.Width = 100;
                img.RenderTransform = new TranslateTransform();

                if (ingredient.Length == 3)
                {
                    AddImage3(img, i);
                }
                else if (ingredient.Length == 4)
                {
                    AddImage4(img, i);
                }
                else
                {
                    AddImage5(img, i);
                }

                _ingredientImagePanel.Children.Add(img);
            }
            InitializePosition();
        }

        private void MoveItems(Transform form,TimeSpan span)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0;
            animation.Duration = span;
            form.BeginAnimation(TranslateTransform.YProperty, animation);
        }

        public void StartMove()
        {
            MoveItems(_ingredientImagePanel.Children[0].RenderTransform, TimeSpan.FromMilliseconds(200));
            MoveItems(_ingredientImagePanel.Children[1].RenderTransform, TimeSpan.FromMilliseconds(300));
            MoveItems(_ingredientImagePanel.Children[2].RenderTransform, TimeSpan.FromMilliseconds(400));
            MoveItems(_ingredientImagePanel.Children[3].RenderTransform, TimeSpan.FromMilliseconds(500));
        }
    }
}
