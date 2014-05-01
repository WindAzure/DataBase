using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PricePanel.xaml
    /// </summary>
    public partial class PricePanel : UserControl, INotifyPropertyChanged
    {
        private String _price;
        private String _calories;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String Price
        {
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
            get
            {
                return _price;
            }
        }

        public String Calories
        {
            set
            {
                _calories = value;
                OnPropertyChanged("Calories");
            }
            get
            {
                return _calories;
            }
        }

        public PricePanel(String fileName)
        {
            InitializeComponent();
            DataContext = this;
            String[] data = System.IO.File.ReadAllLines("../../DrinkInformation/" + fileName + ".txt", Encoding.UTF8);
            Calories = "熱量：" + data[1]+" 卡";
            Price = "建議售價："+data[2] + " 元";
        }

        public void StartMove()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0;
            animation.Duration = ConstValue.INGREDIENT_SHOP_CAR_SPEED;
            Storyboard board = new Storyboard();
            board.Children.Add(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
            Storyboard.SetTarget(animation, _pricePanel);
            board.Begin();
        }
    }
}
