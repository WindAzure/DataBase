using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Interaction logic for RegisterPanel.xaml
    /// </summary>
    public partial class RegisterPanel : UserControl
    {
        public delegate void RegisterPanelEvent(object sender, EventArgs e);
        public event RegisterPanelEvent OnEffectCompleted = null;

        public delegate void RegisterPanelEvent2(object sender, RoutedEventArgs e);
        public event RegisterPanelEvent2 OnClickSendButton = null;


        void MakeScaleAnimation(Double value1, Double value2)
        {
            Storyboard board = new Storyboard();
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.Duration = TimeSpan.FromMilliseconds(300);
            animation1.To = value1;
            board.Children.Add(animation1);
            Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.Children[0].ScaleX"));
            Storyboard.SetTarget(animation1, _panel);

            DoubleAnimation animation2 = new DoubleAnimation();
            animation2.Duration = TimeSpan.FromMilliseconds(300);
            animation2.To = value2;
            board.Children.Add(animation2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.Children[0].ScaleY"));
            Storyboard.SetTarget(animation2, _panel);
            board.Begin();
        }

        void MakeTranslateAnimation(Double value1, Double value2)
        {
            Storyboard board = new Storyboard();
            board.Completed += board_Completed;
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.To = value1;
            animation1.Duration = TimeSpan.FromMilliseconds(300);
            board.Children.Add(animation1);
            Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.Children[1].X"));
            Storyboard.SetTarget(animation1, _panel);

            DoubleAnimation animation2 = new DoubleAnimation();
            animation2.To = value2;
            animation2.Duration = TimeSpan.FromMilliseconds(300);
            board.Children.Add(animation2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.Children[1].Y"));
            Storyboard.SetTarget(animation2, _panel);
            board.Begin();
        }

        void board_Completed(object sender, EventArgs e)
        {
            if (OnEffectCompleted != null)
            {
                OnEffectCompleted(sender, e);
            }
        }

        public void RestoreAnimation()
        {
            MakeScaleAnimation(0.1, 0.1);
            MakeTranslateAnimation(400, -400);
        }

        public RegisterPanel()
        {
            InitializeComponent();
            MakeScaleAnimation(1, 1);
            MakeTranslateAnimation(0, 0);
        }

        private bool CheckNoRepeatAccount(String account)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT	count(*) FROM [dbo].[Member] WHERE Account='"+account+"'", connection);
            Boolean flag = Convert.ToBoolean(command.ExecuteScalar());
            connection.Close();
            return !flag;
        }

        private void ClickSendButton(object sender, RoutedEventArgs e)
        {
            if (_accountBox.Text.Length >= 4 && _accountBox.Text.Length <= 9)
            {
                if (_passwordBox.Password.Length >= 4 && _passwordBox.Password.Length <= 9)
                {
                    if (_phoneBox.Text.Length != 0)
                    {
                        if (_addressBox.Text.Length != 0 && OnClickSendButton != null)
                        {
                            if (CheckNoRepeatAccount(_accountBox.Text))
                            {
                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
                                connection.Open();
                                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Member] ([Account],[Password],[PhoneNumber],[Address],[IsAdmin]) VALUES ('" + _accountBox.Text + "','" + _passwordBox.Password + "','" + _phoneBox.Text + "','" + _addressBox.Text + "','False')", connection);
                                command.ExecuteScalar();
                                connection.Close();
                                MessageBox.Show("註冊成功！");
                                OnClickSendButton(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("此帳號已存在");
                            }
                        }
                        else
                        {
                            _messageBlock.Text = "地址欄位不可留白";
                        }
                    }
                    else
                    {
                        _messageBlock.Text = "手機欄位不可留白";
                    }
                }
                else
                {
                    _messageBlock.Text = "密碼至少四位，最長9位";
                }
            }
            else
            {
                _messageBlock.Text = "帳號至少四位，最長9位";
            }
        }

        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            _accountBox.Text = "";
            _passwordBox.Password = "";
            _addressBox.Text = "";
            _phoneBox.Text = "";
        }
    }
}
