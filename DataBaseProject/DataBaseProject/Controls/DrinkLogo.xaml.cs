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
    /// Interaction logic for DrinkLogo.xaml
    /// </summary>
    public partial class DrinkLogo : UserControl, INotifyPropertyChanged
    {
        private String _imagePath = "";
        private bool _isPress = false;
        public delegate void DrinkLogoEvent(object sender, MouseButtonEventArgs e);
        public event DrinkLogoEvent OnItemMouseUp = null;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public String ImagePath
        {
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImgagePath");
            }
            get
            {
                return _imagePath;
            }
        }

        public DrinkLogo()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Zoom(double value)
        {
            Storyboard board = new Storyboard();

            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.To = value;
            animation1.Duration = TimeSpan.FromMilliseconds(200);
            board.Children.Add(animation1);
            Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(animation1, img);

            DoubleAnimation animation2 = new DoubleAnimation();
            animation2.To = value;
            animation2.Duration = TimeSpan.FromMilliseconds(200);
            board.Children.Add(animation2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(animation2, img);
            board.Begin();
        }

        private void ZoomIn()
        {
            Zoom(0.8);
        }

        private void ZoomOut()
        {
            Zoom(1.0);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isPress = true;
            ZoomIn();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ZoomOut();
            if (_isPress && OnItemMouseUp != null)
            {
                OnItemMouseUp(sender, e);
            }
            _isPress = false;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            ZoomOut();
            _isPress = false;
        }
    }
}
