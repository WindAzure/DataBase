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
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : UserControl,INotifyPropertyChanged
    {
        bool _removeFlag = false;
        private RegisterPanel _panel;
        public String _noticeMessage = "";
        public delegate void LoginPanelEvent(object sender, RoutedEventArgs e);
        public event LoginPanelEvent OnRegisterButtonClick = null;
        public event LoginPanelEvent OnLoginButtonClick = null;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        
        public String Account
        {
            set;
            get;
        }

        public String Password
        {
            set;
            get;
        }

        public String NoticeMessage
        {
            set 
            {
                _noticeMessage = value;
                OnPropertyChanged("NoticeMessage");
            }
            get
            {
                return _noticeMessage;
            }
        }

        public LoginPanel()
        {
            InitializeComponent();
            DataContext = this;
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

        private void ClickLoginButton(object sender, RoutedEventArgs e)
        {
            if (OnLoginButtonClick != null)
            {
                Account = _accountBox.Text;
                Password = _passwordBox.Password;
                OnLoginButtonClick(sender, e);
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
