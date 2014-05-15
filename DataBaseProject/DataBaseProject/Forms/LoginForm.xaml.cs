using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseProject.Forms
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        public LoginForm()
        {
            InitializeComponent();
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
            _loginPanel.RestoreRegisterPanel();
            _backButton.Visibility = Visibility.Hidden;
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        public void ClickRegisterButton(object sender, RoutedEventArgs e)
        {
            _backButton.Visibility = Visibility.Visible;
        }

        public void ClickLoginButton(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT	[Account], [IsAdmin] FROM [dbo].[Member] WHERE Account='" + _loginPanel.Account + "' and Password='" + _loginPanel.Password + "'", connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count>0)
            {
                _loginPanel.NoticeMessage = "";
                PageSwitcher._account = _loginPanel.Account;
                if (Convert.ToBoolean(dataSet.Tables[0].Rows[0].ItemArray[1]))
                {
                    PageSwitcher.Switch(new ManagerHomeForm());
                }
                else
                {
                    PageSwitcher.Switch(new DrinkInformationForm());
                }
            }
            else
            {
                _loginPanel.NoticeMessage = "帳號密碼錯誤，請重新輸入";
            }
            connection.Close();
        }

        public void ClickRegisterSendButton(object sender, RoutedEventArgs e)
        {
            _loginPanel.RestoreRegisterPanel();
            _backButton.Visibility = Visibility.Hidden;
        }
    }
}
