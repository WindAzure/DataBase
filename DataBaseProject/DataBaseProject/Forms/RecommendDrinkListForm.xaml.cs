using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for RecommendDrinkList.xaml
    /// </summary>
    public partial class RecommendDrinkListForm : UserControl,INotifyPropertyChanged
    {
        private String _price;
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

        public RecommendDrinkListForm()
        {
            InitializeComponent();
            DataContext = this;
            String[] data = System.IO.File.ReadAllLines("../../DrinkInformation/_appleJuice.txt", Encoding.UTF8);
            Price = "建議售價：" + data[2] + " 元";
        }

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

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new DrinkInformationForm());
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
