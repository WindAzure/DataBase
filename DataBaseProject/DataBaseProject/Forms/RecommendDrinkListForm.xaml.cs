using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void OnDrinkLogoItemMouseUp(object sender, MouseButtonEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseProject.Properties.Settings.NTUT_DataBaseConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT count(*) FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON [dbo].[Member].Account=[dbo].[OrderRecord].FKAccount WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false'", connection);
            if (Convert.ToBoolean(command1.ExecuteScalar()))
            {
                SqlCommand command2 = new SqlCommand("SELECT count(*) FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount inner join [dbo].[Has] ON Oid=FKOid inner join [dbo].[Drink] ON FKName=ENName WHERE Account='" + PageSwitcher._account + "' and ConfirmState='false' and ENName='appleJuice'", connection);
                if (!Convert.ToBoolean(command2.ExecuteScalar()))
                {
                    SqlCommand command3 = new SqlCommand("INSERT INTO [dbo].[Has] ([FKName],[FKOid],[Quantity],[MakeState]) VALUES (('appleJuice'), (SELECT [Oid] FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount WHERE ConfirmState='false' and Account='" + PageSwitcher._account + "' ),('1'),('false'))", connection);
                    command3.ExecuteScalar();
                }
            }
            else
            {
                SqlCommand command4 = new SqlCommand("INSERT INTO [dbo].[OrderRecord] ([Oid],[ConfirmState],[ConfirmDate],[DeliveryState],[PS],[FKAccount]) VALUES (NEWID(),'false',NULL,'false','','" + PageSwitcher._account + "')", connection);
                command4.ExecuteScalar();
                SqlCommand command5 = new SqlCommand("INSERT INTO [dbo].[Has] ([FKName],[FKOid],[Quantity]) VALUES (('appleJuice'), (SELECT [Oid] FROM [dbo].[Member] inner join [dbo].[OrderRecord] ON Account=FKAccount WHERE ConfirmState='false' and Account='" + PageSwitcher._account + "' ),('1'))", connection);
                command5.ExecuteScalar();
            }
            connection.Close();
            PageSwitcher.Switch(new ShopCarForm());
        }
    }
}
