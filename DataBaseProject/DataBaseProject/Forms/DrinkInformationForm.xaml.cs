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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseProject.Forms
{
    /// <summary>
    /// Interaction logic for DrinkInfomationForm.xaml
    /// </summary>
    public partial class DrinkInformationForm : UserControl
    {
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
                line.Y2 = height-10;
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

        public DrinkInformationForm()
        {
            InitializeComponent();
        }

        private void OnRecommendImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new RecommendDrinkListForm());
        }

        private void OnSpecialOfferImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new SpecialOfferDrinkListForm());
        }

        private void OnTeaImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new DrinkOfTeaListForm());
        }

        private void OnJuiceImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new DrinkOfJuiceListForm());
        }

        private void OnSmoothieImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Switch(new DrinkOfSmoothieListForm());
        }

        private void ClickBackButton(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new LoginForm());
        }

        private void ClickCloseButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MouseUpCheckButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.White;
            block.Background = Brushes.Black;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT count(*) FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON [dbo].[Member].Account=[dbo].[OrderRecord].FKAccount WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false'", connection);
            if (!Convert.ToBoolean(command1.ExecuteScalar()))
            {
                SqlCommand command2 = new SqlCommand("INSERT INTO [dbo].[OrderRecord] ([Oid],[ConfirmState],[ConfirmDate],[DeliveryState],[PS],[FKAccount]) VALUES (NEWID(),'false',NULL,'false','','" + PageSwitcher._account + "')", connection);
                command2.ExecuteScalar();
            }
            connection.Close();
            PageSwitcher.Switch(new ShopCarForm());
        }

        private void MouseDownCheckButton(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            block.Foreground = Brushes.Black;
            block.Background = Brushes.LightGray;
        }
    }
}
