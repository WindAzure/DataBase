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
            else if (fileName == "_bananaJuice")
                return "/Juice/Banana/";
            else if (fileName == "_grapeJuice")
                return "/Juice/Grape/";
            else if (fileName == "_grapeFruitJuice")
                return "/Juice/GrapeFruit/";
            else if (fileName == "_guavaJuice")
                return "/Juice/Guava/";
            else if (fileName == "_kiwiFruitJuice")
                return "/Juice/KiwiFruit/";
            else if (fileName == "_orangeJuice")
                return "/Juice/Orange/";
            else if (fileName == "_papayaJuice")
                return "/Juice/Papaya/";
            else if (fileName == "_passionFruitJuice")
                return "/Juice/PassionFruit/";
            else if (fileName == "_tomatoeJuice")
                return "/Juice/Tomatoe/";
            else if (fileName == "_waterMelonJuice")
                return "/Juice/WaterMelon/";
            else if (fileName == "_blueBerrySmoothies")
                return "/Smoothie/BlueBerry/";
            else if (fileName == "_mangoSmoothies")
                return "/Smoothie/Mango/";
            else if (fileName == "_passionFruitSoomthies")
                return "/Smoothie/PassionFruit/";
            else if (fileName == "_pineAppleSmoothies")
                return "/Smoothie/PineApple/";
            else if (fileName == "_waterMelonSmoothies")
                return "/Smoothie/WaterMelon/";
            else if (fileName == "_blackTea")
                return "/Tea/Black/";
            else if (fileName == "_greenTea")
                return "/Tea/Green/";
            else if (fileName == "_matchaTea")
                return "/Tea/Matcha/";
            else if (fileName == "_oolongTea")
                return "/Tea/Oolong/";
            else if (fileName == "_greengageTea")
                return "/Tea/Greengage/";
            else if (fileName == "_passionFruitTea")
                return "/Tea/PassionFruit/";
            else if (fileName == "_puerTea")
                return "/Tea/Puer/";
            else if (fileName == "_roselleTea")
                return "/Tea/Roselle/";
            else if (fileName == "_yakultTea")
                return "/Tea/Yakult/";
            else
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

        private void MoveItems(Transform form, TimeSpan span)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0;
            animation.Duration = span;
            form.BeginAnimation(TranslateTransform.YProperty, animation);
        }

        public void StartMove()
        {
            for (int i = 0; i < _ingredientImagePanel.Children.Count; i++)
            {
                MoveItems(_ingredientImagePanel.Children[i].RenderTransform, TimeSpan.FromMilliseconds(200 + i * 100));
            }
        }
    }
}
