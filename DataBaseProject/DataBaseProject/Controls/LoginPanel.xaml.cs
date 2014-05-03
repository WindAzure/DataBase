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
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : UserControl
    {
        bool _removeFlag = false;
        private RegisterPanel _panel;
        public delegate void LoginPanelEvent(object sender, RoutedEventArgs e);
        public event LoginPanelEvent OnRegisterButtonClick = null;

        public LoginPanel()
        {
            InitializeComponent();
        }

        private void ClickRegisterButton(object sender, RoutedEventArgs e)
        {
            _panel = new RegisterPanel();
            _panel.OnEffectCompleted += OnEffectCompleted;
            _contentGrid.Children.Add(_panel);

            if (OnRegisterButtonClick != null)
            {
                OnRegisterButtonClick(sender, e);
            }
        }

        void OnEffectCompleted(object sender, EventArgs e)
        {
            if (_removeFlag)
            {
                _contentGrid.Children.RemoveAt(1);
                _removeFlag = false;
            }
        }

        public void RestoreRegisterPanel()
        {
            _panel.RestoreAnimation();
            _removeFlag = true;
        }
    }
}
